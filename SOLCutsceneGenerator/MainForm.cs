using System;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using SOLEntityGenerator;

namespace SOLCutsceneGenerator
{
    /// <summary>
    /// The main form of the too,.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// The path to the file we're working on.
        /// </summary>
        private string _filepath;
        /// <summary>
        /// The cutscene element
        /// </summary>
        public XElement CutsceneElement;
       
        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            CreateNewCutscene();
        }

        /// <summary>
        /// Creates a new cutscene.
        /// </summary>
        private void CreateNewCutscene()
        {
            _filepath = "";
            ChangeTitle();

            CutsceneElement = new XElement("next");

            txtID.Text = "";
        }

        /// <summary>
        /// Changes the title of the window.
        /// </summary>
        private void ChangeTitle()
        {
            Text = "SoL Cutscene Generator - " + _filepath;

            if (_filepath.Equals(""))
            {
                Text += "New Cutscene";
            }
        }

        /// <summary>
        /// Triggers when the new button is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void NewToolStripMenuItemClick(object sender, EventArgs e)
        {
            CreateNewCutscene();
        }

        /// <summary>
        /// Triggers when the open button is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OpenToolStripMenuItemClick(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Spear of Longinus Cutscene (*.slc)|*.slc|Spear of Longinus Gzipped Cutscene (*.gzc)|*.gzc";
            ofd.ShowDialog();

            if (ofd.FileName == "")
            {
                return;
            }

            CreateNewCutscene(); //Initialize the data.

            _filepath = ofd.FileName;

            ChangeTitle();

            LoadFromFile(_filepath);
        }

        /// <summary>
        /// Triggers when the save button is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void SaveToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (_filepath == "")
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Spear of Longinus Cutscene (*.slc)|*.slc|Spear of Longinus Gzipped Cutscene (*.gzc)|*.gzc";
                sfd.ShowDialog();

                if (sfd.FileName != "")
                {
                    _filepath = sfd.FileName;

                    ChangeTitle();
                }
            }

            if (_filepath != "")
            {
                SaveToFile(_filepath);
            }
        }

        /// <summary>
        /// Triggers when the save as button is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void SaveAsToolStripMenuItemClick(object sender, EventArgs e)
        {
            bool valid = false;

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Spear of Longinus Cutscene (*.slc)|*.slc|Spear of Longinus Gzipped Cutscene (*.gzc)|*.gzc";
            sfd.ShowDialog();

            if (sfd.FileName != "")
            {
                valid = true;
                _filepath = sfd.FileName;
                ChangeTitle();
            }

            if (valid)
            {
                SaveToFile(_filepath);
            }
        }

        /// <summary>
        /// Triggers when the exit button is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            Close();
        }

       /// <summary>
        /// Saves to file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <exception cref="System.ArgumentException">File type not supported.</exception>
        private void SaveToFile(string path)
        {
            path = ValidatePath(path);

            XElement saveelement = new XElement("cutscene");

            //Basic attributes
            saveelement.Add(new XAttribute("id", txtID.Text));

            //Custom XML.
            saveelement.Add(CutsceneElement);

            //First we'll back up the old file.
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

            //Then save it!
            var doc = new XDocument(saveelement);

            if (path.Contains(".slc"))
            {
                using (var stream = File.OpenWrite(path))
                {
                    doc.Save(stream);
                }

                return;
            }

            if (path.Contains(".gzc"))
            {
                using (var stream = File.OpenWrite(path))
                {
                    using (var gzipstream = new GZipStream(stream, CompressionMode.Compress))
                    {
                        doc.Save(gzipstream);
                    }
                }

                return;
            }

            throw new ArgumentException("File type not supported.");
        }

        /// <summary>
        /// Loads from file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <exception cref="System.ArgumentException">File type not supported.</exception>
        /// <exception cref="System.Xml.XmlException">No root.
        /// or
        /// Root is not an cutscene.</exception>
        private void LoadFromFile(string path)
        {
            path = ValidatePath(path);

            XDocument doc = null;

            //Load the doc.

            if (path.Contains(".slc"))
            {
                doc = XDocument.Load(path);
            }
            else if (path.Contains(".gzc"))
            {
                using (var filestream = File.OpenRead(path))
                {
                    using (var gzipstream = new GZipStream(filestream, CompressionMode.Decompress))
                    {
                        doc = XDocument.Load(gzipstream);
                    }
                }
            }
            else
            {
                throw new ArgumentException("File type not supported.");
            }

            //Initial disqualifiers.

            if (doc.Root == null)
            {
                throw new XmlException("No root.");
            }

            if (!doc.Root.Name.LocalName.Equals("cutscene", StringComparison.OrdinalIgnoreCase))
            {
                throw new XmlException("Root is not an cutscene.");
            }

            //First, we'll get the attributes.
            foreach (var attribute in doc.Root.Attributes())
            {
                if (attribute.Name.LocalName.Equals("id", StringComparison.OrdinalIgnoreCase))
                {
                    txtID.Text = attribute.Value;
                    continue;
                }
            }

           //Assign the custom xml elements.

            if (doc.Root.Element("next") != null)
            {
                CutsceneElement = doc.Root.Element("next");
            }
        }

        /// <summary>
        /// Validates the path.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        private string ValidatePath(string input)
        {
            while (input.EndsWith(" "))
            {
                input.Remove(input.Length - 2);
            }

            return input;
        }

        /// <summary>
        /// Triggers when the cutscene button is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void BtnCutsceneClick(object sender, EventArgs e)
        {
            var form = new ComponentForm(CutsceneElement);
            form.Show();
        }
    }
}
