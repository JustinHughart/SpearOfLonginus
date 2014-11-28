using System;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using SpearOfLonginus.Entities;

namespace SOLEntityGenerator
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
        /// The components element
        /// </summary>
        public XElement ComponentsElement;
        /// <summary>
        /// The logics element
        /// </summary>
        public XElement LogicsElement;
        /// <summary>
        /// The animations element
        /// </summary>
        public XElement AnimationsElement;
        /// <summary>
        /// The custom element
        /// </summary>
        public XElement CustomElement;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            SetupNumberBoxes();
            FillComboBoxes();
            CreateNewEntity();
        }

        /// <summary>
        /// Sets up the number boxes.
        /// </summary>
        private void SetupNumberBoxes()
        {
            numHitboxX.Minimum = decimal.MinValue;
            numHitboxX.Maximum = decimal.MaxValue;
            numHitboxY.Minimum = decimal.MinValue;
            numHitboxY.Maximum = decimal.MaxValue;
            numHitboxW.Maximum = decimal.MaxValue;
            numHitboxH.Maximum = decimal.MaxValue;
        }

        /// <summary>
        /// Fills the combo boxes.
        /// </summary>
        private void FillComboBoxes()
        {
            //Input Type
            cboInputType.Items.Add(InputType.NPC);
            cboInputType.Items.Add(InputType.Player1);
            cboInputType.Items.Add(InputType.Player2);
            cboInputType.Items.Add(InputType.Player3);
            cboInputType.Items.Add(InputType.Player4);
            cboInputType.Items.Add(InputType.World);
            cboInputType.SelectedIndex = 0;

            //Facing Style
            cboFacingStyle.Items.Add(FacingStyle.Static);
            cboFacingStyle.Items.Add(FacingStyle.FourWay);
            cboFacingStyle.Items.Add(FacingStyle.EightWay);
            cboFacingStyle.SelectedIndex = 2;

            //Facing
            cboFacing.Items.Add(FacingState.North);
            cboFacing.Items.Add(FacingState.South);
            cboFacing.Items.Add(FacingState.East);
            cboFacing.Items.Add(FacingState.West);
            cboFacing.Items.Add(FacingState.Northwest);
            cboFacing.Items.Add(FacingState.Northeast);
            cboFacing.Items.Add(FacingState.Southwest);
            cboFacing.Items.Add(FacingState.Southeast);
            cboFacing.SelectedIndex = 1;
        }

        /// <summary>
        /// Creates a new entity.
        /// </summary>
        private void CreateNewEntity()
        {
            _filepath = "";
            ChangeTitle();

            ComponentsElement = new XElement("components");
            LogicsElement = new XElement("logics");
            AnimationsElement = new XElement("animations");
            CustomElement = new XElement("custom");

            txtID.Text = "";
            cboInputType.SelectedIndex = 0;
            cboFacingStyle.SelectedIndex = 2;
            cboFacing.SelectedIndex = 1;

            numHitboxX.Value = 0;
            numHitboxY.Value = 0;
            numHitboxW.Value = 1;
            numHitboxH.Value = 1;

            chkPersistent.Checked = false;
            chkCanUseDoors.Checked = false;
            chkFloorEffects.Checked = false;
        }

        /// <summary>
        /// Changes the title of the window.
        /// </summary>
        private void ChangeTitle()
        {
            Text = "SoL Entity Generator - " + _filepath;

            if (_filepath.Equals(""))
            {
                Text += "New Entity";
            }
        }

        /// <summary>
        /// Triggers when the new button is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void NewToolStripMenuItemClick(object sender, EventArgs e)
        {
            CreateNewEntity();
        }

        /// <summary>
        /// Triggers when the open button is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OpenToolStripMenuItemClick(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Spear of Longinus Entity (*.sle)|*.sle|Spear of Longinus Gzipped Entity (*.gze)|*.gze";
            ofd.ShowDialog();

            if (ofd.FileName == "")
            {
                return;
            }

            CreateNewEntity(); //Initialize the data.

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
                sfd.Filter = "Spear of Longinus Entity (*.sle)|*.sle|Spear of Longinus Gzipped Entity (*.gze)|*.gze";
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
            sfd.Filter = "Spear of Longinus Entity (*.sle)|*.sle|Spear of Longinus Gzipped Entity (*.gze)|*.gze";
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
        /// Triggers when the components button is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void BtnComponentsClick(object sender, EventArgs e)
        {
            ComponentForm form = new ComponentForm(ComponentsElement);
            form.Show();
        }

        /// <summary>
        /// Triggers when the logics button is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void BtnLogicsClick(object sender, EventArgs e)
        {
            ComponentForm form = new ComponentForm(LogicsElement);
            form.Show();
        }

        /// <summary>
        /// Triggers when the animations button is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void BtnAnimationsClick(object sender, EventArgs e)
        {
            ComponentForm form = new ComponentForm(AnimationsElement);
            form.Show();
        }

        /// <summary>
        /// Triggers when the custom button is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void BtnCustomClick(object sender, EventArgs e)
        {
            ComponentForm form = new ComponentForm(CustomElement);
            form.Show();
        }

        /// <summary>
        /// Saves to file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <exception cref="System.ArgumentException">File type not supported.</exception>
        private void SaveToFile(string path)
        {
            path = ValidatePath(path);

            XElement saveelement = new XElement("entity");

            //Basic attributes
            saveelement.Add(new XAttribute("id", txtID.Text));
            saveelement.Add(new XAttribute("inputtype", cboInputType.SelectedItem));
            saveelement.Add(new XAttribute("facingstyle", cboFacingStyle.SelectedItem));
            saveelement.Add(new XAttribute("facing", cboFacing.SelectedItem));
            saveelement.Add(new XAttribute("persistent", chkPersistent.Checked));
            saveelement.Add(new XAttribute("canusedoors", chkCanUseDoors.Checked));
            saveelement.Add(new XAttribute("triggerflooreffects", chkFloorEffects.Checked));
            
            //Hitbox
            XElement hitboxelement = new XElement("hitbox");
            hitboxelement.Add(new XAttribute("x", numHitboxX.Value));
            hitboxelement.Add(new XAttribute("y", numHitboxY.Value));
            hitboxelement.Add(new XAttribute("w", numHitboxW.Value));
            hitboxelement.Add(new XAttribute("h", numHitboxH.Value));
            hitboxelement.Add(new XAttribute("solid", chkSolid.Checked));
            saveelement.Add(hitboxelement);

            //Custom XML.
            saveelement.Add(ComponentsElement);
            saveelement.Add(LogicsElement);
            saveelement.Add(AnimationsElement);
            saveelement.Add(CustomElement);

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

            if (path.Contains(".sle"))
            {
                using (var stream = File.OpenWrite(path))
                {
                    doc.Save(stream);
                }

                return;
            }

            if (path.Contains(".gze"))
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
        /// <exception cref="System.Xml.XmlException">
        /// No root.
        /// or
        /// Root is not an entity.
        /// </exception>
        private void LoadFromFile(string path)
        {
            path = ValidatePath(path);

            XDocument doc = null;

            //Load the doc.

            if (path.Contains(".sle"))
            {
                doc = XDocument.Load(path);
            }
            else if (path.Contains(".gze"))
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

            if (!doc.Root.Name.LocalName.Equals("entity", StringComparison.OrdinalIgnoreCase))
            {
                throw new XmlException("Root is not an entity.");
            }

            //First, we'll get the attributes.
            foreach (var attribute in doc.Root.Attributes())
            {
                if (attribute.Name.LocalName.Equals("id", StringComparison.OrdinalIgnoreCase))
                {
                    txtID.Text = attribute.Value;
                    continue;
                }

                if (attribute.Name.LocalName.Equals("inputtype", StringComparison.OrdinalIgnoreCase))
                {
                    InputType input;

                    if (InputType.TryParse(attribute.Value, true, out input))
                    {
                        cboInputType.SelectedIndex = cboInputType.Items.IndexOf(input);
                    }

                    continue;
                }

                if (attribute.Name.LocalName.Equals("facingstyle", StringComparison.OrdinalIgnoreCase))
                {
                    FacingStyle input;

                    if (FacingStyle.TryParse(attribute.Value, true, out input))
                    {
                        cboFacingStyle.SelectedIndex = cboFacingStyle.Items.IndexOf(input);
                    }

                    continue;
                }

                if (attribute.Name.LocalName.Equals("facing", StringComparison.OrdinalIgnoreCase))
                {
                    FacingState input;

                    if (FacingState.TryParse(attribute.Value, true, out input))
                    {
                        cboFacing.SelectedIndex = cboFacing.Items.IndexOf(input);
                    }

                    continue;
                }

                if (attribute.Name.LocalName.Equals("persistent", StringComparison.OrdinalIgnoreCase))
                {
                    bool value;

                    if (bool.TryParse(attribute.Value, out value))
                    {
                        chkPersistent.Checked = value;
                    }

                    continue;
                }

                if (attribute.Name.LocalName.Equals("canusedoors", StringComparison.OrdinalIgnoreCase))
                {
                    bool value;

                    if (bool.TryParse(attribute.Value, out value))
                    {
                        chkCanUseDoors.Checked = value;
                    }

                    continue;
                }

                if (attribute.Name.LocalName.Equals("triggerflooreffects", StringComparison.OrdinalIgnoreCase))
                {
                    bool value;

                    if (bool.TryParse(attribute.Value, out value))
                    {
                        chkFloorEffects.Checked = value;
                    }

                    continue;
                }
            }

            //Now we'll do the hitbox.
            XElement hitboxelement = doc.Root.Element("hitbox");

            if (hitboxelement != null)
            {
                foreach (var attribute in hitboxelement.Attributes())
                {
                    if (attribute.Name.LocalName.Equals("x", StringComparison.OrdinalIgnoreCase))
                    {
                        decimal value;

                        if (decimal.TryParse(attribute.Value, out value))
                        {
                            numHitboxX.Value = value;
                        }
                    }

                    if (attribute.Name.LocalName.Equals("y", StringComparison.OrdinalIgnoreCase))
                    {
                        decimal value;

                        if (decimal.TryParse(attribute.Value, out value))
                        {
                            numHitboxY.Value = value;
                        }
                    }

                    if (attribute.Name.LocalName.Equals("w", StringComparison.OrdinalIgnoreCase))
                    {
                        decimal value;

                        if (decimal.TryParse(attribute.Value, out value))
                        {
                            numHitboxW.Value = value;
                        }
                    }

                    if (attribute.Name.LocalName.Equals("h", StringComparison.OrdinalIgnoreCase))
                    {
                        decimal value;

                        if (decimal.TryParse(attribute.Value, out value))
                        {
                            numHitboxH.Value = value;
                        }
                    }

                    if (attribute.Name.LocalName.Equals("solid", StringComparison.OrdinalIgnoreCase))
                    {
                        bool value;

                        if (bool.TryParse(attribute.Value, out value))
                        {
                            chkSolid.Checked = value;
                        }
                    }
                }
            }

            //Assign the custom xml elements.

            if (doc.Root.Element("components") != null)
            {
                ComponentsElement = doc.Root.Element("components");
            }

            if (doc.Root.Element("logics") != null)
            {
                LogicsElement = doc.Root.Element("logics");
            }

            if (doc.Root.Element("animations") != null)
            {
                AnimationsElement = doc.Root.Element("animations");
            }

            if (doc.Root.Element("custom") != null)
            {
                CustomElement = doc.Root.Element("custom");
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
    }
}
