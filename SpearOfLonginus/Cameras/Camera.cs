namespace SpearOfLonginus.Cameras
{
    /// <summary>
    /// An abstract camera class for SOL. Provides the basis to build your own cameras.
    /// </summary>
    public abstract class Camera
    {
        #region Variables

        /// <summary>
        /// The position of the center point of the camera.
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
        /// The map's size.
        /// </summary>
        public Vector MapSize;
        /// <summary>
        /// The size of the border around the screen which you still want to draw. Useful for large entities and rotating maps.
        /// </summary>
        public Vector DrawBorder;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the upper left coordinate of the camera..
        /// </summary>
        /// <value>
        /// The upper left .
        /// </value>
        public Vector Upperleft
        {
            get { return Position - (ScreenResolution / 2 / Zoom); }
        }

        /// <summary>
        /// Gets the upper right coordinate of the camera.
        /// </summary>
        /// <value>
        /// The upper right coordinate.
        /// </value>
        public Vector UpperRight
        {
            get { return Position + new Vector(ScreenResolution.X / 2 / Zoom, - ScreenResolution.Y/ 2 / Zoom); }
        }

        /// <summary>
        /// Gets the lower left coordinate of the camera.
        /// </summary>
        /// <value>
        /// The lower left .
        /// </value>
        public Vector Lowerleft
        {
            get { return Position + new Vector(-ScreenResolution.X / 2/ Zoom, ScreenResolution.Y / 2 / Zoom); }
        }

        /// <summary>
        /// Gets the lower right coordinate of the camera.
        /// </summary>
        /// <value>
        /// The lower right coordinate.
        /// </value>
        public Vector LowerRight
        {
            get { return Position + (ScreenResolution / 2 / Zoom); }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Camera"/> class.
        /// </summary>
        /// <param name="screenresolution">The game's resolution.</param>
        /// <param name="mapsize">The size of the map the camera is for. </param>
        /// <param name="drawborder">The size of the border around the screen which you still want to draw. Useful for large entities and rotating maps.</param>
        public Camera(Vector screenresolution, Vector mapsize, Vector drawborder)
            : this(Vector.Zero, 0f, 1f, screenresolution, mapsize, drawborder)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Camera"/> class.
        /// </summary>
        /// <param name="position">The position of the camera.</param>
        /// <param name="rotation">The rotation of the camera.</param>
        /// <param name="zoom">The zoom of the camera.</param>
        /// <param name="screenresolution">The game's resolution.</param>
        /// <param name="mapsize">The size of the map the camera is for. </param>
        /// <param name="drawborder">The size of the border around the screen which you still want to draw. Useful for large entities and rotating maps.</param>
        public Camera(Vector position, float rotation, float zoom, Vector screenresolution, Vector mapsize, Vector drawborder)
        {
            Position = position;
            Rotation = rotation;
            Zoom = zoom;
            ScreenResolution = screenresolution;
            MapSize = mapsize;
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
            //Here we'll correct the position.
            if (Upperleft.X < 0)
            {
                Position.X = ScreenResolution.X/2/Zoom;
            }

            if (Upperleft.Y < 0)
            {
                Position.Y = ScreenResolution.Y/2/Zoom;
            }

            if (UpperRight.X >= MapSize.X)
            {
                Position.X = MapSize.X - (ScreenResolution.X / 2 / Zoom);
            }

            if (LowerRight.Y >= MapSize.Y)
            {
                Position.Y = MapSize.Y - (ScreenResolution.Y / 2 / Zoom);
            }
        }

        /// <summary>
        /// Gets the area of the map to draw.
        /// </summary>
        /// <returns></returns>
        public virtual Rectangle GetDrawArea()
        {
            //We could do this all in a return, but this method is more straightforward and less confusing. Also performance should not be impacted with anything of notice since this would be called once a frame.
            Rectangle rect = new Rectangle((int) Upperleft.X, (int) Upperleft.Y, (int)(ScreenResolution.X/Zoom), (int)(ScreenResolution.Y/Zoom)); //Set the rect to the center point, with screen size.
            rect.Location -= DrawBorder; //Pull back for the border...
            rect.Size += DrawBorder*2; //And allow the width on both sides.
            return rect; //Give it up!
        }

        #endregion
    }
}