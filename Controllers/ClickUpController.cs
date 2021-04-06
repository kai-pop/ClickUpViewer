using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClickUpViewer.Domain.ClickUp;
using Cysharp.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ClickUpViewer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClickUpController : ControllerBase
    {
        private readonly ILogger _logger;

        public ClickUpController(ILogger<ClickUpController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<object> Get(string teamId, string listId, DateTime? startDate, DateTime? endDate, string tagNames)
        {
            var api = new Infrastructure.WebApi.Api("");

            var members = await api.GetListMembers(listId);

            // タスク取得用タスク
            var paramsGetTasks = new ParamsGetFilteredTeamTasks()
            {
                Subtasks = true,
                IncludeClosed = true,
                ListIds = new List<string>() { listId },
                Statuses = new List<string>(new[] { "完了" })
            };
            var getTasksTask = api.GetFilteredTeamTasks(teamId, paramsGetTasks);

            // 時間取得用タスク
            var paramsGetTimeEntries = new ParamsGetTimeEntries(
                startDate ?? DateTime.MinValue,
                endDate ?? DateTime.Now,
                members.Select(x => x.Id));
            var getTimeEntriesTask = api.GetTimeEntries(teamId, paramsGetTimeEntries);

            // 並列でapiからデータ取得
            await Task.WhenAll(getTasksTask, getTimeEntriesTask);
            var tasks = getTasksTask.Result;
            var timeEntries = getTimeEntriesTask.Result;

            // apiでできない抽出条件を適用
            tasks = tasks.Where(x => x.Tags.Any(x => tagNames.Contains(x.Name))).ToList();

            var timeEntriesGroupingTask = timeEntries
                .GroupBy(x => x.Task.Id)
                .Select(x => new
                {
                    TaskId = x.Key,
                    Duration = DateTimeOffset.FromUnixTimeMilliseconds(x.Sum(y => y.Duration)),
                    End = DateTimeOffset.FromUnixTimeMilliseconds((x.Max(y => y.End)))
                });

            var contents = timeEntriesGroupingTask
                            .Join(tasks, x => x.TaskId, x => x.Id, (time, task) => (time, task))
                            .Select(x => new
                            {
                                Qty = x.task.GetQty(),
                                x.task.Assignees.FirstOrDefault().Username,
                                TagName = x.task.Tags.FirstOrDefault()?.Name,
                                Title = x.task.Name,
                                DurationMinuets = x.time.Duration.TimeOfDay.TotalMinutes,
                                End = x.time.End
                            });
            return contents;
        }
    }
}
