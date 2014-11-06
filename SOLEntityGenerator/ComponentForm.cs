using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SOLEntityGenerator
{
    /// <summary>
    /// A form for handling the creation of XML for components.
    /// </summary>
    public partial class ComponentForm : Form
    {
        /// <summary>
        /// The folder that templates are contained in.
        /// </summary>
        string _templatefolder;

        /// <summary>
        /// The element used for saving and loading.
        /// </summary>
        XElement _element;

        /// <summary>
        /// Initializes a new instance of the <see cref="ComponentForm"/> class.
        /// </summary>
        /// <param name="apppath">The application path.</param>
        /// <param name="element">The element to initialize data with.</param>
        public ComponentForm( string apppath, XElement element)
        {
            _templatefolder = apppath + "\\Templates";
            InitializeComponent();
            _element = element;
            xmlEditor.LoadXml(_element);
        }

        /// <summary>
        /// Triggers when the add button is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnAddClick(object sender, System.EventArgs e)
        {
            xmlEditor.AddNewNode();
        }

        /// <summary>
        /// Triggers when the delete button is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnDeleteClick(object sender, System.EventArgs e)
        {
            xmlEditor.DeleteCurrentNode();
        }

        /// <summary>
        /// Triggers when the save to entity button is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void SaveToEntityToolStripMenuItemClick(object sender, System.EventArgs e)
        {
            var saveelement = xmlEditor.ConvertToXml();

            _element.Name = saveelement.Name;
            _element.RemoveAll();
            _element.Add(saveelement.Attributes());
            _element.Add(saveelement.Elements());

            Close();
        }

        /// <summary>
        /// Triggers when the exit without saving button is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ExitWithoutSavingToolStripMenuItemClick(object sender, System.EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Triggers when the load template from tool strip button is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void LoadTemplateToolStripMenuItemClick(object sender, System.EventArgs e)
        {
            CheckTemplateFolder();

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.RestoreDirectory = true;
            ofd.InitialDirectory = _templatefolder;
            ofd.Filter = "Spear of Longinus Template | *.slt";
            ofd.ShowDialog();

            if (ofd.FileName == "")
            {
                return;
            }

            var doc = XDocument.Load(ofd.FileName);

            xmlEditor.LoadXml(doc.Root, xmlEditor.Nodes[0]);
        }

        /// <summary>
        /// Triggers when the save to template button is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void SaveToTemplateToolStripMenuItemClick(object sender, System.EventArgs e)
        {
            CheckTemplateFolder();

            if (!xmlEditor.IsChildElementOfRoot())
            {
                MessageBox.Show("Please select a child element of the root.");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.RestoreDirectory = true;
            sfd.InitialDirectory = _templatefolder;
            sfd.Filter = "Spear of Longinus Template | *.slt";
            sfd.ShowDialog();

            if (sfd.FileName == "")
            {
                return;
            }

            string path = sfd.FileName;
            
            if (File.Exists(path))
            {
                string backuppath = path + ".bak";

                if (File.Exists(backuppath))
                {
                    File.Delete(backuppath);
                }

                File.Copy(path, backuppath);
                File.Delete(path);
            }

            XDocument doc = new XDocument(xmlEditor.ConvertToXml(xmlEditor.SelectedNode));

            using (var stream = File.OpenWrite(path))
            {
                doc.Save(stream);
            }
        }

        /// <summary>
        /// Checks the template folder.
        /// </summary>
        private void CheckTemplateFolder()
        {
            if (!Directory.Exists(_templatefolder))
            {
                Directory.CreateDirectory(_templatefolder);
            }
        }
    }
}
