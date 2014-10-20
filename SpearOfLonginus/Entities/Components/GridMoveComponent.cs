﻿using SpearOfLonginus.Input;

namespace SpearOfLonginus.Entities.Components
{
    /// <summary>
    /// A movement for moving along a user defined grid.
    /// </summary>
    public class GridMoveComponent : Component
    {
        #region Variables

        /// <summary>
        /// The speed at which the entity walks.
        /// </summary>
        public float WalkSpeed;
        /// <summary>
        /// The speed at which the entity runs.
        /// </summary>
        public float RunSpeed;
        /// <summary>
        /// The size of the grid the entity walks on.
        /// </summary>
        public Vector GridSize;
        /// <summary>
        /// The direction the entity is moving.
        /// </summary>
        protected Vector Direction;
        /// <summary>
        /// The speed at which the entity is moving.
        /// </summary>
        protected float Speed;
        /// <summary>
        /// The target coordinate to stop at.
        /// </summary>
        protected Vector Target;
        /// <summary>
        /// Whether or not the entity is moving.
        /// </summary>
        protected bool Moving;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FreeMoveComponent" /> class.
        /// </summary>
        /// <param name="owner">The entity who owns the component.</param>
        /// <param name="walkspeed">The speed at which the entity walks.</param>
        /// <param name="runspeed">The speed at which the entity walks.</param>
        /// <param name="gridsize">The size of the grid the entity walks on.</param>
        public GridMoveComponent(Entity owner, float walkspeed, float runspeed, Vector gridsize)
            : base(owner)
        {
            WalkSpeed = walkspeed;
            RunSpeed = runspeed;
            GridSize = gridsize;
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
            if (!Moving)
            {
                StartMove(packet);   
            }

            Owner.Velocity += Direction * Speed * deltatime;

            CheckStop();
        }

        /// <summary>
        /// Starts the entity's move cycle.
        /// </summary>
        /// <param name="packet">The packet.</param>
        private void StartMove(InputPacket packet)
        {
            //First we'll get how fast we should go and tell what state we're in...
            float speed = 0;

            if (packet.Run == PressState.Down)
            {
                speed = RunSpeed;
                Owner.MovingState = MovingState.Running;
            }
            else
            {
                speed = WalkSpeed;
                Owner.MovingState = MovingState.Walking;
            }

            //Now we'll find what direction we're going...
            Vector direction = Vector.Zero;

            if (packet.Up == PressState.Down)
            {
                direction = new Vector(0,-1);
            }

            if (packet.Down == PressState.Down)
            {
                direction = new Vector(0, 1);
            }

            if (packet.Left == PressState.Down)
            {
                direction = new Vector(-1,0);
            }

            if (packet.Right == PressState.Down)
            {
                direction = new Vector(1,0);
            }

            //Check if you're actually moving...
            if (!direction.Equals(Vector.Zero))
            {
                //First we'll report to the entity what direction we're moving 
                switch (Owner.FacingStyle)
                {
                    case FacingStyle.Static:
                        //We don't do anything for static!
                        break;
                    case FacingStyle.FourWay:
                        if (direction.X < 0) //W
                        {
                            Owner.Facing = FacingState.West;
                        }
                        else if (direction.X > 0) //E
                        {
                            Owner.Facing = FacingState.East;
                        }

                        if (direction.Y < 0) //N
                        {
                            Owner.Facing = FacingState.North;
                        }
                        else if (direction.Y > 0) //S
                        {
                            Owner.Facing = FacingState.South;
                        }

                        break;
                    case FacingStyle.EightWay:
                        if (direction.Y.Equals(-1) && direction.X.Equals(0)) //N
                        {
                            Owner.Facing = FacingState.North;
                            break;
                        }

                        if (direction.Y.Equals(-1) && direction.X.Equals(-1)) //NW
                        {
                            Owner.Facing = FacingState.Northwest;
                            break;
                        }

                        if (direction.Y.Equals(-1) && direction.X.Equals(1)) //NE
                        {
                            Owner.Facing = FacingState.Northeast;
                            break;
                        }

                        if (direction.Y.Equals(0) && direction.X.Equals(-1)) //W
                        {
                            Owner.Facing = FacingState.West;
                            break;
                        }

                        if (direction.Y.Equals(0) && direction.X.Equals(1)) //E
                        {
                            Owner.Facing = FacingState.East;
                            break;
                        }

                        if (direction.Y.Equals(1) && direction.X.Equals(0)) //S
                        {
                            Owner.Facing = FacingState.South;
                            break;
                        }

                        if (direction.Y.Equals(1) && direction.X.Equals(-1)) //SW
                        {
                            Owner.Facing = FacingState.Southwest;
                            break;
                        }

                        if (direction.Y.Equals(1) && direction.X.Equals(1)) //SE
                        {
                            Owner.Facing = FacingState.Southeast;
                            break;
                        }

                        //If it got to here, then it must not have changed direction.
                        break;
                }

                //Normalize it so that diagonals aren't faster.
                direction.Normalize();

                //Set the course of action.
                Direction = direction;
                Speed = speed;
                Moving = true;

                Target = Owner.Position / GridSize;
                Target += Direction;
                Target *= GridSize;
            }
            else
            {
                //If you aren't moving, you're standing!
                Owner.MovingState = MovingState.Standing;
            }
        }

        /// <summary>
        /// Checks if the entity should stop.
        /// </summary>
        private void CheckStop()
        {
            if ((Owner.Position - Target).Length() < Speed)
            {
                Owner.Position = Target;
                Direction = Vector.Zero;
                Speed = 0;
                Moving = false;
            }
        }

        #endregion
    }
}