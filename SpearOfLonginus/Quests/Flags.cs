using System;
using System.Collections.Generic;

namespace SpearOfLonginus.Quests
{
    /// <summary>
    /// A collection of flags used for scenario control, such as dialogues and opened chests.
    /// </summary>
    public static class Flags
    {
        /// <summary>
        /// The stored flags. Does not hold any blank flags.
        /// </summary>
        private static Dictionary<string, short> _flags;

        /// <summary>
        /// Initializes this class.
        /// </summary>
        public static void Initialize()
        {
            _flags = new Dictionary<string, short>();
        }

        /// <summary>
        /// Checks if the class is initialized.
        /// </summary>
        /// <exception cref="System.Exception">Flags class is not initialized.</exception>
        private static void CheckInitialized()
        {
            if (_flags == null)
            {
                throw new Exception("Flags class is not initialized.");
            }
        }

        /// <summary>
        /// Sets the flag.
        /// </summary>
        /// <param name="key">The flag's key.</param>
        /// <param name="value">The flag's value.</param>
        public static void SetFlag(string key, short value)
        {
            CheckInitialized();

            if (_flags.ContainsKey(key))
            {
                RemoveFlag(key);
            }

            _flags.Add(key, value);
        }

        /// <summary>
        /// Removes the flag from the pool of flags.
        /// </summary>
        /// <param name="key">The flag's key.</param>
        public static void RemoveFlag(string key)
        {
            CheckInitialized();

            _flags.Remove(key);
        }

        /// <summary>
        /// Gets the flag from the pool of flags.
        /// </summary>
        /// <param name="key">The flag's key.</param>
        /// <returns></returns>
        public static short? GetFlag(string key)
        {
            CheckInitialized();

            if (!_flags.ContainsKey(key))
            {
                return null;
            }
            
            return _flags[key];
        }
    }
}