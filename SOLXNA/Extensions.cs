using Microsoft.Xna.Framework;
using SOLXNA.Animations;
using SpearOfLonginus;
using SpearOfLonginus.Animations;
using XnaRect = Microsoft.Xna.Framework.Rectangle;
using SolRect = System.Drawing.Rectangle;

namespace SOLXNA
{
    /// <summary>
    /// A class for holding extension methods related to usage of SOL and XNA.
    /// </summary>
    public static class Extensions
    {
        #region Vectors

        /// <summary>
        /// Converts an XNA Vector2 to a SOL Vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns></returns>
        public static Vector ToSolVector(this Vector2 vector)
        {
            return new Vector(vector.X, vector.Y);
        }

        /// <summary>
        /// Converts a SOL Vector to an XNA Vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns></returns>
        public static Vector2 ToXnaVector(this Vector vector)
        {
            return new Vector2(vector.X, vector.Y);
        }

        #endregion

        #region Rectangles

        /// <summary>
        /// Converts a SOL Rectangle to an XNA Rectangle.
        /// </summary>
        /// <param name="rect">The rectangle.</param>
        /// <returns></returns>
        public static XnaRect ToXnaRectangle(this SolRect rect)
        {
            return new XnaRect(rect.X, rect.Y, rect.Width, rect.Height);
        }

        /// <summary>
        /// Converts an XNA Rectangle to a SOL Rectangle.
        /// </summary>
        /// <param name="rect">The rectangle.</param>
        /// <returns></returns>
        public static SolRect ToSolRectangle(this XnaRect rect)
        {
            return new SolRect(rect.X, rect.Y, rect.Width, rect.Height);
        }

        #endregion

        #region Animation Frames

        /// <summary>
        /// Converts a SOL Frame to an XNA Frame.
        /// </summary>
        /// <param name="frame">The frame.</param>
        /// <returns></returns>
        public static XnaFrame ToXnaFrame(this Frame frame)
        {
            return new XnaFrame(frame.TextureID, frame.DrawArea, frame.Origin, frame.TimeTillNext);
        }

        /// <summary>
        /// Converts an XNA Frame to a SOL Frame.
        /// </summary>
        /// <param name="frame">The frame.</param>
        /// <returns></returns>
        public static Frame ToSolFrame(this XnaFrame frame)
        {
            return new Frame(frame.TextureID, frame.DrawArea, frame.Origin, frame.TimeTillNext);
        }

        #endregion

    }
}
