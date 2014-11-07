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
        protected Entity Owner;

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
