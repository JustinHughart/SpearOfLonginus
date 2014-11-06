using System.Windows.Forms;
using System.Xml.Linq;

namespace SOLEntityGenerator
{
    public partial class ComponentForm : Form
    {
        XElement _element;

        public ComponentForm(XElement element)
        {
            InitializeComponent();
            _element = element;
            xmlEditor.LoadXml(_element);
        }

        private void BtnAddClick(object sender, System.EventArgs e)
        {
            xmlEditor.AddNewNode();
        }

        private void BtnEditClick(object sender, System.EventArgs e)
        {
            xmlEditor.EditCurrentNode();
        }

        private void BtnDeleteClick(object sender, System.EventArgs e)
        {
            xmlEditor.DeleteCurrentNode();
        }

        private void SaveToEntityToolStripMenuItemClick(object sender, System.EventArgs e)
        {
            var saveelement = xmlEditor.ConvertToXml();

            _element.Name = saveelement.Name;
            _element.RemoveAll();
            _element.Add(saveelement.Attributes());
            _element.Add(saveelement.Elements());

            Close();
        }

        private void ExitWithoutSavingToolStripMenuItemClick(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}
