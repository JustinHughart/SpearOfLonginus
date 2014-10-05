using Microsoft.Xna.Framework;
using SpearOfLonginus;
using SpearOfLonginus.Cameras;

namespace SOLXNA.Cameras
{
    /// <summary>
    /// The XNA binding for SOL's Camera class.
    /// </summary>
    public class XnaCamera : Camera
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="XnaCamera"/> class.
        /// </summary>
        /// <param name="screenresolution">The game's resolution.</param>
        /// <param name="mapsize">The size of the map the camera is for.</param>
        /// <param name="drawborder">The size of the border around the screen which you still want to draw. Useful for large entities and rotating maps.</param>
        public XnaCamera(Vector screenresolution, Vector mapsize, Vector drawborder) : base(screenresolution, mapsize, drawborder)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XnaCamera"/> class.
        /// </summary>
        /// <param name="position">The position of the camera.</param>
        /// <param name="rotation">The rotation of the camera.</param>
        /// <param name="zoom">The zoom of the camera.</param>
        /// <param name="screenresolution">The game's resolution.</param>
        /// <param name="mapsize">The size of the map the camera is for.</param>
        /// <param name="drawborder">The size of the border around the screen which you still want to draw. Useful for large entities and rotating maps.</param>
        public XnaCamera(Vector position, float rotation, float zoom, Vector screenresolution, Vector mapsize, Vector drawborder) : base(position, rotation, zoom, screenresolution, mapsize, drawborder)
        {

        }

        #endregion

        #region Functions

        /// <summary>
        /// Gets the camera's transformation matrix.
        /// </summary>
        /// <returns></returns>
        public virtual Matrix GetCameraMatrix()
        {
            return Matrix.CreateTranslation(-Position.X, -Position.Y, 0)* //Move to position.
                   Matrix.CreateTranslation(ScreenResolution.X/2, ScreenResolution.Y/2, 0)* //Move to center of screen.
                   Matrix.CreateRotationZ(Rotation)* //Rotate it.
                   Matrix.CreateScale(Zoom)* //Then zoom it.
                   Matrix.CreateTranslation(-ScreenResolution.X/2, -ScreenResolution.Y/2, 0); //Then move back.
        }

        #endregion
    }
}
