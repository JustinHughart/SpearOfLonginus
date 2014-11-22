using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SOLXNA.Animations;
using SpearOfLonginus.Entities;

namespace SOLXNA.Entities
{
    /// <summary>
    /// Adds drawing functions to logics.
    /// </summary>
    public class XnaLogic : Logic, IXnaDrawable
    {
        /// <summary>
        /// Gets or sets the sprite batch data.
        /// </summary>
        /// <value>
        /// The sprite batch data.
        /// </value>
        public SpriteBatchData SpriteBatchData { get; set; }

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
        /// This is not used in the base SOLXNA system. Please use DrawBefore or DrawAfter instead.
        /// </summary>
        /// <param name="spritebatch">The spritebatch used to draw.</param>
        /// <param name="cameramatrix">The camera's matrix.</param>
        public void Draw(SpriteBatch spritebatch, Matrix cameramatrix)
        {

        }

        /// <summary>
        /// Draw before the entity draws its animation.
        /// </summary>
        /// <param name="spritebatch">The spritebatch used to draw.</param>
        /// <param name="cameramatrix">The camera's matrix.</param>
        public void DrawBefore(SpriteBatch spritebatch, Matrix cameramatrix)
        {

        }

        /// <summary>
        /// Draw after the entity draws its animation.
        /// </summary>
        /// <param name="spritebatch">The spritebatch used to draw.</param>
        /// <param name="cameramatrix">The camera's matrix.</param>
        public void DrawAfter(SpriteBatch spritebatch, Matrix cameramatrix)
        {

        }
    }
}
