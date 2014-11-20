using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Xml.Linq;
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
    public class Entity : IComparable, IXmlLoadable
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
        /// The persistance of the entity. Setting this to true will force the map the entity exists in to be persistant and active for the duration of the time the entity is within it.
        /// </summary>
        public bool Persistent;
        /// <summary>
        /// Whether or not the entity can use doors. If it can't, doors are treating like solid ground.
        /// </summary>
        public bool CanUseDoors;
        /// <summary>
        /// The components used for updating the entity's actions.
        /// </summary>
        public Dictionary<string, Component> Components;
        /// <summary>
        /// The logical components used for heling the AI create input packets. 
        /// </summary>
        public Dictionary<string, Logic> Logics;
        /// <summary>
        /// A list of tags used for intercomponent talking.
        /// </summary>
        public List<String> Tags;
        /// <summary>
        /// The list value used for removing entity flickering when they share Y values.
        /// </summary>
        public float ListValue;
        /// <summary>
        /// Gets the layer of the entity.
        /// </summary>
        /// <value>
        /// The layer.
        /// </value>
        public float Layer { get { return Position.Y + ListValue; } }
        /// <summary>
        /// The components to add to the list at the start of the next frame.
        /// </summary>
        public Dictionary<string, Component> ComponentsToAdd;
        /// <summary>
        /// The logics to add to the list at the start of the next frame.
        /// </summary>
        public Dictionary<string, Logic> LogicsToAdd;  

        #endregion

        #region Constructors

        public Entity()
        {
            Components = new Dictionary<string, Component>();
            Logics = new Dictionary<string, Logic>();
            Tags = new List<string>();
            ComponentsToAdd = new Dictionary<string, Component>();
            LogicsToAdd = new Dictionary<string, Logic>();
            AnimationCache = new AnimationCache();
        }

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
            ComponentsToAdd = new Dictionary<string, Component>();
            LogicsToAdd = new Dictionary<string, Logic>();
            
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
            //Update animation.
            if (CurrentAnimation == null)
            {
                HandleDerivedAnimation();
            }

            CurrentAnimation.Update(deltatime);
            
            //Add new components and logics.
            foreach (var component in ComponentsToAdd)
            {
                AddComponent(component.Key, component.Value);
            }

            foreach (var logic in LogicsToAdd)
            {
                AddLogic(logic.Key, logic.Value);
            }

            //Handle logic.
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

            //Prune dead components.
            List<string> componentstodelete = new List<string>();
            List<string> logicstodelete = new List<string>();

            foreach (var component in Components)
            {
                if (component.Value.Dead)
                {
                    componentstodelete.Add(component.Key);
                }
            }

            foreach (var logic in Logics)
            {
                if (logic.Value.Dead)
                {
                    logicstodelete.Add(logic.Key);
                }
            }

            foreach (var component in componentstodelete)
            {
                Components.Remove(component);
            }

            foreach (var logic in logicstodelete)
            {
                Logics.Remove(logic);
            }

            //Update the hitbox position.
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
            AddComponent(component.GetType().Name, component);
        }

        /// <summary>
        /// Adds the component.
        /// </summary>
        /// <param name="id">The component's identifier.</param>
        /// <param name="component">The component to add.</param>
        public void AddComponent(string id, Component component)
        {
            Components.Add(id, component);
            component.Owner = this;
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
            AddLogic(logic.GetType().Name, logic);
        }

        /// <summary>
        /// Adds the logic to the entity's list of objects.
        /// </summary>
        /// <param name="id">The logic's identifier.</param>
        /// <param name="logic">The logic to add.</param>
        public void AddLogic(string id, Logic logic)
        {
            Logics.Add(id, logic);
            logic.Owner = this;
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
            foreach (var entity in Map.Entities.GetEntityList())
            {
                if (entity != this && entity.Solid)
                {
                    hitboxes.Add(entity.Hitbox);
                }
            }

            //If it can't use doors, treat them as solid, to keep them inside the map.
            if (!CanUseDoors)
            {
                foreach (var door in Map.Doors)
                {
                    hitboxes.Add(door.Hitbox);
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

            return Layer.CompareTo(other.Layer);
        }

        /// <summary>
        /// Uses XML to initialize the object.
        /// </summary>
        /// <param name="element">The element used for loading..</param>
        public virtual void LoadFromXml(XElement element)
        {
            //Initial checks
            if (element == null)
            {
                throw new Exception("Root is null.");
            }

            if (!element.Name.LocalName.Equals("entity", StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception("Element is not an entity element.");
            }

            //Attributes
            foreach (var attribute in element.Attributes())
            {
                if (attribute.Name.LocalName.Equals("id"))
                {
                    ID = attribute.Value;
                    continue;
                }

                if (attribute.Name.LocalName.Equals("inputtype"))
                {
                    InputType.TryParse(attribute.Value, out InputType);
                    continue;
                }

                if (attribute.Name.LocalName.Equals("facingstyle"))
                {
                    FacingStyle.TryParse(attribute.Value, out FacingStyle);
                    continue;
                }

                if (attribute.Name.LocalName.Equals("facing"))
                {
                    FacingState.TryParse(attribute.Value, out Facing);
                    continue;
                }

                if (attribute.Name.LocalName.Equals("persistent"))
                {
                    bool.TryParse(attribute.Value, out Persistent);
                    continue;
                }

                if (attribute.Name.LocalName.Equals("canusedoors"))
                {
                    bool.TryParse(attribute.Value, out CanUseDoors);
                    continue;
                }
            }

            //Hitbox
            XElement hitboxelement = element.Element("hitbox");

            if (hitboxelement != null)
            {
                int x = 0;
                int y = 0;
                int w = 0;
                int h = 0;

                foreach (var attribute in hitboxelement.Attributes())
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

                    if (attribute.Name.LocalName.Equals("w", StringComparison.OrdinalIgnoreCase))
                    {
                        int.TryParse(attribute.Value, out w);

                        continue;
                    }

                    if (attribute.Name.LocalName.Equals("h", StringComparison.OrdinalIgnoreCase))
                    {
                        int.TryParse(attribute.Value, out h);

                        continue;
                    }
                }

                Hitbox = new Rectangle(x, y, w, h);
            }

            //Components
            LoadComponents(element.Element("components"));

            //Logics
            LoadLogics(element.Element("logics"));

            //Animations
            LoadAnimations(element.Element("animations"));

            //Custom
            LoadCustomXml(element.Element("custom"));
        }

        /// <summary>
        /// Loads the components.
        /// </summary>
        /// <param name="element">The element to load from.</param>
        protected virtual void LoadComponents(XElement element)
        {
            foreach (var componentelement in element.Elements())
            {
                Component component = XmlLoader.CreateObject(componentelement.Name.LocalName, componentelement) as Component;

                if (component != null)
                {
                    AddComponent(component);
                }
            }
        }

        /// <summary>
        /// Loads the logics.
        /// </summary>
        /// <param name="element">The element to load from.</param>
        protected virtual void LoadLogics(XElement element)
        {
            foreach (var logicelement in element.Elements())
            {
                Logic logic = XmlLoader.CreateObject(logicelement.Name.LocalName, logicelement) as Logic;

                if (logic != null)
                {
                    AddLogic(logic);
                }
            }
        }

        /// <summary>
        /// Loads the animations.
        /// </summary>
        /// <param name="element">The element to load from.</param>
        protected virtual void LoadAnimations(XElement element)
        {
            foreach (var animationelement in element.Elements())
            {
                Animation animation = XmlLoader.CreateObject(animationelement.Name.LocalName, animationelement) as Animation;

                if (animation != null)
                {
                    AnimationCache.AddAnimation(animation);
                }
            }
        }

        /// <summary>
        /// Loads the custom XML.
        /// </summary>
        /// <param name="element">The element to load from.</param>
        protected virtual void LoadCustomXml(XElement element)
        {
            //Nothing here by default, because obviously it wouldn't be custom!
        }

        #endregion

        #region Static Functions

        /// <summary>
        /// Loads from file.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <returns></returns>
        public static Entity LoadFromFile(string path)
        {
            while (path.EndsWith(" "))
            {
                path.Remove(path.Length - 2);
            }

            XDocument doc = null;

            //Load the doc.

            if (!File.Exists(path))
            {
                return null;
            }

            if (path.Contains(".sle"))
            {
                doc = XDocument.Load(path);
            }
            else if (path.Contains(".gze"))
            {
                using (var filestream = File.OpenRead(path))
                {
                    using (var gzipstream = new GZipStream(filestream, CompressionMode.Decompress))
                    {
                        doc = XDocument.Load(gzipstream);
                    }
                }
            }
            else
            {
                throw new ArgumentException("File type not supported.");
            }

            Entity entity = new Entity();
            entity.LoadFromXml(doc.Root);
            return entity;
        }

        #endregion
    }
}
