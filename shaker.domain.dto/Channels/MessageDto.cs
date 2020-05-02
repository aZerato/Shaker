using System;

namespace shaker.domain.dto.Channels
{
    public class MessageDto : IBaseDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int ChannelId { get; set; }

        public MessageType Type { get; set; }

        public string Content { get; set; }

        public DateTime Creation { get; set; }

        public string Error { get; set; }
    }
}
