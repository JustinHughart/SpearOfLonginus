using SOLXNA.Animations;
using SpearOfLonginus.Maps;

namespace SOLXNA.Maps
{
    /// <summary>
    /// 
    /// </summary>
    public class XnaWorld : World
    {
        #region Variables

        /// <summary>
        /// The texture cache used for loading textures.
        /// </summary>
        protected TextureCache TextureCache;

        #endregion

       #region Functions

        /// <summary>
        /// Loads the content of all maps.
        /// </summary>
        /// <param name="texturecache">The texturecache.</param>
        public virtual void LoadContent(TextureCache texturecache)
        {
            TextureCache = texturecache;

            foreach (var map in Maps.Values)
            {
                var xnamap = map as XnaMap;

                if (xnamap == null)
                {
                    continue;
                }

                xnamap.LoadContent(texturecache);
            }
        }

        /// <summary>
        /// Unloads the content of all maps.
        /// </summary>
        public virtual void UnloadContent()
        {
            foreach (var map in Maps.Values)
            {
                var xnamap = map as XnaMap;

                if (xnamap == null)
                {
                    continue;
                }

                xnamap.UnloadContent();
            }
        }

        /// <summary>
        /// Loads the map from file.
        /// </summary>
        /// <param name="mappath">The map's path.</param>
        /// <returns></returns>
        public override Map LoadMap(string mappath)
        {
            XnaMap map = new XnaMap(mappath);
            map.World = this;

            if (TextureCache != null)
            {
                map.LoadContent(TextureCache);
            }

            return map;
        }

        /// <summary>
        /// Removes the map from the cache.
        /// </summary>
        /// <param name="mapid">The map's ID.</param>
        public override void RemoveMap(string mapid)
        {
            XnaMap map = GetMap(mapid) as XnaMap;

            if (map != null)
            {
                map.UnloadContent();
            }

            base.RemoveMap(mapid);
        }

        #endregion

    }
}
