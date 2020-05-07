﻿namespace shaker.domain.dto.Movements
{
    public class MovementDto : IBaseDto
    {
        public string Id { get; set; }

        public MovementTypeDto MovementType { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImgPath { get; set; }

        public string Error { get; set; }
    }
}
