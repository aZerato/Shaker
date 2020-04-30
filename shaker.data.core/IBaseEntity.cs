using System;

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
        int Id { get; set; }

        /// <summary>
        /// Gets or sets the Creation date.
        /// </summary>
        DateTime Creation { get; set; }
    }
}
