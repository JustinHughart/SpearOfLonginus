using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Xml.Linq;
using SpearOfLonginus.Animation;

namespace SpearOfLonginus.Maps
{
    public class SOLMap
    {
        protected List<SOLTile> TileSet;

        public SOLVector Size;
        public SOLVector TileSize;

        protected int[] CollisionLayer;
        protected int[] BackgroundLayer;
        protected int[] ForegroundLayer;

        protected List<SOLMapLogic> Logics;

        protected List<SOLBackdrop> Backdrops;
        protected List<SOLBackdrop> Foredrops;

        public SOLMap(string path)
        {
            LoadMap(path);
        }

        public virtual void Update(float animspeed)
        {
            foreach (var tile in TileSet)
            {
                tile.Update(animspeed);
            }

            foreach (var logic in Logics)
            {
                logic.Update();
            }

            foreach (var backdrop in Backdrops)
            {
                backdrop.Update();
            }

            foreach (var foredrop in Foredrops)
            {
                foredrop.Update();
            }
        }

        protected virtual void LoadMap(string path)
        {
            if (File.Exists(path)) //Check if file exists.
            {
                XDocument doc = XDocument.Load(path); //Load the file.
                LoadMap(doc.Root);
            }
            else
            {
                throw new Exception("Failed to find map.");
            }
        }

        protected virtual void LoadMap(XElement root)
        {
            if (root == null) //First check if the document actually has a root.
            {
                throw new Exception("Document has no root.");
            }

            if (root.Name != "map") //Then check if it's a tiled map.
            {
                throw new Exception("Document is not a Tiled map.");
            }

            //With that out of the way, we will check the map attributes.
            foreach (var attribute in root.Attributes())
            {
                if (attribute.Name == "width")
                {
                    float.TryParse(attribute.Value, out Size.X);
                    continue;
                }

                if (attribute.Name == "height")
                {
                    float.TryParse(attribute.Value, out Size.Y);
                    continue;
                }

                if (attribute.Name == "tilewidth")
                {
                    float.TryParse(attribute.Value, out TileSize.X);
                    continue;
                }

                if (attribute.Name == "tileheight")
                {
                    float.TryParse(attribute.Value, out TileSize.Y);
                }
            }

            //Load map properties here? We don't have any by default. 

            //Here we will load the external tilesets. External tilesets are extremely recommended (required by vanilla SOL actually) due to keeping everything uniform amongst your maps.

            TileSet = new List<SOLTile>();
            TileSet.Add(null); //Add a null value to equate GID 0 (no tile) to it.

            foreach (var element in root.Elements("tileset"))
            {
                var source = element.Attribute("source");

                if (source == null) //If there's no source value, this means the tileset is internal. This is terrible for RPGs, as you want uniformity between your reused tilesets.
                {
                    throw new Exception("Tileset is not external. Please make your tileset external.");
                }

                LoadTileset(source.Value);
            }

            //Load tile layers.
            foreach (var layer in root.Elements("layer"))
            {
                LoadLayer(layer);
            }






            //Load object layers.
        }

        protected virtual void LoadTileset(string path)
        {
            if (File.Exists(path)) //Check if file exists.
            {
                XDocument doc = XDocument.Load(path); //Load the file.
                LoadTileset(doc.Root, TileSize);
            }
            else
            {
                throw new Exception("Failed to find external tileset.");
            }
        }

        protected virtual void LoadTileset(XElement root, SOLVector tilesize)
        {
            if (root == null) //First check if the document actually has a root.
            {
                throw new Exception("Document has no root.");
            }

            if (root.Name != "tileset") //Then check if it's an external tileset.
            {
                throw new Exception("Document is not an external tileset.");
            }

            //With that out of the way, we will check the image attributes.
            string textureid = "";
            SOLVector imagesize = SOLVector.Zero;

            var imageelement = root.Element("image");

            if (imageelement == null) //Gotta check if that's null.
            {
                throw new Exception("No texture element in proper location in document.");
            }

            //Check the attributes. We are ignoring trans since you should know how to handle your transparency in your given framework.

            foreach (var attribute in imageelement.Attributes())
            {
                if (attribute.Name == "source")
                {
                    textureid = attribute.Value;
                    continue;
                }

                if (attribute.Name == "width")
                {
                    float.TryParse(attribute.Value, out imagesize.X);
                    continue;
                }

                if (attribute.Name == "height")
                {
                    float.TryParse(attribute.Value, out imagesize.Y);
                }
            }

            //Divide the image size by tilesize to get the size in tiles. TileSize is already loaded from LoadMap, so it'll work fine. DOES NOT SUPPORT MALFORMED IMAGES, such as 37 x 43. All images must be a multiple of TileSize.
            imagesize /= tilesize;

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
                for (int x = 0; y < imagesize.X; x++)
                {
                    //Get the element if it exists.
                    XElement element = null;

                    if (tileproperties.ContainsKey(gid))
                    {
                        element = tileproperties[gid];
                    }

                    //LOAD IT
                    TileSet.Add(LoadTile(textureid, x, y, tilesize, element));

                    //Increment the GID, for identification purposes.
                    gid++;
                }
            }
            
