using System.Xml.Linq;
using SpearOfLonginus.Input;

namespace SpearOfLonginus.Entities
{
    /// <summary>
    /// An AI component for the entity class.
    /// </summary>
    public abstract class Logic : IXmlLoadable
    {
        #region Variables
        /// <summary>
        /// The entity who owns the logic.
        /// </summary>
        protected Entity Owner;
        /// <summary>
        /// Gets or sets a value indicating whether the logic is initialized..
        /// </summary>
        /// <value>
        ///   <c>true</c> if [initialized]; otherwise, <c>false</c>.
        /// </value>
        public bool Initialized { get; protected set; }
        /// <summary>
        /// Whether or not the logic is dead and should be removed.
        /// </summary>
        public bool Dead;

        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Logic"/> class.
        /// </summary>
        public Logic()
        {
            Initialized = false;
            Dead = false;
        }

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
            if (!Initialized)
            {
                Initialize();
            }
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public virtual void Initialize()
        {
            Initialized = true;
        }

        /// <summary>
        /// Modifies the packet to determine its input.
        /// </summary>
        /// <param name="packet">The packet.</param>
        public virtual void GetInput(InputPacket packet)
        {

        }

        /// <summary>
        /// Uses XML to initialize the object.
        /// </summary>
        /// <param name="element">The element used for loading..</param>
        public virtual void LoadFromXml(XElement element)
        {

        }

        #endregion
        
    }
}
