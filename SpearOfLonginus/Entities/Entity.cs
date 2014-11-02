using System;
using System.Collections.Generic;
using System.Text;
using SpearOfLonginus.Animations;
using SpearOfLonginus.Input;
using SpearOfLonginus.Maps;

namespace SpearOfLonginus.Entities
{
    /// <summary>
    /// An enemy for determining who is in control of the entity.
    /// </summary>
    public enum InputType
    {
        /// <summary>
        /// The entity is controlled by its AI components.
        /// </summary>
        NPC,
        /// <summary>
        /// The entity is controlled by Player 1.
        /// </summary>
        Player1,
        /// <summary>
        /// The entity is controlled by Player 2.
        /// </summary>
        Player2,
        /// <summary>
        /// The entity is controlled by Player 3.
        /// </summary>
        Player3,
        /// <summary>
        /// The entity is controlled by Player 4.
        /// </summary>
        Player4,
        /// <summary>
        /// The entity does no logical updating other than updating its animation.
        /// </summary>
        World, 
    }

    /// <summary>
    /// Shows which way the entity is facing.
    /// </summary>
    public enum FacingState
    {
        /// <summary>
        /// Denotes north.
        /// </summary>
        North,
        /// <summary>
        /// Denotes northeast.
        /// </summary>
        Northeast,
        /// <summary>
        /// Denotes east.
        /// </summary>
        East,
        /// <summary>
        /// Denotes southeast.
        /// </summary>
        Southeast,
        /// <summary>
        /// Denotes south.
        /// </summary>
        South,
        /// <summary>
        /// Denotes southwest.
        /// </summary>
        Southwest,
        /// <summary>
        /// Denotes west.
        /// </summary>
        West,
        /// <summary>
        /// Denotes northwest.
        /// </summary>
        Northwest,
    }

    /// <summary>
    /// How the animation changes based on the entity's state.
    /// </summary>
    public enum FacingStyle
    {
        /// <summary>
        /// Animation does not change at all.
        /// </summary>
        Static,
        /// <summary>
        /// Support for four facing directions.
        /// </summary>
        FourWay,
        /// <summary>
        /// Support for eight facing directions.
        /// </summary>
        EightWay,
    }

    /// <summary>
    /// The entity's state for how it is moving.
    /// </summary>
    public enum MovingState
    {
        /// <summary>
        /// Denotes standing still.
        /// </summary>
        Standing,
        /// <summary>
        /// Denotes walking.
        /// </summary>
        Walking,
        /// <summary>
        /// Denotes running.
        /// </summary>
        Running,
    }

    /// <summary>
    /// A component-based entity for Spear Of Longinus.
    /// </summary>
    public class Entity : IComparable 
    {
        #region Variables
        /// <summary>
        /// The entity's identifier.
        /// </summary>
        public string ID;
        /// <summary>
        /// Shows how the entity gets its input.
        /// </summary>
        public InputType InputType;
        /// <summary>
        /// The map the entity belongs to.
        /// </summary>
        public Map Map;
        /// <summary>
        /// The entity's current animation.
        /// </summary>
        public Animation CurrentAnimation;
        /// <summary>
        /// The cache holding all the entities animations.
        /// </summary>
        public AnimationCache AnimationCache;
        /// <summary>
        /// Whether or no the animation is overridden.
        /// </summary>
        public bool IsAnimationOverridden;
        /// <summary>
        ///  The component that is in control.
        /// </summary>
        public Component ComponentInControl;
        /// <summary>
        /// The position of the entity.
        /// </summary>
        public Vector Position;
        /// <summary>
        /// The entity's velocity.
        /// </summary>
        public Vector Velocity;
        /// <summary>
        /// The style scheme for facing.
        /// </summary>
        public FacingStyle FacingStyle;
        /// <summary>
        /// The direction the entity is facing.
        /// </summary>
        public FacingState Facing;
        /// <summary>
        /// How the entity is currently moving.
        /// </summary>
        public MovingState MovingState;
        /// <summary>
        /// The hitbox that causes interactions with the world.
        /// </summary>
        public Rectangle Hitbox;
        /// <summary>
        /// Whether or not you may pass through this entity.
        /// </summary>
        public bool Solid;
        /// <summary>
        /// The components used for updating the entity's actions.
        /// </summary>
        protected Dictionary<string, Component> Components;
        /// <summary>
        /// The logical components used for heling the AI create input packets. 
        /// </summary>
        protected Dictionary<string, Logic> Logics;
        /// <summary>
        /// A list of tags used for intercomponent talking.
        /// </summary>
        protected List<String> Tags; 

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class.
        /// </summary>
        /// <param name="animationcache">The cache holding all the entities animations.</param>
        public Entity(AnimationCache animationcache) : this(animationcache, new Rectangle(0,0,1,1))
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class.
        /// </summary>
        /// <param name="animationcache">The cache holding all the entities animations.</param>
        /// <param name="hitbox">The hitbox that causes interactions with the world.</param>
        public Entity(AnimationCache animationcache, Rectangle hitbox)
        {
            AnimationCache = animationcache;
            Hitbox = hitbox;

            Components = new Dictionary<string, Component>();
            Logics = new Dictionary<string, Logic>();
            Tags = new List<string>();

            HandleDerivedAnimation();
        }

