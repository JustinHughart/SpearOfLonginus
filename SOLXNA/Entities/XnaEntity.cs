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

        public XnaEntity(AnimationCache animationcache, Rectangle hitbox) : this(animationcache, hitbox, new SpriteBatchData())
        {
            
        }

        public XnaEntity(AnimationCache animationcache, Rectangle hitbox, SpriteBatchData spritebatchdata)
            : base(animationcache, hitbox)
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

        public virtual void DrawHitbox(SpriteBatch spritebatch, Matrix cameramatrix, Texture2D texture)
        {
            Color color = Color.Aqua * .7f;

            spritebatch.Begin(SpriteSortMode.Immediate, SpriteBatchData.BlendState, SpriteBatchData.SamplerState, SpriteBatchData.DepthStencilState, SpriteBatchData.RasterizerState, SpriteBatchData.Effect, cameramatrix);

            spritebatch.Draw(texture, Hitbox.Location.ToXnaVector(), texture.Bounds, color, 0f, Vector2.Zero, Hitbox.Size.ToXnaVector(), SpriteEffects.None, 0f);

            spritebatch.End();



        }
    }
}
