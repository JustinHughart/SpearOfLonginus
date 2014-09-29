namespace SpearOfLonginus.Input
{
    /// <summary>
    /// An abstract class to define functions for a manager to create input packets from player input.
    /// </summary>
    public abstract class InputManager
    {
        /// <summary>
        /// Gets the input packet for player one.
        /// </summary>
        /// <returns></returns>
        public abstract InputPacket GetPlayer1Packet();
        /// <summary>
        /// Gets the input packet for player two.
        /// </summary>
        /// <returns></returns>
        public abstract InputPacket GetPlayer2Packet();
        /// <summary>
        /// Gets the input packet for player three.
        /// </summary>
        /// <returns></returns>
        public abstract InputPacket GetPlayer3Packet();
        /// <summary>
        /// Gets the input packet for player four.
        /// </summary>
        /// <returns></returns>
        public abstract InputPacket GetPlayer4Packet();
    }
}