            //And we're done, finally!
        }

        protected virtual SOLTile LoadTile(string textureid, SOLVector position, SOLVector tilesize, XElement element)
        {
            return LoadTile(textureid, (int) position.X, (int) position.Y, tilesize, element);
        }

        protected virtual SOLTile LoadTile(string textureid, int x, int y, SOLVector tilesize, XElement element)
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
                        
                        if (name != null && value != null)
                        {
                            if (name.Value == "animframes") //The number of frames the animation has, stacked vertically directly below.
                            {
                                int.TryParse(value.Value, out numframes);
                                continue;
                            }

                            if (name.Value == "animrate") //The rate at which the animation goes.
                            {
                                float.TryParse(value.Value, out animrate);
                            }
                        }
                    }
                }
            }

            //Get the list of frames.
            var frames = new List<SOLFrame>();

            for (int i = 0; i < numframes; i++)
            {
                GetTileFrame(textureid, x, y, tilesize, animrate);

                y++;
            }

            return new SOLTile(new SOLAnimation(true, false, frames));
        }
        
        protected void LoadLayer(XElement layer)
        {
            string name = "";

            var nameattribute = layer.Attribute("name");

            if (nameattribute != null)
            {
                name = nameattribute.Value;
            }
            else
            {
                throw new Exception("Malformed layer. Has no name.");
            }

            //Load properties here. I need to figure out a good solution for that.

            //Load layer data.

            var data = layer.Element("data");

            if (data != null)
            {
                var encoding = data.Attribute("encoding");
                var compression = data.Attribute("gzip");

                if (encoding == null || compression == null)
                {
                    throw new Exception("Improper layer. Please make sure your file is in base64 GZIP format.");
                }

                if (encoding.Value != "base64" || compression.Value != "gzip")
                {
                    throw new Exception("Improper layer. Please make sure your file is in base64 GZIP format.");
                }

                string datastring = data.Value;
                
                char[] chardata = datastring.ToCharArray();

                byte[] bytedata = Convert.FromBase64CharArray(chardata, 0, chardata.Length);

                Stream memstream = new MemoryStream(bytedata);

                Stream gzipstream = new GZipStream(memstream, CompressionMode.Decompress, false);

                BinaryReader binaryreader = new BinaryReader(gzipstream);

                int length = (int)(Size.X * Size.Y);

                int[] decodeddata = new int[length];

                for (int i = 0; i < length; i++)
                {
                    decodeddata[i] = binaryreader.ReadInt32() - 1;
                }

                binaryreader.Close();

                if (name.ToLower() == "background")
                {
                    BackgroundLayer = decodeddata;
                    return; 
                }

                if (name.ToLower() == "foreground")
                {
                    ForegroundLayer = decodeddata;
                    return;
                }

                if (name.ToLower() == "collision")
                {
                    CollisionLayer = decodeddata;
                    return;
                }

                throw new Exception("Only 3 layers are supported, Background, Foreground, Collision. Please update your tilemap to these specifications.");
            }
            else
            {
                throw new Exception("Data element is null.");
            }

        }

        protected virtual SOLFrame GetTileFrame(string textureid, int x, int y, SOLVector tilesize, float animrate)
        {
            Rectangle drawrect = new Rectangle(x * (int)tilesize.X, y * (int)tilesize.Y, (int)tilesize.X, (int)tilesize.Y);
            
            return new SOLFrame(textureid, drawrect, SOLVector.Zero, animrate);
        }
    }
}
