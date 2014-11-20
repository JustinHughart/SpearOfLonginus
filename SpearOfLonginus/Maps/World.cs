﻿using System.Collections.Generic;
using SpearOfLonginus.Entities;

namespace SpearOfLonginus.Maps
{
    /// <summary>
    /// A class containing the game world.
    /// </summary>
    public class World
    {
        /// <summary>
        /// The maps in the game world.
        /// </summary>
        protected Dictionary<string, Map> Maps;
        /// <summary>
        /// The transition that occurs between maps.
        /// </summary>
        protected Transition Transition;

        /// <summary>
        /// Initializes a new instance of the <see cref="World"/> class.
        /// </summary>
        public World()
        {
            Maps = new Dictionary<string, Map>();
        }

        /// <summary>
        /// Gets the map from the cache.
        /// </summary>
        /// <param name="mapid">The map's ID.</param>
        /// <returns></returns>
        public virtual Map GetMap(string mapid)
        {
            if (!Maps.ContainsKey(mapid))
            {
                Maps.Add(mapid, LoadMap(mapid));
            }

            return Maps[mapid];
        }

        /// <summary>
        /// Loads the map from file.
        /// </summary>
        /// <param name="mappath">The map's path.</param>
        /// <returns></returns>
        public virtual Map LoadMap(string mappath)
        {
            return new Map(mappath);
        }

        /// <summary>
        /// Removes the map from the cache.
        /// </summary>
        /// <param name="mapid">The map's ID.</param>
        public virtual void RemoveMap(string mapid)
        {
            Maps.Remove(mapid);
        }

        /// <summary>
        /// Moves an entity between maps.
        /// </summary>
        /// <param name="entity">The entity changing maps.</param>
        /// <param name="oldmapid">The old map's ID.</param>
        /// <param name="newmapid">The new map's ID.</param>
        public virtual void ChangeMaps(Entity entity, string oldmapid, string newmapid)
        {
            Map oldmap = GetMap(oldmapid);
            Map newmap = GetMap(newmapid);

            oldmap.Entities.RemoveEntity(entity.ID);
            newmap.Entities.AddEntity(entity);

            if (!oldmap.IsPersistent())
            {
                RemoveMap(oldmapid);
            }

            if (entity.IsPlayer())
            {
                //TRANSITION STUFF
            }
        }
    }
}
