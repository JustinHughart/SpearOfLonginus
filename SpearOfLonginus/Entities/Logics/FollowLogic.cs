using System;
using System.Xml.Linq;
using SpearOfLonginus.Input;

namespace SpearOfLonginus.Entities.Logics
{
    /// <summary>
    /// A logic for one entity to follow another.
    /// </summary>
    public class FollowLogic : Logic
    {
        #region Variables

        /// <summary>
        /// The target to follow.
        /// </summary>
        protected Entity Target;

        /// <summary>
        /// The ID of the target.
        /// </summary>
        protected string TargetID;

        /// <summary>
        /// The distance from the target to start moving at.
        /// </summary>
        protected float Distance;

        /// <summary>
        /// The distance from the target at which to start running.
        /// </summary>
        protected float RunDistance;

        /// <summary>
        /// The distance of where NOT to move, so you don't move for 1 pixel changes.
        /// </summary>
        protected Vector Safezone;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FollowLogic"/> class.
        /// </summary>
        public FollowLogic()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FollowLogic" /> class.
        /// </summary>
        /// <param name="owner">The entity who owns the component.</param>
        /// <param name="target">The target to follow.</param>
        /// <param name="distance">The distance from the target to start moving at.</param>
        /// <param name="rundistance">The distance from the target at which to start running.</param>
        /// <param name="safezone">The distance of where NOT to move, so you don't move for 1 pixel changes.</param>
        public FollowLogic(Entity owner, Entity target, float distance, float rundistance, Vector safezone)
            : base(owner)
        {
            Target = target;
            Distance = distance;
            Safezone = safezone;
            RunDistance = rundistance;
        }

        #endregion

        #region Functions

        /// <summary>
        /// Modifies the packet to determine its input.
        /// </summary>
        /// <param name="packet">The packet to modify.</param>
        public override void GetInput(InputPacket packet)
        {
            if (Target == null)
            {
                return;
            }

            Vector diff = Target.Position - Owner.Position;

            if (diff.Length() > Distance)
            {
                //Check for Down.
                if (diff.Y > 0)
                {
                    if (Math.Abs(diff.Y) > Safezone.Y)
                    {
                        packet.Down = PressState.Down;
                    }
                }

                //Check for Up.
                if (diff.Y < 0)
                {
                    if (Math.Abs(diff.Y) > Safezone.Y)
                    {
                        packet.Up = PressState.Down;
                    }
                }

                //Check for Right.
                if (diff.X > 0)
                {
                    if (Math.Abs(diff.X) > Safezone.X)
                    {
                        packet.Right = PressState.Down;
                    }
                }

                //Check for Left.
                if (diff.X < 0)
                {
                    if (Math.Abs(diff.X) > Safezone.X)
                    {
                        packet.Left = PressState.Down;
                    }
                }
            }

            //Check if you should run.
            if (diff.Length() > RunDistance)
            {
                packet.Run = PressState.Down;
            }
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            if (Target == null)
            {
                Target = Owner.Map.Entities.GetEntity(TargetID);
            }
        }

        /// <summary>
        /// Uses XML to initialize the object.
        /// </summary>
        /// <param name="element">The element used for loading..</param>
        public override void LoadFromXml(System.Xml.Linq.XElement element)
        {
            //Attributes
            foreach (var attribute in element.Attributes())
            {
                if (attribute.Name.LocalName.Equals("targetid", StringComparison.OrdinalIgnoreCase))
                {
                    TargetID = attribute.Value;
                    continue;
                }

                if (attribute.Name.LocalName.Equals("distance", StringComparison.OrdinalIgnoreCase))
                {
                    float value;

                    if (float.TryParse(attribute.Value, out value))
                    {
                        Distance = value;
                    }

                    continue;
                }

                if (attribute.Name.LocalName.Equals("rundistance", StringComparison.OrdinalIgnoreCase))
                {
                    float value;

                    if (float.TryParse(attribute.Value, out value))
                    {
                        RunDistance = value;
                    }

                    continue;
                }
            }

            //Safe zone.
            XElement safezoneelement = element.Element("safezone");

            if (safezoneelement != null)
            {
                int x = 0;
                int y = 0;

                foreach (var attribute in safezoneelement.Attributes())
                {
                    if (attribute.Name.LocalName.Equals("x", StringComparison.OrdinalIgnoreCase))
                    {
                        int.TryParse(attribute.Value, out x);

                        continue;
                    }

                    if (attribute.Name.LocalName.Equals("y", StringComparison.OrdinalIgnoreCase))
                    {
                        int.TryParse(attribute.Value, out y);

                        continue;
                    }
                }

                Safezone = new Vector(x, y);
            }

            #endregion
        }
    }
}
