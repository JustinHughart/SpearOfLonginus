using System;
using System.Text;
using SpearOfLonginus.Animations;
using SpearOfLonginus.Input;
using SpearOfLonginus.Maps;

namespace SpearOfLonginus.Entities
{
    public enum PlayerType
    {
        NPC, 
        Player1,
        Player2,
        Player3,
        Player4,
        World, 
    }

    public enum FacingState
    {
        North,
        Northeast, 
        East,
        Southeast,
        South,
        Southwest, 
        West,
        Northwest,
    }

    public enum FacingStyle
    {
        Static,
        FourWay,
        EightWay,
    }

    public enum MovingState
    {
        Standing,
        Walking,
        Running,
    }

    public class Entity : IComparable 
    {
        #region Variables

        public PlayerType PlayerType { get; protected set; }
        public Map Map;

        protected Animation CurrentAnimation;
        protected AnimationCache AnimationCache;

        public Vector Position;
        public float WalkSpeed;
        public float RunSpeed;

        public FacingStyle FacingStyle;
        public FacingState Facing;
        public MovingState MovingState;

        public Rectangle WorldHitbox { get; protected set; }

        #endregion

        #region Constructors

        public Entity()
        {

        }

        #endregion

        #region Functions

        public virtual void Update(InputPacket packet, float deltatime)
        {
            CurrentAnimation.Update(deltatime);
            
            if (packet != null)
            {
                Move(packet, deltatime);
                CheckAnimation();
            }
        }

        public virtual InputPacket GetAIPacket()
        {
            //AI is unsupported for now!
            return null;
        }

        protected virtual void Move(InputPacket packet, float deltatime)
        {
            //First we'll get how fast we should go and tell what state we're in...
            float speed = 0;

            if (packet.Run == PressState.Down)
            {
                speed = RunSpeed;
                MovingState = MovingState.Running;
            }
            else
            {
                speed = WalkSpeed;
                MovingState = MovingState.Walking;
            }

            //And multiply it by deltatime.
            speed *= deltatime;

            //Now we'll find what direction we're going...
            Vector direction = Vector.Zero;

            if (packet.Up == PressState.Down)
            {
                direction.Y--;
            }

            if (packet.Down == PressState.Down)
            {
                direction.Y++;
            }

            if (packet.Left == PressState.Down)
            {
                direction.X--;
            }

            if (packet.Right == PressState.Down)
            {
                direction.X++;
            }

            //Check if you're actually moving...
            if (!direction.Equals(Vector.Zero))
            {
                //First we'll report to the entity what direction we're moving 
                switch (FacingStyle)
                {
                    case FacingStyle.Static:
                        //We don't do anything for static!
                        break;
                    case FacingStyle.FourWay:
                        if (direction.Y < 0) //W
                        {
                            Facing = FacingState.West;
                        }
                        else if (direction.Y > 0) //E
                        {
                            Facing = FacingState.East;
                        }

                        if (direction.Y < 0) //N
                        {
                            Facing = FacingState.North;
                        }
                        else if (direction.Y > 0) //S
                        {
                            Facing = FacingState.South;
                        }

                        break;
                    case FacingStyle.EightWay:
                        if (direction.Y.Equals(-1) && direction.X.Equals(0)) //N
                        {
                            Facing = FacingState.North;
                            break;
                        }

                        if (direction.Y.Equals(-1) && direction.X.Equals(-1)) //NW
                        {
                            Facing = FacingState.Northwest;
                            break;
                        }

                        if (direction.Y.Equals(-1) && direction.X.Equals(1)) //NE
                        {
                            Facing = FacingState.Northeast;
                            break;
                        }

                        if (direction.Y.Equals(0) && direction.X.Equals(-1)) //W
                        {
                            Facing = FacingState.West;
                            break;
                        }

                        if (direction.Y.Equals(0) && direction.X.Equals(1)) //E
                        {
                            Facing = FacingState.East;
                            break;
                        }

                        if (direction.Y.Equals(1) && direction.X.Equals(0)) //S
                        {
                            Facing = FacingState.South;
                            break;
                        }

                        if (direction.Y.Equals(1) && direction.X.Equals(-1)) //SW
                        {
                            Facing = FacingState.Southwest;
                            break;
                        }

                        if (direction.Y.Equals(1) && direction.X.Equals(1)) //SE
                        {
                            Facing = FacingState.Southeast;
                            break;
                        }
                        
                        //If it got to here, then it must not have changed direction.
                        break;
                }




                //Normalize it so that diagonals aren't faster.
                direction.Normalize();
                
                //Rudimentary movement for now. We'll replace this with proper collision checking later.
                Position += direction * speed;
            }
            else
            {
                //If you aren't moving, you're standing!
                MovingState = MovingState.Standing;
            }
        }

        protected virtual void CheckAnimation()
        {
            //We're going to use a string builder for this.
            StringBuilder idbuilder = new StringBuilder();

            //First we want to start with the raw state...
            switch (MovingState)
            {
                case MovingState.Standing:
                    idbuilder.Append("stand");
                    break;
                case MovingState.Walking:
                    idbuilder.Append("walk");
                    break;
                case MovingState.Running:
                    idbuilder.Append("run");
                    break;
            }

            idbuilder.Append("_");

            //Then we'll add direction...
            switch (Facing)
            {
                case FacingState.North:
                    idbuilder.Append("n");
                    break;
                case FacingState.Northeast:
                    idbuilder.Append("ne");
                    break;
                case FacingState.East:
                    idbuilder.Append("e");
                    break;
                case FacingState.Southeast:
                    idbuilder.Append("se");
                    break;
                case FacingState.South:
                    idbuilder.Append("s");
                    break;
                case FacingState.Southwest:
                    idbuilder.Append("sw");
                    break;
                case FacingState.West:
                    idbuilder.Append("w");
                    break;
                case FacingState.Northwest:
                    idbuilder.Append("nw");
                    break;
            }

            //Now we get the string...
            string expectedid = idbuilder.ToString();

            //If the expected ID doesn't match the current animation's ID...
            if (CurrentAnimation.ID != expectedid)
            {
                //We need to change it!
                CurrentAnimation = AnimationCache.GetAnimation(expectedid);
            }
        }

        public int CompareTo(object obj)
        {
            var other = obj as Entity;

            if (other == null)
            {
                return 0;
            }

            return Position.Y.CompareTo(other.Position.Y);
        }

        #endregion
    }
}
