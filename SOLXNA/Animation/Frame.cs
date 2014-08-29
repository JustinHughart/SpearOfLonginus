using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpearOfLonginus;
using SpearOfLonginus.Animation;
using SOLRect = System.Drawing.Rectangle;
using XNARect = Microsoft.Xna.Framework.Rectangle;

namespace SOLXNA.Animation
{
    /// <summary>
    /// An XNA binding for SOLFrame. 
    /// </summary>
    public class Frame : SOLFrame
    {
        #region Variables

        /// <summary>
        /// The loaded texture.
        /// </summary>
        public Texture2D Texture;

        /// <summary>
        /// Returns the draw area in SOLFrame converted to an XNA Rectangle.
        /// </summary>
        /// <value>
        /// The draw area in SOLFrame converted to an XNA Rectangle.
        /// </value>
        public XNARect XNADrawArea
        {
            get { return new XNARect(DrawArea.X, DrawArea.Y, DrawArea.Width, DrawArea.Height); }
        }

        /// <summary>
        /// Returns the origin in SOLFrame converted to a Vector2.
        /// </summary>
        /// <value>
        /// The origin in SOLFrame converted to a Vector2.
        /// </value>
        public Vector2 XNAOrigin
        {
            get { return new Vector2(Origin.X, Origin.Y); }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Frame"/> class.
        /// </summary>
        /// <param name="textureid">The ID used for  texture loading.</param>
        /// <param name="drawarea">The area on the texture that should be drawn.</param>
        /// <param name="origin">The origin of the frame. Used for rotation and to offset the sprite.</param>
        /// <param name="timetillnext">The time until the frame changes.</param>
        public Frame(string textureid, SOLRect drawarea, SOLVector origin, float timetillnext)
            : base(textureid, drawarea, origin, timetillnext)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Frame"/> class.
        /// </summary>
        /// <param name="frame">The SOLFrame to be converted to Frame.</param>
        public Frame(SOLFrame frame)
            : base(frame.TextureID, frame.DrawArea, frame.Origin, frame.TimeTillNext)
        {

        }

        #endregion

        #region Functions

        /// <summary>
        /// Loads the texture.
        /// </summary>
        /// <param name="texturemanager">The texture manager to use for texture loading.</param>
        public virtual void LoadContent(TextureManager texturemanager)
        {
            Texture = texturemanager.GetTexture(TextureID);
        }

        /// <summary>
        /// Unloads the texture.
        /// </summary>
        public virtual void UnloadContent()
        {
            Texture = null; //Is removing the texture enough? The texture manager should dispose of anything. I'm not sure if this piece of code is the right thing to do, in all honesty.
        }

        #endregion
    }
}