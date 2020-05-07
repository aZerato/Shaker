namespace shaker.domain.dto.Movements
{
    public class BodyPartDto : IBaseDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImgPath { get; set; }

        public string Error { get; set; }
    }
}
