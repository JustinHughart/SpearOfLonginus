using System.Xml.Linq;
using SpearOfLonginus.Entities;

namespace SpearOfLonginus.Maps
{
    /// <summary>
    /// A tile's effect. 
    /// </summary>
    public class TileEffect : IXmlLoadable
    {
        /// <summary>
        /// The owner of the effect.
        /// </summary>
        public Tile Owner;

        /// <summary>
        /// Initializes a new instance of the <see cref="TileEffect"/> class.
        /// </summary>
        public TileEffect()
        {
           
        }

        /// <summary>
        /// Called when the effect is activated.
        /// </summary>
        /// <param name="position">The position of the tile that was activated.</param>
        /// <param name="entity">The entity that triggered the activation.</param>
        public virtual void OnActivate(Vector position, Entity entity)
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
