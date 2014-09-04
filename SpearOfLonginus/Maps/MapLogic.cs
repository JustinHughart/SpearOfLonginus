namespace SpearOfLonginus.Maps
{
    /// <summary>
    /// An abstract class for level logic.
    /// </summary>
    public abstract class MapLogic
    {
        #region Variables

        /// <summary>
        /// The owner of the logic component.
        /// </summary>
        protected Map Owner;

        #endregion 

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MapLogic"/> class.
        /// </summary>
        /// <param name="owner">The owner of the logic component.</param>
        public MapLogic(Map owner)
        {
            Owner = owner;
        }

        #endregion

        #region Functions

        /// <summary>
        /// Updates the logic component.
        /// </summary>
        public abstract void Update();

        #endregion
    }
}
