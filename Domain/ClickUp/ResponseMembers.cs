using System.Collections.Generic;
using Newtonsoft.Json;

namespace ClickUpViewer.Domain.ClickUp
{
    public class ResponseMembers : Chinchilla.ClickUp.Helpers.IResponse
    {
        public ResponseMembers(){}

        [JsonProperty("members")]
        public List<ResponseModelMember> Members { get; set; }
    }
}