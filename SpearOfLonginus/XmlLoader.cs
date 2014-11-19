using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace SpearOfLonginus
{
    /// <summary>
    /// Loads objects using IXmlLoadable.
    /// </summary>
    public static class XmlLoader
    {
        /// <summary>
        /// The types used for generating loadables.
        /// </summary>
        private static Dictionary<string, Type> _types;
        /// <summary>
        /// Gets a value indicating whether [initialized].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [initialized]; otherwise, <c>false</c>.
        /// </value>
        public static bool Initialized { get; private set; }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public static void Initialize()
        {
            _types = new Dictionary<string, Type>();
            Initialized = true;
        }

        /// <summary>
        /// Checks if initialized, calling an exception if it isn't.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">XmlLoader is not initialized. Please run Initialize first.</exception>
        public static void CheckInitialized()
        {
            if (!Initialized)
            {
                throw new InvalidOperationException("XmlLoader is not initialized. Please run Initialize first.");
            }
        }

        /// <summary>
        /// Adds the sample to the list of loadable types.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public static void AddSample(IXmlLoadable item)
        {
            var type = item.GetType();
            
            AddSample(type.Name, type);
        }

        /// <summary>
        /// Adds the sample to the list of loadable types.
        /// </summary>
        /// <param name="key">The key of the item.</param>
        /// <param name="item">The item to add.</param>
        public static void AddSample(string key, IXmlLoadable item)
        {
            AddSample(key, item.GetType());
        }

        /// <summary>
        /// Adds the sample to the list of loadable types.
        /// </summary>
        /// <param name="key">The key of the item.</param>
        /// <param name="type">The item's type.</param>
        public static void AddSample(string key, Type type)
        {
            CheckInitialized();

            _types.Add(key.ToLower(), type);
        }

        /// <summary>
        /// Creates an object from the list of loadable types.
        /// </summary>
        /// <param name="key">The key of the object.</param>
        /// <param name="element">The element to give the object.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Item is not IXmlLoadable</exception>
        public static IXmlLoadable CreateObject(string key, XElement element)
        {
            CheckInitialized();

            Type type = _types[key.ToLower()]; //We'll let this throw natural exceptions. 

            var item = Activator.CreateInstance(type) as IXmlLoadable; //Create the object and convert to IXmlLoadable

            if (item == null)
            {
                throw new Exception("Item is not IXmlLoadable");
            }

            item.LoadFromXml(element); 

            return item;
        }
    }
}