namespace SpearOfLonginus.Cameras
{
    /// <summary>
    /// An abstract camera class for SOL. Provides the basis to build your own cameras.
    /// </summary>
    public abstract class Camera
    {
        #region Variables

        /// <summary>
        /// The position of the camera.
        /// </summary>
        public Vector Position;
        /// <summary>
        /// The rotation of the camera.
        /// </summary>
        public float Rotation;
        /// <summary>
        /// The zoom of the camera.
        /// </summary>
        public float Zoom;
        /// <summary>
        /// The game's resolution.
        /// </summary>
        public Vector ScreenResolution;
        /// <summary>
        /// The size of the border around the screen which you still want to draw. Useful for large entities and rotating maps.
        /// </summary>
        public Vector DrawBorder;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Camera"/> class.
        /// </summary>
        /// <param name="screenresolution">The game's resolution.</param>
        /// <param name="drawborder">The size of the border around the screen which you still want to draw. Useful for large entities and rotating maps.</param>
        public Camera(Vector screenresolution, Vector drawborder)
            : this(Vector.Zero, 0f, 1f, screenresolution, drawborder)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Camera"/> class.
        /// </summary>
        /// <param name="position">The position of the camera.</param>
        /// <param name="rotation">The rotation of the camera.</param>
        /// <param name="zoom">The zoom of the camera.</param>
        /// <param name="screenresolution">The game's resolution.</param>
        /// <param name="drawborder">The size of the border around the screen which you still want to draw. Useful for large entities and rotating maps.</param>
       public Camera(Vector position, float rotation, float zoom, Vector screenresolution, Vector drawborder)
        {
            Position = position;
            Rotation = rotation;
            Zoom = zoom;
            ScreenResolution = screenresolution;
            DrawBorder = drawborder;
        }

        #endregion

        #region Functions

        /// <summary>
        /// Updates the camera.
        /// </summary>
        /// <param name="deltatime">The delta time.</param>
        public virtual void Update(float deltatime)
        {

        }

        /// <summary>
        /// Gets the area of the map to draw.
        /// </summary>
        /// <returns></returns>
        public virtual Rectangle GetDrawArea()
        {
            //We could do this all in a return, but this method is more straightforward and less confusing. Also performance should not be impacted with anything of notice since this would be called once a frame.
            Rectangle rect = new Rectangle((int) Position.X, (int) Position.Y, (int) ScreenResolution.X, (int) ScreenResolution.Y); //Set the rect to the center point, with screen size.
            rect.Location -= ScreenResolution/2; //Offset the position to the center.
            rect.Location -= DrawBorder; //Pull back for the border...
            rect.Size += DrawBorder*2; //And allow the width on both sides.
            rect.Size *= Zoom; //Scale by zoom.
            return rect; //Give it up!
        }

        #endregion
    }
}