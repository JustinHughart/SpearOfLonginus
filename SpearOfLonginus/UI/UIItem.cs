using System;
using System.Xml.Linq;

namespace SpearOfLonginus.UI
{
    /// <summary>
    /// An item for a UI.
    /// </summary>
    public abstract class UIItem : IXmlLoadable, IComparable
    {
        /// <summary>
        /// The item's identifier.
        /// </summary>
        public string ID;
        /// <summary>
        /// The layer the item is on.
        /// </summary>
        public string Layer;

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
        public virtual void LoadFromXml(XElement element)
        {
            
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance is less than <paramref name="obj" />. Zero This instance is equal to <paramref name="obj" />. Greater than zero This instance is greater than <paramref name="obj" />.
        /// </returns>
        public int CompareTo(object obj)
        {
            UIItem other = obj as UIItem;

            if (other == null)
            {
                return 0;
            }

            return this.Layer.CompareTo(other.Layer);
        }
    }
}
