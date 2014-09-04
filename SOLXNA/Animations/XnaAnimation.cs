using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpearOfLonginus.Animations;

namespace SOLXNA.Animations
{
    /// <summary>
    /// An XNA binding for SOLAnimation.
    /// </summary>
    public class XnaAnimation : Animation
    {
        #region Variables

        /// <summary>
        /// The texture manager used for loading textures.
        /// </summary>
        protected TextureManager TextureManager;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="XnaAnimation"/> class.
        /// </summary>
        /// <param name="loop">Whether or not the animation is looping.</param>
        /// <param name="resetindex">Whether or not to reset the timing index when the frame changes. Turn this on to ensure that each frame gets viewed at least once.</param>
        public XnaAnimation(bool loop, bool resetindex) : base(loop, resetindex)
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XnaAnimation"/> class.
        /// </summary>
        /// <param name="loop">Whether or not the animation is looping.</param>
        /// <param name="resetindex">Whether or not to reset the timing index when the frame changes. Turn this on to ensure that each frame gets viewed at least once.</param>
        /// <param name="texturemanager">The texture manager used for loading textures.</param>
        public XnaAnimation(bool loop, bool resetindex, TextureManager texturemanager) : base(loop, resetindex)
        {
            if (TextureManager != null)
            {
                LoadContent(texturemanager);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XnaAnimation"/> class.
        /// </summary>
        /// <param name="loop">Whether or not the animation is looping.</param>
        /// <param name="resetindex">Whether or not to reset the timing index when the frame changes. Turn this on to ensure that each frame gets viewed at least once.</param>
        /// <param name="frames">The list of the animation's frames.</param>
        public XnaAnimation(bool loop, bool resetindex, List<Frame> frames) : base(loop, resetindex, new List<Frame>())
        {
            foreach (var frame in frames) //Convert SOLFrame to Frame.
            {
                AddFrame(new XnaFrame(frame));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XnaAnimation"/> class.
        /// </summary>
        /// <param name="loop">Whether or not the animation is looping.</param>
        /// <param name="resetindex">Whether or not to reset the timing index when the frame changes. Turn this on to ensure that each frame gets viewed at least once.</param>
        /// <param name="frames">The list of the animation's frames.</param>
        /// <param name="texturemanager">The texture manager used for loading textures.</param>
        public XnaAnimation(bool loop, bool resetindex, List<Frame> frames, TextureManager texturemanager)
            : base(loop, resetindex, new List<Frame>())
        {
            foreach (var frame in frames) //Convert SOLFrame to XNA Frame.
            {
                AddFrame(new XnaFrame(frame));
            }

            if (TextureManager != null)
            {
                LoadContent(texturemanager);
            }
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Adds the frame to the list of frames in the animation.
        /// </summary>
        /// <param name="frame">The frame to add.</param>
        public override void AddFrame(Frame frame)
        {
            base.AddFrame(frame);

            if (TextureManager == null)
            {
                return;
            }

            var xnaframe = (XnaFrame)frame;

            xnaframe.LoadContent(TextureManager);
        }

        #endregion

        #region

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public override Animation Clone()
        {
            return new XnaAnimation(IsLooping, ResetIndex, Frames, TextureManager);
        }
        
        #endregion

        #region Functions

        /// <summary>
        /// Gets the current frame as an XNA frame.
        /// </summary>
        /// <returns></returns>
        public XnaFrame GetCurrentFrameAsXNAFrame()
        {
            return (XnaFrame) base.GetCurrentFrame();
        }

        /// <summary>
        /// Clonesthe animation and returns it as an XNA animation.
        /// </summary>
        /// <returns></returns>
        public XnaAnimation CloneAsXNAAnimation()
        {
            return (XnaAnimation) Clone();
        }

        /// <summary>
        /// Loads the textures.
        /// </summary>
        /// <param name="texturemanager">The texture manager used for loading textures.</param>
        public virtual void LoadContent(TextureManager texturemanager)
        {
            foreach (XnaFrame frame in Frames)
            {
                frame.LoadContent(texturemanager);
            }

            TextureManager = texturemanager;
        }

        /// <summary>
        /// Unloads the textures.
        /// </summary>
        public virtual void UnloadContent()
        {
            foreach (XnaFrame frame in Frames)
            {
                frame.UnloadContent();
            }
        }

        /// <summary>
        /// Draws the current frame.
        /// </summary>
        /// <param name="spritebatch">The SpriteBatch used for drawing.</param>
        /// <param name="position">The position of the animation.</param>
        /// <param name="tint">The animation's tint.</param>
        /// <param name="rotation">The animation's rotation.</param>
        /// <param name="scale">The animation's scale.</param>
        /// <param name="effects">The sprite effects used for flipping the sprite.</param>
        /// <param name="layer">The layer to draw the sprite on.</param>
        public virtual void Draw(SpriteBatch spritebatch, Vector2 position, Color tint, float rotation, Vector2 scale, SpriteEffects effects, float layer)
        {
            var currnode = (XnaFrame) GetCurrentFrame();
            
            spritebatch.Draw(currnode.Texture, position, currnode.XNADrawArea, tint, rotation, currnode.XNAOrigin, scale, effects, layer);
        }

        #endregion
    }
}
