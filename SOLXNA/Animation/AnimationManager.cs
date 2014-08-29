using System;
using System.Collections.Generic;
using SpearOfLonginus.Animation;

namespace SOLXNA.Animation
{
    /// <summary>
    /// A class that holds animations.
    /// </summary>
    public class AnimationManager : SOLAnimationManager 
    {
        #region Functions

        /// <summary>
        /// Loads the textures
        /// </summary>
        /// <param name="texturemanager">The texture manager used for loading textures.</param>
        public virtual void LoadContent(TextureManager texturemanager)
        {
            foreach (var animation  in Animations)
            {
                var anim = animation.Value as Animation;

                anim.LoadContent(texturemanager);
            }
        }

        /// <summary>
        /// Unloads the textures.
        /// </summary>
        public virtual void UnloadContent()
        {
            foreach (var animation in Animations)
            {
                var anim = (Animation)animation.Value;

                anim.UnloadContent();
            }
        }

        public virtual Animation GetXNAAnimation(string key)
        {
            if (!Animations.ContainsKey(key))
            {
                var anim = (Animation)Animations[key];

                return anim.CloneAsXNAAnimation();
            }

            throw new Exception("Animation \"" + key+ "\" does not exist.");
        }

        #endregion
    }
}