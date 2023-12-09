namespace Lambda.Views {
    partial class InputBoxView {
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
            btn_cancel = new Button();
            btn_ok = new Button();
            tb_input = new TextBox();
            lbl_message = new Label();
            tlp_main = new TableLayoutPanel();
            ExtraButton = new Button();
            tlp_main.SuspendLayout();
            SuspendLayout();
            // 
            // btn_cancel
            // 
            btn_cancel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btn_cancel.Location = new Point(371, 62);
            btn_cancel.Margin = new Padding(0);
            btn_cancel.Name = "btn_cancel";
            btn_cancel.Size = new Size(75, 28);
            btn_cancel.TabIndex = 0;
            btn_cancel.Text = "Cancel";
            btn_cancel.UseVisualStyleBackColor = true;
            // 
            // btn_ok
            // 
            btn_ok.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btn_ok.Location = new Point(296, 62);
            btn_ok.Margin = new Padding(0);
            btn_ok.Name = "btn_ok";
            btn_ok.Size = new Size(75, 28);
            btn_ok.TabIndex = 1;
            btn_ok.Text = "OK";
            btn_ok.UseVisualStyleBackColor = true;
            // 
            // tb_input
            // 
            tb_input.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tlp_main.SetColumnSpan(tb_input, 3);
            tb_input.Location = new Point(3, 22);
            tb_input.Name = "tb_input";
            tb_input.Size = new Size(440, 23);
            tb_input.TabIndex = 2;
            // 
            // lbl_message
            // 
            lbl_message.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lbl_message.AutoSize = true;
            tlp_main.SetColumnSpan(lbl_message, 3);
            lbl_message.Location = new Point(3, 2);
            lbl_message.Name = "lbl_message";
            lbl_message.Size = new Size(440, 15);
            lbl_message.TabIndex = 3;
            lbl_message.Text = "<Message>";
            // 
            // tlp_main
            // 
            tlp_main.AutoSize = true;
            tlp_main.ColumnCount = 4;
            tlp_main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlp_main.ColumnStyles.Add(new ColumnStyle());
            tlp_main.ColumnStyles.Add(new ColumnStyle());
            tlp_main.ColumnStyles.Add(new ColumnStyle());
            tlp_main.Controls.Add(btn_cancel, 2, 3);
            tlp_main.Controls.Add(lbl_message, 0, 0);
            tlp_main.Controls.Add(btn_ok, 1, 3);
            tlp_main.Controls.Add(tb_input, 0, 1);
            tlp_main.Controls.Add(ExtraButton, 3, 1);
            tlp_main.Dock = DockStyle.Fill;
            tlp_main.Location = new Point(0, 0);
            tlp_main.MinimumSize = new Size(0, 90);
            tlp_main.Name = "tlp_main";
            tlp_main.RowCount = 4;
            tlp_main.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlp_main.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            tlp_main.RowStyles.Add(new RowStyle(SizeType.Absolute, 15F));
            tlp_main.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            tlp_main.Size = new Size(521, 90);
            tlp_main.TabIndex = 4;
            // 
            // ExtraButton
            // 
            ExtraButton.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ExtraButton.Location = new Point(446, 19);
            ExtraButton.Margin = new Padding(0);
            ExtraButton.Name = "ExtraButton";
            ExtraButton.Size = new Size(75, 28);
            ExtraButton.TabIndex = 4;
            ExtraButton.Text = "...";
            ExtraButton.UseVisualStyleBackColor = true;
            // 
            // InputBoxView
            // 
            AcceptButton = btn_ok;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            CancelButton = btn_cancel;
            Controls.Add(tlp_main);
            MinimumSize = new Size(300, 90);
            Name = "InputBoxView";
            Size = new Size(521, 90);
            Title = "InputBoxView";
            tlp_main.ResumeLayout(false);
            tlp_main.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btn_cancel;
        private Button btn_ok;
        private TextBox tb_input;
        private TableLayoutPanel tlp_main;
        private Label lbl_message;
        public Button ExtraButton;
    }
}
