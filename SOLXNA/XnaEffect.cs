using Microsoft.Xna.Framework.Graphics;
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
