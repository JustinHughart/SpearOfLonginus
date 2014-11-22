using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SOLXNA.Animations;
using SpearOfLonginus.UI;

namespace SOLXNA.UI
{
    /// <summary>
    /// An XNA binding for SOL's UIItem.
    /// </summary>
    public class XnaUIItem : UIItem, IXnaDrawable
    {
        /// <summary>
        /// Gets or sets the spritebatch data.
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
        /// Draw what the object needs to.
        /// </summary>
        /// <param name="spritebatch">The spritebatch used to draw.</param>
        /// <param name="cameramatrix">The camera's matrix.</param>
        public void Draw(SpriteBatch spritebatch, Matrix cameramatrix)
        {
            
        }

        /// <summary>
        /// Draw before something happens.
        /// </summary>
        /// <param name="spritebatch">The spritebatch used to draw.</param>
        /// <param name="cameramatrix">The camera's matrix.</param>
        public void DrawBefore(SpriteBatch spritebatch, Matrix cameramatrix)
        {
            
        }

        /// <summary>
        /// Draw after something happens.
        /// </summary>
        /// <param name="spritebatch">The spritebatch used to draw.</param>
        /// <param name="cameramatrix">The camera's matrix.</param>
        public void DrawAfter(SpriteBatch spritebatch, Matrix cameramatrix)
        {
            
        }
    }
}
