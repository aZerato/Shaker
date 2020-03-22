using System;
using Newtonsoft.Json;

namespace shaker.Areas.WebSocketArea.Modules.ChatModule.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ChatWsMessage
    {
        public ChatWsMessage()
        {
            Date = DateTime.Now.ToShortDateString();
            Type = ChatWsMessageType.Message;
        }

        [JsonProperty(PropertyName = "date")]
        public string Date { get; set; }

        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

        [JsonIgnore]
        public ChatWsMessageType Type { get; set; }
    }
}
