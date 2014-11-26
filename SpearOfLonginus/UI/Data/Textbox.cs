using System.Xml.Linq;

namespace SpearOfLonginus.UI.Data
{
    /// <summary>
    /// A scalable text box for Spear of Longinus.
    /// </summary>
    public class Textbox : IXmlLoadable
    {
        /// <summary>
        /// The text to be displayed.
        /// </summary>
        public string Text;
        /// <summary>
        /// The area of the screen the box takes up.
        /// </summary>
        public Rectangle Area;

        /// <summary>
        /// Updates the specified deltatime.
        /// </summary>
        /// <param name="deltatime">The deltatime.</param>
        public void Update(float deltatime)
        {
            
        }

        /// <summary>
        /// Uses XML to initialize the object.
        /// </summary>
        /// <param name="element">The element used for loading.</param>
        public virtual void LoadFromXml(XElement element)
        {
            
        }
    }
}
