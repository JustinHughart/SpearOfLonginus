namespace SOLEntityGenerator
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.cboFacing = new System.Windows.Forms.ComboBox();
            this.lblFacing = new System.Windows.Forms.Label();
            this.cboFacingStyle = new System.Windows.Forms.ComboBox();
            this.lblFacingStyle = new System.Windows.Forms.Label();
            this.cboInputType = new System.Windows.Forms.ComboBox();
            this.lblInput = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.lblID = new System.Windows.Forms.Label();
            this.menuFileMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel3 = new System.Windows.Forms.Panel();
            this.chkSolid = new System.Windows.Forms.CheckBox();
            this.lblSolid = new System.Windows.Forms.Label();
            this.lblHitboxH = new System.Windows.Forms.Label();
            this.lblHitboxW = new System.Windows.Forms.Label();
            this.lblHitboxY = new System.Windows.Forms.Label();
            this.lblHitboxX = new System.Windows.Forms.Label();
            this.numHitboxH = new System.Windows.Forms.NumericUpDown();
            this.numHitboxW = new System.Windows.Forms.NumericUpDown();
            this.numHitboxY = new System.Windows.Forms.NumericUpDown();
            this.numHitboxX = new System.Windows.Forms.NumericUpDown();
            this.btnComponents = new System.Windows.Forms.Button();
            this.btnLogics = new System.Windows.Forms.Button();
            this.btnAnimations = new System.Windows.Forms.Button();
            this.btnCustom = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblPersistent = new System.Windows.Forms.Label();
            this.chkPersistent = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.menuFileMenu.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHitboxH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHitboxW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHitboxY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHitboxX)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.cboFacing);
            this.panel1.Controls.Add(this.lblFacing);
            this.panel1.Controls.Add(this.cboFacingStyle);
            this.panel1.Controls.Add(this.lblFacingStyle);
            this.panel1.Controls.Add(this.cboInputType);
            this.panel1.Controls.Add(this.lblInput);
            this.panel1.Controls.Add(this.txtID);
            this.panel1.Controls.Add(this.lblID);
            this.panel1.Location = new System.Drawing.Point(12, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(258, 115);
            this.panel1.TabIndex = 0;
            // 
            // cboFacing
            // 
            this.cboFacing.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFacing.FormattingEnabled = true;
            this.cboFacing.Location = new System.Drawing.Point(79, 85);
            this.cboFacing.Name = "cboFacing";
            this.cboFacing.Size = new System.Drawing.Size(168, 21);
            this.cboFacing.TabIndex = 7;
            // 
            // lblFacing
            // 
            this.lblFacing.AutoSize = true;
            this.lblFacing.Location = new System.Drawing.Point(28, 88);
            this.lblFacing.Name = "lblFacing";
            this.lblFacing.Size = new System.Drawing.Size(45, 13);
            this.lblFacing.TabIndex = 6;
            this.lblFacing.Text = "Facing: ";
            // 
            // cboFacingStyle
            // 
            this.cboFacingStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFacingStyle.FormattingEnabled = true;
            this.cboFacingStyle.Location = new System.Drawing.Point(79, 57);
            this.cboFacingStyle.Name = "cboFacingStyle";
            this.cboFacingStyle.Size = new System.Drawing.Size(168, 21);
            this.cboFacingStyle.TabIndex = 5;
            // 
            // lblFacingStyle
            // 
            this.lblFacingStyle.AutoSize = true;
            this.lblFacingStyle.Location = new System.Drawing.Point(2, 60);
            this.lblFacingStyle.Name = "lblFacingStyle";
            this.lblFacingStyle.Size = new System.Drawing.Size(71, 13);
            this.lblFacingStyle.TabIndex = 4;
            this.lblFacingStyle.Text = "Facing Style: ";
            // 
            // cboInputType
            // 
            this.cboInputType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboInputType.FormattingEnabled = true;
            this.cboInputType.Location = new System.Drawing.Point(79, 30);
            this.cboInputType.Name = "cboInputType";
            this.cboInputType.Size = new System.Drawing.Size(168, 21);
            this.cboInputType.TabIndex = 3;
            // 
            // lblInput
            // 
            this.lblInput.AutoSize = true;
            this.lblInput.Location = new System.Drawing.Point(9, 33);
            this.lblInput.Name = "lblInput";
            this.lblInput.Size = new System.Drawing.Size(64, 13);
            this.lblInput.TabIndex = 2;
            this.lblInput.Text = "Input Type: ";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(79, 4);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(168, 20);
            this.txtID.TabIndex = 1;
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(49, 7);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(24, 13);
            this.lblID.TabIndex = 0;
            this.lblID.Text = "ID: ";
            // 
            // menuFileMenu
            // 
            this.menuFileMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuFileMenu.Location = new System.Drawing.Point(0, 0);
            this.menuFileMenu.Name = "menuFileMenu";
            this.menuFileMenu.Size = new System.Drawing.Size(381, 24);
            this.menuFileMenu.TabIndex = 1;
            this.menuFileMenu.Text = "FileMenu";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.NewToolStripMenuItemClick);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItemClick);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItemClick);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.SaveAsToolStripMenuItemClick);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItemClick);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.chkSolid);
            this.panel3.Controls.Add(this.lblSolid);
            this.panel3.Controls.Add(this.lblHitboxH);
            this.panel3.Controls.Add(this.lblHitboxW);
            this.panel3.Controls.Add(this.lblHitboxY);
            this.panel3.Controls.Add(this.lblHitboxX);
            this.panel3.Controls.Add(this.numHitboxH);
            this.panel3.Controls.Add(this.numHitboxW);
            this.panel3.Controls.Add(this.numHitboxY);
            this.panel3.Controls.Add(this.numHitboxX);
            this.panel3.Location = new System.Drawing.Point(12, 148);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(258, 130);
            this.panel3.TabIndex = 2;
            // 
            // chkSolid
            // 
            this.chkSolid.AutoSize = true;
            this.chkSolid.Location = new System.Drawing.Point(79, 109);
            this.chkSolid.Name = "chkSolid";
            this.chkSolid.Size = new System.Drawing.Size(15, 14);
            this.chkSolid.TabIndex = 13;
            this.chkSolid.UseVisualStyleBackColor = true;
            // 
            // lblSolid
            // 
            this.lblSolid.AutoSize = true;
            this.lblSolid.Location = new System.Drawing.Point(37, 109);
            this.lblSolid.Name = "lblSolid";
            this.lblSolid.Size = new System.Drawing.Size(36, 13);
            this.lblSolid.TabIndex = 12;
            this.lblSolid.Text = "Solid: ";
            // 
            // lblHitboxH
            // 
            this.lblHitboxH.AutoSize = true;
            this.lblHitboxH.Location = new System.Drawing.Point(19, 84);
            this.lblHitboxH.Name = "lblHitboxH";
            this.lblHitboxH.Size = new System.Drawing.Size(54, 13);
            this.lblHitboxH.TabIndex = 11;
            this.lblHitboxH.Text = "Hitbox H: ";
            // 
            // lblHitboxW
            // 
            this.lblHitboxW.AutoSize = true;
            this.lblHitboxW.Location = new System.Drawing.Point(16, 58);
            this.lblHitboxW.Name = "lblHitboxW";
            this.lblHitboxW.Size = new System.Drawing.Size(57, 13);
            this.lblHitboxW.TabIndex = 10;
            this.lblHitboxW.Text = "Hitbox W: ";
            // 
            // lblHitboxY
            // 
            this.lblHitboxY.AutoSize = true;
            this.lblHitboxY.Location = new System.Drawing.Point(20, 32);
            this.lblHitboxY.Name = "lblHitboxY";
            this.lblHitboxY.Size = new System.Drawing.Size(53, 13);
            this.lblHitboxY.TabIndex = 9;
            this.lblHitboxY.Text = "Hitbox Y: ";
            // 
            // lblHitboxX
            // 
            this.lblHitboxX.AutoSize = true;
            this.lblHitboxX.Location = new System.Drawing.Point(20, 6);
            this.lblHitboxX.Name = "lblHitboxX";
            this.lblHitboxX.Size = new System.Drawing.Size(53, 13);
            this.lblHitboxX.TabIndex = 8;
            this.lblHitboxX.Text = "Hitbox X: ";
            // 
            // numHitboxH
            // 
            this.numHitboxH.Location = new System.Drawing.Point(79, 82);
            this.numHitboxH.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numHitboxH.Name = "numHitboxH";
            this.numHitboxH.Size = new System.Drawing.Size(168, 20);
            this.numHitboxH.TabIndex = 3;
            this.numHitboxH.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numHitboxW
            // 
            this.numHitboxW.Location = new System.Drawing.Point(79, 56);
            this.numHitboxW.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numHitboxW.Name = "numHitboxW";
            this.numHitboxW.Size = new System.Drawing.Size(168, 20);
            this.numHitboxW.TabIndex = 2;
            this.numHitboxW.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numHitboxY
            // 
            this.numHitboxY.Location = new System.Drawing.Point(79, 30);
            this.numHitboxY.Name = "numHitboxY";
            this.numHitboxY.Size = new System.Drawing.Size(168, 20);
            this.numHitboxY.TabIndex = 1;
            // 
            // numHitboxX
            // 
            this.numHitboxX.Location = new System.Drawing.Point(79, 4);
            this.numHitboxX.Name = "numHitboxX";
            this.numHitboxX.Size = new System.Drawing.Size(168, 20);
            this.numHitboxX.TabIndex = 0;
            // 
            // btnComponents
            // 
            this.btnComponents.Location = new System.Drawing.Point(3, 5);
            this.btnComponents.Name = "btnComponents";
            this.btnComponents.Size = new System.Drawing.Size(89, 23);
            this.btnComponents.TabIndex = 4;
            this.btnComponents.Text = "Components";
            this.btnComponents.UseVisualStyleBackColor = true;
            this.btnComponents.Click += new System.EventHandler(this.BtnComponentsClick);
            // 
            // btnLogics
            // 
            this.btnLogics.Location = new System.Drawing.Point(3, 33);
            this.btnLogics.Name = "btnLogics";
            this.btnLogics.Size = new System.Drawing.Size(89, 23);
            this.btnLogics.TabIndex = 5;
            this.btnLogics.Text = "Logics";
            this.btnLogics.UseVisualStyleBackColor = true;
            this.btnLogics.Click += new System.EventHandler(this.BtnLogicsClick);
            // 
            // btnAnimations
            // 
            this.btnAnimations.Location = new System.Drawing.Point(3, 60);
            this.btnAnimations.Name = "btnAnimations";
            this.btnAnimations.Size = new System.Drawing.Size(89, 23);
            this.btnAnimations.TabIndex = 6;
            this.btnAnimations.Text = "Animations";
            this.btnAnimations.UseVisualStyleBackColor = true;
            this.btnAnimations.Click += new System.EventHandler(this.BtnAnimationsClick);
            // 
            // btnCustom
            // 
            this.btnCustom.Location = new System.Drawing.Point(3, 88);
            this.btnCustom.Name = "btnCustom";
            this.btnCustom.Size = new System.Drawing.Size(89, 23);
            this.btnCustom.TabIndex = 7;
            this.btnCustom.Text = "Custom XML";
            this.btnCustom.UseVisualStyleBackColor = true;
            this.btnCustom.Click += new System.EventHandler(this.BtnCustomClick);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnComponents);
            this.panel2.Controls.Add(this.btnCustom);
            this.panel2.Controls.Add(this.btnLogics);
            this.panel2.Controls.Add(this.btnAnimations);
            this.panel2.Location = new System.Drawing.Point(277, 27);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(97, 115);
            this.panel2.TabIndex = 8;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.chkPersistent);
            this.panel4.Controls.Add(this.lblPersistent);
            this.panel4.Location = new System.Drawing.Point(277, 146);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(97, 132);
            this.panel4.TabIndex = 9;
            // 
            // lblPersistent
            // 
            this.lblPersistent.AutoSize = true;
            this.lblPersistent.Location = new System.Drawing.Point(3, 8);
            this.lblPersistent.Name = "lblPersistent";
            this.lblPersistent.Size = new System.Drawing.Size(56, 13);
            this.lblPersistent.TabIndex = 0;
            this.lblPersistent.Text = "Persistent:";
            // 
            // chkPersistent
            // 
            this.chkPersistent.AutoSize = true;
            this.chkPersistent.Location = new System.Drawing.Point(77, 8);
            this.chkPersistent.Name = "chkPersistent";
            this.chkPersistent.Size = new System.Drawing.Size(15, 14);
            this.chkPersistent.TabIndex = 1;
            this.chkPersistent.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 282);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuFileMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuFileMenu;
            this.Name = "MainForm";
            this.Text = "Spear of Longinus Entity Generator";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuFileMenu.ResumeLayout(false);
            this.menuFileMenu.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHitboxH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHitboxW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHitboxY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHitboxX)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.ComboBox cboInputType;
        private System.Windows.Forms.Label lblInput;
        private System.Windows.Forms.ComboBox cboFacingStyle;
        private System.Windows.Forms.Label lblFacingStyle;
        private System.Windows.Forms.ComboBox cboFacing;
        private System.Windows.Forms.Label lblFacing;
        private System.Windows.Forms.MenuStrip menuFileMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox chkSolid;
        private System.Windows.Forms.Label lblSolid;
        private System.Windows.Forms.Label lblHitboxH;
        private System.Windows.Forms.Label lblHitboxW;
        private System.Windows.Forms.Label lblHitboxY;
        private System.Windows.Forms.Label lblHitboxX;
        private System.Windows.Forms.NumericUpDown numHitboxH;
        private System.Windows.Forms.NumericUpDown numHitboxW;
        private System.Windows.Forms.NumericUpDown numHitboxY;
        private System.Windows.Forms.NumericUpDown numHitboxX;
        private System.Windows.Forms.Button btnComponents;
        private System.Windows.Forms.Button btnLogics;
        private System.Windows.Forms.Button btnAnimations;
        private System.Windows.Forms.Button btnCustom;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.CheckBox chkPersistent;
        private System.Windows.Forms.Label lblPersistent;
    }
}

