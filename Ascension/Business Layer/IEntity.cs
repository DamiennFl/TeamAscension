using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascension.Business_Layer
{
    /// <summary>
    /// Interface for all entities in the game.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Gets or Sets Entity Health.
        /// </summary>
        int Health { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or Sets a value indicating if the entity is invincible.
        /// </summary>
        bool IsInvincible { get; set; }

        /// <summary>
        /// Gets a value indicating whether the entity is dead.
        /// </summary>
        bool IsDead { get; }

        /// <summary>
        /// Activates the invincibility of the entity.
        /// </summary>
        void ActivateInvincibility();
    }
}
