namespace Lambda.Views {
    partial class SettingsView {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent() {
            tlp_menu = new TableLayoutPanel();
            btn_save = new Button();
            btn_cancel = new Button();
            lb_gameDirectories = new ListBox();
            tlp_main = new TableLayoutPanel();
            btn_moveUp = new Button();
            btn_moveDown = new Button();
            btn_remove = new Button();
            btn_edit = new Button();
            btn_add = new Button();
            btn_setDefault = new Button();
            cb_advertise = new CheckBox();
            label2 = new Label();
            cb_validate = new CheckBox();
            cb_prepare = new CheckBox();
            cb_firstStart = new CheckBox();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label1 = new Label();
            nud_serverPort = new NumericUpDown();
            label6 = new Label();
            cb_server = new CheckBox();
            label7 = new Label();
            cb_useIPv4 = new CheckBox();
            tlp_menu.SuspendLayout();
            tlp_main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nud_serverPort).BeginInit();
            SuspendLayout();
            // 
            // tlp_menu
            // 
            tlp_menu.ColumnCount = 3;
            tlp_menu.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlp_menu.ColumnStyles.Add(new ColumnStyle());
            tlp_menu.ColumnStyles.Add(new ColumnStyle());
            tlp_menu.Controls.Add(btn_save, 2, 0);
            tlp_menu.Controls.Add(btn_cancel, 1, 0);
            tlp_menu.Dock = DockStyle.Bottom;
            tlp_menu.Location = new Point(0, 277);
            tlp_menu.Name = "tlp_menu";
            tlp_menu.RowCount = 1;
            tlp_menu.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlp_menu.Size = new Size(759, 28);
            tlp_menu.TabIndex = 11;
            // 
            // btn_save
            // 
            btn_save.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btn_save.Location = new Point(684, 0);
            btn_save.Margin = new Padding(0);
            btn_save.Name = "btn_save";
            btn_save.Size = new Size(75, 28);
            btn_save.TabIndex = 0;
            btn_save.Text = "Save";
            btn_save.UseVisualStyleBackColor = true;
            btn_save.Click += btn_save_Click;
            // 
            // btn_cancel
            // 
            btn_cancel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btn_cancel.Location = new Point(609, 0);
            btn_cancel.Margin = new Padding(0);
            btn_cancel.Name = "btn_cancel";
            btn_cancel.Size = new Size(75, 28);
            btn_cancel.TabIndex = 1;
            btn_cancel.Text = "Cancel";
            btn_cancel.UseVisualStyleBackColor = true;
            // 
            // lb_gameDirectories
            // 
            tlp_main.SetColumnSpan(lb_gameDirectories, 3);
            lb_gameDirectories.Dock = DockStyle.Fill;
            lb_gameDirectories.FormattingEnabled = true;
            lb_gameDirectories.ItemHeight = 15;
            lb_gameDirectories.Location = new Point(3, 3);
            lb_gameDirectories.Name = "lb_gameDirectories";
            tlp_main.SetRowSpan(lb_gameDirectories, 2);
            lb_gameDirectories.Size = new Size(518, 103);
            lb_gameDirectories.TabIndex = 0;
            // 
            // tlp_main
            // 
            tlp_main.AutoSize = true;
            tlp_main.ColumnCount = 4;
            tlp_main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
            tlp_main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            tlp_main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            tlp_main.ColumnStyles.Add(new ColumnStyle());
            tlp_main.Controls.Add(lb_gameDirectories, 0, 0);
            tlp_main.Controls.Add(btn_moveUp, 3, 0);
            tlp_main.Controls.Add(btn_moveDown, 3, 1);
            tlp_main.Controls.Add(btn_remove, 2, 2);
            tlp_main.Controls.Add(btn_edit, 1, 2);
            tlp_main.Controls.Add(btn_add, 0, 2);
            tlp_main.Controls.Add(btn_setDefault, 3, 2);
            tlp_main.Controls.Add(cb_advertise, 1, 3);
            tlp_main.Controls.Add(label2, 0, 3);
            tlp_main.Controls.Add(cb_validate, 1, 4);
            tlp_main.Controls.Add(cb_prepare, 1, 5);
            tlp_main.Controls.Add(cb_firstStart, 1, 6);
            tlp_main.Controls.Add(label3, 0, 4);
            tlp_main.Controls.Add(label4, 0, 5);
            tlp_main.Controls.Add(label5, 0, 6);
            tlp_main.Controls.Add(label1, 2, 4);
            tlp_main.Controls.Add(nud_serverPort, 3, 4);
            tlp_main.Controls.Add(label6, 2, 3);
            tlp_main.Controls.Add(cb_server, 3, 3);
            tlp_main.Controls.Add(label7, 2, 5);
            tlp_main.Controls.Add(cb_useIPv4, 3, 5);
            tlp_main.Dock = DockStyle.Fill;
            tlp_main.Location = new Point(0, 0);
            tlp_main.Name = "tlp_main";
            tlp_main.RowCount = 8;
            tlp_main.RowStyles.Add(new RowStyle());
            tlp_main.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlp_main.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            tlp_main.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            tlp_main.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            tlp_main.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            tlp_main.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            tlp_main.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            tlp_main.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlp_main.Size = new Size(759, 277);
            tlp_main.TabIndex = 7;
            // 
            // btn_moveUp
            // 
            btn_moveUp.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btn_moveUp.Location = new Point(524, 0);
            btn_moveUp.Margin = new Padding(0);
            btn_moveUp.Name = "btn_moveUp";
            btn_moveUp.Size = new Size(235, 27);
            btn_moveUp.TabIndex = 1;
            btn_moveUp.Text = "Move up";
            btn_moveUp.UseVisualStyleBackColor = true;
            btn_moveUp.Click += btn_moveUp_Click;
            // 
            // btn_moveDown
            // 
            btn_moveDown.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btn_moveDown.Location = new Point(524, 27);
            btn_moveDown.Margin = new Padding(0);
            btn_moveDown.Name = "btn_moveDown";
            btn_moveDown.Size = new Size(235, 25);
            btn_moveDown.TabIndex = 2;
            btn_moveDown.Text = "Move down";
            btn_moveDown.UseVisualStyleBackColor = true;
            btn_moveDown.Click += btn_moveDown_Click;
            // 
            // btn_remove
            // 
            btn_remove.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btn_remove.Location = new Point(349, 109);
            btn_remove.Margin = new Padding(0);
            btn_remove.Name = "btn_remove";
            btn_remove.Size = new Size(175, 28);
            btn_remove.TabIndex = 5;
            btn_remove.Text = "Remove";
            btn_remove.UseVisualStyleBackColor = true;
            btn_remove.Click += btn_remove_Click;
            // 
            // btn_edit
            // 
            btn_edit.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btn_edit.Location = new Point(174, 109);
            btn_edit.Margin = new Padding(0);
            btn_edit.Name = "btn_edit";
            btn_edit.Size = new Size(175, 28);
            btn_edit.TabIndex = 4;
            btn_edit.Text = "Edit";
            btn_edit.UseVisualStyleBackColor = true;
            btn_edit.Click += btn_edit_Click;
            // 
            // btn_add
            // 
            btn_add.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btn_add.Location = new Point(0, 109);
            btn_add.Margin = new Padding(0);
            btn_add.Name = "btn_add";
            btn_add.Size = new Size(174, 28);
            btn_add.TabIndex = 3;
            btn_add.Text = "Add";
            btn_add.UseVisualStyleBackColor = true;
            btn_add.Click += btn_add_Click;
            // 
            // btn_setDefault
            // 
            btn_setDefault.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btn_setDefault.Location = new Point(524, 109);
            btn_setDefault.Margin = new Padding(0);
            btn_setDefault.Name = "btn_setDefault";
            btn_setDefault.Size = new Size(235, 28);
            btn_setDefault.TabIndex = 6;
            btn_setDefault.Text = "Set default";
            btn_setDefault.UseVisualStyleBackColor = true;
            btn_setDefault.Click += btn_setDefault_Click;
            // 
            // cb_advertise
            // 
            cb_advertise.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cb_advertise.AutoSize = true;
            cb_advertise.Location = new Point(177, 144);
            cb_advertise.Name = "cb_advertise";
            cb_advertise.Size = new Size(169, 14);
            cb_advertise.TabIndex = 8;
            cb_advertise.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new Point(3, 143);
            label2.Name = "label2";
            label2.Size = new Size(168, 15);
            label2.TabIndex = 15;
            label2.Text = "Advertise new games in LAN";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cb_validate
            // 
            cb_validate.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cb_validate.AutoSize = true;
            cb_validate.Location = new Point(177, 172);
            cb_validate.Name = "cb_validate";
            cb_validate.Size = new Size(169, 14);
            cb_validate.TabIndex = 12;
            cb_validate.UseVisualStyleBackColor = true;
            // 
            // cb_prepare
            // 
            cb_prepare.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cb_prepare.AutoSize = true;
            cb_prepare.Location = new Point(177, 200);
            cb_prepare.Name = "cb_prepare";
            cb_prepare.Size = new Size(169, 14);
            cb_prepare.TabIndex = 9;
            cb_prepare.UseVisualStyleBackColor = true;
            // 
            // cb_firstStart
            // 
            cb_firstStart.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cb_firstStart.AutoSize = true;
            cb_firstStart.Location = new Point(177, 228);
            cb_firstStart.Name = "cb_firstStart";
            cb_firstStart.Size = new Size(169, 14);
            cb_firstStart.TabIndex = 10;
            cb_firstStart.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Location = new Point(3, 171);
            label3.Name = "label3";
            label3.Size = new Size(168, 15);
            label3.TabIndex = 16;
            label3.Text = "Auto validate after fetch";
            label3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Location = new Point(3, 193);
            label4.Name = "label4";
            label4.Size = new Size(168, 28);
            label4.TabIndex = 17;
            label4.Text = "Auto prepare after install/fetch";
            label4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label5.AutoSize = true;
            label5.Location = new Point(3, 227);
            label5.Name = "label5";
            label5.Size = new Size(168, 15);
            label5.TabIndex = 18;
            label5.Text = "Ignore first start commands";
            label5.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(352, 171);
            label1.Name = "label1";
            label1.Size = new Size(169, 15);
            label1.TabIndex = 13;
            label1.Text = "Server Port:";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // nud_serverPort
            // 
            nud_serverPort.Anchor = AnchorStyles.Left;
            nud_serverPort.Location = new Point(527, 168);
            nud_serverPort.Maximum = new decimal(new int[] { 65565, 0, 0, 0 });
            nud_serverPort.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nud_serverPort.Name = "nud_serverPort";
            nud_serverPort.Size = new Size(50, 23);
            nud_serverPort.TabIndex = 14;
            nud_serverPort.Value = new decimal(new int[] { 1337, 0, 0, 0 });
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label6.AutoSize = true;
            label6.Location = new Point(352, 143);
            label6.Name = "label6";
            label6.Size = new Size(169, 15);
            label6.TabIndex = 19;
            label6.Text = "Enable Advertise-Server";
            label6.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cb_server
            // 
            cb_server.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cb_server.AutoSize = true;
            cb_server.Location = new Point(527, 144);
            cb_server.Name = "cb_server";
            cb_server.Size = new Size(229, 14);
            cb_server.TabIndex = 20;
            cb_server.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label7.AutoSize = true;
            label7.Location = new Point(352, 199);
            label7.Name = "label7";
            label7.Size = new Size(169, 15);
            label7.TabIndex = 21;
            label7.Text = "Use IPv4 instead of IPv6";
            label7.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cb_useIPv4
            // 
            cb_useIPv4.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cb_useIPv4.AutoSize = true;
            cb_useIPv4.Location = new Point(527, 200);
            cb_useIPv4.Name = "cb_useIPv4";
            cb_useIPv4.Size = new Size(229, 14);
            cb_useIPv4.TabIndex = 22;
            cb_useIPv4.UseVisualStyleBackColor = true;
            // 
            // SettingsView
            // 
            AcceptButton = btn_save;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            CancelButton = btn_cancel;
            Controls.Add(tlp_main);
            Controls.Add(tlp_menu);
            MinimumSize = new Size(635, 305);
            Name = "SettingsView";
            Size = new Size(759, 305);
            Title = "Settings";
            Load += SettingsView_Load;
            tlp_menu.ResumeLayout(false);
            tlp_main.ResumeLayout(false);
            tlp_main.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nud_serverPort).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tlp_menu;
        private Button btn_save;
        private Button btn_cancel;
        private ListBox lb_gameDirectories;
        private TableLayoutPanel tlp_main;
        private Button btn_moveUp;
        private Button btn_moveDown;
        private Button btn_remove;
        private Button btn_edit;
        private Button btn_add;
        private Button btn_setDefault;
        private CheckBox cb_firstStart;
        private CheckBox cb_prepare;
        private CheckBox cb_advertise;
        private CheckBox cb_validate;
        private Label label1;
        private NumericUpDown nud_serverPort;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private CheckBox cb_server;
        private Label label7;
        private CheckBox cb_useIPv4;
    }
}
