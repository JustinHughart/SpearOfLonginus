using System;
using System.Collections.Generic;
using System.Xml.Linq;
using SpearOfLonginus.Input;
using SpearOfLonginus.Maps;

namespace SpearOfLonginus.Entities.Components
{
    /// <summary>
    /// A component for entities to use to interact with the world around them.
    /// </summary>
    public class InteractComponent : Component
    {
        #region Variables

        /// <summary>
        /// The starting distance of the ray.
        /// </summary>
        protected float StartDistance;
        /// <summary>
        /// The maximum distance of the ray.
        /// </summary>
        protected float MaxDistance;
        /// <summary>
        /// The iteration rate of the ray.
        /// </summary>
        protected float IterationRate;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="InteractComponent"/> class.
        /// </summary>
        public InteractComponent()
        {
            MaxDistance = 32;
            IterationRate = 1;
        }

        #endregion

        #region Functions

        /// <summary>
        /// Updates the component.
        /// </summary>
        /// <param name="packet">The packet of input data.</param>
        /// <param name="deltatime">The time that has passed since last update.</param>
        public override void Update(InputPacket packet, float deltatime)
        {
            base.Update(packet, deltatime);

            if (packet.Interact == PressState.Pressed)
            {
                //First we'll get where the reticle is.
                Vector position = Owner.Hitbox.Center;
                Vector direction = Vector.Zero;

                switch (Owner.Facing)
                {
                    case (FacingState.North):
                        direction.Y--;
                        break;
                    case (FacingState.Northeast):
                        direction.X++;
                        direction.Y--;
                        break;
                    case (FacingState.East):
                        direction.X++;
                        break;
                    case (FacingState.Southeast):
                        direction.X++;
                        direction.Y++;
                        break;
                    case (FacingState.South):
                        direction.Y++;
                        break;
                    case (FacingState.Southwest):
                        direction.X--;
                        direction.Y++;
                        break;
                    case (FacingState.West):
                        direction.X--;
                        break;
                    case (FacingState.Northwest):
                        direction.X--;
                        direction.Y--;
                        break;
                }

                direction.Normalize();

                position += direction * StartDistance;
                
                for (float i = StartDistance; i < MaxDistance; i += IterationRate)
                {
                    //First we'll check entities.
                    //This needs more optimizations. It's rudimentary for now.
                    Rectangle hitbox = new Rectangle((int)position.X, (int)position.Y, 1, 1);
                    
                    foreach (var entity in Owner.Map.Entities.GetEntityList())
                    {
                        if (entity.Hitbox.Intersects(hitbox))
                        {
                            if (entity.OnInteraction(Owner))
                            {
                                //We're done if we find something.
                                return;
                            }
                        }
                    }

                    //Then we'll check tiles.
                    Vector tileposition = new Vector((int)position.X / (int)Owner.Map.TileSize.X, (int)position.Y / (int)Owner.Map.TileSize.Y);
                    Tile tile = Owner.Map.GetTile(tileposition, Owner.Map.CollisionLayer);

                    if (tile != null)
                    {
                        if (tile.HandleCheckEffects(tileposition, Owner))
                        {
                            //We're done if we find something.
                            return;
                        }
                    }

                    position += direction * IterationRate;
                }
            }
        }

        /// <summary>
        /// Uses XML to initialize the object.
        /// </summary>
        /// <param name="element">The element used for loading.</param>
        public override void LoadFromXml(XElement element)
        {
            //Attributes
            foreach (var attribute in element.Attributes())
            {
                if (attribute.Name.LocalName.Equals("startdistance", StringComparison.OrdinalIgnoreCase))
                {
                    float value;

                    if (float.TryParse(attribute.Value, out value))
                    {
                        StartDistance = value;
                    }

                    continue;
                }

                if (attribute.Name.LocalName.Equals("maxdistance", StringComparison.OrdinalIgnoreCase))
                {
                    float value;

                    if (float.TryParse(attribute.Value, out value))
                    {
                        MaxDistance = value;
                    }

                    continue;
                }

                if (attribute.Name.LocalName.Equals("iterationrate", StringComparison.OrdinalIgnoreCase))
                {
                    float value;

                    if (float.TryParse(attribute.Value, out value))
                    {
                        IterationRate = value;
                    }

                    continue;
                }
            }
        }

        #endregion

    }
}
