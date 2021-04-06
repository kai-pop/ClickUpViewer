using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chinchilla.ClickUp.Helpers;
using Chinchilla.ClickUp.Params;
using Chinchilla.ClickUp.Responses;
using Chinchilla.ClickUp.Responses.Model;
using ClickUpViewer.Domain.ClickUp;
using RestSharp;
using Newtonsoft.Json;
using ClickUpViewer.Domain.Repositories;

namespace ClickUpViewer.Infrastructure.WebApi
{
    public class Api : IClickUpRepository
    {
        private const string BASE_URL = "https://api.clickup.com/api/v2/";

        private string _token;
        private Chinchilla.ClickUp.ClickUpApi _clickUpApi;
        private IRestClient _client = new RestClient(BASE_URL);
        private HttpClient _httpClient;

        public Api(string token)
        {
            _token = token;
            _clickUpApi = new Chinchilla.ClickUp.ClickUpApi(_token);
            _httpClient = new HttpClient() { BaseAddress = new Uri(BASE_URL) };
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("authorization", _token);
        }

        public Chinchilla.ClickUp.ClickUpApi CoreApi => _clickUpApi;

        public void ChangeToken(string token) {
            _token = token;
            _clickUpApi = new Chinchilla.ClickUp.ClickUpApi(token);
        }

        public async Task<List<ResponseModelTeam>> Teams()
        {
            return (await _clickUpApi.GetAuthorizedTeamsAsync()).ResponseSuccess.Teams;
        }

        public async Task<ResponseModelTeam> Team(string name)
        {
            return (await Teams()).FirstOrDefault(x => x.Name == "データ作成課");
        }

        public async Task<List<ResponseModelSpace>> Spaces(string teamId)
        {
            return (await _clickUpApi.GetTeamSpacesAsync(new ParamsGetTeamSpaces(teamId))).ResponseSuccess.Spaces;
        }

        public async Task<ResponseModelSpace> Space(string teamId, string name)
        {
            return (await Spaces(teamId)).FirstOrDefault(x => x.Name == name);
        }

        public async Task<List<ResponseModelFolder>> Folders(string spaceId)
        {
            return (await _clickUpApi.GetSpaceFoldersAsync(new ParamsGetSpaceFolders(spaceId))).ResponseSuccess.Folders;
        }

        public async Task<ResponseModelFolder> Folder(string spaceId, string name)
        {
            return (await Folders(spaceId)).FirstOrDefault(x => x.Name == name);
        }

        public async Task<(string, ResponseModelList)> List(string teamName, string spaceName, string folderName, string listName)
        {
            var team = await Team(teamName);
            var space = await Space(team.Id, spaceName);
            var folder = await Folder(space.Id, folderName);
            return (team.Id, folder.Lists.FirstOrDefault(x => x.Name == listName));
        }

        public async Task<List<ResponseModelMember>> GetListMembers(string listId)
        {
            var request = CreateRequest($"list/{listId}/member", Method.GET);
            var res = await RestSharperHelper.ExecuteRequestAsync<ResponseMembers, ResponseError>(_client, request);
            if (res.ResponseError != null)
            {
                throw new Exception($"{res.ResponseError.ECode} {res.ResponseError.Status} {res.ResponseError.Err}");
            }
            return res.ResponseSuccess.Members;
        }

        public async Task<List<ResponseModelTimeEntry>> GetTimeEntries(string teamId, ParamsGetTimeEntries param)
        {
            var queryString = OptionalParamGenerator.GenerateOptionalParams(param);
            using var response = await _httpClient.GetAsync($"team/{teamId}/time_entries?{queryString}");
            var responseData = await response.Content.ReadAsStringAsync();

            // task:"0" という変なデータを{}に置換
            responseData = responseData.Replace(@"""task"":""0""", @"""task"": null");
            var timeEntries = JsonConvert.DeserializeObject<ResponseTimeEntries>(responseData);
            return timeEntries.Data.Where(x => x.Task != null).ToList();

            // var request = CreateRequest($"team/{teamId}/time_entries?{queryString}", Method.GET);
            // var res = await RestSharperHelper.ExecuteRequestAsync<ResponseTimeEntries, ResponseError>(_client, request);
            // if (res.ResponseError != null)
            // {
            //     throw new Exception($"{res.ResponseError.ECode} {res.ResponseError.Status} {res.ResponseError.Err}");
            // }
            // return res.ResponseSuccess.Data;
        }

        public async Task<List<ResponseModelTask>> GetTasks(ParamsGetTasks param)
        {
            var tasks = new List<ResponseModelTask>();
            var page = 0;
            while (true)
            {
                param.Page = page++;
                var queryString = OptionalParamGenerator.GenerateOptionalParams(param);
                var request = CreateRequest($"team/{param.TeamId}/task?{queryString}", Method.GET);
                var res = await RestSharperHelper.ExecuteRequestAsync<ResponseTasks, ResponseError>(_client, request);

                if (res.ResponseError != null)
                {
                    throw new Exception($"{res.ResponseError.ECode} {res.ResponseError.Status} {res.ResponseError.Err}");
                }

                if (!res.ResponseSuccess.Tasks.Any())
                {
                    break;
                }

                tasks.AddRange(res.ResponseSuccess.Tasks);
            }
            return tasks;
        }


        public async Task<List<ResponseModelTaskFull>> GetFilteredTeamTasks(string teamId, ParamsGetFilteredTeamTasks param)
        {
            var tasks = new List<ResponseModelTaskFull>();
            var page = 0;
            while (true)
            {
                param.Page = page++;
                var queryString = OptionalParamGenerator.GenerateOptionalParams(param);
                var request = CreateRequest($"team/{teamId}/task?{queryString}", Method.GET);
                var res = await RestSharperHelper.ExecuteRequestAsync<ResponseTasksFull, ResponseError>(_client, request);

                if (res.ResponseError != null)
                {
                    throw new Exception($"{res.ResponseError.ECode} {res.ResponseError.Status} {res.ResponseError.Err}");
                }

                if (!res.ResponseSuccess.Tasks.Any())
                {
                    break;
                }

                tasks.AddRange(res.ResponseSuccess.Tasks);
            }
            return tasks;
        }

        private RestRequest CreateRequest(string resouce, RestSharp.Method method, object body = null)
        {
            var request = new RestRequest(resouce, method);
            request.AddHeader("authorization", _token);
            if (body != null) request.AddJsonBody(body);
            return request;
        }

    }
}