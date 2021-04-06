using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Chinchilla.ClickUp.Helpers;
using Newtonsoft.Json;

namespace ClickUpViewer.Domain.ClickUp
{
    public class ParamsGetFilteredTeamTasks
    {
        [DataMember(Name = "page")]
        [JsonProperty("page")]
        public int? Page { get; set; }

        [DataMember(Name = "subtasks")]
        [JsonProperty("subtasks")]
        public bool? Subtasks { get; set; }

        [DataMember(Name = "list_ids")]
        [JsonProperty("list_ids")]
        public List<string> ListIds { get; set; }

        [DataMember(Name = "include_closed")]
        [JsonProperty("include_closed")]
        public bool? IncludeClosed { get; set; }

        [DataMember(Name = "statuses")]
        [JsonProperty("statuses")]
        public List<string> Statuses { get; set; }


        /// <summary>
        /// 複数設定すると and 条件になる。 全てのタグを含むタスクが対象となる
        /// </summary>
        /// <value></value>
        [DataMember(Name = "tags")]
        [JsonProperty("tags")]
        public List<string> Tags { get; set; }

        [DataMember(Name = "date_updated_lt")]
        [JsonConverter(typeof(JsonConverterDateTimeMilliseconds))]
        [JsonProperty("date_updated_lt")]
        public DateTime? DateUpdatedLessThan { get; set; }
    }
}