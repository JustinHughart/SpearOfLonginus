using System;
using System.Collections.Generic;
using System.Xml.Linq;
using SpearOfLonginus.Animations;

namespace SpearOfLonginus.Maps
{
    /// <summary>
    /// A tile for a tile map in Spear of Longinus.
    /// </summary>
    public class Tile
    {
        #region Variables

        /// <summary>
        /// The tile's animation.
        /// </summary>
        /// <value>
        /// The tile's animation.
        ///   </value>
        public Animation Animation;
        /// <summary>
        /// The tile's hitbox, used for collisions.
        /// </summary>
        protected Rectangle Hitbox;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Tile" /> class.
        /// </summary>
        /// <param name="animation">The tile's animation.</param>
        public Tile(Animation animation)
        {
            Animation = animation;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Tile" /> class.
        /// </summary>
        /// <param name="textureid">The texture's ID.</param>
        /// <param name="position">The position on the tilesheet.</param>
        /// <param name="tilesize">The size of tiles.</param>
        /// <param name="element">The element used for loading the tile.</param>
        public Tile(string textureid, Vector position, Vector tilesize, XElement element)
        {
            LoadTile(textureid, position, tilesize, element);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Tile" /> class.
        /// </summary>
        /// <param name="textureid">The texture's ID.</param>
        /// <param name="x">The X position of the tile.</param>
        /// <param name="y">The Y position of the tile.</param>
        /// <param name="tilesize">The size of tiles.</param>
        /// <param name="element">The element used for loading the tile.</param>
        public Tile(string textureid, int x, int y, Vector tilesize, XElement element)
        {
            LoadTile(textureid, x, y, tilesize, element);
        }

        #endregion 

        #region Functions

        /// <summary>
        /// Updates the tile.
        /// </summary>
        /// <param name="deltatime">The speed at which to update the animation of the tile.</param>
        public virtual void Update(float deltatime)
        {
            Animation.Update(deltatime);
        }

        /// <summary>
        /// Loads the tile.
        /// </summary>
        /// <param name="textureid">The texture's ID.</param>
        /// <param name="position">The position on the tilesheet.</param>
        /// <param name="tilesize">The size of tiles.</param>
        /// <param name="element">The XML element.</param>
        protected virtual void LoadTile(string textureid, Vector position, Vector tilesize, XElement element)
        {
            LoadTile(textureid, (int)position.X, (int)position.Y, tilesize, element);
        }

        /// <summary>
        /// Loads the tile.
        /// </summary>
        /// <param name="textureid">The texture's ID.</param>
        /// <param name="x">The X position of the tile.</param>
        /// <param name="y">The Y position of the tile.</param>
        /// <param name="tilesize">The size of tiles.</param>
        /// <param name="element">The element used for loading the tile.</param>
        protected virtual void LoadTile(string textureid, int x, int y, Vector tilesize, XElement element)
        {
            int numframes = 1;
            float animrate = 1;

            if (element != null) //If there's a data element with the tile...
            {
                var properties = element.Element("properties");
                if (properties != null) //Ensure it actually has properties first.
                {
                    foreach (var property in properties.Elements("property")) //Check the properties.
                    {
                        var name = property.Attribute("name");
                        var value = property.Attribute("value");

                        if (name == null || value == null)
                        {
                            continue;
                        }

                        if (name.Value.Equals("animframes", StringComparison.OrdinalIgnoreCase)) //The number of frames the animation has, stacked vertically directly below.
                        {
                            int.TryParse(value.Value, out numframes);
                            continue;
                        }

                        if (name.Value.Equals("animrate", StringComparison.OrdinalIgnoreCase)) //The rate at which the animation goes.
                        {
                            float.TryParse(value.Value, out animrate);
                        }
                    }
                }
            }

            //Get the list of frames.
            var frames = new List<Frame>();

            for (int i = 0; i < numframes; i++)
            {
                frames.Add(CreateTileFrame(textureid, x, y, tilesize, animrate));

                y++;
            }

            Animation = new Animation("tile_" +  textureid + "_" + x + "," + y, true, false, frames);

            //Create the hitbox.
            CreateHitbox(tilesize, element);
        }

        /// <summary>
        /// Creates a new animation from for your tile.
        /// </summary>
        /// <param name="textureid">The texture's ID.</param>
        /// <param name="x">The X position of the tile.</param>
        /// <param name="y">The Y position of the tile.</param>
        /// <param name="tilesize">The size of tiles.</param>
        /// <param name="animrate">The time at which the frame should change..</param>
        /// <returns></returns>
        protected virtual Frame CreateTileFrame(string textureid, int x, int y, Vector tilesize, float animrate)
        {
            var drawrect = new Rectangle(x * (int)tilesize.X, y * (int)tilesize.Y, (int)tilesize.X, (int)tilesize.Y);

            return new Frame(textureid, drawrect, Vector.Zero, animrate);
        }

        /// <summary>
        /// Creates the hitbox.
        /// </summary>
        /// <param name="tilesize">The size of tiles.</param>
        /// <param name="element">The element used for loading the tile.</param>
        protected virtual void CreateHitbox(Vector tilesize, XElement element)
        {
            //Element will be used later to allow for custom hitboxes.

            Hitbox = new Rectangle(0, 0, (int)tilesize.X, (int)tilesize.Y);
        }

        /// <summary>
        /// Gets the tile's hitbox.
        /// </summary>
        /// <param name="position">The position the hitbox is in.</param>
        /// <returns></returns>
        public virtual Rectangle GetHitbox(Vector position)
        {
            Hitbox.Location = position;

            return Hitbox;
        }

        #endregion
    }
}
