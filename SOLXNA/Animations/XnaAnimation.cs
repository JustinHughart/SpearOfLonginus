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
        /// The texture cache used for loading textures.
        /// </summary>
        public TextureCache TextureCache { get; protected set; }

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
        /// <param name="texturecache">The texture cache used for loading textures.</param>
        public XnaAnimation(bool loop, bool resetindex, TextureCache texturecache) : base(loop, resetindex)
        {
            if (TextureCache != null)
            {
                LoadContent(texturecache);
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
            foreach (var frame in frames) //Convert SOL Frame to Frame.
            {
                AddFrame(frame.ToXnaFrame());
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XnaAnimation"/> class.
        /// </summary>
        /// <param name="loop">Whether or not the animation is looping.</param>
        /// <param name="resetindex">Whether or not to reset the timing index when the frame changes. Turn this on to ensure that each frame gets viewed at least once.</param>
        /// <param name="frames">The list of the animation's frames.</param>
        /// <param name="texturecache">The texture cache used for loading textures.</param>
        public XnaAnimation(bool loop, bool resetindex, List<Frame> frames, TextureCache texturecache)
            : base(loop, resetindex, new List<Frame>())
        {
            foreach (var frame in frames) //Convert SOL Frame to XNA Frame.
            {
                AddFrame(frame.ToXnaFrame());
            }

            if (TextureCache != null)
            {
                LoadContent(texturecache);
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

            if (TextureCache == null)
            {
                return;
            }

            var xnaframe = (XnaFrame)frame;

            xnaframe.LoadContent(TextureCache);
        }

        #endregion

        #region

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public override Animation Clone()
        {
            return new XnaAnimation(IsLooping, ResetIndex, Frames, TextureCache);
        }

        #endregion

        #region Functions

        /// <summary>
        /// Loads the textures.
        /// </summary>
        /// <param name="texturecache">The texture cache used for loading textures.</param>
        public virtual void LoadContent(TextureCache texturecache)
        {
            foreach (XnaFrame frame in Frames)
            {
                frame.LoadContent(texturecache);
            }

            TextureCache = texturecache;
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
            
            spritebatch.Draw(currnode.Texture, position, currnode.DrawArea.ToXnaRectangle(), tint, rotation, currnode.Origin.ToXnaVector(), scale, effects, layer);
        }

        #endregion
    }
}
