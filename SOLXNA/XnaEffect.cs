using Microsoft.Xna.Framework.Graphics;
using SOLXNA.Animations;
using SpearOfLonginus;

namespace SOLXNA
{
    /// <summary>
    /// Adds drawing functions for effects.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class XnaEffect <T> : Effect<T>
    {
        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="texturecache">The texturecache.</param>
        public void LoadContent(TextureCache texturecache)
        {

        }

        /// <summary>
        /// Unloads the content.
        /// </summary>
        public void UnloadContent()
        {

        }

        /// <summary>
        /// Draw before the owner draws its animation.
        /// </summary>
        /// <param name="spritebatch">The spritebatch used to draw.</param>
        public void DrawBefore(SpriteBatch spritebatch)
        {

        }

        /// <summary>
        /// Draw after the owner draws its animation.
        /// </summary>
        /// <param name="spritebatch">The spritebatch used to draw.</param>
        public void DrawAfter(SpriteBatch spritebatch)
        {

        }
    }
}
