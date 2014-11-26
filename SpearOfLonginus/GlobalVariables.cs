using System.Collections.Generic;
using System.Xml.Linq;
using SpearOfLonginus.Entities;
using SpearOfLonginus.Maps;
using SpearOfLonginus.UI;

namespace SpearOfLonginus
{
    /// <summary>
    /// A class containing global variables for Spear of Longinus.
    /// </summary>
    public static class GlobalVariables
    {
        /// <summary>
        /// The world the characters exist in.
        /// </summary>
        public static World World;
        /// <summary>
        /// The UI Manager for players.
        /// </summary>
        public static Dictionary<InputType, UIManager> UIManagers;
        /// <summary>
        /// The entity players are controlling.
        /// </summary>
        public static Dictionary<InputType, Entity> Players;
        /// <summary>
        /// The default textbox settings.
        /// </summary>
        public static XElement DefaultTextboxSettings;


        /// <summary>
        /// Initializes this class.
        /// </summary>
        public static void Initialize()
        {
            UIManagers = new Dictionary<InputType, UIManager>();
            Players = new Dictionary<InputType, Entity>();
        }
    }
}
