using SpearOfLonginus.Entities;

namespace SpearOfLonginus.Maps
{
    /// <summary>
    /// A door used for moving between maps in Spear of Longinus.
    /// </summary>
    public class Door
    {
        /// <summary>
        /// The area the door exists in.
        /// </summary>
        public Rectangle Hitbox;
        /// <summary>
        /// The target map.
        /// </summary>
        public string TargetMap;
        /// <summary>
        /// The target position.
        /// </summary>
        protected Vector TargetPosition;

        /// <summary>
        /// Initializes a new instance of the <see cref="Door"/> class.
        /// </summary>
        /// <param name="hitbox">The area the door exists in.</param>
        /// <param name="targetmap">The target map.</param>
        /// <param name="targetposition">The target position.</param>
        public Door(Rectangle hitbox, string targetmap, Vector targetposition)
        {
            Hitbox = hitbox;
            TargetMap = targetmap;
            TargetPosition = targetposition;
        }

        /// <summary>
        /// Returns whether or not an entity is in a door.
        /// </summary>
        /// <param name="entity">The entity to check.</param>
        /// <returns></returns>
        public virtual bool EntityInDoor(Entity entity)
        {
            return Hitbox.Intersects(entity.Hitbox);
        }

        /// <summary>
        /// Gets the target position.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual Vector GetTargetPosition(Entity entity)
        {
            return TargetPosition + (entity.Position - new Vector(Hitbox.X, Hitbox.Y));
        }
    }
}
