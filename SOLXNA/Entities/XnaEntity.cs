using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SOLXNA.Animations;
using SpearOfLonginus.Animations;
using SpearOfLonginus.Entities;
using Rectangle = SpearOfLonginus.Rectangle;

namespace SOLXNA.Entities
{
    public class XnaEntity : Entity
    {
        protected SpriteBatchData SpriteBatchData;

        public XnaEntity(AnimationCache animationcache) : this( animationcache, new Rectangle(0,0,1,1))
        {

        }

        public XnaEntity(AnimationCache animationcache, Rectangle worldhitbox) : this(animationcache, worldhitbox, new SpriteBatchData())
        {
            
        }

        public XnaEntity(AnimationCache animationcache, Rectangle worldhitbox, SpriteBatchData spritebatchdata)
            : base(animationcache, worldhitbox)
        {
            SpriteBatchData = spritebatchdata;
        }

        public void LoadContent(TextureCache texturecache)
        {
            var xnaanims = AnimationCache as XnaAnimationCache;

            if (xnaanims != null)
            {
                xnaanims.LoadContent(texturecache);
            }
        }

        public virtual void Draw(SpriteBatch spritebatch, Matrix cameramatrix)
        {
            var anim = CurrentAnimation as XnaAnimation;

            if (anim != null)
            {
                spritebatch.Begin(SpriteSortMode.Immediate, SpriteBatchData.BlendState, SpriteBatchData.SamplerState, SpriteBatchData.DepthStencilState, SpriteBatchData.RasterizerState, SpriteBatchData.Effect, cameramatrix);
                
                anim.Draw(spritebatch, Position.ToXnaVector(), Color.White, 0f, 1f, SpriteEffects.None, 0f);

                spritebatch.End();
            }
        }
    }
}
