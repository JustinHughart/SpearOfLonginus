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
        public Entity Target;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntitySnapCamera"/> class.
        /// </summary>
        /// <param name="screenresolution">The screenresolution.</param>
        /// <param name="mapsize">The mapsize.</param>
        /// <param name="drawborder">The drawborder.</param>
        /// <param name="target">The target.</param>
        public EntitySnapCamera(Vector screenresolution, Vector mapsize, Vector drawborder, Entity target) : base(screenresolution, mapsize, drawborder)
        {
            Target = target;
            Position = Target.Position;
        }

        /// <summary>
        /// Updates the camera.
        /// </summary>
        /// <param name="deltatime">The delta time.</param>
        public override void Update(float deltatime)
        {
            Position = Target.Position;

            base.Update(deltatime);
        }
    }
}
