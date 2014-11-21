using SpearOfLonginus.Entities;

namespace SpearOfLonginus.Maps
{
    /// <summary>
    /// A tile's effect. 
    /// </summary>
    public class TileEffect
    {
        /// <summary>
        /// The owner of the effect.
        /// </summary>
        protected Tile Owner;

        /// <summary>
        /// Initializes a new instance of the <see cref="TileEffect"/> class.
        /// </summary>
        /// <param name="owner">The owner of the effect.</param>
        public TileEffect(Tile owner)
        {
            Owner = owner;
        }

        /// <summary>
        /// Called when the effect is activated.
        /// </summary>
        /// <param name="position">The position of the tile that was activated.</param>
        /// <param name="entity">The entity that triggered the activation.</param>
        public virtual void OnActivate(Vector position, Entity entity)
        {

        }
    }
}
