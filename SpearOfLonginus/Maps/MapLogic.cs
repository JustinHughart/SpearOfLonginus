namespace SpearOfLonginus.Maps
{
    /// <summary>
    /// An abstract class for level logic.
    /// </summary>
    public abstract class MapLogic
    {
        /// <summary>
        /// The owner of the logic component.
        /// </summary>
        protected SOLMap Owner;

        /// <summary>
        /// Initializes a new instance of the <see cref="MapLogic"/> class.
        /// </summary>
        /// <param name="owner">The owner of the logic component.</param>
        public MapLogic(SOLMap owner)
        {
            Owner = owner;
        }

        /// <summary>
        /// Updates the logic component.
        /// </summary>
        public abstract void Update();
    }
}
