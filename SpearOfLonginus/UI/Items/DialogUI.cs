using System.Xml.Linq;
using SpearOfLonginus.UI.Data;

namespace SpearOfLonginus.UI.Items
{
    public class DialogUI : UIItem
    {
        #region Variables

        protected Textbox Namebox;
        protected Textbox Textbox;
        

        #endregion

        #region Constructors

        #endregion

        #region Functions

        public override void Update(float deltatime)
        {
            if (Textbox != null)
            {
                Textbox.Update(deltatime);
            }

            if (Namebox != null)
            {
                Namebox.Update(deltatime);
            }
        }


        public override void LoadFromXml(XElement element)
        {
            



        } 

        #endregion

    }
}
