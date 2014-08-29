using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpearOfLonginus.Animation;

namespace SOLXNA.Animation
{
    public class Animation : SOLAnimation
    {
        public Animation(bool loop, bool resetindex) : base(loop, resetindex)
        {

        }

        public Animation(bool loop, bool resetindex, List<SOLFrame> frames) : base(loop, resetindex, new List<SOLFrame>())
        {
            foreach (var frame in frames) //Convert SOLFrame to Frame.
            {
                AddFrame(new Frame(frame));
            }
        }

        public virtual void Draw(SpriteBatch spritebatch, Vector2 position, Color tint, float rotation, Vector2 scale, SpriteEffects effects, float layer)
        {
            var currnode = (Frame) GetCurrentFrame();
            
            spritebatch.Draw(currnode.Texture, position, currnode.XNADrawArea, tint, rotation, currnode.XNAOrigin, scale, effects, layer);

        }
    }
}
