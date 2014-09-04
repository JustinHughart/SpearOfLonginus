using System;
using SpearOfLonginus.Animations;

namespace SOLXNA.Animations
{
    /// <summary>
    /// A class that holds animations.
    /// </summary>
    public class XnaAnimationCache : AnimationCache 
    {
        #region Functions

        /// <summary>
        /// Loads the textures
        /// </summary>
        /// <param name="texturecache">The texture cache used for loading textures.</param>
        public virtual void LoadContent(TextureCache texturecache)
        {
            foreach (var animation  in Animations)
            {
                var anim = (XnaAnimation)animation.Value;

                anim.LoadContent(texturecache);
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