﻿using System;
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

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Backdrop"/> class.
        /// </summary>
        /// <param name="textureid">The texture ID.</param>
        /// <param name="position">The initial position of the backdrop.</param>
        /// <param name="parallax">The rate at which parallax is applied to the object.</param>
        /// <param name="autoparallax">The rate at which a drop automatically scrolls.</param>
        /// <param name="loopx">Whether or not the backdrop is looped horizontally.</param>
        /// <param name="loopy">Whether or not the backdrop is looped horizontally.</param>
        /// <param name="layer">The layer pf the backdrop, used for sorting.</param>
        public Backdrop(string textureid, Vector position, Vector parallax, Vector autoparallax, bool loopx, bool loopy, int layer)
        {
            TextureID = textureid;
            Position = position;
            Parallax = parallax;
            AutoParallax = autoparallax;
            LoopX = loopx;
            LoopY = loopy;
            Layer = layer;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Backdrop"/> class.
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
        public virtual void Update()
        {
            Position += AutoParallax;
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
        public virtual void LoadFromXElement(XElement element)
        {
            TextureID = "";
            Position = new Vector(0);
            Parallax = new Vector(0);
            AutoParallax = new Vector(0);
            LoopX = false;
            LoopY = false;
            Layer = 0;

            var properties = element.Attributes();

            foreach (var attribute in properties)
            {
                if (attribute.Name == "texture")
                {
                    TextureID = attribute.Value;
                    continue;
                }

                if (attribute.Name == "positionx")
                {
                    float.TryParse(attribute.Value, out Position.X);
                    continue;
                }

                if (attribute.Name == "positiony")
                {
                    float.TryParse(attribute.Value, out Position.Y);
                    continue;
                }

                if (attribute.Name == "parrallaxx")
                {
                    float.TryParse(attribute.Value, out Parallax.X);
                    continue;
                }

                if (attribute.Name == "parrallaxy")
                {
                    float.TryParse(attribute.Value, out Parallax.Y);
                    continue;
                }

                if (attribute.Name == "autoparrallaxx")
                {
                    float.TryParse(attribute.Value, out AutoParallax.X);
                    continue;
                }

                if (attribute.Name == "autoparrallaxy")
                {
                    float.TryParse(attribute.Value, out AutoParallax.Y);
                    continue;
                }

                if (attribute.Name == "loopx")
                {
                    LoopX = true;
                    continue;
                }

                if (attribute.Name == "loopy")
                {
                    LoopY = true;
                    continue;
                }

                if (attribute.Name == "layer")
                {
                    int.TryParse(attribute.Value, out Layer);
                }
            }
        }

        #endregion
    }
}