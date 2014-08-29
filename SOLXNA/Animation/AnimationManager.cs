using System;
using System.Collections.Generic;

namespace SOLXNA.Animation
{
    /// <summary>
    /// A class that holds animations.
    /// </summary>
    public class AnimationManager
    {
        #region Variables

        /// <summary>
        /// The keyed list of animations.
        /// </summary>
        protected Dictionary<string, Animation> Animations;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AnimationManager" /> class.
        /// </summary>
        public AnimationManager()
        {
            Animations = new Dictionary<string, Animation>();
        }

        #endregion

        #region Functions

        /// <summary>
        /// Loads the textures
        /// </summary>
        /// <param name="texturemanager">The texture manager used for loading textures.</param>
        public void LoadContent(TextureManager texturemanager)
        {
            foreach (var animation in Animations)
            {
                animation.Value.LoadContent(texturemanager);
            }
        }

        /// <summary>
        /// Unloads the textures.
        /// </summary>
        public void UnloadContent()
        {
            foreach (var animation in Animations)
            {
                animation.Value.UnloadContent();
            }
        }

        /// <summary>
        /// Adds the animation to the list of animations.
        /// </summary>
        /// <param name="key">The animation's key.</param>
        /// <param name="animation">The animation to add.</param>
        public virtual void AddAnimation(string key, Animation animation)
        {
            Animations.Add(key, animation);
        }

        /// <summary>
        /// Retrieves the animation from the list of animations. Throws an exception if it doesn't exist.
        /// </summary>
        /// <param name="key">The key of the animation you're requesting..</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Animation "key" does not exist.</exception>
        public virtual Animation GetAnimation(string key)
        {
            if (!Animations.ContainsKey(key))
            {
                return Animations[key].CloneAsXNAAnimation();
            }

            throw new Exception("Animation \"" + key+ "\" does not exist.");
        }

        #endregion
    }
}