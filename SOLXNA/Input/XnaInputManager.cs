using Microsoft.Xna.Framework.Input;
using SpearOfLonginus.Input;

namespace SOLXNA.Input
{
    public class XnaInputManager : InputManager
    {
        public KeyboardProfile KBProfile;

        protected KeyboardState CurrKBState;
        protected KeyboardState PrevKBState;

        public XnaInputManager()
        {
            KBProfile = new KeyboardProfile();

            CurrKBState = PrevKBState = Keyboard.GetState();
        }

        public void Update()
        {
            PrevKBState = CurrKBState;
            CurrKBState = Keyboard.GetState();
        }

        public override InputPacket GetPlayer1Packet()
        {
            return GetKeyboardPacket();
        }

        public override InputPacket GetPlayer2Packet()
        {
            throw new System.NotImplementedException();
        }

        public override InputPacket GetPlayer3Packet()
        {
            throw new System.NotImplementedException();
        }

        public override InputPacket GetPlayer4Packet()
        {
            throw new System.NotImplementedException();
        }

        private InputPacket GetKeyboardPacket()
        {
            return new InputPacket(
                GetKeyState(KBProfile.Up),
                GetKeyState(KBProfile.Down),
                GetKeyState(KBProfile.Left),
                GetKeyState(KBProfile.Right),
                GetKeyState(KBProfile.Accept),
                GetKeyState(KBProfile.Cancel),
                GetKeyState(KBProfile.Run),
                GetKeyState(KBProfile.Check)
                );
        }

        public PressState GetKeyState(Keys key)
        {
            if (CurrKBState.IsKeyDown(key))
            {
                if (PrevKBState.IsKeyDown(key))
                {
                    return PressState.Down;
                }
                else
                {
                    return PressState.Pressed;
                }
            }
            else
            {
                if (PrevKBState.IsKeyUp(key))
                {
                    return PressState.Up;
                }
                else
                {
                    return PressState.Released;
                }
            }
        }
    }
}
