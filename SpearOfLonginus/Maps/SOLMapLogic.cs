namespace SpearOfLonginus.Maps
{
    /// <summary>
    /// An abstract class for level logic.
    /// </summary>
    public abstract class SOLMapLogic
    {
        /// <summary>
        /// The owner of the logic component.
        /// </summary>
        protected SOLMap Owner;

        /// <summary>
        /// Initializes a new instance of the <see cref="SOLMapLogic"/> class.
        /// </summary>
        /// <param name="owner">The owner of the logic component.</param>
        public SOLMapLogic(SOLMap owner)
        {
            Owner = owner;
        }

        /// <summary>
        /// Updates the logic component.
        /// </summary>
        public abstract void Update();
    }
}
