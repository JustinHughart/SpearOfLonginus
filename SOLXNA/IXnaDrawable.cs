using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SOLXNA.Animations;

namespace SOLXNA
{
    /// <summary>
    /// Adds drawing functions. 
    /// </summary>
    public interface IXnaDrawable
    {
        /// <summary>
        /// Gets or sets the spritebatch data.
        /// </summary>
        /// <value>
        /// The spritebatch data.
        /// </value>
        SpriteBatchData SpriteBatchData { get; set; }

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="texturecache">The texturecache.</param>
        void LoadContent(TextureCache texturecache);

        /// <summary>
        /// Unloads the content.
        /// </summary>
        void UnloadContent();

        /// <summary>
        /// Draw what the object needs to.
        /// </summary>
        /// <param name="spritebatch">The spritebatch used to draw.</param>
        /// <param name="cameramatrix">The camera's matrix.</param>
        void Draw(SpriteBatch spritebatch, Matrix cameramatrix);

        /// <summary>
        /// Draw before something happens. 
        /// </summary>
        /// <param name="spritebatch">The spritebatch used to draw.</param>
        /// <param name="cameramatrix">The camera's matrix.</param>
        void DrawBefore(SpriteBatch spritebatch, Matrix cameramatrix);

        /// <summary>
        /// Draw after something happens.
        /// </summary>
        /// <param name="spritebatch">The spritebatch used to draw.</param>
        /// <param name="cameramatrix">The camera's matrix.</param>
        void DrawAfter(SpriteBatch spritebatch, Matrix cameramatrix);
    }
}
