using System;
using SpearOfLonginus;
using SpearOfLonginus.Entities;

namespace SOLXNA.Cameras
{
    /// <summary>
    /// A camera designed to snap to an entity's position.
    /// </summary>
    public class EntitySnapCamera : XnaCamera
    {
        /// <summary>
        /// The target of the camera.
        /// </summary>
        public InputType Target;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntitySnapCamera"/> class.
        /// </summary>
        /// <param name="screenresolution">The screenresolution.</param>
        /// <param name="mapsize">The mapsize.</param>
        /// <param name="drawborder">The drawborder.</param>
        /// <param name="target">The target.</param>
        public EntitySnapCamera(Vector screenresolution, Vector mapsize, Vector drawborder, InputType target)
            : base(screenresolution, mapsize, drawborder)
        {
            Target = target;
            Position = GetTargetPosition();
        }

        /// <summary>
        /// Updates the camera.
        /// </summary>
        /// <param name="deltatime">The delta time.</param>
        public override void Update(float deltatime)
        {
            Position = GetTargetPosition();

            base.Update(deltatime);
        }

        /// <summary>
        /// Gets the target's position.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">Input type must be a player.</exception>
        protected virtual Vector GetTargetPosition()
        {
            return GlobalVariables.Players[Target].Position;
        }
    }
}
