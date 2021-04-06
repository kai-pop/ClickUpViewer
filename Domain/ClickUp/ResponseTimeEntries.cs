using System.Collections.Generic;
using Newtonsoft.Json;

namespace ClickUpViewer.Domain.ClickUp
{
    public class ResponseTimeEntries : Chinchilla.ClickUp.Helpers.IResponse
    {
        public ResponseTimeEntries(){}

        [JsonProperty("data")]
        public List<ResponseModelTimeEntry> Data { get; set; }
    }
}