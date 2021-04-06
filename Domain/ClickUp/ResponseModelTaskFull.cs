
using System;
using System.Collections.Generic;
using System.Linq;
using Chinchilla.ClickUp.Helpers;
using Chinchilla.ClickUp.Responses.Model;
using Newtonsoft.Json;

namespace ClickUpViewer.Domain.ClickUp
{
    public partial class ResponseModelTaskFull : IResponse
    {
        public ResponseModelTaskFull()
        {
        }
        
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("project")]
        public ResponseModelFolder Project { get; set; }
        [JsonProperty("list")]
        public ResponseModelList List { get; set; }
        [JsonConverter(typeof(JsonConverterTimeSpanMilliseconds))]
        [JsonProperty("time_estimated")]
        public TimeSpan TimeEstimate { get; set; }
        [JsonProperty("points")]
        public double? Points { get; set; }
        [JsonConverter(typeof(JsonConverterDateTimeMilliseconds))]
        [JsonProperty("start_date")]
        public DateTime? StartDate { get; set; }
        [JsonConverter(typeof(JsonConverterDateTimeMilliseconds))]
        [JsonProperty("due_date")]
        public DateTime? DueDate { get; set; }
        [JsonProperty("priority")]
        public ResponseModelPriority Priority { get; set; }
        [JsonProperty("parent")]
        public string Parent { get; set; }
        [JsonProperty("tags")]
        public List<ResponseModelTag> Tags { get; set; }
        [JsonProperty("assignees")]
        public List<ResponseModelUser> Assignees { get; set; }
        [JsonProperty("creator")]
        public ResponseModelUser Creator { get; set; }
        [JsonConverter(typeof(JsonConverterDateTimeMilliseconds))]
        [JsonProperty("date_closed")]
        public DateTime? DateClosed { get; set; }
        [JsonConverter(typeof(JsonConverterDateTimeMilliseconds))]
        [JsonProperty("date_updated")]
        public DateTime? DateUpdated { get; set; }
        [JsonConverter(typeof(JsonConverterDateTimeMilliseconds))]
        [JsonProperty("date_created")]
        public DateTime? DateCreated { get; set; }
        [JsonProperty("orderindex")]
        public string OrderIndex { get; set; }
        [JsonProperty("status")]
        public ResponseModelStatus Status { get; set; }
        [JsonProperty("text_content")]
        public string TextContent { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("space")]
        public ResponseModelSpace Space { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }

        public int GetQty()
        {
            return Description.Split(Environment.NewLine)
                .Where(x => x.StartsWith("#qty:"))
                .Select(x => x.Substring(5))
                .Select(int.Parse)
                .FirstOrDefault();
        }
    }
}