using SpearOfLonginus.Input;

namespace SpearOfLonginus.Entities
{
    /// <summary>
    /// A logical component for the entity class.
    /// </summary>
    public abstract class EntityComponent
    {
        /// <summary>
        /// The entity who owns the component.
        /// </summary>
        protected Entity Owner;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityComponent"/> class.
        /// </summary>
        /// <param name="owner">The entity who owns the component.</param>
        public EntityComponent(Entity owner)
        {
            Owner = owner;
        }

        /// <summary>
        /// Updates the specified packet.
        /// </summary>
        /// <param name="packet">The packet of input data.</param>
        /// <param name="deltatime">The time that has passed since last update.</param>
        public virtual void Update(InputPacket packet, float deltatime)
        {
            
        }
    }
}
