using Microsoft.Xna.Framework.Graphics;

namespace SOLXNA
{
    /// <summary>
    /// A class for holding data pertaining to the SpriteBatch class, for use of easily accessible shader support.
    /// </summary>
    public class SpriteBatchData
    {
        #region Variables

        /// <summary>
        /// The blend state to initialize the spritebatch with.
        /// </summary>
        public BlendState BlendState;
        /// <summary>
        /// The sampler state to initialize the spritebatch with.
        /// </summary>
        public SamplerState SamplerState;
        /// <summary>
        /// The depth stencil state to initialize the spritebatch with.
        /// </summary>
        public DepthStencilState DepthStencilState; //Is this needed? I'm adding it for completionist's sake but I don't know if it's needed since the map runs in immediate mode to support shaders. Might be handy in a 2D/3D graphical mix? 
        /// <summary>
        /// The rasterizer state to initialize the spritebatch with.
        /// </summary>
        public RasterizerState RasterizerState; //Same with this one. 
        /// <summary>
        /// The effect to initialize the spritebatch with.
        /// </summary>
        public Effect Effect;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteBatchData" /> class.
        /// </summary>
        public SpriteBatchData() : this(null, null, null, null, null)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteBatchData" /> class.
        /// </summary>
        /// <param name="blendstate">The blend state to initialize the spritebatch with.</param>
        /// <param name="samplerstate">The sampler state to initialize the spritebatch with.</param>
        /// <param name="depthstencilstate">The depth stencil state to initialize the spritebatch with.</param>
        /// <param name="rasterizerstate">The rasterizer state to initialize the spritebatch with.</param>
        /// <param name="effect">The effect to initialize the spritebatch with.</param>
        public SpriteBatchData(BlendState blendstate, SamplerState samplerstate, DepthStencilState depthstencilstate, RasterizerState rasterizerstate, Effect effect)
        {
            if (blendstate == null)
            {
                BlendState = BlendState.AlphaBlend;
            }
            else
            {
                BlendState = blendstate;
            }

            if (samplerstate == null)
            {
                SamplerState = SamplerState.PointClamp;
            }
            else
            {
                SamplerState = samplerstate;
            }

            if (depthstencilstate == null)
            {
                DepthStencilState = DepthStencilState.Default;
            }
            else
            {
                DepthStencilState = depthstencilstate;
            }

            if (RasterizerState == null)
            {
                RasterizerState = RasterizerState.CullNone;
            }
            else
            {
                RasterizerState = rasterizerstate;
            }

            Effect = effect;
        }

        #endregion

    }
}
