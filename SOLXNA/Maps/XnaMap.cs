using System.Collections.Generic;
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
                    tile.Animation = new XnaAnimation(tile.Animation, texturecache);
                }
            }

            var newdrops = new Dictionary<string, Backdrop>();

            foreach (var backdrop in Backdrops)
            {
                newdrops.Add(backdrop.Key, new XnaBackdrop(backdrop.Value, texturecache));
            }

            Backdrops = newdrops;
            newdrops = new Dictionary<string, Backdrop>();

            foreach (var foredrop in Foredrops)
            {
                newdrops.Add(foredrop.Key, new XnaBackdrop(foredrop.Value, texturecache));
            }

            Foredrops = newdrops;
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

        public virtual void DrawBackground(SpriteBatch spritebatch, Rectangle drawarea, Vector2 camerapos)
        {
            foreach (var backdrop in Backdrops)
            {
                var bd = (XnaBackdrop) backdrop.Value;

                bd.Draw(spritebatch, drawarea, camerapos);
            }

            DrawTileLayer(spritebatch, drawarea, BackgroundLayer);
            DrawTileLayer(spritebatch, drawarea, CollisionLayer);
        }

        public virtual void DrawForeground(SpriteBatch spritebatch, Rectangle drawarea, Vector2 camerapos)
        {
            DrawTileLayer(spritebatch, drawarea, ForegroundLayer);

            foreach (var foredrop in Foredrops)
            {
                var fd = (XnaBackdrop)foredrop.Value;

                fd.Draw(spritebatch, drawarea, camerapos);
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
                    var tile = GetTile(new Vector2(x, y), layer);

                    if (tile != null)
                    {
                        var anim = (XnaAnimation)tile.Animation;

                        anim.Draw(spritebatch, new Vector2(x, y) * TileSize.ToXnaVector(), Color.White, 0f, Vector2.One, SpriteEffects.None, 0);
                    }
                }
            }
        }

        public virtual Tile GetTile(Vector2 position, int[] layer)
        {
            int index = (int) position.X + (int) (position.Y*Size.X);
            int gid = layer[index];

            if (gid == -1)
            {
                return null;
            }

            return TileSet[gid];
        }
    }
}
