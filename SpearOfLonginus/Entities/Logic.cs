using SpearOfLonginus.Input;

namespace SpearOfLonginus.Entities
{
    /// <summary>
    /// An AI component for the entity class.
    /// </summary>
    public abstract class Logic
    {
        #region Variables
        /// <summary>
        /// The entity who owns the logic.
        /// </summary>
        protected Entity Owner;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Logic"/> class.
        /// </summary>
        /// <param name="owner">The entity who owns the component.</param>
        public Logic(Entity owner)
        {
            Owner = owner;
        }

        #endregion

        #region Functions

        /// <summary>
        /// Updates the component.
        /// </summary>
        /// <param name="packet">The packet of input data.</param>
        /// <param name="deltatime">The time that has passed since last update.</param>
        public virtual void Update(InputPacket packet, float deltatime)
        {

        }

        /// <summary>
        /// Modifies the packet to determine its input.
        /// </summary>
        /// <param name="packet">The packet.</param>
        public virtual void GetInput(InputPacket packet)
        {

        }

        #endregion

    }
}
