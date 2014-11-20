using Microsoft.Xna.Framework;
using SOLXNA.Animations;
using SOLXNA.Entities;
using SOLXNA.Maps;
using SpearOfLonginus;
using SpearOfLonginus.Animations;
using SpearOfLonginus.Entities;
using SpearOfLonginus.Maps;
using XnaRect = Microsoft.Xna.Framework.Rectangle;
using SolRect = SpearOfLonginus.Rectangle;

namespace SOLXNA
{
    /// <summary>
    /// A class for holding extension methods related to usage of SOL and XNA.
    /// </summary>
    public static class Extensions
    {
        #region Vectors

        /// <summary>
        /// Converts an XNA Vector2 to a SOL Vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns></returns>
        public static Vector ToSolVector(this Vector2 vector)
        {
            return new Vector(vector.X, vector.Y);
        }

        /// <summary>
        /// Converts a SOL Vector to an XNA Vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns></returns>
        public static Vector2 ToXnaVector(this Vector vector)
        {
            return new Vector2(vector.X, vector.Y);
        }

        #endregion

        #region Rectangles

        /// <summary>
        /// Converts a SOL Rectangle to an XNA Rectangle.
        /// </summary>
        /// <param name="rect">The rectangle.</param>
        /// <returns></returns>
        public static XnaRect ToXnaRectangle(this SolRect rect)
        {
            return new XnaRect(rect.X, rect.Y, rect.Width, rect.Height);
        }

        /// <summary>
        /// Converts an XNA Rectangle to a SOL Rectangle.
        /// </summary>
        /// <param name="rect">The rectangle.</param>
        /// <returns></returns>
        public static SolRect ToSolRectangle(this XnaRect rect)
        {
            return new SolRect(rect.X, rect.Y, rect.Width, rect.Height);
        }

        #endregion

        #region Animation Frames

        /// <summary>
        /// Converts a SOL Frame to an XNA Frame.
        /// </summary>
        /// <param name="frame">The frame.</param>
        /// <returns></returns>
        public static XnaFrame ToXnaFrame(this Frame frame)
        {
            return new XnaFrame(frame.TextureID, frame.DrawArea, frame.Origin, frame.TimeTillNext);
        }

        /// <summary>
        /// Converts an XNA Frame to a SOL Frame.
        /// </summary>
        /// <param name="frame">The frame.</param>
        /// <returns></returns>
        public static Frame ToSolFrame(this XnaFrame frame)
        {
            return new Frame(frame.TextureID, frame.DrawArea, frame.Origin, frame.TimeTillNext);
        }

        #endregion

        #region Backdrops

        /// <summary>
        /// Converts a SOL backdrop to an XNA backdrop.
        /// </summary>
        /// <param name="backdrop">The backdrop.</param>
        /// <returns></returns>
        public static XnaBackdrop ToXnaBackdrop(this Backdrop backdrop)
        {
            return new XnaBackdrop(backdrop.TextureID, backdrop.Position, backdrop.Parallax, backdrop.Velocity, backdrop.LoopX, backdrop.LoopY, backdrop.Layer, backdrop.WrapCoordsX, backdrop.WrapCoordsY);
        }

        /// <summary>
        /// Converts an XNA backdrop to a SOL backdrop.
        /// </summary>
        /// <param name="backdrop">The backdrop.</param>
        /// <returns></returns>
        public static Backdrop ToSolBackdrop(this XnaBackdrop backdrop)
        {
            return new Backdrop(backdrop.TextureID, backdrop.Position, backdrop.Parallax, backdrop.Velocity, backdrop.LoopX, backdrop.LoopY, backdrop.Layer, backdrop.WrapCoordsX, backdrop.WrapCoordsY);
        }

        #endregion

        #region Entities
        /// <summary>
        /// Converts a SOL Entity to an XNA entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static XnaEntity ToXnaEntity(this Entity entity)
        {
            XnaEntity xnaentity = new XnaEntity(entity.AnimationCache);

            xnaentity.ID = entity.ID;
            xnaentity.InputType = entity.InputType;
            xnaentity.FacingStyle = entity.FacingStyle;
            xnaentity.Facing = entity.Facing;
            xnaentity.CurrentAnimation = entity.CurrentAnimation;
            xnaentity.IsAnimationOverridden = entity.IsAnimationOverridden;
            xnaentity.ComponentInControl = entity.ComponentInControl;
            xnaentity.Position = entity.Position;
            xnaentity.Velocity = entity.Velocity;
            xnaentity.MovingState = entity.MovingState;
            xnaentity.Hitbox = entity.Hitbox;
            xnaentity.Solid = entity.Solid;
            xnaentity.Persistent = entity.Persistent;
            xnaentity.CanUseDoors = entity.CanUseDoors;

            foreach (var component in entity.Components)
            {
                xnaentity.AddComponent(component.Key, component.Value);
            }

            foreach (var logic in entity.Logics)
            {
                xnaentity.AddLogic(logic.Key, logic.Value);
            }

            xnaentity.Tags = entity.Tags;

            xnaentity.ComponentsToAdd = entity.ComponentsToAdd;
            xnaentity.LogicsToAdd = entity.LogicsToAdd;

            return xnaentity;
        }

        #endregion
    }
}
