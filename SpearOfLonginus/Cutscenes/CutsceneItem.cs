using System.Collections.Generic;
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
        /// <summary>
        /// The index of which element to use.
        /// </summary>
        protected int DecisionIndex;
        /// <summary>
        /// The elements used for showing the next action.
        /// </summary>
        protected List<XElement> NextElements;  

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CutsceneItem"/> class.
        /// </summary>
        public CutsceneItem()
        {
            IsFinished = false;
            DecisionIndex = 0;
            NextElements = new List<XElement>();
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
        /// Gets the next element for loading the next action.
        /// </summary>
        /// <returns></returns>
        public virtual XElement GetNextElement()
        {
            if (NextElements.Count == 0)
            {
                return null;
            }

            return NextElements[DecisionIndex];
        }

        /// <summary>
        /// Uses XML to initialize the object.
        /// </summary>
        /// <param name="element">The element used for loading.</param>
        public virtual void LoadFromXml(XElement element)
        {
            var nextelement = element.Element("next");

            if (nextelement == null)
            {
                return;
            }

            foreach (var xelement in nextelement.Elements())
            {
                NextElements.Add(xelement);
            }
        }

        #endregion

    }
}
