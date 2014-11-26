using System.Xml.Linq;

namespace SpearOfLonginus.Cutscenes
{
    /// <summary>
    /// An item for a cutscene. 
    /// </summary>
    public class CutsceneItem : IXmlLoadable
    {
        #region Variables

        /// <summary>
        /// Whether or not the item is finished doing what it needs to do.
        /// </summary>
        public bool IsFinished;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CutsceneItem"/> class.
        /// </summary>
        public CutsceneItem()
        {
            IsFinished = false;
        }

        #endregion

        #region Functions

        /// <summary>
        /// Updates the item.
        /// </summary>
        /// <param name="deltatime">The time since last update.</param>
        public virtual void Update(float deltatime)
        {

        }

        /// <summary>
        /// Uses XML to initialize the object.
        /// </summary>
        /// <param name="element">The element used for loading.</param>
        public virtual void LoadFromXml(XElement element)
        {

        }

        #endregion

    }
}
