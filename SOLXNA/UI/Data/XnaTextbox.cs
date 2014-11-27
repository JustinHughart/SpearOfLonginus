using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SOLXNA.Animations;
using SpearOfLonginus.UI.Data;

namespace SOLXNA.UI.Data
{
    /// <summary>
    /// An XNA binding for SoL's Textbox class.
    /// </summary>
    public class XnaTextbox : Textbox, IXnaDrawable
    {
        /// <summary>
        /// The texture's ID.
        /// </summary>
        protected string TextureID;
        /// <summary>
        /// The texture for the box.
        /// </summary>
        protected Texture2D Texture;
        /// <summary>
        /// The background color
        /// </summary>
        protected Color BackgroundColor;
        /// <summary>
        /// The upper left corner of the box.
        /// </summary>
        protected Rectangle UpperLeftCorner;
        /// <summary>
        /// The upper right corner of the box.
        /// </summary>
        protected Rectangle UpperRightCorner;
        /// <summary>
        /// The lower left corner of the box.
        /// </summary>
        protected Rectangle LowerLeftCorner;
        /// <summary>
        /// The lower right corner of the box.
        /// </summary>
        protected Rectangle LowerRightCorner;
        /// <summary>
        /// The top part of the box that is stretched across the box. Width should be 1.
        /// </summary>
        protected Rectangle TopStretch;
        /// <summary>
        /// The bottom part of the box that is stretched across the box. Width should be 1.
        /// </summary>
        protected Rectangle BottomStretch;
        /// <summary>
        /// The left part of the box that is stretched across the box. Height should be 1.
        /// </summary>
        protected Rectangle LeftStretch;
        /// <summary>
        /// The left part of the box that is stretched across the box. Height should be 1.
        /// </summary>
        protected Rectangle RightStretch;

        /// <summary>
        /// Gets or sets the spritebatch data.
        /// </summary>
        /// <value>
        /// The spritebatch data.
        /// </value>
        public SpriteBatchData SpriteBatchData { get; set; }
        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="texturecache">The texturecache.</param>
        public void LoadContent(TextureCache texturecache)
        {
            Texture = texturecache.GetTexture(TextureID);
        }

        /// <summary>
        /// Unloads the content.
        /// </summary>
        public void UnloadContent()
        {
            Texture = null;
        }

        /// <summary>
        /// Draw what the object needs to.
        /// </summary>
        /// <param name="spritebatch">The spritebatch used to draw.</param>
        /// <param name="cameramatrix">The camera's matrix.</param>
        public void Draw(SpriteBatch spritebatch, Matrix cameramatrix)
        {
            spritebatch.Begin(SpriteSortMode.Immediate, SpriteBatchData.BlendState, SpriteBatchData.SamplerState, SpriteBatchData.DepthStencilState, SpriteBatchData.RasterizerState, SpriteBatchData.Effect, cameramatrix);
            
            //Draw the background.
            spritebatch.Draw(XnaGlobalVariables.ClearTexture, Area.Location.ToXnaVector(), XnaGlobalVariables.ClearTexture.Bounds, BackgroundColor, 0f, Vector2.Zero, Area.Size.ToXnaVector(), SpriteEffects.None, 0f);

            //Draw the stretched sides.

            //Draw the corners.

            //Draw the text.

            spritebatch.End();
        }

        /// <summary>
        /// Draw before something happens.
        /// </summary>
        /// <param name="spritebatch">The spritebatch used to draw.</param>
        /// <param name="cameramatrix">The camera's matrix.</param>
        public void DrawBefore(SpriteBatch spritebatch, Matrix cameramatrix)
        {
            //We won't use this.
        }

        /// <summary>
        /// Draw after something happens.
        /// </summary>
        /// <param name="spritebatch">The spritebatch used to draw.</param>
        /// <param name="cameramatrix">The camera's matrix.</param>
        public void DrawAfter(SpriteBatch spritebatch, Matrix cameramatrix)
        {
            //We won't use this, either.
        }

        /// <summary>
        /// Uses XML to initialize the object.
        /// </summary>
        /// <param name="element">The element used for loading.</param>
        public override void LoadFromXml(XElement element)
        {
            SpriteBatchData = new SpriteBatchData();

            base.LoadFromXml(element);

            //DO MORE HERE.
        }
    }
}