using shaker.data.core;

namespace shaker.domain
{
    /// <summary>
    /// Contract for Dto.
    /// </summary>
    public interface IBaseDto : IBaseEntity
    {
        public string Error { get; set; }
    }
}
