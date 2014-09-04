using System.Xml.Linq;
using Microsoft.Xna.Framework.Graphics;
using SOLXNA.Animations;
using SpearOfLonginus;
using SpearOfLonginus.Maps;

namespace SOLXNA.Maps
{
    public class XnaBackdrop : Backdrop
    {
        #region Variables

        /// <summary>
        /// The loaded texture.
        /// </summary>
        public Texture2D Texture { get; protected set; }

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
        public XnaBackdrop(string textureid, Vector position, Vector parallax, Vector autoparallax, bool loopx, bool loopy, int layer)
            : base(textureid, position, parallax, autoparallax, loopx, loopy, layer)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Backdrop"/> class.
        /// </summary>
        /// <param name="element">The element used for loading.</param>
        public XnaBackdrop(XElement element)
            : base(element)
        {

        }

        #endregion

        #region Functions

        /// <summary>
        /// Loads the texture.
        /// </summary>
        /// <param name="texturecache">The texture cache to use for texture loading.</param>
        public virtual void LoadContent(TextureCache texturecache)
        {
            Texture = texturecache.GetTexture(TextureID);
        }

        /// <summary>
        /// Unloads the texture.
        /// </summary>
        public virtual void UnloadContent()
        {
            Texture = null; //Is removing the texture enough? The texture cache should dispose of anything. I'm not sure if this piece of code is the right thing to do, in all honesty.
        }

        #endregion
    }
}