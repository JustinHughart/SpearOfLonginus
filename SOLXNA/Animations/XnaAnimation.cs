using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpearOfLonginus.Animations;

namespace SOLXNA.Animations
{
    /// <summary>
    /// An XNA binding for SOL's Animation.
    /// </summary>
    public class XnaAnimation : Animation
    {
        #region Variables

        /// <summary>
        /// The texture cache used for loading textures.
        /// </summary>
        /// <value>
        /// The texture cache.
        /// </value>
        public TextureCache TextureCache { get; protected set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="XnaAnimation" /> class.
        /// </summary>
        /// <param name="id">The identifier of the animation.</param>
        /// <param name="loop">Whether or not the animation is looping.</param>
        /// <param name="resetindex">Whether or not to reset the timing index when the frame changes. Turn this on to ensure that each frame gets viewed at least once.</param>
        public XnaAnimation(string id, bool loop, bool resetindex) : base(id, loop, resetindex)
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XnaAnimation" /> class.
        /// </summary>
        /// <param name="id">The identifier of the animation.</param>
        /// <param name="loop">Whether or not the animation is looping.</param>
        /// <param name="resetindex">Whether or not to reset the timing index when the frame changes. Turn this on to ensure that each frame gets viewed at least once.</param>
        /// <param name="texturecache">The texture cache used for loading textures.</param>
        public XnaAnimation(string id, bool loop, bool resetindex, TextureCache texturecache) : base(id, loop, resetindex)
        {
            if (texturecache != null)
            {
                LoadContent(texturecache);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XnaAnimation" /> class.
        /// </summary>
        /// <param name="id">The identifier of the animation.</param>
        /// <param name="loop">Whether or not the animation is looping.</param>
        /// <param name="resetindex">Whether or not to reset the timing index when the frame changes. Turn this on to ensure that each frame gets viewed at least once.</param>
        /// <param name="frames">The list of the animation's frames.</param>
        public XnaAnimation(string id, bool loop, bool resetindex, List<Frame> frames) : base(id, loop, resetindex, null)
        {
            foreach (var frame in frames) //Convert SOL Frame to Frame.
            {
                AddFrame(frame.ToXnaFrame());
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XnaAnimation" /> class.
        /// </summary>
        /// <param name="id">The identifier of the animation.</param>
        /// <param name="loop">Whether or not the animation is looping.</param>
        /// <param name="resetindex">Whether or not to reset the timing index when the frame changes. Turn this on to ensure that each frame gets viewed at least once.</param>
        /// <param name="frames">The list of the animation's frames.</param>
        /// <param name="texturecache">The texture cache used for loading textures.</param>
        public XnaAnimation(string id, bool loop, bool resetindex, List<Frame> frames, TextureCache texturecache)
            : base(id, loop, resetindex, null)
        {
            foreach (var frame in frames) //Convert SOL Frame to XNA Frame.
            {
                AddFrame(frame.ToXnaFrame());
            }

            if (texturecache != null)
            {
                LoadContent(texturecache);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XnaAnimation" /> class.
        /// </summary>
        /// <param name="id">The identifier of the animation.</param>
        /// <param name="loop">Whether or not the animation is looping.</param>
        /// <param name="resetindex">Whether or not to reset the timing index when the frame changes. Turn this on to ensure that each frame gets viewed at least once.</param>
        /// <param name="frames">The list of the animation's frames.</param>
        /// <param name="currentframe">The frame the animation is currently on.</param>
        /// <param name="timingindex">The timing index of the animation.</param>
        public XnaAnimation(string id, bool loop, bool resetindex, List<Frame> frames, int currentframe, float timingindex)
            : base(id, loop, resetindex, null)
        {
            foreach (var frame in frames) //Convert SOL Frame to XNA Frame.
            {
                AddFrame(frame.ToXnaFrame());
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XnaAnimation" /> class.
        /// </summary>
        /// <param name="id">The identifier of the animation.</param>
        /// <param name="loop">Whether or not the animation is looping.</param>
        /// <param name="resetindex">Whether or not to reset the timing index when the frame changes. Turn this on to ensure that each frame gets viewed at least once.</param>
        /// <param name="frames">The list of the animation's frames.</param>
        /// <param name="texturecache">The texture cache used for loading textures.</param>
        /// <param name="currentframe">The frame the animation is currently on.</param>
        /// <param name="timingindex">The timing index of the animation.</param>
        public XnaAnimation(string id, bool loop, bool resetindex, List<Frame> frames, int currentframe, float timingindex, TextureCache texturecache)
            : base(id, loop, resetindex, null)
        {
            foreach (var frame in frames) //Convert SOL Frame to XNA Frame.
            {
                AddFrame(frame.ToXnaFrame());
            }

            if (texturecache != null)
            {
                LoadContent(texturecache);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XnaAnimation" /> class.
        /// </summary>
        /// <param name="anim">The animation to use the data to load.</param>
        /// <param name="texturecache">The texture cache used to load the textures..</param>
        public XnaAnimation(Animation anim, TextureCache texturecache)
            : base(anim.ID, anim.IsLooping, anim.ResetIndex, new List<Frame>(), anim.CurrentFrame, anim.TimingIndex)
        {
            foreach (var frame in anim.GetFramesList()) //Convert SOL Frame to XNA Frame.
            {
                AddFrame(frame.ToXnaFrame());
            }

            if (texturecache != null)
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
        
        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public override Animation Clone()
        {
            return Clone(CurrentFrame, TimingIndex);
        }

        /// <summary>
        /// Clones the specified frame.
        /// </summary>
        /// <param name="frame">The frame the animation should start on.</param>
        /// <param name="timingindex">The timing index the animation should start on.</param>
        /// <returns></returns>
        public override Animation Clone(int frame, float timingindex)
        {
            return new XnaAnimation(ID, IsLooping, ResetIndex, GetFramesList(), frame, timingindex, TextureCache);
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
        public virtual void Draw(SpriteBatch spritebatch, Vector2 position, Color tint, float rotation, float scale, SpriteEffects effects, float layer)
        {
            Draw(spritebatch, position, tint, rotation, new Vector2(scale), effects, layer);
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

            //Sprite effect correction. If incompatible modes are found, it will favor node's effect. If like modes are found, they cancel each other out.
            if (effects == SpriteEffects.FlipHorizontally)
            {
                if (currnode.SpriteEffect == SpriteEffects.FlipHorizontally)
                {
                    effects = SpriteEffects.None;
                }

                if (currnode.SpriteEffect == SpriteEffects.FlipVertically)
                {
                    effects = currnode.SpriteEffect;
                }
            }
            else if (effects == SpriteEffects.FlipVertically)
            {
                if (currnode.SpriteEffect == SpriteEffects.FlipHorizontally)
                {
                    effects = currnode.SpriteEffect;
                }

                if (currnode.SpriteEffect == SpriteEffects.FlipVertically)
                {
                    effects = SpriteEffects.None;
                }
            }
            else
            {
                effects = currnode.SpriteEffect;
            }

            spritebatch.Draw(currnode.Texture, position, currnode.DrawArea.ToXnaRectangle(), tint, rotation, currnode.Origin.ToXnaVector(), scale, effects, layer);
        }

        #endregion
    }
}
