using Microsoft.Xna.Framework.Input;

namespace SOLXNA.Input
{
    /// <summary>
    /// A profile for dynamic keyboard input.
    /// </summary>
    public class KeyboardProfile
    {
        #region Variables

        /// <summary>
        /// The button for moving north.
        /// </summary>
        public Keys Up;
        /// <summary>
        /// The button for moving south.
        /// </summary>
        public Keys Down;
        /// <summary>
        /// The button for moving west.
        /// </summary>
        public Keys Left;
        /// <summary>
        /// The button for moving east.
        /// </summary>
        public Keys Right;
        /// <summary>
        /// The button used to accept menu choices.
        /// </summary>
        public Keys Accept;
        /// <summary>
        /// The button used to cancel menu choices.
        /// </summary>
        public Keys Cancel;
        /// <summary>
        /// The button used to sprint.
        /// </summary>
        public Keys Run;
        /// <summary>
        /// The button used to check objects, talk to people, etc.
        /// </summary>
        public Keys Check;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyboardProfile"/> class.
        /// </summary>
        public KeyboardProfile() : this(Keys.Up, Keys.Down, Keys.Left, Keys.Right, Keys.X, Keys.Z, Keys.Z, Keys.X)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyboardProfile"/> class.
        /// </summary>
        /// <param name="up">The button for moving north.</param>
        /// <param name="down">The button for moving south.</param>
        /// <param name="left">The button for moving west.</param>
        /// <param name="right">The button for moving east.</param>
        /// <param name="accept">The button used to accept menu choices.</param>
        /// <param name="cancel">The button used to cancel menu choices.</param>
        /// <param name="run">The button used to sprint.</param>
        /// <param name="check">The button used to check objects, talk to people, etc.</param>
        public KeyboardProfile(Keys up, Keys down, Keys left, Keys right, Keys accept, Keys cancel, Keys run, Keys check)
        {
            Up = up;
            Down = down;
            Left = left;
            Right = right;
            Accept = accept;
            Cancel = cancel;
            Run = run;
            Check = check;
        }

        #endregion
    }
}
