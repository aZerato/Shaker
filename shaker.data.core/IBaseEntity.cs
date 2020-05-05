using LiteDB;

namespace shaker.data.core
{
    /// <summary>
    /// Contract for Entity.
    /// </summary>
    public interface IBaseEntity
    {
        /// <summary>
        /// Gets or sets the Identifier.
        /// </summary>
        [BsonId]
        string Id { get; }
    }
}
