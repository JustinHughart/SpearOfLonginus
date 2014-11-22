using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SOLXNA.Animations;
using SpearOfLonginus.UI;

namespace SOLXNA.UI
{
    /// <summary>
    /// A XNA binding for SOL's UIManager.
    /// </summary>
    public class XnaUIManager : UIManager, IXnaDrawable
    {
        /// <summary>
        /// Gets or sets the spritebatch data. This is actually unused in this class.
        /// </summary>
        /// <value>
        /// The spritebatch data.
        /// </value>
        public SpriteBatchData SpriteBatchData { get; set; }
        
        /// <summary>
        /// The texture cache used for loading textures.
        /// </summary>
        protected TextureCache TextureCache;

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="texturecache">The texturecache.</param>
        public void LoadContent(TextureCache texturecache)
        {
            TextureCache = texturecache;

            foreach (var item in Items.Values)
            {
                IXnaDrawable xnaitem = item as IXnaDrawable;

                if (xnaitem != null)
                {
                    xnaitem.LoadContent(texturecache);
                }
            }
        }

        /// <summary>
        /// Unloads the content.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void UnloadContent()
        {
            foreach (var item in Items.Values)
            {
                IXnaDrawable xnaitem = item as IXnaDrawable;

                if (xnaitem != null)
                {
                    xnaitem.UnloadContent();
                }
            }
        }

        /// <summary>
        /// Runs the full draw cycle.
        /// </summary>
        /// <param name="spritebatch">The spritebatch used to draw.</param>
        /// <param name="cameramatrix">The camera's matrix.</param>
        public void DrawCycle(SpriteBatch spritebatch, Matrix cameramatrix)
        {
            DrawBefore(spritebatch, cameramatrix);
            Draw(spritebatch, cameramatrix);
            DrawAfter(spritebatch, cameramatrix);
        }

        /// <summary>
        /// Draw what the object needs to.
        /// </summary>
        /// <param name="spritebatch">The spritebatch used to draw.</param>
        /// <param name="cameramatrix">The camera's matrix.</param>
        public void Draw(SpriteBatch spritebatch, Matrix cameramatrix)
        {
            foreach (var item in Items.Values)
            {
                IXnaDrawable xnaitem = item as IXnaDrawable;

                if (xnaitem != null)
                {
                    xnaitem.Draw(spritebatch, cameramatrix);
                }
            }
        }

        /// <summary>
        /// Draw before something happens.
        /// </summary>
        /// <param name="spritebatch">The spritebatch used to draw.</param>
        /// <param name="cameramatrix">The camera's matrix.</param>
        public void DrawBefore(SpriteBatch spritebatch, Matrix cameramatrix)
        {
            foreach (var item in Items.Values)
            {
                IXnaDrawable xnaitem = item as IXnaDrawable;

                if (xnaitem != null)
                {
                    xnaitem.DrawBefore(spritebatch, cameramatrix);
                }
            }
        }

        /// <summary>
        /// Draw after something happens.
        /// </summary>
        /// <param name="spritebatch">The spritebatch used to draw.</param>
        /// <param name="cameramatrix">The camera's matrix.</param>
        public void DrawAfter(SpriteBatch spritebatch, Matrix cameramatrix)
        {
            foreach (var item in Items.Values)
            {
                IXnaDrawable xnaitem = item as IXnaDrawable;

                if (xnaitem != null)
                {
                    xnaitem.DrawAfter(spritebatch, cameramatrix);
                }
            }
        }

        /// <summary>
        /// Adds the item to the pool of items.
        /// </summary>
        /// <param name="key">The item's key.</param>
        /// <param name="item">The item to add.</param>
        public override void AddItem(string key, UIItem item)
        {
            base.AddItem(key, item);

            if (TextureCache != null)
            {
                XnaUIItem xnaitem = GetItemAs<XnaUIItem>(key);

                if (xnaitem != null)
                {
                    xnaitem.LoadContent(TextureCache);
                }
            }
        }
    }
}
