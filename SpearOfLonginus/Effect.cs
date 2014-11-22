using System.Xml.Linq;
using SpearOfLonginus.Entities;

namespace SpearOfLonginus
{
    /// <summary>
    /// An effect that is activated when interacted with. 
    /// </summary>
    public class Effect<T> : IXmlLoadable where T : class
    {
        /// <summary>
        /// The owner of the effect.
        /// </summary>
        public T Owner;

        /// <summary>
        /// Initializes a new instance of the <see cref="Effect{T}"/> class.
        /// </summary>
        public Effect()
        {
           
        }

        /// <summary>
        /// Called when the effect is activated.
        /// </summary>
        /// <param name="position">The position of the tile that was activated.</param>
        /// <param name="entity">The entity that triggered the activation.</param>
        public virtual bool OnActivate(Vector position, Entity entity)
        {
            return false;
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        /// <param name="deltatime">The time that has passed since the last frame.</param>
        public virtual void Update(float deltatime)
        {

        }

        /// <summary>
        /// Uses XML to initialize the object.
        /// </summary>
        /// <param name="element">The element used for loading.</param>
        public void LoadFromXml(XElement element)
        {
            
        }
    }
}