        #endregion

        #region Functions

        /// <summary>
        /// Updates entity using an update packet.
        /// </summary>
        /// <param name="packet">The packet of input data.</param>
        /// <param name="deltatime">The time that has passed since last update.</param>
        public virtual void Update(InputPacket packet, float deltatime)
        {
            CurrentAnimation.Update(deltatime);
            
            if (packet != null)
            {
                foreach (var component in Components)
                {
                    component.Value.Update(packet, deltatime);
                }

                foreach (var logic in Logics)
                {
                    logic.Value.Update(packet, deltatime);
                }

                HandleVelocity();

                if (!IsAnimationOverridden)
                {
                    HandleDerivedAnimation();
                }
            }

            UpdateHitbox();
        }

        /// <summary>
        /// Updates the hitbox location.
        /// </summary>
        protected virtual void UpdateHitbox()
        {
            Hitbox.Location = Position;
        }

        /// <summary>
        /// Gets the AI input packet based on AI components.
        /// </summary>
        /// <returns></returns>
        public virtual InputPacket GetAIPacket()
        {
            InputPacket packet = new InputPacket();

            foreach (var logic in Logics)
            {
                logic.Value.GetInput(packet);
            }
            
            return packet;
        }

        /// <summary>
        /// Overrides the entity's animation.
        /// </summary>
        /// <param name="component">The component that's to be in charge of the animation..</param>
        /// <param name="id">The identifier of the animation to be pulled from the cache.</param>
        public virtual void OverrideAnimation(Component component, string id)
        {
            OverrideAnimation(component, AnimationCache.GetAnimation(id));
        }

        /// <summary>
        /// Overrides the entity's animation.
        /// </summary>
        /// <param name="component">The component that's to be in charge of the animation.</param>
        /// <param name="animation">The animation to use as the override.</param>
        public virtual void OverrideAnimation(Component component, Animation animation)
        {
            IsAnimationOverridden = true;
            ComponentInControl = component;
            CurrentAnimation = animation;
        }

        /// <summary>
        /// Clears the animation override.
        /// </summary>
        public virtual void ClearAnimationOverride()
        {
            IsAnimationOverridden = false;
            ComponentInControl = null;
            HandleDerivedAnimation();
        }

        /// <summary>
        /// Handles the animation derived from state.
        /// </summary>
        protected virtual void HandleDerivedAnimation()
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

            //If the current animation doesn't exist...
            if (CurrentAnimation == null)
            {
                //We need to change it!
                CurrentAnimation = AnimationCache.GetAnimation(expectedid).Clone();
            }

            //If the expected ID doesn't match the current animation's ID...
            if (CurrentAnimation.ID != expectedid)
            {
                //We need to change it!
                CurrentAnimation = AnimationCache.GetAnimation(expectedid).Clone(CurrentAnimation.CurrentFrame, CurrentAnimation.TimingIndex);
            }
        }

        /// <summary>
        /// Adds the component to the entity's list of components.
        /// </summary>
        /// <param name="component">The component to add.</param>
        public void AddComponent(Component component)
        {
            Components.Add(component.GetType().Name, component);
        }

        /// <summary>
        /// Removes the component from the entity's list of components..
        /// </summary>
        /// <param name="id">The component's identifier. </param>
        public void RemoveComponent(String id)
        {
            Components.Remove(id);
        }

        /// <summary>
        /// Gets the component from the entity's list of components.
        /// </summary>
        /// <param name="id">The component's identifier.</param>
        /// <returns></returns>
        public Component GetComponent(String id)
        {
            if (Components.ContainsKey(id))
            {
                return Components[id];
            }

            return null;
        }

        /// <summary>
        /// Adds the logic to the entity's list of objects.
        /// </summary>
        /// <param name="logic">The logic to add.</param>
        public void AddLogic(Logic logic)
        {
            Logics.Add(logic.GetType().Name, logic);
        }

