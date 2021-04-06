using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Chinchilla.ClickUp.Helpers;
using Newtonsoft.Json;

namespace ClickUpViewer.Domain.ClickUp
{
    public class ParamsGetTimeEntries
    {
        public ParamsGetTimeEntries(DateTime? startDate, DateTime? endDate, IEnumerable<string> assignee)
        {
            StartDate = startDate;
            EndDate = endDate;
            Assignee = assignee;
        }

        [DataMember(Name = "start_date")]
        [JsonConverter(typeof(JsonConverterDateTimeMilliseconds))]
        [JsonProperty("start_date")]
        public DateTime? StartDate { get; set; }

        [DataMember(Name = "end_date")]
        [JsonConverter(typeof(JsonConverterDateTimeMilliseconds))]
        [JsonProperty("end_date")]
        public DateTime? EndDate { get; set; }

        [DataMember(Name = "assignee")]
        [JsonProperty("assignee")]
        public IEnumerable<string> Assignee { get; set; }
    }
}