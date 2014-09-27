using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SOLXNA.Animations;
using SpearOfLonginus;
using SpearOfLonginus.Maps;
using Rectangle = SpearOfLonginus.Rectangle;

namespace SOLXNA.Maps
{
    /// <summary>
    /// An XNA implementation of SOL's Backdrop.
    /// </summary>
    public class XnaBackdrop : Backdrop
    {
        #region Variables

        /// <summary>
        /// The loaded texture.
        /// </summary>
        /// <value>
        /// The texture.
        /// </value>
        public Texture2D Texture { get; protected set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Backdrop" /> class.
        /// </summary>
        /// <param name="textureid">The texture ID.</param>
        /// <param name="position">The initial position of the backdrop.</param>
        /// <param name="parallax">The rate at which parallax is applied to the object.</param>
        /// <param name="autoparallax">The rate at which a backdrop automatically scrolls.</param>
        /// <param name="loopx">Whether or not the backdrop is looped horizontally.</param>
        /// <param name="loopy">Whether or not the backdrop is looped horizontally.</param>
        /// <param name="layer">The layer of the backdrop, used for sorting.</param>
        /// <param name="wrapcoordsx">Whether or not the wrap the coordinates of the backdrop on the X axis.</param>
        /// <param name="wrapcoordsy">Whether or not the wrap the coordinates of the backdrop on the Y axis.</param>
        public XnaBackdrop(string textureid, Vector position, Vector parallax, Vector autoparallax, bool loopx, bool loopy, int layer, bool wrapcoordsx, bool wrapcoordsy)
            : base(textureid, position, parallax, autoparallax, loopx, loopy, layer, wrapcoordsx, wrapcoordsy)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Backdrop" /> class.
        /// </summary>
        /// <param name="element">The element used for loading.</param>
        public XnaBackdrop(XElement element)
            : base(element)
        {

        }


        /// <summary>
        /// Initializes a new instance of the <see cref="XnaBackdrop"/> class.
        /// </summary>
        /// <param name="backdrop">The backdrop.</param>
        /// <param name="texturecache">The texturecache.</param>
        public XnaBackdrop(Backdrop backdrop, TextureCache texturecache) : base(backdrop.TextureID, backdrop.Position, backdrop.Parallax, backdrop.AutoParallax, backdrop.LoopX, backdrop.LoopY, backdrop.Layer, backdrop.WrapCoordsX, backdrop.WrapCoordsY)
        {
            if (texturecache != null)
            {
                LoadContent(texturecache);
            }
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

        /// <summary>
        /// Updates the backdrop.
        /// </summary>
        /// <param name="deltatime">The speed to update the backdrop at.</param>
        public override void Update(float deltatime)
        {
            base.Update(deltatime);

            if (WrapCoordsX)
            {
                if (Position.X < 0)
                {
                    Position.X += Texture.Width;
                }

                if (Position.X >= Texture.Width)
                {
                    Position.X -= Texture.Width;
                }
            }

            if (WrapCoordsY)
            {
                if (Position.Y < 0)
                {
                    Position.Y += Texture.Height;
                }

                if (Position.Y >= Texture.Height)
                {
                    Position.Y -= Texture.Height;
                }
            }
        }

        /// <summary>
        /// Draws the backdrop
        /// </summary>
        /// <param name="spritebatch">The spritebatch to use for drawing.</param>
        /// <param name="drawarea">The area of the screen.</param>
        /// <param name="cameraposition">The camera's position.</param>
        public virtual void Draw(SpriteBatch spritebatch, Rectangle drawarea, Vector2 cameraposition)
        {
            var drawposition = Position.ToXnaVector() + (cameraposition*Parallax.ToXnaVector());

            if (LoopX)
            {
                if (LoopY)
                {
                    DrawLoopBoth(spritebatch, drawarea, drawposition);
                }
                else
                {
                    DrawLoopX(spritebatch, drawarea, drawposition);
                }
            }
            else
            {
                if (LoopY)
                {
                    DrawLoopY(spritebatch, drawarea, drawposition);
                }
                else
                {
                    DrawBackdrop(spritebatch, drawarea, drawposition);
                }
            }
        }

        /// <summary>
        /// Actually draws the backdrop.
        /// </summary>
        /// <param name="spritebatch">The spritebatch to use for drawing.</param>
        /// <param name="drawarea">The area of the screen.</param>
        /// <param name="drawposition">The position to draw at.</param>
        protected virtual void DrawBackdrop(SpriteBatch spritebatch, Rectangle drawarea, Vector2 drawposition)
        {
            spritebatch.Draw(Texture, drawposition, Texture.Bounds, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        /// <summary>
        /// Draws the backdrop looping on the X axis.
        /// </summary>
        /// <param name="spritebatch">The spritebatch to use for drawing.</param>
        /// <param name="drawarea">The area of the screen.</param>
        /// <param name="drawposition">The position to draw at.</param>
        protected virtual void DrawLoopX(SpriteBatch spritebatch, Rectangle drawarea, Vector2 drawposition)
        {
            //First move it outside of the draw area

            while (drawposition.X >= drawarea.X)
            {
                drawposition.X -= Texture.Width;
            }

            //Draw, then move the position till it's offscreen, then stop.

            while (drawposition.X <= drawarea.Right)
            {
                DrawBackdrop(spritebatch, drawarea, drawposition);

                drawposition.X += Texture.Width;
            }
        }

        /// <summary>
        /// Draws the backdrop looping on the Y axis.
        /// </summary>
        /// <param name="spritebatch">The spritebatch to use for drawing.</param>
        /// <param name="drawarea">The area of the screen.</param>
        /// <param name="drawposition">The position to draw at.</param>
        protected virtual void DrawLoopY(SpriteBatch spritebatch, Rectangle drawarea, Vector2 drawposition)
        {
            //First move it outside of the draw area

            while (drawposition.Y >= drawarea.Y)
            {
                drawposition.Y -= Texture.Height;
            }

            //Draw, then move the position till it's offscreen, then stop.

            while (drawposition.Y <= drawarea.Bottom)
            {
                DrawBackdrop(spritebatch, drawarea, drawposition);

                drawposition.Y += Texture.Height;
            }
        }

        /// <summary>
        /// Draws the backdrop looping on both axes.
        /// </summary>
        /// <param name="spritebatch">The spritebatch to use for drawing.</param>
        /// <param name="drawarea">The area of the screen.</param>
        /// <param name="drawposition">The position to draw at.</param>
        protected virtual void DrawLoopBoth(SpriteBatch spritebatch, Rectangle drawarea, Vector2 drawposition)
        {
            //First move it outside of the draw area

            while (drawposition.X >= drawarea.X)
            {
                drawposition.X -= Texture.Width;
            }

            //Call the draw loop y, then move the position till it's offscreen, then stop.

            while (drawposition.X <= drawarea.Right)
            {
                DrawLoopY(spritebatch, drawarea, drawposition);

                drawposition.X += Texture.Width;
            }
        }

        #endregion
    }
}