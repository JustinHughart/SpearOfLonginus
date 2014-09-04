using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SOLXNA.Animations;
using SpearOfLonginus.Maps;

namespace SOLXNA.Maps
{
    public class XnaMap : Map
    {
        public XnaMap(string path) : base(path)
        {
            
        }

        public virtual void LoadContent(TextureCache texturecache)
        {
            foreach (var tile in TileSet)
            {
                if (tile != null)
                {
                    var anim = (XnaAnimation) tile.Animation;

                    tile.Animation = new XnaAnimation(anim, texturecache);
                }
            }

            foreach (var backdrop in Backdrops)
            {
                Backdrops[backdrop.Key] = new XnaBackdrop(backdrop.Value, texturecache);
            }

            foreach (var foredrop in Foredrops)
            {
                Backdrops[foredrop.Key] = new XnaBackdrop(foredrop.Value, texturecache);
            }
        }

        public virtual void UnloadContent()
        {
            foreach (var tile in TileSet)
            {
                if (tile != null)
                {
                    var anim = (XnaAnimation) tile.Animation;

                    anim.UnloadContent();
                }
            }

            foreach (var backdrop in Backdrops)
            {
                var bd = (XnaBackdrop) backdrop.Value;

                bd.UnloadContent();
            }

            foreach (var foredrop in Foredrops)
            {
                var fd = (XnaBackdrop)foredrop.Value;

                fd.UnloadContent();
            }
        }

        public virtual void DrawBackground(SpriteBatch spritebatch, Rectangle drawarea)
        {
            foreach (var backdrop in Backdrops)
            {
                var bd = (XnaBackdrop) backdrop.Value;

                bd.Draw(spritebatch, drawarea);
            }

            DrawTileLayer(spritebatch, drawarea, BackgroundLayer);
            DrawTileLayer(spritebatch, drawarea, CollisionLayer);
        }

        public virtual void DrawForeground(SpriteBatch spritebatch, Rectangle drawarea)
        {
            DrawTileLayer(spritebatch, drawarea, ForegroundLayer);

            foreach (var foredrop in Foredrops)
            {
                var fd = (XnaBackdrop)foredrop.Value;

                fd.Draw(spritebatch, drawarea);
            }
        }

        protected virtual void DrawTileLayer(SpriteBatch spritebatch, Rectangle drawarea, int[] layer)
        {
            int xstart = (int)(drawarea.X/TileSize.X);
            int ystart = (int) (drawarea.Y/TileSize.Y);
            int xend = xstart + (int)(drawarea.Width / TileSize.X);
            int yend = ystart + (int)(drawarea.Height / TileSize.Y);

            for (int y = ystart; y < yend; y++)
            {
                for (int x = xstart; x < xend; x++)
                {
                    var tile = GetTile(new Vector2(x, y));

                    var anim = (XnaAnimation) tile.Animation;

                    anim.Draw(spritebatch, new Vector2(x,y) * TileSize.ToXnaVector(), Color.White, 0f, Vector2.One, SpriteEffects.None, 0);
                }
            }
        }

        public virtual Tile GetTile(Vector2 position)
        {
            int gid = (int) position.X + (int) (position.Y*?.X) + 1;

            return 
        }

    }
}
