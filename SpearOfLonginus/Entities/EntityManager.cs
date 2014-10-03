using System.Collections.Generic;
using System.ComponentModel;
using SpearOfLonginus.Input;
using SpearOfLonginus.Maps;

namespace SpearOfLonginus.Entities
{
    /// <summary>
    /// A class that handles entities for a map.
    /// </summary>
    public class EntityManager
    {
        #region Variables

        /// <summary>
        /// The entities that are being handled.
        /// </summary>
        protected List<Entity> Entities;
        /// <summary>
        /// The map the entities reside in.
        /// </summary>
        protected Map Map;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityManager"/> class.
        /// </summary>
        /// <param name="map">The map.</param>
        public EntityManager(Map map)
        {
           Entities = new List<Entity>();
            Map = map;
        }

        #endregion

        #region Functions

        /// <summary>
        /// Gets the entity list copy.
        /// </summary>
        /// <returns></returns>
        public virtual List<Entity> GetEntityListCopy()
        {
            return new List<Entity>(Entities.ToArray());
        }

        /// <summary>
        /// Adds the entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void AddEntity(Entity entity)
        {
            entity.Map = Map;
            Entities.Add(entity);
        }

        /// <summary>
        /// Removes the entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void RemoveEntity(Entity entity)
        {
            entity.Map = null;
            Entities.Remove(entity);
        }

        public virtual void Update(InputManager inputmanager, float deltatime)
        {
            foreach (var entity in Entities)
            {
                switch (entity.PlayerType)
                {
                    case PlayerType.NPC:
                        entity.Update(entity.GetAIPacket(), deltatime);
                        break;
                    case PlayerType.Player1:
                        entity.Update(inputmanager.GetPlayer1Packet(), deltatime);
                        break;
                    case PlayerType.Player2:
                        entity.Update(inputmanager.GetPlayer2Packet(), deltatime);
                        break;
                    case PlayerType.Player3:
                        entity.Update(inputmanager.GetPlayer3Packet(), deltatime);
                        break;
                    case PlayerType.Player4:
                        entity.Update(inputmanager.GetPlayer4Packet(), deltatime);
                        break;
                        case PlayerType.World:
                        entity.Update(null, deltatime); //We will send a lack of a packet, so we can update animation, but not handle any logic.
                        break;
                    default:
                        throw new InvalidEnumArgumentException("Unsupported player type in Entity.");
                }
            }

            Entities.Sort(); //Sort the entities by Y value.
        }

        #endregion
    }
}
