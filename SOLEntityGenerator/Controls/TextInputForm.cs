using System.Windows.Forms;

namespace SOLEntityGenerator.Controls
{
    /// <summary>
    /// A class for receiving input from the user.
    /// </summary>
    public partial class TextInputForm : Form
    {
        /// <summary>
        /// The input received from the user.
        /// </summary>
        public string Input;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextInputForm"/> class.
        /// </summary>
        /// <param name="title">The title of the window.</param>
        public TextInputForm(string title)
        {
            InitializeComponent();
            Input = "";
            txtInput.Focus();
            Text = title;
        }

        /// <summary>
        /// The click command for btnOK.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnOKClick(object sender, System.EventArgs e)
        {
            Input = txtInput.Text;
        }
    }
}
