namespace Lambda.Views {
    partial class ProgressView<T> {
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
            progressBar = new ProgressBar();
            tlp_main = new TableLayoutPanel();
            lbl_status = new Label();
            tlp_main.SuspendLayout();
            SuspendLayout();
            // 
            // progressBar
            // 
            progressBar.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            progressBar.Location = new Point(8, 28);
            progressBar.Margin = new Padding(8);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(484, 34);
            progressBar.TabIndex = 0;
            // 
            // tlp_main
            // 
            tlp_main.ColumnCount = 1;
            tlp_main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlp_main.Controls.Add(progressBar, 0, 1);
            tlp_main.Controls.Add(lbl_status, 0, 0);
            tlp_main.Dock = DockStyle.Fill;
            tlp_main.Location = new Point(0, 0);
            tlp_main.Margin = new Padding(0);
            tlp_main.Name = "tlp_main";
            tlp_main.RowCount = 2;
            tlp_main.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlp_main.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlp_main.Size = new Size(500, 70);
            tlp_main.TabIndex = 1;
            // 
            // lbl_status
            // 
            lbl_status.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lbl_status.Location = new Point(0, 0);
            lbl_status.Margin = new Padding(0);
            lbl_status.Name = "lbl_status";
            lbl_status.Size = new Size(500, 20);
            lbl_status.TabIndex = 1;
            lbl_status.Text = "<status>";
            lbl_status.TextAlign = ContentAlignment.MiddleCenter;
            lbl_status.TextChanged += lbl_status_TextChanged;
            // 
            // ProgressView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            Controls.Add(tlp_main);
            MinimumSize = new Size(500, 50);
            Name = "ProgressView";
            Size = new Size(500, 70);
            tlp_main.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ProgressBar progressBar;
        private TableLayoutPanel tlp_main;
        private Label lbl_status;
    }
}
