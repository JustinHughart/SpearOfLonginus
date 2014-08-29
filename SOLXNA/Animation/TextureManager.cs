using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SOLXNA.Animation
{
    public class TextureManager
    {
        #region Variables

        protected GraphicsDevice GFX;
       protected Dictionary<string, Texture2D> Textures;
        protected Color ColorKey;

        #endregion

        #region Constructors

        public TextureManager(GraphicsDevice gfx)
        {
            GFX = gfx;
            ColorKey = Color.Transparent;
        }

        public TextureManager(GraphicsDevice gfx, Color colorkey)
        {
            GFX = gfx;
            ColorKey = colorkey;
        }

        #endregion

        #region Functions

        public virtual Texture2D GetTexture(string path)
        {
            if (!Textures.ContainsKey(path))
            {
                LoadTexture(path);
            }
            
            return Textures[path];
        }

        protected virtual void LoadTexture(string path)
        {
            if (File.Exists(path))
            {
                Stream stream = File.OpenRead(path);
                Texture2D texture = Texture2D.FromStream(GFX, stream);
                stream.Close();

                if (ColorKey != Color.Transparent)
                {
                    Color[] colordata = new Color[texture.Width * texture.Height];
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

        public virtual void PreloadTextures(List<String> paths)
        {
            foreach (var path in paths)
            {
                GetTexture(path);
            }
        }

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
