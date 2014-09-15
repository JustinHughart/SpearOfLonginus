using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SOLXNA.Animations;
using SpearOfLonginus.Maps;

namespace SOLXNA.Maps
{
    public class XnaMap : Map
    {
        public SpriteBatchData BackgroundData { get; protected set; }
        public SpriteBatchData ForegroundData { get; protected set; }
        public SpriteBatchData BackdropData { get; protected set; }
        public SpriteBatchData ForedropData { get; protected set; }
        
        public XnaMap(string path) : this(path, null, null, null, null)
        {
            
        }

        public XnaMap(string path, SpriteBatchData backgrounddata, SpriteBatchData foregrounddata, SpriteBatchData backdropdata, SpriteBatchData foredropdata) : base(path)
        {
            if (backgrounddata == null)
            {
                BackgroundData = new SpriteBatchData();
            }
            else
            {
                BackgroundData = backdropdata;
            }

            if (foregrounddata == null)
            {
                ForegroundData = new SpriteBatchData();
            }
            else
            {
                ForegroundData = foregrounddata;
            }

            if (backdropdata == null)
            {
                BackdropData = new SpriteBatchData();
            }
            else
            {
                BackdropData = backdropdata;
            }

            if (foredropdata == null)
            {
                ForedropData = new SpriteBatchData();
            }
            else
            {
                ForedropData = foredropdata;
            }
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
            spritebatch.Begin(SpriteSortMode.Immediate, BackdropData.BlendState, BackdropData.SamplerState, BackdropData.DepthStencilState, BackdropData.RasterizerState, BackdropData.Effect);
            
            foreach (var backdrop in Backdrops)
            {
                var bd = (XnaBackdrop) backdrop.Value;

                bd.Draw(spritebatch, drawarea, camerapos);
            }

            spritebatch.End();

            spritebatch.Begin(SpriteSortMode.Immediate, BackgroundData.BlendState, BackgroundData.SamplerState, BackgroundData.DepthStencilState, BackgroundData.RasterizerState, BackgroundData.Effect);
            
            DrawTileLayer(spritebatch, drawarea, BackgroundLayer);
            DrawTileLayer(spritebatch, drawarea, CollisionLayer);

            spritebatch.End();
        }

        public virtual void DrawForeground(SpriteBatch spritebatch, Rectangle drawarea, Vector2 camerapos)
        {
            spritebatch.Begin(SpriteSortMode.Immediate, ForegroundData.BlendState, ForegroundData.SamplerState, ForegroundData.DepthStencilState, ForegroundData.RasterizerState, ForegroundData.Effect);
            
            DrawTileLayer(spritebatch, drawarea, ForegroundLayer);

            spritebatch.End();

            spritebatch.Begin(SpriteSortMode.Immediate, ForedropData.BlendState, ForedropData.SamplerState, ForedropData.DepthStencilState, ForedropData.RasterizerState, ForedropData.Effect);
            
            foreach (var foredrop in Foredrops)
            {
                var fd = (XnaBackdrop)foredrop.Value;

                fd.Draw(spritebatch, drawarea, camerapos);
            }

            spritebatch.End();
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
