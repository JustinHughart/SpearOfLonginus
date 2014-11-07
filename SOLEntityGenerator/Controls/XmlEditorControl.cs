using System;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SOLEntityGenerator.Controls
{
    /// <summary>
    /// An extension of TreeView designed for editing XML.
    /// </summary>
    public partial class XmlEditorControl : TreeView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XmlEditorControl" /> class.
        /// </summary>
        public XmlEditorControl()
        {
            InitializeComponent();
            HideSelection = false;
            LabelEdit = true;
            BeforeLabelEdit += CheckIfEditIsValid;
            AfterLabelEdit += CheckIfLabelIsValid;
        }

        /// <summary>
        /// Loads the XML into the tree.
        /// </summary>
        /// <param name="element">The element.</param>
        public void LoadXml(XElement element)
        {
            Nodes.Clear();
            RecurseLoad(element, null);
        }

        /// <summary>
        /// Loads the XML into the tree.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="parent">The parent to add the contents of element to.</param>
        public void LoadXml( XElement element, TreeNode parent)
        {
            RecurseLoad(element, parent);
        }

        /// <summary>
        /// Loads XML recursively.
        /// </summary>
        /// <param name="element">The element to load.</param>
        /// <param name="parent">The element's parent.</param>
        private void RecurseLoad(XElement element, TreeNode parent)
        {
            //Load attributes.
            TreeNode node = new TreeNode(element.Name.ToString());
            TreeNode attributenode = new TreeNode("Attributes");
            node.Nodes.Add(attributenode);

            var attributes = element.Attributes();

            foreach (var attribute in attributes)
            {
                TreeNode namenode = new TreeNode(attribute.Name.ToString());
                attributenode.Nodes.Add(namenode);
                namenode.Expand();
                TreeNode valuenode = new TreeNode(attribute.Value);
                namenode.Nodes.Add(valuenode);
                valuenode.Expand();
            }

            attributenode.Expand();

            if (parent == null)
            {
                Nodes.Add(node);
            }
            else
            {
                parent.Nodes.Add(node);
            }

            //Load elements.
            var elementsnode = new TreeNode("Elements");
            node.Nodes.Add(elementsnode);
            node.Expand();

            if (node.Parent != null)
            {
                node.Parent.Expand();
            }

            if (element.HasElements)
            {
                foreach (var child in element.Elements())
                {
                    RecurseLoad(child, elementsnode);
                }
            }
        }

        /// <summary>
        /// Converts the contents of the entire tree to XML.
        /// </summary>
        /// <returns></returns>
        public XElement ConvertToXml()
        {
            return RecurseSave(null, Nodes[0]);
        }

        /// <summary>
        /// Converts the contents of the node to XML.
        /// </summary>
        /// <param name="node">The node to convert.</param>
        /// <returns></returns>
        public XElement ConvertToXml(TreeNode node)
        {
            return RecurseSave(null, node);
        }

        /// <summary>
        /// Recurses the contents of the tree to XML recursively.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="node">The node.</param>
        /// <returns></returns>
        private XElement RecurseSave(XElement parent, TreeNode node)
        {
            XElement newnode = new XElement(node.Text);

            if (parent != null)
            {
                parent.Add(newnode);
            }

            var attribnode = node.Nodes[0];

            foreach (TreeNode attrib in attribnode.Nodes)
            {
                newnode.Add(new XAttribute(attrib.Text, attrib.Nodes[0].Text));
            }

            var elenode = node.Nodes[1];

            foreach (TreeNode child in elenode.Nodes)
            {
                RecurseSave(newnode, child);
            }

            return newnode;
        }

        /// <summary>
        /// Gets text inputted from the user
        /// </summary>
        /// <returns></returns>
        protected string GetTextInput()
        {
            return GetTextInput("Please enter an ID for the new node.");
        }

        /// <summary>
        /// Gets text inputted from the user
        /// </summary>
        /// <param name="title">The title of the window.</param>
        /// <returns></returns>
        protected string GetTextInput(string title)
        {
            TextInputForm input = new TextInputForm(title);
            input.ShowDialog();

            if (input.DialogResult == DialogResult.OK)
            {
                return input.Input;
            }

            return "";
        }

        /// <summary>
        /// Adds a new node to the selected node.
        /// </summary>
        public void AddNewNode()
        {
            if (SelectedNode == null)
            {
                MessageBox.Show("Please select a node first.");
                return;
            }

            if (!SelectedNode.Text.Equals("Attributes", StringComparison.OrdinalIgnoreCase) && !SelectedNode.Text.Equals("Elements", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Please select either an 'attributes' or 'elements' node before attempting to add an item.");
                return;
            }

            String nodename = GetTextInput();

            if (nodename != "")
            {
                foreach (TreeNode node in SelectedNode.Nodes)
                {
                    if (node.Text == nodename)
                    {
                        MessageBox.Show("Node already exists.");
                        return;
                    }
                }

                if (nodename.Equals("attributes", StringComparison.OrdinalIgnoreCase) || nodename.Equals("elements", StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Name is invalid.");
                    return;
                }

                var newnode = new TreeNode(nodename);
                SelectedNode.Nodes.Add(newnode);
                SelectedNode.Expand();

                if (SelectedNode.Text.Equals("Attributes", StringComparison.OrdinalIgnoreCase))
                {
                    var valuenode = new TreeNode(GetTextInput("Please enter the value of the new attribute."));
                    newnode.Nodes.Add(valuenode);
                    newnode.Expand();
                }

                if (SelectedNode.Text.Equals("Elements", StringComparison.OrdinalIgnoreCase))
                {
                    var attribnode = new TreeNode("Attributes");
                    var elenode = new TreeNode("Elements");

                    newnode.Nodes.Add(attribnode);
                    newnode.Nodes.Add(elenode);

                    newnode.Expand();
                }
            }
        }

        /// <summary>
        /// Deletes the current node.
        /// </summary>
        public void DeleteCurrentNode()
        {
            if (SelectedNode == null)
            {
                MessageBox.Show("Please select a node first.");
                return;
            }

            if (SelectedNode.Parent == null || SelectedNode == Nodes[0])
            {
                MessageBox.Show("Cannot delete this node.");
                return;
            }

            if (!SelectedNode.Parent.Text.Equals("Attributes", StringComparison.OrdinalIgnoreCase) && !SelectedNode.Parent.Text.Equals("Elements", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Cannot delete this node.");
                return;
            }

            var result = MessageBox.Show("Are you sure you want to delete this node and all subnodes?", "Really delete?", MessageBoxButtons.OKCancel);

            if (result == DialogResult.OK)
            {
                SelectedNode.Parent.Nodes.Remove(SelectedNode);
                MessageBox.Show("Node deleted.");
            }
        }

        /// <summary>
        /// Determines whether the current node is an element.
        /// </summary>
        /// <returns></returns>
        public bool IsCurrentNodeChildElement()
        {
            if (SelectedNode == null)
            {
                return false;
            }

            if (SelectedNode.Parent == null)
            {
                return false;
            }

            return SelectedNode.Parent.Text.Equals("Elements", StringComparison.OrdinalIgnoreCase);
        }

        public bool IsCurrentNodeElement()
        {
            return SelectedNode.Text.Equals("Elements", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Checks if node is edittable.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void CheckIfEditIsValid(object sender, EventArgs e)
        {
            if (SelectedNode.Text.Equals("Attributes", StringComparison.OrdinalIgnoreCase) || SelectedNode.Text.Equals("Elements", StringComparison.OrdinalIgnoreCase) || SelectedNode == Nodes[0])
            {
                SelectedNode.EndEdit(true);
            }
        }

        /// <summary>
        /// Checks if the label is valid before allowing the edit to go through..
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="NodeLabelEditEventArgs"/> instance containing the event data.</param>
        private void CheckIfLabelIsValid(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label == null)
            {
                return;
            }

            if (e.Label.Equals("Attributes", StringComparison.OrdinalIgnoreCase) || e.Label.Equals("Elements", StringComparison.OrdinalIgnoreCase) || e.Label == "")
            {
                e.CancelEdit = true;
            }
        }
    }
}
