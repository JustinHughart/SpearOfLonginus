namespace SpearOfLonginus.Input
{
    /// <summary>
    /// An enum to track button states.
    /// </summary>
    public enum PressState
    {
        /// <summary>
        /// Shows that the button has been up for more than a frame.
        /// </summary>
        Up,
        /// <summary>
        /// Shows that the button has been down for more than a frame.
        /// </summary>
        Down,
        /// <summary>
        /// Shows that the button was released this frame.
        /// </summary>
        Released,
        /// <summary>
        /// Shows that the button was pressed this frame.
        /// </summary>
        Pressed,
    }

    /// <summary>
    /// A packet describing the player's input on a particular frame.
    /// </summary>
    public class InputPacket
    {
        #region Variables

        /// <summary>
        /// The button for moving north.
        /// </summary>
        public PressState Up;
        /// <summary>
        /// The button for moving south.
        /// </summary>
        public PressState Down;
        /// <summary>
        /// The button for moving west.
        /// </summary>
        public PressState Left;
        /// <summary>
        /// The button for moving east.
        /// </summary>
        public PressState Right;
        /// <summary>
        /// The button used to accept menu choices.
        /// </summary>
        public PressState Accept;
        /// <summary>
        /// The button used to cancel menu choices.
        /// </summary>
        public PressState Cancel;
        /// <summary>
        /// The button used to sprint.
        /// </summary>
        public PressState Run;
        /// <summary>
        /// The button used to check objects, talk to people, etc.
        /// </summary>
        public PressState Interact;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="InputPacket" /> class.
        /// </summary>
        public InputPacket() : this (PressState.Up, PressState.Up, PressState.Up, PressState.Up, PressState.Up, PressState.Up, PressState.Up, PressState.Up)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InputPacket" /> class.
        /// </summary>
        /// <param name="up">The button for moving north.</param>
        /// <param name="down">The button for moving south.</param>
        /// <param name="left">The button for moving west.</param>
        /// <param name="right">The button for moving east.</param>
        /// <param name="accept">The button used to accept menu choices.</param>
        /// <param name="cancel">The button used to cancel menu choices.</param>
        /// <param name="run">The button used to sprint.</param>
        /// <param name="interact">The button used to check objects, talk to people, etc.</param>
        public InputPacket(PressState up, PressState down, PressState left, PressState right, PressState accept, PressState cancel, PressState run, PressState interact)
        {
            Up = up;
            Down = down;
            Left = left;
            Right = right;

            Accept = accept;
            Cancel = cancel;

            Run = run;
            Interact = interact;
        }

        #endregion

    }
}
