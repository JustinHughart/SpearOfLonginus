using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Xml.Linq;
using SpearOfLonginus.Entities;
using SpearOfLonginus.Input;

namespace SpearOfLonginus.Maps
{
    /// <summary>
    /// A tile-based map for Spear of Longinus.
    /// </summary>
    public class Map
    {
        #region Variables

        /// <summary>
        /// The list of tiles to be used.
        /// </summary>
        protected List<Tile> TileSet;
        /// <summary>
        /// The size of the map in tiles.
        /// </summary>
        public Vector Size;
        /// <summary>
        /// The size of the tiles in pixels.
        /// </summary>
        public Vector TileSize;
        /// <summary>
        /// The layer of tiles that collides with entities.
        /// </summary>
        protected int[] CollisionLayer;
        /// <summary>
        /// The background layer of tiles that does not collide with entities.
        /// </summary>
        protected int[] BackgroundLayer;
        /// <summary>
        /// The foreground layer of tiles that does not collide with entities.
        /// </summary>
        protected int[] ForegroundLayer;
        /// <summary>
        /// The list of logics that the map uses. This can be used to change map properties systematically.
        /// </summary>
        protected List<MapLogic> Logics;
        /// <summary>
        /// The list of backdrops that get drawn before the map.
        /// </summary>
        protected Dictionary<string, Backdrop> Backdrops;
        /// <summary>
        /// The list of foredrops that get drawn after the map.
        /// </summary>
        protected Dictionary<string, Backdrop> Foredrops;
        /// <summary>
        /// The map's doors that lead to other maps.
        /// </summary>
        public List<Door> Doors;
        /// <summary>
        /// The entities contained inside the map.
        /// </summary>
        public EntityManager Entities { get; protected set; }
        /// <summary>
        /// Whether or not to use the hitbox cache.
        /// </summary>
        public bool UseHitboxCache { get; protected set; }
        /// <summary>
        /// Whether or not the map remains loaded when a player leaves it.
        /// </summary>
        protected bool Persistant;
        /// <summary>
        /// Whether or not the map updates when a player is outside of it.
        /// </summary>
        public bool IsActive;
        /// <summary>
        /// The cache of the collision layer's hitboxes.
        /// </summary>
        protected Rectangle[] HitboxCache;

        #endregion 

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Map" /> class.
        /// </summary>
        /// <param name="path">The file path of the base64 gzipped Tiled map.</param>
        public Map(string path)
        {
            Logics = new List<MapLogic>();
            Backdrops = new Dictionary<string, Backdrop>();
            Foredrops = new Dictionary<string, Backdrop>();
            Entities = new EntityManager(this);
            Doors = new List<Door>();

            LoadMap(path);
        }

        public Map(string path, bool usehitboxcache) : this(path)
        {
            UseHitboxCache = usehitboxcache;

            if (usehitboxcache)
            {
                CreateHitboxCache();
            }
        }

        #endregion

        #region Functions

        /// <summary>
        /// Updates the map.
        /// </summary>
        /// <param name="inputmanager">The input manager from which to receive player packets. </param>
        /// <param name="deltatime">The speed at which the map should change.</param>
        public virtual void Update(InputManager inputmanager, float deltatime)
        {
            foreach (var tile in TileSet)
            {
                tile.Update(deltatime);
            }

            foreach (var logic in Logics)
            {
                logic.Update(deltatime);
            }

            foreach (var backdrop in Backdrops)
            {
                backdrop.Value.Update(deltatime);
            }

            foreach (var foredrop in Foredrops)
            {
                foredrop.Value.Update(deltatime);
            }

            Entities.Update(inputmanager, deltatime);
        }

        /// <summary>
        /// Gets the tile.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="layer">The layer.</param>
        /// <returns></returns>
        public virtual Tile GetTile(Vector position, int[] layer)
        {
            int index = (int)position.X + (int)(position.Y * Size.X);

            if (index < 0 || index >= layer.Length)
            {
                return null;
            }

            int gid = layer[index];

            if (gid == -1)
            {
                return null;
            }

            return TileSet[gid];
        }

