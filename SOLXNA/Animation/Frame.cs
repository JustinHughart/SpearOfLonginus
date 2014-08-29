using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpearOfLonginus;
using SpearOfLonginus.Animation;
using SOLRect = System.Drawing.Rectangle;
using XNARect = Microsoft.Xna.Framework.Rectangle;

namespace SOLXNA.Animation
{
    public class Frame : SOLFrame
    {
        public Texture2D Texture;

        public XNARect XNADrawArea
        {
            get { return new XNARect(DrawArea.X, DrawArea.Y, DrawArea.Width, DrawArea.Height); }
        }

        public Vector2 XNAOrigin
        {
            get { return new Vector2(Origin.X, Origin.Y); }
        }

        public Frame(string textureid, SOLRect drawarea, SOLVector origin, float timetillnext) : base(textureid, drawarea, origin, timetillnext)
        {
            
        }

        public Frame(SOLFrame frame) : base(frame.TextureID, frame.DrawArea, frame.Origin, frame.TimeTillNext)
        {
            
        }

        public virtual void LoadContent(TextureManager texturemanager)
        {
            Texture = texturemanager.GetTexture(TextureID);
        }

        public virtual void UnloadContent()
        {
            Texture = null; //Is removing the texture enough? The texture manager should dispose of anything. I'm not sure if this piece of code is the right thing to do, in all honesty.
        }
    }
}
