using System;
using Newtonsoft.Json;

namespace shaker.domain.dto.Channels
{
    public class MessageDto : IBaseDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("channelId")]
        public int ChannelId { get; set; }

        [JsonProperty("type")]
        public MessageType Type { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("creation")]
        public DateTime Creation { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }
    }
}