        /// <summary>
        /// Gets the hitbox of the collision layer's tile.
        /// </summary>
        /// <param name="position">The position of the tile. This is the x,y index, not the pixel position.</param>
        /// <returns></returns>
        public virtual Rectangle GetHitbox(Vector position)
        {
            if (UseHitboxCache)
            {
                int index = (int)position.X + (int)(position.Y * Size.X);

                if (index < 0 || index >= HitboxCache.Length)
                {
                    return new Rectangle();
                }

                return HitboxCache[index];

            }
            else
            {
                var tile = GetTile(position, CollisionLayer);

                if (tile == null)
                {
                    return new Rectangle();
                }

                return tile.GetHitbox(position * TileSize);
            }
        }

        public bool IsPersistant()
        {
            if (Persistant)
            {
                return true;
            }

            //If it isn't, we must see if there's an entity inside it that is persistant.
            foreach (var entity in Entities.GetEntityList())
            {
                if (entity.Persistent)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region Loading

        /// <summary>
        /// Loads the map.
        /// </summary>
        /// <param name="path">The file path of the base64 gzipped Tiled map.</param>
        /// <exception cref="System.Exception">Failed to find map.</exception>
        protected virtual void LoadMap(string path)
        {
            if (!File.Exists(path))
            {
                throw new Exception("Failed to find map.");
            }

            XDocument doc = XDocument.Load(path); //Load the file.
            var root = doc.Root;

            if (root == null) //First check if the document actually has a root.
            {
                throw new Exception("Document has no root.");
            }
           
            if (!root.Name.LocalName.Equals("map", StringComparison.OrdinalIgnoreCase)) //Then check if it's a tiled map.
            {
                throw new Exception("Document is not a Tiled map.");
            }

            //With that out of the way, we will check the map attributes.
            foreach (var attribute in root.Attributes())
            {
                if (attribute.Name.LocalName.Equals("width", StringComparison.OrdinalIgnoreCase))
                {
                    float.TryParse(attribute.Value, out Size.X);
                    continue;
                }

                if (attribute.Name.LocalName.Equals("height", StringComparison.OrdinalIgnoreCase))
                {
                    float.TryParse(attribute.Value, out Size.Y);
                    continue;
                }

                if (attribute.Name.LocalName.Equals("tilewidth", StringComparison.OrdinalIgnoreCase))
                {
                    float.TryParse(attribute.Value, out TileSize.X);
                    continue;
                }

                if (attribute.Name.LocalName.Equals("tileheight", StringComparison.OrdinalIgnoreCase))
                {
                    float.TryParse(attribute.Value, out TileSize.Y);
                }
            }

            //Load map properties here.
            LoadMapProperties(root.Element("properties"));

            //Here we will load the external tilesets. External tilesets are extremely recommended (required by vanilla SOL actually) due to keeping everything uniform amongst your maps.

            TileSet = new List<Tile>();

            foreach (var element in root.Elements("tileset"))
            {
                var source = element.Attribute("source");

                if (source == null) //If there's no source value, this means the tileset is internal. This is terrible for RPGs, as you want uniformity between your reused tilesets.
                {
                    throw new Exception("Tileset is not external. Please make your tileset external.");
                }

                var tilesetpath = Path.GetDirectoryName(path) + "/" + source.Value;

                LoadTileset(tilesetpath);
            }

            //Load tile layers.
            foreach (var layer in root.Elements("layer"))
            {
                LoadLayer(layer);
            }

            //Load object layers. 
            foreach (var objectgroup in root.Elements("objectgroup"))
            {
                var nameattribute = objectgroup.Attribute("name");

                if (nameattribute == null)
                {
                    throw new Exception("No name attribute in object group.");
                }

                //Backdrops

                if (nameattribute.Value.Equals("backdrops", StringComparison.OrdinalIgnoreCase))
                {
                    foreach (var backdrop in objectgroup.Elements("object"))
                    {
                        var nameelement = backdrop.Attribute("name");

                        if (nameelement == null)
                        {
                            throw new Exception("No name element in backdrop.");
                        }

                        string name = nameelement.Value;

                        if (name.Equals("", StringComparison.OrdinalIgnoreCase))
                        {
                            name = Backdrops.Count.ToString();
                        }

                        Backdrops.Add(name, new Backdrop(backdrop));
                    }
                }

                //Foredrops

                if(nameattribute.Value.Equals("foredrops", StringComparison.OrdinalIgnoreCase))
                {
                    foreach (var foredrop in objectgroup.Elements("object"))
                    {
                        var nameelement = foredrop.Attribute("name");

                        if (nameelement == null)
                        {
                            throw new Exception("No name element in backdrop.");
                        }

                        string name = nameelement.Value;

                        if (name.Equals("", StringComparison.OrdinalIgnoreCase))
                        {
                            name = Foredrops.Count.ToString();
                        }

                        Foredrops.Add(name, new Backdrop(foredrop));
                    }
                }

                //Entities

                if (nameattribute.Value.Equals("entities", StringComparison.OrdinalIgnoreCase))
                {
                    foreach (var entityelement in objectgroup.Elements("object"))
                    {
                        //Get the position.
                        int x = 0;
                        int y = 0;

                        foreach (var attribute in entityelement.Attributes())
                        {
                            if (attribute.Name.LocalName.Equals("x", StringComparison.OrdinalIgnoreCase))
                            {
                                int.TryParse(attribute.Value, out x);

                                continue;
                            }

                            if (attribute.Name.LocalName.Equals("y", StringComparison.OrdinalIgnoreCase))
                            {
                                int.TryParse(attribute.Value, out y);

                                continue;
                            }
                        }

                        //Get the path property.
                        string entitypath = "";

                        XElement propertieselement = entityelement.Element("properties");

                        if (propertieselement != null)
                        {
                            foreach (var property in propertieselement.Elements("property"))
                            {
                                if (property.Attribute("name") == null)
                                {
                                    continue;
                                }

                                if (property.Attribute("value") == null)
                                {
                                    continue;
                                }

                                if (property.Attribute("name").Value.Equals("data", StringComparison.OrdinalIgnoreCase))
                                {
                                    entitypath = property.Attribute("value").Value;
                                }
                            }
                        }

                        Entity entity = Entity.LoadFromFile(entitypath);
                        entity.Position = new Vector(x, y);

                        Entities.AddEntity(entity);
                    }
                }

                //Doors

                if (nameattribute.Value.Equals("doors", StringComparison.OrdinalIgnoreCase))
                {
                    foreach (var door in objectgroup.Elements("object"))
                    {
                        Rectangle hitbox = new Rectangle();
                        String targetmap = "";
                        Vector targetpos = Vector.Zero;

                        //Handle attributes.

                        foreach (var attribute in door.Attributes())
                        {
                            if (attribute.Name.LocalName.Equals("x", StringComparison.OrdinalIgnoreCase))
                            {
                                int.TryParse(attribute.Value, out hitbox.X);
                            }

                            if (attribute.Name.LocalName.Equals("y", StringComparison.OrdinalIgnoreCase))
                            {
                                int.TryParse(attribute.Value, out hitbox.Y);
                            }

                            if (attribute.Name.LocalName.Equals("width", StringComparison.OrdinalIgnoreCase))
                            {
                                int.TryParse(attribute.Value, out hitbox.Width);
                            }

                            if (attribute.Name.LocalName.Equals("height", StringComparison.OrdinalIgnoreCase))
                            {
                                int.TryParse(attribute.Value, out hitbox.Height);
                            }
                        }

                        //Handle properties.

                        foreach (var property in door.Element("properties").Elements("property"))
                        {
                            var name = property.Attribute("name");
                            var value = property.Attribute("value");

                            if (name == null || value == null)
                            {
                                continue;
                            }

                            if (name.Value.Equals("map", StringComparison.OrdinalIgnoreCase))
                            {
                                targetmap = value.Value;
                                continue;
                            }

                            if (name.Value.Equals("positionx", StringComparison.OrdinalIgnoreCase))
                            {
                                float.TryParse(value.Value, out targetpos.X);
                                continue;
                            }

                            if (name.Value.Equals("positiony", StringComparison.OrdinalIgnoreCase))
                            {
                                float.TryParse(value.Value, out targetpos.Y);
                                continue;
                            }
                        }

                        //Create door
                        Doors.Add(new Door(hitbox, targetmap, targetpos));
                    }
                }
            }
        }

        /// <summary>
        /// Loads the tileset.
        /// </summary>
        /// <param name="path">The path tot he Tiled external tileset.</param>
        /// <exception cref="System.Exception">Failed to find external tileset.</exception>
        protected virtual void LoadTileset(string path)
        {
            if (!File.Exists(path))
            {
                throw new Exception("Failed to find external tileset.");
            }

            XDocument doc = XDocument.Load(path); //Load the file.
            var root = doc.Root;

            if (root == null) //First check if the document actually has a root.
            {
                throw new Exception("Document has no root.");
            }

            if (!root.Name.LocalName.Equals("tileset", StringComparison.OrdinalIgnoreCase)) //Then check if it's an external tileset.
            {
                throw new Exception("Document is not an external tileset.");
            }

            if (root.Element("image") == null)
            {
                throw new Exception("Document is not an external tileset.");
            }
            
            if (root.Element("image").Attribute("source") == null)
            {
                throw new Exception("Document is not an external tileset.");
            }

            //With that out of the way, we will check the image attributes.
            string textureid = "";
            Vector imagesize = Vector.Zero;

            var imageelement = root.Element("image");

            if (imageelement == null) //Gotta check if that's null.
            {
                throw new Exception("No texture element in proper location in document.");
            }

            //Check the attributes. We are ignoring trans since you should know how to handle your transparency in your given framework.

            foreach (var attribute in imageelement.Attributes())
            {
                if (attribute.Name.LocalName.Equals("source", StringComparison.OrdinalIgnoreCase))
                {
                    textureid = Path.GetDirectoryName(path) + "/" + attribute.Value;
                    continue;
                }

                if (attribute.Name.LocalName.Equals("width", StringComparison.OrdinalIgnoreCase))
                {
                    float.TryParse(attribute.Value, out imagesize.X);
                    continue;
                }

                if (attribute.Name.LocalName.Equals("height", StringComparison.OrdinalIgnoreCase))
                {
                    float.TryParse(attribute.Value, out imagesize.Y);
                }
            }

            //Divide the image size by tilesize to get the size in tiles. TileSize is already loaded from LoadMap, so it'll work fine. DOES NOT SUPPORT MALFORMED IMAGES, such as 37 x 43. All images must be a multiple of TileSize.
            imagesize /= TileSize;

            //Next, we will get sort the tiles with special properties into a dictionary for easy access.
            var tileproperties = new Dictionary<int, XElement>();

            foreach (var element in root.Elements("tile"))
            {
                var id = element.Attribute("id");

                if (id == null)
                {
                    continue;
                }

                tileproperties.Add(int.Parse(id.Value), element);
            }

            //Load the tiles into the tileset.

            int gid = 0;

            for (int y = 0; y < imagesize.Y; y++)
            {
                for (int x = 0; x < imagesize.X; x++)
                {
                    //Get the element if it exists.
                    XElement element = null;

                    if (tileproperties.ContainsKey(gid))
                    {
                        element = tileproperties[gid];
                    }

                    //LOAD IT
                    TileSet.Add(new Tile(textureid, x, y, TileSize, element));

                    //Increment the GID, for identification purposes.
                    gid++;
                }
            }

            //And we're done, finally!
        }

        /// <summary>
        /// Loads the layer from XML.
        /// </summary>
        /// <param name="layer">The layer element to be loaded.</param>
        /// <exception cref="System.Exception">Malformed layer. Has no name.
        /// or
        /// Improper layer. Please make sure your file is in base64 GZIP format.
        /// or
        /// Improper layer. Please make sure your file is in base64 GZIP format.
        /// or
        /// Only 3 layers are supported, Background, Foreground, Collision. Please update your tilemap to these specifications.
        /// or
        /// Data element is null.</exception>
        protected virtual void LoadLayer(XElement layer)
        {
            string name;

            var nameattribute = layer.Attribute("name");

            if (nameattribute != null)
            {
                name = nameattribute.Value;
            }
            else
            {
                throw new Exception("Malformed layer. Has no name.");
            }

            //Load properties. 
            LoadLayerProperties(name, layer.Element("properties"));

            //Load layer data.

            var data = layer.Element("data");

            if (data == null)
            {
                throw new Exception("Data element is null.");
            }

            var encoding = data.Attribute("encoding");
            var compression = data.Attribute("compression");

            if (encoding == null || compression == null)
            {
                throw new Exception("Improper layer. Please make sure your file is in base64 GZIP format.");
            }

            if (!encoding.Value.Equals("base64", StringComparison.OrdinalIgnoreCase) || !compression.Value.Equals("gzip", StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception("Improper layer. Please make sure your file is in base64 GZIP format.");
            }

            string datastring = data.Value;

            char[] chardata = datastring.ToCharArray();

            byte[] bytedata = Convert.FromBase64CharArray(chardata, 0, chardata.Length);

            int[] decodeddata;

            using (Stream memstream = new MemoryStream(bytedata))
            {
                using (Stream gzipstream = new GZipStream(memstream, CompressionMode.Decompress, false))
                {
                    using (var binaryreader = new BinaryReader(gzipstream))
                    {
                        var length = (int) (Size.X*Size.Y);

                        decodeddata = new int[length];

                        for (int i = 0; i < length; i++)
                        {
                            decodeddata[i] = binaryreader.ReadInt32() - 1;
                        }
                    }
                }
            }

            if (name.Equals("background", StringComparison.OrdinalIgnoreCase))
            {
                BackgroundLayer = decodeddata;
                return;
            }

            if (name.Equals("foreground", StringComparison.OrdinalIgnoreCase))
            {
                ForegroundLayer = decodeddata;
                return;
            }

            if (name.Equals("collision", StringComparison.OrdinalIgnoreCase))
            {
                CollisionLayer = decodeddata;
                return;
            }

            throw new Exception("Only 3 layers are supported, Background, Foreground, Collision. Please update your tilemap to these specifications.");
        }

        /// <summary>
        /// Loads the map's properties.
        /// </summary>
        /// <param name="element">The properties element.</param>
        protected virtual void LoadMapProperties(XElement element)
        {
            foreach (var property in element.Elements("property"))
            {
                var name = property.Attribute("name");
                var value = property.Attribute("value");

                if (name == null || value == null)
                {
                    continue;
                }

                if (name.Value.Equals("active", StringComparison.OrdinalIgnoreCase))
                {
                    bool.TryParse(value.Value, out IsActive);
                    continue;
                }

                if (name.Value.Equals("persistent", StringComparison.OrdinalIgnoreCase))
                {
                    bool.TryParse(value.Value, out Persistant);
                    continue;
                }
            }
        }

        /// <summary>
        /// Loads the layer's properties.
        /// </summary>
        /// <param name="name">The name of the layer.</param>
        /// <param name="element">The properties element.</param>
        protected virtual void LoadLayerProperties(string name, XElement element)
        {
            //No properties by default... For now! 
        }

        /// <summary>
        /// Creates the hitbox cache.
        /// </summary>
        protected virtual void CreateHitboxCache()
        {
            HitboxCache = new Rectangle[(int)(Size.X * Size.Y)];

            for (int y = 0; y < Size.Y; y++)
            {
                for (int x = 0; x < Size.X; x++)
                {
                    int index = x+ (int)(y * Size.X);

                    Tile tile = GetTile(new Vector(x,y), CollisionLayer);

                    if (tile != null)
                    {
                        HitboxCache[index] = tile.GetHitbox(new Vector(x, y)*TileSize);
                    }
                }    
            }
        }

        #endregion
    }
}
