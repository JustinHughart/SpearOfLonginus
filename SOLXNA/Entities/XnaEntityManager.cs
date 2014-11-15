using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SOLXNA.Animations;
using SpearOfLonginus.Entities;
using SpearOfLonginus.Maps;
using Rectangle = SpearOfLonginus.Rectangle;

namespace SOLXNA.Entities
{
    /// <summary>
    /// An XNA binding for SoL's EntityManager.
    /// </summary>
    public class XnaEntityManager : EntityManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XnaEntityManager"/> class.
        /// </summary>
        /// <param name="map">The map.</param>
        public XnaEntityManager(Map map) : base(map)
        {

        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="texturecache">The texture cache used for loading textures.</param>
        public virtual void LoadContent(TextureCache texturecache)
        {
            foreach (var entity in Entities.Values)
            {
                var xnaentity = entity as XnaEntity;

                if (xnaentity != null)
                {
                    xnaentity.LoadContent(texturecache);
                }
            }
        }

        /// <summary>
        /// Draws the specified spritebatch.
        /// </summary>
        /// <param name="spritebatch">The spritebatch to use for drawing.</param>
        /// <param name="drawarea">The area of the screen.</param>
        /// <param name="cameramatrix">The camera's transformation matrix. </param>
        public void Draw(SpriteBatch spritebatch, Rectangle drawarea, Matrix cameramatrix)
        {
            foreach (var entity in GetSortedEntities())
            {
                if (drawarea.Intersects(entity.Hitbox))
                {
                    var xnaentity = entity as XnaEntity;

                    if (xnaentity != null)
                    {
                        xnaentity.Draw(spritebatch, cameramatrix);
                    }
                }
            }
        }

        /// <summary>
        /// Draws the hitboxes.
        /// </summary>
        /// <param name="spritebatch">The spritebatch to use for drawing.</param>
        /// <param name="drawarea">The area of the screen.</param>
        /// <param name="cameramatrix">The camera's transformation matrix. </param>
        /// <param name="texture">The texture used for drawing the hitbox. Should be a 1x1 white pixel.</param>
        public void DrawHitboxes(SpriteBatch spritebatch, Rectangle drawarea, Matrix cameramatrix, Texture2D texture)
        {
            foreach (var entity in GetSortedEntities())
            {
                if (drawarea.Intersects(entity.Hitbox))
                {
                    var xnaentity = entity as XnaEntity;

                    if (xnaentity != null)
                    {
                        xnaentity.DrawHitbox(spritebatch, cameramatrix, texture);
                    }
                }
            }
        }
    }
}
