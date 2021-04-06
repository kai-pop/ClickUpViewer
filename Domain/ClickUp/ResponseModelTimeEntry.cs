using Chinchilla.ClickUp.Responses.Model;
using Newtonsoft.Json;

namespace ClickUpViewer.Domain.ClickUp
{
    public class ResponseModelTimeEntry : Chinchilla.ClickUp.Helpers.IResponse
    {
        public ResponseModelTimeEntry() { }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("task")]
        public ResponseModelTask Task { get; set; }

        [JsonProperty("user")]
        public ResponseModelUser User { get; set; }

        [JsonProperty("start")]
        public long Start { get; set; }

        [JsonProperty("end")]
        public long End { get; set; }

        [JsonProperty("duration")]
        public long Duration { get; set; }
    }
}