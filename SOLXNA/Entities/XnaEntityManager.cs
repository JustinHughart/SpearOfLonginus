
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SOLXNA.Animations;
using SpearOfLonginus.Entities;
using SpearOfLonginus.Maps;
using Rectangle = SpearOfLonginus.Rectangle;

namespace SOLXNA.Entities
{
    public class XnaEntityManager : EntityManager
    {
        public XnaEntityManager(Map map) : base(map)
        {

        }

        public virtual void LoadContent(TextureCache texturecache)
        {
            foreach (var entity in Entities)
            {
                var xnaentity = entity as XnaEntity;

                if (xnaentity != null)
                {
                    xnaentity.LoadContent(texturecache);
                }
            }
        }

        public void Draw(SpriteBatch spritebatch, Rectangle drawarea, Matrix cameramatrix)
        {
            foreach (var entity in Entities)
            {
                if (drawarea.Intersects(entity.WorldHitbox))
                {
                    var xnaentity = entity as XnaEntity;

                    if (xnaentity != null)
                    {
                        xnaentity.Draw(spritebatch, cameramatrix);
                    }
                }
            }
        }

        public void DrawHitboxes(SpriteBatch spritebatch, Rectangle drawarea, Matrix cameramatrix, Texture2D texture)
        {
            foreach (var entity in Entities)
            {
                if (drawarea.Intersects(entity.WorldHitbox))
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
