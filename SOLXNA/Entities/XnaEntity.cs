using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SOLXNA.Animations;
using SpearOfLonginus.Animations;
using SpearOfLonginus.Entities;
using Rectangle = SpearOfLonginus.Rectangle;

namespace SOLXNA.Entities
{
    /// <summary>
    /// An XNA binding for SOL's Entity.
    /// </summary>
    public class XnaEntity : Entity
    {
        /// <summary>
        /// The sprite batch data.
        /// </summary>
        protected SpriteBatchData SpriteBatchData;

        /// <summary>
        /// Initializes a new instance of the <see cref="XnaEntity"/> class.
        /// </summary>
        /// <param name="animationcache">The cache holding all the entities animations.</param>
        public XnaEntity(AnimationCache animationcache) : this( animationcache, new Rectangle(0,0,1,1))
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XnaEntity"/> class.
        /// </summary>
        /// <param name="animationcache">The cache holding all the entities animations.</param>
        /// <param name="hitbox">The hitbox that causes interactions with the world.</param>
        public XnaEntity(AnimationCache animationcache, Rectangle hitbox) : this(animationcache, hitbox, new SpriteBatchData())
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XnaEntity"/> class.
        /// </summary>
        /// <param name="animationcache">The animationcache.</param>
        /// <param name="hitbox">The hitbox.</param>
        /// <param name="spritebatchdata">The spritebatchdata.</param>
        public XnaEntity(AnimationCache animationcache, Rectangle hitbox, SpriteBatchData spritebatchdata)
            : base(animationcache, hitbox)
        {
            SpriteBatchData = spritebatchdata;
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="texturecache">The texture cache used to load the content.</param>
        public void LoadContent(TextureCache texturecache)
        {
            var xnaanims = AnimationCache as XnaAnimationCache;

            if (xnaanims == null)
            {
                var oldcache = AnimationCache;

                AnimationCache = new XnaAnimationCache();

                foreach (var animation in oldcache.Animations.Values)
                {
                    AnimationCache.AddAnimation(animation);
                }

                xnaanims = AnimationCache as XnaAnimationCache;
            }

            xnaanims.LoadContent(texturecache);
        }

        /// <summary>
        /// Draws the entity.
        /// </summary>
        /// <param name="spritebatch">The sprite batch.</param>
        /// <param name="cameramatrix">The camera matrix.</param>
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

        /// <summary>
        /// Draws the hitbox.
        /// </summary>
        /// <param name="spritebatch">The sprite batch.</param>
        /// <param name="cameramatrix">The camera matrix.</param>
        /// <param name="texture">The texture.</param>
        public virtual void DrawHitbox(SpriteBatch spritebatch, Matrix cameramatrix, Texture2D texture)
        {
            Color color = Color.Aqua * .7f;

            spritebatch.Begin(SpriteSortMode.Immediate, SpriteBatchData.BlendState, SpriteBatchData.SamplerState, SpriteBatchData.DepthStencilState, SpriteBatchData.RasterizerState, SpriteBatchData.Effect, cameramatrix);

            spritebatch.Draw(texture, Hitbox.Location.ToXnaVector(), texture.Bounds, color, 0f, Vector2.Zero, Hitbox.Size.ToXnaVector(), SpriteEffects.None, 0f);

            spritebatch.End();
        }
    }
}
