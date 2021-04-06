using System.Collections.Generic;
using Chinchilla.ClickUp.Helpers;
using Chinchilla.ClickUp.Responses.Model;
using Newtonsoft.Json;

namespace ClickUpViewer.Domain.ClickUp
{
    public class ResponseTasksFull : IResponse
    {
        public ResponseTasksFull() { }

        [JsonProperty("tasks")]
        public List<ResponseModelTaskFull> Tasks { get; set; }
    }
}