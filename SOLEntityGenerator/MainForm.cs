using System;
using System.Windows.Forms;
using System.Xml.Linq;
using SpearOfLonginus.Entities;

namespace SOLEntityGenerator
{
    public partial class MainForm : Form
    {
        public XElement ComponentsElement;
        public XElement LogicsElement;

        public MainForm()
        {
            InitializeComponent();

            SetupNumberBoxes();
            FillComboBoxes();
            CreateNewEntity();
        }

        private void SetupNumberBoxes()
        {
            numHitboxX.Minimum = decimal.MinValue;
            numHitboxX.Maximum = decimal.MaxValue;
            numHitboxY.Minimum = decimal.MinValue;
            numHitboxY.Maximum = decimal.MaxValue;
            numHitboxW.Maximum = decimal.MaxValue;
            numHitboxH.Maximum = decimal.MaxValue;
        }

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

        private void CreateNewEntity()
        {
            ComponentsElement = new XElement("components");
            LogicsElement = new XElement("logics");
        }

        private void NewToolStripMenuItemClick(object sender, EventArgs e)
        {

        }

        private void OpenToolStripMenuItemClick(object sender, EventArgs e)
        {

        }

        private void SaveToolStripMenuItemClick(object sender, EventArgs e)
        {

        }

        private void SaveAsToolStripMenuItemClick(object sender, EventArgs e)
        {

        }

        private void ExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            
        }

        private void BtnComponentsClick(object sender, EventArgs e)
        {
            ComponentForm form = new ComponentForm(ComponentsElement);
            form.Show();
        }

        private void BtnLogicsClick(object sender, EventArgs e)
        {
            ComponentForm form = new ComponentForm(LogicsElement);
            form.Show();
        }
    }
}
