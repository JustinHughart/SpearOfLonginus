namespace SpearOfLonginus.Maps
{
    /// <summary>
    /// A transition between maps.
    /// </summary>
    public class Transition
    {
        /// <summary>
        /// Whether or not the transition is complete.
        /// </summary>
        public bool Complete;
        /// <summary>
        /// The new map.
        /// </summary>
        public Map NewMap;
        /// <summary>
        /// The old map.
        /// </summary>
        public Map OldMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="Transition"/> class.
        /// </summary>
        public Transition(Map newmap, Map oldmap)
        {
            Complete = false;
            NewMap = newmap;
            OldMap = oldmap;
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public virtual void Update(float deltatime)
        {
            Complete = true;
        }
    }
}
