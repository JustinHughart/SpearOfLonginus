using System.Xml.Linq;
using SpearOfLonginus.Input;

namespace SpearOfLonginus.Entities
{
    /// <summary>
    /// A logical component for the entity class.
    /// </summary>
    public abstract class Component : IXmlLoadable
    {
        #region Variables
        /// <summary>
        /// The entity who owns the component.
        /// </summary>
        public Entity Owner;
        /// <summary>
        /// Gets or sets a value indicating whether the component is initialized..
        /// </summary>
        /// <value>
        ///   <c>true</c> if [initialized]; otherwise, <c>false</c>.
        /// </value>
        public bool Initialized { get; protected set; }
        /// <summary>
        /// Whether or not the the component is dead and should be removed.
        /// </summary>
        public bool Dead;

        #endregion 

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Component"/> class.
        /// </summary>
        public Component()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Component"/> class.
        /// </summary>
        /// <param name="owner">The entity who owns the component.</param>
        public Component(Entity owner)
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
        /// Uses XML to initialize the object.
        /// </summary>
        /// <param name="element">The element used for loading.</param>
        public virtual void LoadFromXml(XElement element)
        {

        }

        #endregion
        
    }
}
