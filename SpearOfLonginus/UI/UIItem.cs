using System.Xml.Linq;

namespace SpearOfLonginus.UI
{
    /// <summary>
    /// An item for a UI.
    /// </summary>
    public abstract class UIItem : IXmlLoadable
    {
        /// <summary>
        /// The item's identifier.
        /// </summary>
        public string ID;

        /// <summary>
        /// Updates this instance.
        /// </summary>
        /// <param name="deltatime">The time since the last frame.</param>
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
