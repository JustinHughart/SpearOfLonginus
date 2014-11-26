using System.Xml.Linq;

namespace SpearOfLonginus.Cutscenes
{
    /// <summary>
    /// A cutscene for Spear of Longinus.
    /// </summary>
    public class Cutscene : IXmlLoadable
    {
        #region Variables

        /// <summary>
        /// The current item in the cutscene.
        /// </summary>
        protected CutsceneItem CurrentItem;
        /// <summary>
        /// Whether or not the cutscene is finished.
        /// </summary>
        protected bool IsFinished;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Cutscene"/> class.
        /// </summary>
        public Cutscene()
        {
            IsFinished = false;
        }

        #endregion

        #region Functions

        /// <summary>
        /// Updates the cutscene.
        /// </summary>
        /// <param name="deltatime">The deltatime.</param>
        public virtual void Update(float deltatime)
        {
            if (IsFinished)
            {
                return;
            }

            if (CurrentItem.IsFinished)
            {
                GetNext();
            }

            if (CurrentItem == null)
            {
                IsFinished = true;
                return;
            }

            CurrentItem.Update(deltatime);
        }

        /// <summary>
        /// Gets the next item in the cutscene.
        /// </summary>
        protected virtual void GetNext()
        {
            XElement element = CurrentItem.GetNextElement();

            CurrentItem = XmlLoader.CreateObject(element.Name.LocalName, element) as CutsceneItem;
        }

        /// <summary>
        /// Uses XML to initialize the object.
        /// </summary>
        /// <param name="element">The element used for loading.</param>
        public virtual void LoadFromXml(XElement element)
        {
            //Get the first element.
            foreach (var xelement in element.Elements())
            {
                element = xelement;
                break;
            }
            
            //Creates the first item with it.
            CurrentItem = XmlLoader.CreateObject(element.Name.LocalName, element) as CutsceneItem;
        }

        #endregion

    }
}