using System;
using System.Xml.Linq;

namespace SpearOfLonginus.Animations
{
    /// <summary>
    /// An animation frame for Spear of Longinus.
    /// </summary>
    public class Frame : IXmlLoadable
    {
        #region Variables

        /// <summary>
        /// The ID used for  texture loading.
        /// </summary>
        /// <value>
        /// The ID used for  texture loading.
        /// </value>
        public string TextureID { get; protected set; }

        /// <summary>
        /// The area on the texture that should be drawn.
        /// </summary>
        /// <value>
        /// The area on the texture that should be drawn.
        /// </value>
        public Rectangle DrawArea { get; protected set; }

        /// <summary>
        /// The origin of the frame. Used for rotation and to offset the sprite.
        /// </summary>
        /// <value>
        /// The origin of the frame. Used for rotation and to offset the sprite.
        /// </value>
        public Vector Origin { get; protected set; }

        /// <summary>
        /// The time until the frame changes. Is a float to support delta time.
        /// </summary>
        /// <value>
        /// The time until the frame changes. Is a float to support delta time.
        /// </value>
        public float TimeTillNext { get; protected set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Frame"/> class.
        /// </summary>
        public Frame()
        {
            TextureID = "";
            DrawArea = new Rectangle(0, 0, 0, 0);
            Origin = Vector.Zero;
            TimeTillNext = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Frame" /> class.
        /// </summary>
        /// <param name="textureid">The ID used for  texture loading.</param>
        /// <param name="drawarea">The area on the texture that should be drawn.</param>
        /// <param name="origin">The origin of the frame. Used for rotation and to offset the sprite.</param>
        /// <param name="timetillnext">The time until the frame changes.</param>
        public Frame(string textureid, Rectangle drawarea, Vector origin, float timetillnext)
        {
            TextureID = textureid;
            DrawArea = drawarea;
            Origin = origin;
            TimeTillNext = timetillnext;
        }

        #endregion

        #region Functions

        /// <summary>
        /// Uses XML to initialize the object.
        /// </summary>
        /// <param name="element">The element used for loading..</param>
        public virtual void LoadFromXml(XElement element)
        {
            //Attributes first.
            foreach (var attribute in element.Attributes())
            {
                if (attribute.Name.LocalName.Equals("textureid", StringComparison.OrdinalIgnoreCase))
                {
                    TextureID = attribute.Value;
                    continue;
                }

                if (attribute.Name.LocalName.Equals("timetillnext", StringComparison.OrdinalIgnoreCase))
                {
                    float value;

                    if (float.TryParse(attribute.Value, out value))
                    {
                        TimeTillNext = value;
                    }

                    continue;
                }
            }

            //Draw Area.
            XElement drawareaelement = element.Element("drawarea");

            if (drawareaelement != null)
            {
                int x = 0;
                int y = 0;
                int w = 0;
                int h = 0;

                foreach (var attribute in drawareaelement.Attributes())
                {
                    if (attribute.Name.LocalName.Equals("x", StringComparison.OrdinalIgnoreCase))
                    {
                        int.TryParse(attribute.Value, out x);

                        continue;
                    }

                    if (attribute.Name.LocalName.Equals("y", StringComparison.OrdinalIgnoreCase))
                    {
                        int.TryParse(attribute.Value, out y);

                        continue;
                    }

                    if (attribute.Name.LocalName.Equals("w", StringComparison.OrdinalIgnoreCase))
                    {
                        int.TryParse(attribute.Value, out w);

                        continue;
                    }

                    if (attribute.Name.LocalName.Equals("h", StringComparison.OrdinalIgnoreCase))
                    {
                        int.TryParse(attribute.Value, out h);

                        continue;
                    }    
                }

                DrawArea = new Rectangle(x, y, w, h);
            }

            //Origin
            XElement originelement = element.Element("origin");

            if (originelement != null)
            {
                int x = 0;
                int y = 0;

                foreach (var attribute in originelement.Attributes())
                {
                    if (attribute.Name.LocalName.Equals("x", StringComparison.OrdinalIgnoreCase))
                    {
                        int.TryParse(attribute.Value, out x);

                        continue;
                    }

                    if (attribute.Name.LocalName.Equals("y", StringComparison.OrdinalIgnoreCase))
                    {
                        int.TryParse(attribute.Value, out y);

                        continue;
                    }
                }

                Origin = new Vector(x, y);
            }
        }

        #endregion

    }
}