        /// <summary>
        /// Removes the logic from the entity's list of objects..
        /// </summary>
        /// <param name="id">The identifier of the logic to remove.</param>
        public void RemoveLogic(String id)
        {
            Logics.Remove(id);
        }

        /// <summary>
        /// Gets the logic from the entity's list of AI components.
        /// </summary>
        /// <param name="id">The identifier of the logic to receive.</param>
        /// <returns></returns>
        public Logic GetLogic(String id)
        {
            if (Logics.ContainsKey(id))
            {
                return Logics[id];
            }

            return null;
        }

        /// <summary>
        /// Adds the tag to the entity's list of tags 
        /// </summary>
        /// <param name="tag">The tag to add.</param>
        public void AddTag(string tag)
        {
            if (!Tags.Contains(tag))
            {
                Tags.Add(tag);
            }
        }

        /// <summary>
        /// Removes the tag from the entity's list of tags.
        /// </summary>
        /// <param name="tag">The tag to remove.</param>
        public void RemoveTag(string tag)
        {
            Tags.Remove(tag);
        }

        /// <summary>
        /// Checks if a tag exists
        /// </summary>
        /// <param name="tag">The tag to check.</param>
        /// <returns></returns>
        public bool TagExists(string tag)
        {
            return Tags.Contains(tag);
        }

        /// <summary>
        /// Handles the velocity of the entity..
        /// </summary>
        public void HandleVelocity()
        {
            float steprate = 1; //I need to think of a better algorithm than this. This will hold for now. It's really embarrasing though.

            bool end = false;

            //Check X.

            while (!end)
            {
                if (Velocity.X.Equals(0))
                {
                    break;
                }

                if (Velocity.X > 0)
                {
                    float update = Math.Min(Velocity.X, steprate);
                    Velocity.X -= update;
                    Position.X += update;
                    UpdateHitbox();

                    if (CheckCollision())
                    {
                        Position.X -= update;
                        break; 
                    }

                    continue; 
                }
                else if (Velocity.X < 0)
                {
                    float update = Math.Max(Velocity.X, -steprate);
                    Velocity.X -= update;
                    Position.X += update;
                    UpdateHitbox();

                    if (CheckCollision())
                    {
                        Position.X -= update;
                        break;
                    }

                    continue;
                }

                end = true; 
            }

            end = false;

            //Check Y.

            while (!end)
            {
                if (Velocity.Y > 0)
                {
                    float update = Math.Min(Velocity.Y, steprate);
                    Velocity.Y -= update;
                    Position.Y += update;
                    UpdateHitbox();

                    if (CheckCollision())
                    {
                        Position.Y -= update;
                        break;
                    }

                    continue;
                }
                else if (Velocity.Y < 0)
                {
                    float update = Math.Max(Velocity.Y, -steprate);
                    Velocity.Y -= update;
                    Position.Y += update;
                    UpdateHitbox();

                    if (CheckCollision())
                    {
                        Position.Y -= update;
                        break;
                    }

                    continue;
                }

                end = true;
            }

            Velocity = Vector.Zero;
        }

        /// <summary>
        /// Checks the collision of the entity.
        /// </summary>
        /// <returns></returns>
        public bool CheckCollision()
        {
            List<Rectangle> hitboxes = new List<Rectangle>();

            int xstart = Hitbox.X / (int)Map.TileSize.X;
            int ystart = Hitbox.Y / (int)Map.TileSize.Y;
            int xend = Hitbox.Width / (int)Map.TileSize.X;
            int yend = Hitbox.Height / (int)Map.TileSize.Y;

            //I'm not sure why the following two lines of code make it work but apparantly... It does. Bah, this is terrible.
            xend += xstart + 2;
            yend += ystart + 2;
            
            Rectangle empty = new Rectangle();

            for (int y = ystart; y < yend; y++)
            {
                for (int x = xstart; x < xend; x++)
                {
                    var hitbox = Map.GetHitbox(new Vector(x, y));

                    if (!hitbox.Equals(empty))
                    {
                        hitboxes.Add(hitbox);
                    }
                }
            }

            //Check for entities. Can use more optimizations.
            foreach (var entity in Map.Entities.GetEntityListCopy())
            {
                if (entity != this && entity.Solid)
                {
                    hitboxes.Add(entity.Hitbox);
                }
            }

            //Actually check all the hitboxes.

            foreach (var hitbox in hitboxes)
            {
                if (Hitbox.Intersects(hitbox))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object. Sorts by Y value;
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance is less than <paramref name="obj" />. Zero This instance is equal to <paramref name="obj" />. Greater than zero This instance is greater than <paramref name="obj" />.
        /// </returns>
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
