using SpearOfLonginus.Animation;

namespace SpearOfLonginus.Maps
{
    /// <summary>
    /// A tile for a tile map in Spear of Longinus.
    /// </summary>
    public class SOLTile
    {
        #region Variables

        /// <summary>
        /// The tile's animation.
        /// </summary>
        /// <value>
        /// The tile's animation.
        /// </value>
        public SOLAnimation Animation { get; protected set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SOLTile"/> class.
        /// </summary>
        /// <param name="animation">The tile's animation.</param>
        public SOLTile(SOLAnimation animation)
        {
            Animation = animation;
        }

        #endregion 

        #region Functions

        /// <summary>
        /// Updates the tile.
        /// </summary>
        /// <param name="animspeed">The speed at which to update the animation of the tile.</param>
        public virtual void Update(float animspeed)
        {
            Animation.Update(animspeed);
        }

        #endregion
    }
}
