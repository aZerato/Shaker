using System;
using System.Collections.Generic;

namespace shaker.domain.dto.Channels
{
    public class ChannelDto : IBaseDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImgPath { get; set; }

        public IEnumerable<MessageDto> Messages { get; set; }

        public DateTime Creation { get; set; }

        public string Error { get; set; }
    }
}
