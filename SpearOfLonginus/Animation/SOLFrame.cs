using System.Drawing;

namespace SpearOfLonginus.Animation
{
    /// <summary>
    /// An animation frame for Spear of Longinus.
    /// </summary>
    public class SOLFrame
    {
        #region Variables

        /// <summary>
        /// The ID used for  texture loading.
        /// </summary>
        /// <value>
        /// The ID.
        /// </value>
        public string TextureID { get; protected set; }
        /// <summary>
        /// The area on the texture that should be drawn.
        /// </summary>
        /// <value>
        /// The draw area.
        /// </value>
        public Rectangle DrawArea { get; protected set; }
        /// <summary>
        /// The origin of the frame. Used for rotation and to offset the sprite.
        /// </summary>
        /// <value>
        /// The origin.
        /// </value>
        public SOLVector Origin { get; protected set; }
        /// <summary>
        /// The time until the frame changes.
        /// </summary>
        /// <value>
        /// The time till change.
        /// </value>
        public int TimeTillNext { get; protected set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SOLFrame" /> class.
        /// </summary>
        /// <param name="textureid">The ID used for  texture loading.</param>
        /// <param name="drawarea">The area on the texture that should be drawn.</param>
        /// <param name="origin">The origin of the frame. Used for rotation and to offset the sprite.</param>
        /// <param name="timetillnext">The time until the frame changes.</param>
        public SOLFrame(string textureid, Rectangle drawarea, SOLVector origin, int timetillnext)
        {
            TextureID = textureid;
            DrawArea = drawarea;
            Origin = origin;
            TimeTillNext = timetillnext;
        }

        #endregion
    }
}
