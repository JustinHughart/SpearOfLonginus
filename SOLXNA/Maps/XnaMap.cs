using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SOLXNA.Animations;
using SOLXNA.Entities;
using SpearOfLonginus;
using SpearOfLonginus.Maps;
using Rectangle = SpearOfLonginus.Rectangle;

namespace SOLXNA.Maps
{
    /// <summary>
    /// An XNA implementation of SOL's Map.
    /// </summary>
    public class XnaMap : Map
    {
        #region Variables

        /// <summary>
        /// The data for initializing spritebatch for drawing the background.
        /// </summary>
        /// <value>
        /// The background data.
        /// </value>
        public SpriteBatchData BackgroundData { get; protected set; }
        /// <summary>
        /// The data for initializing spritebatch for drawing theforeground
        /// </summary>
        /// <value>
        /// The foreground data.
        /// </value>
        public SpriteBatchData ForegroundData { get; protected set; }
        /// <summary>
        /// The data for initializing spritebatch for drawing the backdrops.
        /// </summary>
        /// <value>
        /// The backdrop data.
        /// </value>
        public SpriteBatchData BackdropData { get; protected set; }
        /// <summary>
        /// The data for initializing spritebatch for drawing the foredrops.
        /// </summary>
        /// <value>
        /// The foredrop data.
        /// </value>
        public SpriteBatchData ForedropData { get; protected set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="XnaMap"/> class.
        /// </summary>
        /// <param name="path">The file path of the base64 gzipped Tiled map.</param>
        public XnaMap(string path) : this(path, null, null, null, null)
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XnaMap"/> class.
        /// </summary>
        /// <param name="path">The file path of the base64 gzipped Tiled map.</param>
        /// <param name="usehitboxcache">Whether or not to use the hitbox cache.</param>
        public XnaMap(string path, bool usehitboxcache)
            : this(path, usehitboxcache, null, null, null, null)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XnaMap"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="backgrounddata">The background's spritebatch data.</param>
        /// <param name="foregrounddata">The foreground's spritebatch data.</param>
        /// <param name="backdropdata">The backdrop's spritebatch data.</param>
        /// <param name="foredropdata">The foredrop's spritebatch data.</param>
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

            //WILL CHANGE LATER
            Entities = new XnaEntityManager(this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XnaMap" /> class.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="usehitboxcache">Whether or not to use the hitbox cache.</param>
        /// <param name="backgrounddata">The background's spritebatch data.</param>
        /// <param name="foregrounddata">The foreground's spritebatch data.</param>
        /// <param name="backdropdata">The backdrop's spritebatch data.</param>
        /// <param name="foredropdata">The foredrop's spritebatch data.</param>
        public XnaMap(string path, bool usehitboxcache, SpriteBatchData backgrounddata, SpriteBatchData foregrounddata, SpriteBatchData backdropdata, SpriteBatchData foredropdata): this(path, backgrounddata, foregrounddata, backdropdata, foredropdata)
        {
            UseHitboxCache = usehitboxcache;

            if (usehitboxcache)
            {
                CreateHitboxCache();
            }
        }

        #endregion

        #region Functions

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="texturecache">The texture cache used for loading textures.</param>
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

            var entities = Entities as XnaEntityManager;

            if (entities != null)
            {
                entities.LoadContent(texturecache);
            }
        }

        /// <summary>
        /// Unloads the content.
        /// </summary>
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

        /// <summary>
        /// Runs the general draw cycle for a map.
        /// </summary>
        /// <param name="spritebatch">The spritebatch to use for drawing.</param>
        /// <param name="drawarea">The area of the screen.</param>
        /// <param name="cameraposition">The camera's position.</param>
        /// <param name="cameramatrix">The camera's transformation matrix. </param>
        public virtual void DrawCycle(SpriteBatch spritebatch, Rectangle drawarea, Vector2 cameraposition, Matrix cameramatrix)
        {
            DrawBackground(spritebatch, drawarea, cameraposition, cameramatrix);

            var entities = Entities as XnaEntityManager;

            if (entities != null)
            {
                entities.Draw(spritebatch, drawarea, cameramatrix);
            }

            DrawForeground(spritebatch, drawarea, cameraposition, cameramatrix);
        }

        /// <summary>
        /// Draws the debug information.
        /// </summary>
        /// <param name="spritebatch">The spritebatch to use for drawing.</param>
        /// <param name="drawarea">The area of the screen.</param>
        /// <param name="cameramatrix">The camera's transformation matrix.</param>
        /// <param name="texture">The texture use to draw. It should be a 1x1 white pixel texture.</param>
        public virtual void DrawDebugInfo(SpriteBatch spritebatch, Rectangle drawarea, Matrix cameramatrix, Texture2D texture)
        {
            var entities = Entities as XnaEntityManager;

            if (entities != null)
            {
                entities.DrawHitboxes(spritebatch, drawarea, cameramatrix, texture);
            }
        }

        /// <summary>
        /// Draws the background.
        /// </summary>
        /// <param name="spritebatch">The spritebatch to use for drawing.</param>
        /// <param name="drawarea">The area of the screen.</param>
        /// <param name="cameraposition">The camera's position.</param>
        /// <param name="cameramatrix">The camera's transformation matrix. </param>
        public virtual void DrawBackground(SpriteBatch spritebatch, Rectangle drawarea, Vector2 cameraposition, Matrix cameramatrix)
        {
            spritebatch.Begin(SpriteSortMode.Immediate, BackdropData.BlendState, BackdropData.SamplerState, BackdropData.DepthStencilState, BackdropData.RasterizerState, BackdropData.Effect, cameramatrix);
            
            var drops = new List<XnaBackdrop>(Backdrops.Count);

            foreach (var backdrop in Backdrops)
            {
                drops.Add((XnaBackdrop) backdrop.Value);
            }

            drops.Sort();

            foreach (var drop in drops)
            {
                drop.Draw(spritebatch, drawarea, cameraposition);
            }

            spritebatch.End();

            spritebatch.Begin(SpriteSortMode.Immediate, BackgroundData.BlendState, BackgroundData.SamplerState, BackgroundData.DepthStencilState, BackgroundData.RasterizerState, BackgroundData.Effect, cameramatrix);
            
            DrawTileLayer(spritebatch, drawarea, BackgroundLayer);
            DrawTileLayer(spritebatch, drawarea, CollisionLayer);

            spritebatch.End();
        }

        /// <summary>
        /// Draws the foreground.
        /// </summary>
        /// <param name="spritebatch">The spritebatch to use for drawing.</param>
        /// <param name="drawarea">The area of the screen.</param>
        /// <param name="cameraposition">The camera's position.</param>
        /// <param name="cameramatrix">The camera's transformation matrix. </param>
        public virtual void DrawForeground(SpriteBatch spritebatch, Rectangle drawarea, Vector2 cameraposition, Matrix cameramatrix)
        {
            spritebatch.Begin(SpriteSortMode.Immediate, ForegroundData.BlendState, ForegroundData.SamplerState, ForegroundData.DepthStencilState, ForegroundData.RasterizerState, ForegroundData.Effect, cameramatrix);
            
            DrawTileLayer(spritebatch, drawarea, ForegroundLayer);

            spritebatch.End();

            spritebatch.Begin(SpriteSortMode.Immediate, ForedropData.BlendState, ForedropData.SamplerState, ForedropData.DepthStencilState, ForedropData.RasterizerState, ForedropData.Effect, cameramatrix);

            var drops = new List<XnaBackdrop>(Foredrops.Count);

            foreach (var foredrop in Foredrops)
            {
                drops.Add((XnaBackdrop)foredrop.Value);
            }

            drops.Sort();

            foreach (var drop in drops)
            {
                drop.Draw(spritebatch, drawarea, cameraposition);
            }

            spritebatch.End();
        }

        /// <summary>
        /// Draws the tile layer.
        /// </summary>
        /// <param name="spritebatch">The spritebatch to use for drawing.</param>
        /// <param name="drawarea">The area of the screen.</param>
        /// <param name="layer">The layer to draw.</param>
        protected virtual void DrawTileLayer(SpriteBatch spritebatch, Rectangle drawarea, int[] layer)
        {
            var xstart = (int)(drawarea.X/TileSize.X);
            var ystart = (int) (drawarea.Y/TileSize.Y);
            var xend = xstart + (int)(drawarea.Width / TileSize.X) + 2;
            var yend = ystart + (int)(drawarea.Height / TileSize.Y) + 2;

            for (var y = ystart; y < yend; y++)
            {
                for (var x = xstart; x < xend; x++)
                {
                    var tile = GetTile(new Vector(x, y), layer);

                    if (tile != null)
                    {
                        var anim = (XnaAnimation)tile.Animation;

                        anim.Draw(spritebatch, new Vector2(x, y) * TileSize.ToXnaVector(), Color.White, 0f, Vector2.One, SpriteEffects.None, 0);
                    }
                }
            }
        }

        /// <summary>
        /// Draws the collision maps.
        /// </summary>
        /// <param name="spritebatch">The spritebatch to use for drawing.</param>
        /// <param name="drawarea">The area of the screen.</param>
        /// <param name="cameramatrix">The camera's transformation matrix. </param>
        /// <param name="texture">The texture use to draw. It should be a 1x1 white pixel texture.</param>
        public virtual void DrawCollisionMaps(SpriteBatch spritebatch, Rectangle drawarea, Matrix cameramatrix, Texture2D texture)
        {
            var xstart = (int)(drawarea.X / TileSize.X);
            var ystart = (int)(drawarea.Y / TileSize.Y);
            var xend = xstart + (int)(drawarea.Width / TileSize.X) + 2;
            var yend = ystart + (int)(drawarea.Height / TileSize.Y) + 2;

            Rectangle empty = new Rectangle();

            spritebatch.Begin(SpriteSortMode.Immediate, null, SamplerState.PointClamp, null, null, null, cameramatrix);

            for (var y = ystart; y < yend; y++)
            {
                for (var x = xstart; x < xend; x++)
                {
                    var hitbox = GetHitbox(new Vector(x, y));
                    
                    if (!hitbox.Equals(empty))
                    {
                        spritebatch.Draw(texture, new Vector2(hitbox.X, hitbox.Y), texture.Bounds, Color.Aqua * .7f, 0f, Vector2.Zero, new Vector2(hitbox.Width, hitbox.Height), SpriteEffects.None, 0f);
                    }
                }
            }

            spritebatch.End();
        }

        #endregion
    }
}
