using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpearOfLonginus.Animation;

namespace SOLXNA.Animation
{
    /// <summary>
    /// An XNA binding for SOLAnimation.
    /// </summary>
    public class Animation : SOLAnimation
    {
        #region Variables

        /// <summary>
        /// The texture manager used for loading textures.
        /// </summary>
        protected TextureManager TextureManager;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Animation"/> class.
        /// </summary>
        /// <param name="loop">Whether or not the animation is looping.</param>
        /// <param name="resetindex">Whether or not to reset the timing index when the frame changes. Turn this on to ensure that each frame gets viewed at least once.</param>
        public Animation(bool loop, bool resetindex) : base(loop, resetindex)
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Animation"/> class.
        /// </summary>
        /// <param name="loop">Whether or not the animation is looping.</param>
        /// <param name="resetindex">Whether or not to reset the timing index when the frame changes. Turn this on to ensure that each frame gets viewed at least once.</param>
        /// <param name="texturemanager">The texture manager used for loading textures.</param>
        public Animation(bool loop, bool resetindex, TextureManager texturemanager) : base(loop, resetindex)
        {
            if (TextureManager != null)
            {
                LoadContent(texturemanager);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Animation"/> class.
        /// </summary>
        /// <param name="loop">Whether or not the animation is looping.</param>
        /// <param name="resetindex">Whether or not to reset the timing index when the frame changes. Turn this on to ensure that each frame gets viewed at least once.</param>
        /// <param name="frames">The list of the animation's frames.</param>
        public Animation(bool loop, bool resetindex, List<SOLFrame> frames) : base(loop, resetindex, new List<SOLFrame>())
        {
            foreach (var frame in frames) //Convert SOLFrame to Frame.
            {
                AddFrame(new Frame(frame));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Animation"/> class.
        /// </summary>
        /// <param name="loop">Whether or not the animation is looping.</param>
        /// <param name="resetindex">Whether or not to reset the timing index when the frame changes. Turn this on to ensure that each frame gets viewed at least once.</param>
        /// <param name="frames">The list of the animation's frames.</param>
        /// <param name="texturemanager">The texture manager used for loading textures.</param>
        public Animation(bool loop, bool resetindex, List<SOLFrame> frames, TextureManager texturemanager)
            : base(loop, resetindex, new List<SOLFrame>())
        {
            foreach (var frame in frames) //Convert SOLFrame to XNA Frame.
            {
                AddFrame(new Frame(frame));
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
        public override void AddFrame(SOLFrame frame)
        {
            base.AddFrame(frame);

            if (TextureManager != null)
            {
                Frame xnaframe = (Frame)frame;

                xnaframe.LoadContent(TextureManager);
            }
        }

        #endregion

        #region

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public override SOLAnimation Clone()
        {
            return new Animation(IsLooping, ResetIndex, Frames, TextureManager);
        }
        
        #endregion

        #region Functions

        /// <summary>
        /// Gets the current frame as an XNA frame.
        /// </summary>
        /// <returns></returns>
        public Frame GetCurrentFrameAsXNAFrame()
        {
            return (Frame) base.GetCurrentFrame();
        }

        /// <summary>
        /// Clonesthe animation and returns it as an XNA animation.
        /// </summary>
        /// <returns></returns>
        public Animation CloneAsXNAAnimation()
        {
            return (Animation) Clone();
        }

        /// <summary>
        /// Loads the textures.
        /// </summary>
        /// <param name="texturemanager">The texture manager used for loading textures.</param>
        public virtual void LoadContent(TextureManager texturemanager)
        {
            foreach (Frame frame in Frames)
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
            foreach (Frame frame in Frames)
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
            var currnode = (Frame) GetCurrentFrame();
            
            spritebatch.Draw(currnode.Texture, position, currnode.XNADrawArea, tint, rotation, currnode.XNAOrigin, scale, effects, layer);
        }

        #endregion
    }
}
