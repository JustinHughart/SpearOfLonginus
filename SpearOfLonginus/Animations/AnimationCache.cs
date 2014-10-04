using System;
using System.Collections.Generic;

namespace SpearOfLonginus.Animations
{
    /// <summary>
    /// A class that holds animations.
    /// </summary>
    public class AnimationCache
    {
        #region Variables

        /// <summary>
        /// The keyed list of animations.
        /// </summary>
        protected Dictionary<string, Animation> Animations;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AnimationCache" /> class.
        /// </summary>
        public AnimationCache()
        {
            Animations = new Dictionary<string, Animation>();
        }

        #endregion

        #region Functions

        /// <summary>
        /// Adds the animation to the list of animations.
        /// </summary>
        /// <param name="animation">The animation to add.</param>
        public virtual void AddAnimation(Animation animation)
        {
            Animations.Add(animation.ID, animation);
        }

        /// <summary>
        /// Retrieves the animation from the list of animations. Throws an exception if it doesn't exist.
        /// </summary>
        /// <param name="key">The key of the animation you're requesting..</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Animation "key" does not exist.</exception>
        public virtual Animation GetAnimation(string key)
        {
            if (Animations.ContainsKey(key))
            {
                return Animations[key].Clone();
            }

            throw new Exception("Animation \"" + key + "\" does not exist.");
        }

        #endregion
    }
}