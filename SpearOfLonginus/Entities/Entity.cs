using SpearOfLonginus.Animations;
using SpearOfLonginus.Input;
using SpearOfLonginus.Maps;

namespace SpearOfLonginus.Entities
{
    public enum PlayerType
    {
        NPC, 
        Player1,
        Player2,
        Player3,
        Player4,
        World, 
    }

    public class Entity
    {
        #region Variables

        public PlayerType PlayerType { get; protected set; }
        public Map Map;
        public Vector Position;
        public Animation CurrentAnimation { get; protected set; }
        public AnimationCache AnimationCache { get; protected set; }
        public Rectangle WorldHitbox { get; protected set; }

        #endregion

        #region Constructors

        #endregion

        #region Functions

        public virtual void Update(InputPacket packet)
        {
            
        }

        public virtual InputPacket GetAIPacket()
        {
            //AI is unsupported for now!
            return null;
        }

        #endregion

        
    }
}
