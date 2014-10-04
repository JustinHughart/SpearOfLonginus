namespace SpearOfLonginus
{
    /// <summary>
    /// A rectangle class emulating the most useful functions of XNA's Rectangle class.
    /// </summary>
    public struct Rectangle
    {
        #region Variables

        /// <summary>
        /// The X coordinate of the rectangle.
        /// </summary>
        public int X;

        /// <summary>
        /// The Y coordinate of the rectangle.
        /// </summary>
        public int Y;

        /// <summary>
        /// The width of the rectangle.
        /// </summary>
        public int Width;

        /// <summary>
        /// The height of the rectangle.
        /// </summary>
        public int Height;

        #endregion

        #region Properties

        /// <summary>
        /// Gets half of the rectangle's width.
        /// </summary>
        /// <value>
        /// The value representing half of the rectangle's width.
        /// </value>
        public int HalfWidth
        {
            get { return Width/2; }
        }

        /// <summary>
        /// Gets half of the rectangle's width
        /// </summary>
        /// <value>
        /// The value representing half of the rectangle's height.
        /// </value>
        public int HalfHeight
        {
            get { return Height/2; }
        }

        /// <summary>
        /// Gets the Y value of the top of the rectangle.
        /// </summary>
        /// <value>
        /// The top of the rectangle.
        /// </value>
        public int Top
        {
            get { return Y; }
        }

        /// <summary>
        /// Gets the Y value of the bottom of the rectangle.
        /// </summary>
        /// <value>
        /// The bottom of the rectangle.
        /// </value>
        public int Bottom
        {
            get { return Y + Height; }
        }

        /// <summary>
        /// Gets the X value of the left side of the rectangle.
        /// </summary>
        /// <value>
        /// The left side of the rectangle.
        /// </value>
        public int Left
        {
            get { return X; }
        }

        /// <summary>
        /// Gets the X value of the right side of the rectangle.
        /// </summary>
        /// <value>
        /// The right side of the rectangle.
        /// </value>
        public int Right
        {
            get { return X + Width; }
        }

        /// <summary>
        /// Gets the upper left coordinate of the rectangle.
        /// </summary>
        /// <value>
        /// The upperleft corner of the rectangle.
        /// </value>
        public Vector Upperleft
        {
            get { return new Vector(X, Y); }
        }

        /// <summary>
        /// Gets the upper right coordinate of the rectangle.
        /// </summary>
        /// <value>
        /// The upper right corner of the rectangle.
        /// </value>
        public Vector Upperright
        {
            get { return new Vector(X + Width, Y); }
        }

        /// <summary>
        /// Gets the lower left coordinate of the rectangle.
        /// </summary>
        /// <value>
        /// The lower left corner of the rectangle.
        /// </value>
        public Vector LowerLeft
        {
            get { return new Vector(X, Y + Height); }
        }

        /// <summary>
        /// Gets the lower right coordinate of the rectangle.
        /// </summary>
        /// <value>
        /// The lower right corner of the rectangle.
        /// </value>
        public Vector LowerRight
        {
            get { return new Vector(X + Width, Y + Height); }
        }

        /// <summary>
        /// Gets the coordinate of the center of the rectangle.
        /// </summary>
        /// <value>
        /// The center. of the rectangle.
        /// </value>
        public Vector Center
        {
            get { return new Vector(X + (Width/2), Y + (Height/2)); }
        }

        /// <summary>
        /// Gets or sets the location of the rectangle.
        /// </summary>
        /// <value>
        /// The location of the rectangle.
        /// </value>
        public Vector Location
        {
            get { return new Vector(X, Y); }
            set
            {
                X = (int) value.X;
                Y = (int) value.Y;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle"/> struct.
        /// </summary>
        /// <param name="x">The X coordinate of the rectangle.</param>
        /// <param name="y">The Y coordinate of the rectangle.</param>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The height of the rectangle.</param>
        public Rectangle(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        #endregion

        #region Functions

        /// <summary>
        /// Checks if the other rectangle interesects with this one.
        /// </summary>
        /// <param name="other">The other rectangle.</param>
        /// <returns></returns>
        public bool Intersects(Rectangle other)
        {
            //Code for this function has been made with help by examining System.Drawing.Rectangle's code.

            return 
            (other.X < Right) && // If the other rect's X isn't beyond the right side of this rect...
            (X < (other.Right)) && //...And this rect's X also isn't past the other rect's right side...
            (other.Y < Bottom) && //... And the same applies to the y axis...
            (Y < other.Bottom); //... Then they must intersect!
        }

        #endregion

    }
}
