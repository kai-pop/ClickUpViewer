using System.Collections.Generic;
using System.Threading.Tasks;
using Chinchilla.ClickUp.Params;
using Chinchilla.ClickUp.Responses.Model;
using ClickUpViewer.Domain.ClickUp;

namespace ClickUpViewer.Domain.Repositories
{
    public interface IClickUpRepository
    {
         Task<List<ResponseModelTeam>> Teams();
         Task<ResponseModelTeam> Team(string name);
         Task<List<ResponseModelSpace>> Spaces(string teamId);
         Task<ResponseModelSpace> Space(string teamId, string name);
         Task<List<ResponseModelFolder>> Folders(string spaceId);
         Task<ResponseModelFolder> Folder(string spaceId, string name);
         Task<(string, ResponseModelList)> List(string teamName, string spaceName, string folderName, string listName);
         Task<List<ResponseModelMember>> GetListMembers(string listId);
         Task<List<ResponseModelTimeEntry>> GetTimeEntries(string teamId, ParamsGetTimeEntries param);
         Task<List<ResponseModelTask>> GetTasks(ParamsGetTasks param);
         Task<List<ResponseModelTaskFull>> GetFilteredTeamTasks(string teamId, ParamsGetFilteredTeamTasks param);
         
    }
}