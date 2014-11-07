using System.Xml.Linq;

namespace SpearOfLonginus
{
    /// <summary>
    /// An interface that says you may load the object from XML.
    /// </summary>
    public interface IXmlLoadable
    {
        /// <summary>
        /// Uses XML to initialize the object.
        /// </summary>
        /// <param name="element">The element used for loading..</param>
        void LoadFromXml(XElement element);
    }
}