namespace SpearOfLonginus.UI
{
    /// <summary>
    /// An item for a UI.
    /// </summary>
    public abstract class UIItem
    {
        /// <summary>
        /// Updates this instance.
        /// </summary>
        /// <param name="deltatime">The time since the last frame.</param>
        public virtual void Update(float deltatime)
        {

        }
    }
}
