using System;
using System.Collections.Generic;
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

        public PlayerType PlayerType;
        public Map Map;

        protected Animation CurrentAnimation;
        protected AnimationCache AnimationCache;

        public Vector Position;
        public Vector Velocity;

        public FacingStyle FacingStyle;
        public FacingState Facing;
        public MovingState MovingState;

        public Rectangle WorldHitbox;

        protected Dictionary<string, Component> Components;
        protected Dictionary<string, Logic> Logics;


        protected List<String> Tags; 
        

        #endregion

        #region Constructors

        public Entity(AnimationCache animationcache) : this(animationcache, new Rectangle(0,0,1,1))
        {

        }

        public Entity(AnimationCache animationcache, Rectangle worldhitbox)
        {
            AnimationCache = animationcache;
            WorldHitbox = worldhitbox;

            Components = new Dictionary<string, Component>();
            Logics = new Dictionary<string, Logic>();

            CheckAnimation();
        }

        #endregion

        #region Functions

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

                Position += Velocity;
                Velocity = Vector.Zero;
                
                CheckAnimation();
            }

            WorldHitbox.Location = Position;
        }

        public virtual InputPacket GetAIPacket()
        {
            InputPacket packet = new InputPacket();

            foreach (var logic in Logics)
            {
                logic.Value.GetInput(packet);
            }
            
            return packet;
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

        public void AddComponent(Component component)
        {
            Components.Add(component.GetType().Name, component);
        }

        public void RemoveComponent(String id)
        {
            Components.Remove(id);
        }

        public Component GetComponent(String id)
        {
            if (Components.ContainsKey(id))
            {
                return Components[id];
            }

            return null;
        }

        public void AddLogic(Logic logic)
        {
            Logics.Add(logic.GetType().Name, logic);
        }

        public void RemoveLogic(String id)
        {
            Logics.Remove(id);
        }

        public Logic GetLogic(String id)
        {
            if (Logics.ContainsKey(id))
            {
                return Logics[id];
            }

            return null;
        }

        public void AddTag(string tag)
        {
            if (!Tags.Contains(tag))
            {
                Tags.Add(tag);
            }
        }

        public void RemoveTag(string tag)
        {
            Tags.Remove(tag);
        }

        public bool TagExists(string tag)
        {
            return Tags.Contains(tag);
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
