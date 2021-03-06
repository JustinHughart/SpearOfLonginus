﻿using System.Xml.Linq;
using Microsoft.Xna.Framework.Graphics;
using SpearOfLonginus;
using SpearOfLonginus.Animations;
using SOLRect = SpearOfLonginus.Rectangle;
using XNARect = Microsoft.Xna.Framework.Rectangle;

namespace SOLXNA.Animations
{
    /// <summary>
    /// An XNA binding for SOL's Frame.
    /// </summary>
    public class XnaFrame : Frame
    {
        #region Variables

        /// <summary>
        /// The loaded texture.
        /// </summary>
        public Texture2D Texture;
        /// <summary>
        /// The sprite effect used to flip the texture.
        /// </summary>
        public SpriteEffects SpriteEffect;
        
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="XnaFrame"/> class.
        /// </summary>
        public XnaFrame()
        {
            SpriteEffect = SpriteEffects.None;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XnaFrame" /> class.
        /// </summary>
        /// <param name="textureid">The ID used for  texture loading.</param>
        /// <param name="drawarea">The area on the texture that should be drawn.</param>
        /// <param name="origin">The origin of the frame. Used for rotation and to offset the sprite.</param>
        /// <param name="timetillnext">The time until the frame changes.</param>
        public XnaFrame(string textureid, SOLRect drawarea, Vector origin, float timetillnext): base(textureid, drawarea, origin, timetillnext)
        {
            SpriteEffect = SpriteEffects.None;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XnaFrame" /> class.
        /// </summary>
        /// <param name="textureid">The ID used for  texture loading.</param>
        /// <param name="drawarea">The area on the texture that should be drawn.</param>
        /// <param name="origin">The origin of the frame. Used for rotation and to offset the sprite.</param>
        /// <param name="timetillnext">The time until the frame changes.</param>
        /// <param name="spriteeffect">The sprite effect used to flip the texture.</param>
        public XnaFrame(string textureid, SOLRect drawarea, Vector origin, float timetillnext, SpriteEffects spriteeffect)
            : this(textureid, drawarea, origin, timetillnext)
        {
            SpriteEffect = spriteeffect;
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
        /// Uses XML to initialize the object.
        /// </summary>
        /// <param name="element">The element used for loading..</param>
        public override void LoadFromXml(XElement element)
        {
            base.LoadFromXml(element);

            XAttribute effectsattribute = element.Attribute("spriteeffects");

            if (effectsattribute != null)
            {
                SpriteEffects.TryParse(effectsattribute.Value, out SpriteEffect);
            }
        }

        #endregion
    }
}