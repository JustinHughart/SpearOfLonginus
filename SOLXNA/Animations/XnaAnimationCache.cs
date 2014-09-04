using System;
using SpearOfLonginus.Animations;

namespace SOLXNA.Animations
{
    /// <summary>
    /// A class that holds animations.
    /// </summary>
    public class XnaAnimationCache : AnimationManager 
    {
        #region Functions

        /// <summary>
        /// Loads the textures
        /// </summary>
        /// <param name="texturemanager">The texture manager used for loading textures.</param>
        public virtual void LoadContent(TextureCache texturemanager)
        {
            foreach (var animation  in Animations)
            {
                var anim = (XnaAnimation)animation.Value;

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
                var anim = (XnaAnimation)animation.Value;

                anim.UnloadContent();
            }
        }

        #endregion
    }
}