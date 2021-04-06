using Newtonsoft.Json;

namespace ClickUpViewer.Domain.ClickUp
{
    public class ResponseModelMember : Chinchilla.ClickUp.Helpers.IResponse
    {
        public ResponseModelMember() { }

        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("profilePicture")]
        public string ProfilePicture { get; set; }
    }
}