using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SOLXNA.Animation
{
    /// <summary>
    /// A class for loading and keeping track of textures from file, outside of XNA's content pipeline. Has support for color keying.
    /// </summary>
    public class TextureManager
    {
        #region Variables

        /// <summary>
        /// The graphics device used for texture loading.
        /// </summary>
        protected GraphicsDevice GraphicsDevice;
        /// <summary>
        /// The keyed list of textures, for quick access.
        /// </summary>
        protected Dictionary<string, Texture2D> Textures;
        /// <summary>
        /// The color that will be changed to transparency. If it's Transparent(0,0,0,0), then it will not key the texture.
        /// </summary>
        protected Color ColorKey;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TextureManager"/> class.
        /// </summary>
        /// <param name="graphicsdevice">The graphics device used for texture loading.</param>
        public TextureManager(GraphicsDevice graphicsdevice)
        {
            GraphicsDevice = graphicsdevice;
            ColorKey = Color.Transparent;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextureManager"/> class.
        /// </summary>
        /// <param name="graphicsdevice">The graphics device used for texture loading.</param>
        /// <param name="colorkey">The color that will be changed to transparency. If it's Transparent(0,0,0,0), then it will not key the texture.</param>
        public TextureManager(GraphicsDevice graphicsdevice, Color colorkey)
        {
            GraphicsDevice = graphicsdevice;
            ColorKey = colorkey;
        }

        #endregion

        #region Functions

        /// <summary>
        /// Gets a texture from the file path.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <returns></returns>
        public virtual Texture2D GetTexture(string path)
        {
            if (!Textures.ContainsKey(path))
            {
                LoadTexture(path);
            }

            return Textures[path];
        }

        /// <summary>
        /// Loads the texture from file and keys it if necessary..
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <exception cref="System.Exception">File doesn't exist.</exception>
        protected virtual void LoadTexture(string path)
        {
            if (File.Exists(path))
            {
                Stream stream = File.OpenRead(path);
                Texture2D texture = Texture2D.FromStream(GraphicsDevice, stream);
                stream.Close();

                if (ColorKey != Color.Transparent)
                {
                    Color[] colordata = new Color[texture.Width*texture.Height];
                    texture.GetData(colordata);

                    for (int i = 0; i < colordata.Length; i++)
                    {
                        if (colordata[i] == ColorKey)
                        {
                            colordata[i] = Color.Transparent;
                        }
                    }

                    texture.SetData(colordata);
                }

                Textures.Add(path, texture);
            }
            else
            {
                throw new Exception("File doesn't exist.");
            }
        }

        /// <summary>
        /// Preloads a list of textures.
        /// </summary>
        /// <param name="paths">A list of file paths.</param>
        public virtual void PreloadTextures(List<String> paths)
        {
            foreach (var path in paths)
            {
                GetTexture(path);
            }
        }

        //LoadContent doesn't exist since it shouldn't be needed, right? This class can't be created until after the main game's LoadContent()
        //due to the graphics device needed, which is created during LoadContent().

        /// <summary>
        /// Unloads the content.
        /// </summary>
        public virtual void UnloadContent()
        {
            foreach (var texture in Textures)
            {
                if (!texture.Value.IsDisposed)
                {
                    texture.Value.Dispose();
                }
            }

            Textures.Clear();
        }

        #endregion
    }
}