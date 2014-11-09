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
        protected Dictionary<string, Entity> Entities;
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
           Entities = new Dictionary<string, Entity>();
            Map = map;
        }

        #endregion

        #region Functions

        /// <summary>
        /// Gets the entity list copy.
        /// </summary>
        /// <returns></returns>
        public virtual List<Entity> GetEntityList()
        {
            return new List<Entity>(Entities.Values);
        }

        /// <summary>
        /// Adds the entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void AddEntity(Entity entity)
        {
            DetermineListValue(entity);
            entity.Map = Map;
            Entities.Add(entity.ID, entity);
        }

        /// <summary>
        /// Gets the entity specified by the ID.
        /// </summary>
        /// <param name="id">The identifier of the entity.</param>
        /// <returns></returns>
        public virtual Entity GetEntity(string id)
        {
            if (!Entities.ContainsKey(id))
            {
                return null;
            }

            return Entities[id];
        }

        /// <summary>
        /// Removes the entity.
        /// </summary>
        /// <param name="id">The identifier of the entity.</param>
        public virtual void RemoveEntity(string id)
        {
            if (!Entities.ContainsKey(id))
            {
                return;
            }

            Entities[id].Map = null;
            Entities.Remove(id);
        }

        /// <summary>
        /// Updates the entities
        /// </summary>
        /// <param name="inputmanager">The input manager for gathering input..</param>
        /// <param name="deltatime">The time since last frame.</param>
        /// <exception cref="System.ComponentModel.InvalidEnumArgumentException">Unsupported player type in Entity.</exception>
        public virtual void Update(InputManager inputmanager, float deltatime)
        {
            foreach (var entity in Entities.Values)
            {
                switch (entity.InputType)
                {
                    case InputType.NPC:
                        entity.Update(entity.GetAIPacket(), deltatime);
                        break;
                    case InputType.Player1:
                        entity.Update(inputmanager.GetPlayer1Packet(), deltatime);
                        break;
                    case InputType.Player2:
                        entity.Update(inputmanager.GetPlayer2Packet(), deltatime);
                        break;
                    case InputType.Player3:
                        entity.Update(inputmanager.GetPlayer3Packet(), deltatime);
                        break;
                    case InputType.Player4:
                        entity.Update(inputmanager.GetPlayer4Packet(), deltatime);
                        break;
                        case InputType.World:
                        entity.Update(null, deltatime); //We will send a lack of a packet, so we can update animation, but not handle any logic.
                        break;
                    default:
                        throw new InvalidEnumArgumentException("Unsupported player type in Entity.");
                }
            }
        }

        /// <summary>
        /// Gets a y-sorted list of entities.
        /// </summary>
        /// <returns></returns>
        public List<Entity> GetSortedEntities()
        {
            List<Entity> list = new List<Entity>(Entities.Count);

            foreach (var entity in Entities.Values)
            {
                list.Add(entity);
            }

            list.Sort();

            return list;
        }

        /// <summary>
        /// Determines the value of the entity in the list.
        /// </summary>
        /// <param name="entity">The entity.</param>
        protected void DetermineListValue(Entity entity)
        {
            float value = float.Epsilon;

            bool valid = false;

            while (!valid)
            {
                bool found = false;

                foreach (var other in Entities.Values)
                {
                    if (other.ListValue.Equals(value))
                    {
                        value += float.Epsilon;
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    valid = true;
                }
            }

            entity.ListValue = value;
        }

        #endregion
    }
}
