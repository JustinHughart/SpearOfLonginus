using System;
using System.Xml.Linq;

namespace SpearOfLonginus.Maps
{
    /// <summary>
    /// A backdrop for SOL. These can appear either in the background or foreground.
    /// </summary>
    public class Backdrop : IComparable
    {
        #region Variables

        /// <summary>
        /// The texture ID.
        /// </summary>
        public string TextureID;
        /// <summary>
        /// The initial position of the backdrop.
        /// </summary>
        public Vector Position;
        /// <summary>
        /// The rate at which parallax is applied to the object.
        /// </summary>
        public Vector Parallax;
        /// <summary>
        /// The rate at which a drop automatically scrolls.
        /// </summary>
        public Vector AutoParallax;
        /// <summary>
        /// Whether or not the backdrop is looped horizontally.
        /// </summary>
        public bool LoopX;
        /// <summary>
        /// Whether or not the backdrop is looped vertically.
        /// </summary>
        public bool LoopY;
        /// <summary>
        /// The layer of the backdrop, used for sorting.
        /// </summary>
        public int Layer;
        /// <summary>
        /// Whether or not the wrap the coordinates of the backdrop on the X axis.
        /// </summary>
        public bool WrapCoordsX;
        /// <summary>
        /// Whether or not the wrap the coordinates of the backdrop on the Y axis.
        /// </summary>
        public bool WrapCoordsY;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Backdrop" /> class.
        /// </summary>
        /// <param name="textureid">The texture ID.</param>
        /// <param name="position">The initial position of the backdrop.</param>
        /// <param name="parallax">The rate at which parallax is applied to the object.</param>
        /// <param name="autoparallax">The rate at which a drop automatically scrolls.</param>
        /// <param name="loopx">Whether or not the backdrop is looped horizontally.</param>
        /// <param name="loopy">Whether or not the backdrop is looped horizontally.</param>
        /// <param name="layer">The layer pf the backdrop, used for sorting.</param>
        /// <param name="wrapcoordsx">Whether or not the wrap the coordinates of the backdrop on the X axis.</param>
        /// <param name="wrapcoordsy">Whether or not the wrap the coordinates of the backdrop on the Y axis.</param>
        public Backdrop(string textureid, Vector position, Vector parallax, Vector autoparallax, bool loopx, bool loopy, int layer, bool wrapcoordsx, bool wrapcoordsy)
        {
            TextureID = textureid;
            Position = position;
            Parallax = parallax;
            AutoParallax = autoparallax;
            LoopX = loopx;
            LoopY = loopy;
            Layer = layer;
            WrapCoordsX = wrapcoordsx;
            WrapCoordsY = wrapcoordsy;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Backdrop" /> class.
        /// </summary>
        /// <param name="element">The element used for loading.</param>
        public Backdrop(XElement element)
        {
           LoadFromXElement(element);
        }

        #endregion

        #region Functions

        /// <summary>
        /// Updates the backdrop.
        /// </summary>
        /// <param name="deltatime">The speed to update the backdrop at.</param>
        public virtual void Update(float deltatime)
        {
            Position += AutoParallax * deltatime;
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
            if (obj is Backdrop)
            {
                var back2 = obj as Backdrop;
                return Layer.CompareTo(back2.Layer);
            }

            return 0;
        }

        /// <summary>
        /// Loads from and XElement.
        /// </summary>
        /// <param name="element">The element used for loading.</param>
        /// <exception cref="System.Exception">
        /// No properties in backdrop.
        /// or
        /// Name attribute is missing from backdrop.
        /// or
        /// Value attribute is missing from backdrop.
        /// </exception>
        public virtual void LoadFromXElement(XElement element)
        {
            TextureID = "";
            Position = new Vector(0);
            Parallax = new Vector(0);
            AutoParallax = new Vector(0);
            LoopX = false;
            LoopY = false;
            Layer = 0;
            WrapCoordsX = false;
            WrapCoordsY = false;

            var properties = element.Element("properties");

            if (properties == null)
            {
                throw new Exception("No properties in backdrop.");
            }
            
            foreach (var attribute in properties.Elements("property"))
            {
                var nameattribute = attribute.Attribute("name");
                var valueattribute = attribute.Attribute("value");

                if (nameattribute == null)
                {
                    throw new Exception("Name attribute is missing from backdrop.");
                }

                if (valueattribute == null)
                {
                    throw new Exception("Value attribute is missing from backdrop.");
                }

                string name = nameattribute.Value;
                string value = valueattribute.Value;

                if (name.Equals("texture", StringComparison.OrdinalIgnoreCase))
                {
                    TextureID = value;
                    continue;
                }

                if (name.Equals("positionx", StringComparison.OrdinalIgnoreCase))
                {
                    float.TryParse(value, out Position.X);
                    continue;
                }

                if (name.Equals("positiony", StringComparison.OrdinalIgnoreCase))
                {
                    float.TryParse(value, out Position.Y);
                    continue;
                }

                if (name.Equals("parallaxx", StringComparison.OrdinalIgnoreCase))
                {
                    float.TryParse(value, out Parallax.X);
                    continue;
                }

                if (name.Equals("parallaxy", StringComparison.OrdinalIgnoreCase))
                {
                    float.TryParse(value, out Parallax.Y);
                    continue;
                }

                if (name.Equals("autoparallaxx", StringComparison.OrdinalIgnoreCase))
                {
                    float.TryParse(value, out AutoParallax.X);
                    continue;
                }

                if (name.Equals("autoparallaxy", StringComparison.OrdinalIgnoreCase))
                {
                    float.TryParse(value, out AutoParallax.Y);
                    continue;
                }

                if (name.Equals("loopx", StringComparison.OrdinalIgnoreCase))
                {
                    LoopX = true;
                    continue;
                }

                if (name.Equals("loopy", StringComparison.OrdinalIgnoreCase))
                {
                    LoopY = true;
                    continue;
                }

                if (name.Equals("layer", StringComparison.OrdinalIgnoreCase))
                {
                    int.TryParse(value, out Layer);
                    continue;
                }

                if (name.Equals("wrapcoordsx", StringComparison.OrdinalIgnoreCase))
                {
                    WrapCoordsX = true;
                    continue;
                }

                if (name.Equals("wrapcoordsy", StringComparison.OrdinalIgnoreCase))
                {
                    WrapCoordsY = true;
                    continue;
                }

                if (name.Equals("wrapcoords", StringComparison.OrdinalIgnoreCase))
                {
                    WrapCoordsX = true;
                    WrapCoordsY = true;
                    continue;
                }
            }
        }

        #endregion
    }
}
