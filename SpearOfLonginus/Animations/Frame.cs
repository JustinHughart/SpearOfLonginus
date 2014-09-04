using System.Drawing;

namespace SpearOfLonginus.Animations
{
    /// <summary>
    /// An animation frame for Spear of Longinus.
    /// </summary>
    public class Frame
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
        public Vector Origin { get; protected set; }
        /// <summary>
        /// The time until the frame changes. Is a float to support delta time.
        /// </summary>
        /// <value>
        /// The time till change.
        /// </value>
        public float TimeTillNext { get; protected set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Frame" /> class.
        /// </summary>
        /// <param name="textureid">The ID used for  texture loading.</param>
        /// <param name="drawarea">The area on the texture that should be drawn.</param>
        /// <param name="origin">The origin of the frame. Used for rotation and to offset the sprite.</param>
        /// <param name="timetillnext">The time until the frame changes.</param>
        public Frame(string textureid, Rectangle drawarea, Vector origin, float timetillnext)
        {
            TextureID = textureid;
            DrawArea = drawarea;
            Origin = origin;
            TimeTillNext = timetillnext;
        }

        #endregion
    }
}
