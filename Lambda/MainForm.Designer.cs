namespace Lambda {
    partial class MainForm {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            lv_games = new ListView();
            tlp_main = new TableLayoutPanel();
            tlp_game = new TableLayoutPanel();
            btn_execute = new Button();
            btn_fetch = new Button();
            btn_install = new Button();
            btn_validate = new Button();
            btn_remove = new Button();
            pb_image = new PictureBox();
            lbl_name = new Label();
            richTextBox1 = new RichTextBox();
            tlp_gamePath = new TableLayoutPanel();
            btn_edit = new Button();
            lbl_path = new Label();
            tpl_menu = new TableLayoutPanel();
            btn_settings = new Button();
            btn_refresh = new Button();
            btn_import = new Button();
            tlp_main.SuspendLayout();
            tlp_game.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pb_image).BeginInit();
            tlp_gamePath.SuspendLayout();
            tpl_menu.SuspendLayout();
            SuspendLayout();
            // 
            // lv_games
            // 
            lv_games.Dock = DockStyle.Fill;
            lv_games.Location = new Point(4, 35);
            lv_games.Name = "lv_games";
            lv_games.Size = new Size(943, 288);
            lv_games.TabIndex = 0;
            lv_games.UseCompatibleStateImageBehavior = false;
            // 
            // tlp_main
            // 
            tlp_main.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tlp_main.ColumnCount = 1;
            tlp_main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlp_main.Controls.Add(lv_games, 0, 1);
            tlp_main.Controls.Add(tlp_game, 0, 2);
            tlp_main.Controls.Add(tpl_menu, 0, 0);
            tlp_main.Dock = DockStyle.Fill;
            tlp_main.Location = new Point(0, 0);
            tlp_main.Name = "tlp_main";
            tlp_main.RowCount = 3;
            tlp_main.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tlp_main.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlp_main.RowStyles.Add(new RowStyle(SizeType.Absolute, 180F));
            tlp_main.Size = new Size(951, 508);
            tlp_main.TabIndex = 1;
            // 
            // tlp_game
            // 
            tlp_game.ColumnCount = 3;
            tlp_game.ColumnStyles.Add(new ColumnStyle());
            tlp_game.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlp_game.ColumnStyles.Add(new ColumnStyle());
            tlp_game.Controls.Add(btn_execute, 3, 0);
            tlp_game.Controls.Add(btn_fetch, 3, 1);
            tlp_game.Controls.Add(btn_install, 3, 2);
            tlp_game.Controls.Add(btn_validate, 3, 3);
            tlp_game.Controls.Add(btn_remove, 3, 4);
            tlp_game.Controls.Add(pb_image, 0, 1);
            tlp_game.Controls.Add(lbl_name, 0, 0);
            tlp_game.Controls.Add(richTextBox1, 1, 1);
            tlp_game.Controls.Add(tlp_gamePath, 1, 4);
            tlp_game.Dock = DockStyle.Fill;
            tlp_game.Location = new Point(4, 330);
            tlp_game.Name = "tlp_game";
            tlp_game.RowCount = 5;
            tlp_game.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlp_game.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlp_game.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlp_game.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlp_game.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlp_game.Size = new Size(943, 174);
            tlp_game.TabIndex = 2;
            // 
            // btn_execute
            // 
            btn_execute.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btn_execute.AutoSize = true;
            btn_execute.Location = new Point(862, 0);
            btn_execute.Margin = new Padding(0);
            btn_execute.Name = "btn_execute";
            btn_execute.Size = new Size(81, 34);
            btn_execute.TabIndex = 0;
            btn_execute.Text = "Execute";
            btn_execute.UseVisualStyleBackColor = true;
            // 
            // btn_fetch
            // 
            btn_fetch.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btn_fetch.Location = new Point(862, 34);
            btn_fetch.Margin = new Padding(0);
            btn_fetch.Name = "btn_fetch";
            btn_fetch.Size = new Size(81, 34);
            btn_fetch.TabIndex = 1;
            btn_fetch.Text = "Fetch";
            btn_fetch.UseVisualStyleBackColor = true;
            // 
            // btn_install
            // 
            btn_install.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btn_install.Location = new Point(862, 68);
            btn_install.Margin = new Padding(0);
            btn_install.Name = "btn_install";
            btn_install.Size = new Size(81, 34);
            btn_install.TabIndex = 2;
            btn_install.Text = "Install";
            btn_install.UseVisualStyleBackColor = true;
            // 
            // btn_validate
            // 
            btn_validate.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btn_validate.Location = new Point(862, 102);
            btn_validate.Margin = new Padding(0);
            btn_validate.Name = "btn_validate";
            btn_validate.Size = new Size(81, 34);
            btn_validate.TabIndex = 3;
            btn_validate.Text = "Validate";
            btn_validate.UseVisualStyleBackColor = true;
            // 
            // btn_remove
            // 
            btn_remove.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btn_remove.Location = new Point(862, 136);
            btn_remove.Margin = new Padding(0);
            btn_remove.Name = "btn_remove";
            btn_remove.Size = new Size(81, 38);
            btn_remove.TabIndex = 4;
            btn_remove.Text = "Remove";
            btn_remove.UseVisualStyleBackColor = true;
            // 
            // pb_image
            // 
            pb_image.BorderStyle = BorderStyle.FixedSingle;
            pb_image.Dock = DockStyle.Fill;
            pb_image.Location = new Point(3, 37);
            pb_image.Name = "pb_image";
            tlp_game.SetRowSpan(pb_image, 4);
            pb_image.Size = new Size(128, 134);
            pb_image.TabIndex = 5;
            pb_image.TabStop = false;
            // 
            // lbl_name
            // 
            lbl_name.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tlp_game.SetColumnSpan(lbl_name, 2);
            lbl_name.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_name.Location = new Point(3, 0);
            lbl_name.Name = "lbl_name";
            lbl_name.Size = new Size(856, 34);
            lbl_name.TabIndex = 6;
            lbl_name.Text = "The Game";
            lbl_name.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // richTextBox1
            // 
            richTextBox1.BorderStyle = BorderStyle.FixedSingle;
            richTextBox1.Dock = DockStyle.Fill;
            richTextBox1.Location = new Point(137, 37);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            tlp_game.SetRowSpan(richTextBox1, 3);
            richTextBox1.ScrollBars = RichTextBoxScrollBars.Vertical;
            richTextBox1.Size = new Size(722, 96);
            richTextBox1.TabIndex = 7;
            richTextBox1.Text = "";
            // 
            // tlp_gamePath
            // 
            tlp_gamePath.ColumnCount = 2;
            tlp_gamePath.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlp_gamePath.ColumnStyles.Add(new ColumnStyle());
            tlp_gamePath.Controls.Add(btn_edit, 1, 0);
            tlp_gamePath.Controls.Add(lbl_path, 0, 0);
            tlp_gamePath.Dock = DockStyle.Fill;
            tlp_gamePath.Location = new Point(134, 136);
            tlp_gamePath.Margin = new Padding(0);
            tlp_gamePath.Name = "tlp_gamePath";
            tlp_gamePath.RowCount = 1;
            tlp_gamePath.RowStyles.Add(new RowStyle());
            tlp_gamePath.Size = new Size(728, 38);
            tlp_gamePath.TabIndex = 8;
            // 
            // btn_edit
            // 
            btn_edit.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btn_edit.Location = new Point(653, 0);
            btn_edit.Margin = new Padding(0);
            btn_edit.Name = "btn_edit";
            btn_edit.Size = new Size(75, 38);
            btn_edit.TabIndex = 0;
            btn_edit.Text = "Edit";
            btn_edit.UseVisualStyleBackColor = true;
            // 
            // lbl_path
            // 
            lbl_path.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lbl_path.Location = new Point(3, 0);
            lbl_path.Name = "lbl_path";
            lbl_path.Size = new Size(647, 38);
            lbl_path.TabIndex = 1;
            lbl_path.Text = "\\\\..\\path\\to\\game";
            lbl_path.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tpl_menu
            // 
            tpl_menu.ColumnCount = 3;
            tpl_menu.ColumnStyles.Add(new ColumnStyle());
            tpl_menu.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tpl_menu.ColumnStyles.Add(new ColumnStyle());
            tpl_menu.Controls.Add(btn_settings, 2, 0);
            tpl_menu.Controls.Add(btn_refresh, 0, 0);
            tpl_menu.Controls.Add(btn_import, 1, 0);
            tpl_menu.Dock = DockStyle.Fill;
            tpl_menu.Location = new Point(1, 1);
            tpl_menu.Margin = new Padding(0);
            tpl_menu.Name = "tpl_menu";
            tpl_menu.RowCount = 1;
            tpl_menu.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tpl_menu.Size = new Size(949, 30);
            tpl_menu.TabIndex = 3;
            // 
            // btn_settings
            // 
            btn_settings.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            btn_settings.Location = new Point(874, 0);
            btn_settings.Margin = new Padding(0);
            btn_settings.Name = "btn_settings";
            btn_settings.Size = new Size(75, 30);
            btn_settings.TabIndex = 1;
            btn_settings.Text = "Settings";
            btn_settings.UseVisualStyleBackColor = true;
            btn_settings.Click += btn_settings_Click;
            // 
            // btn_refresh
            // 
            btn_refresh.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            btn_refresh.Location = new Point(0, 0);
            btn_refresh.Margin = new Padding(0);
            btn_refresh.Name = "btn_refresh";
            btn_refresh.Size = new Size(75, 30);
            btn_refresh.TabIndex = 2;
            btn_refresh.Text = "Refresh";
            btn_refresh.UseVisualStyleBackColor = true;
            btn_refresh.Click += btn_refresh_Click;
            // 
            // btn_import
            // 
            btn_import.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            btn_import.Location = new Point(437, 0);
            btn_import.Margin = new Padding(0);
            btn_import.Name = "btn_import";
            btn_import.Size = new Size(75, 30);
            btn_import.TabIndex = 3;
            btn_import.Text = "Import";
            btn_import.UseVisualStyleBackColor = true;
            btn_import.Click += btn_import_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(951, 508);
            Controls.Add(tlp_main);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Lambda";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            Shown += MainForm_Shown;
            tlp_main.ResumeLayout(false);
            tlp_game.ResumeLayout(false);
            tlp_game.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pb_image).EndInit();
            tlp_gamePath.ResumeLayout(false);
            tpl_menu.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ListView lv_games;
        private TableLayoutPanel tlp_main;
        private Button btn_settings;
        private TableLayoutPanel tlp_game;
        private Button btn_execute;
        private Button btn_fetch;
        private Button btn_install;
        private Button btn_validate;
        private Button btn_remove;
        private PictureBox pb_image;
        private Label lbl_name;
        private RichTextBox richTextBox1;
        private TableLayoutPanel tlp_gamePath;
        private Button btn_edit;
        private Label lbl_path;
        private TableLayoutPanel tpl_menu;
        private Button btn_refresh;
        private Button btn_import;
    }
}
