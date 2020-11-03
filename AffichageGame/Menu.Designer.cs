namespace AffichageGame
{
    partial class Menu
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
            this.btnSolo = new System.Windows.Forms.Button();
            this.btnMulti = new System.Windows.Forms.Button();
            this.btnThemes = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSolo
            // 
            this.btnSolo.Location = new System.Drawing.Point(275, 65);
            this.btnSolo.Name = "btnSolo";
            this.btnSolo.Size = new System.Drawing.Size(200, 51);
            this.btnSolo.TabIndex = 0;
            this.btnSolo.Text = "Solo";
            this.btnSolo.UseVisualStyleBackColor = true;
            this.btnSolo.Click += new System.EventHandler(this.btnSolo_Click);
            // 
            // btnMulti
            // 
            this.btnMulti.Location = new System.Drawing.Point(275, 183);
            this.btnMulti.Name = "btnMulti";
            this.btnMulti.Size = new System.Drawing.Size(200, 51);
            this.btnMulti.TabIndex = 0;
            this.btnMulti.Text = "Multi";
            this.btnMulti.UseVisualStyleBackColor = true;
            // 
            // btnThemes
            // 
            this.btnThemes.AutoSize = true;
            this.btnThemes.Location = new System.Drawing.Point(275, 298);
            this.btnThemes.Name = "btnThemes";
            this.btnThemes.Size = new System.Drawing.Size(200, 51);
            this.btnThemes.TabIndex = 0;
            this.btnThemes.Text = "Themes";
            this.btnThemes.UseVisualStyleBackColor = true;
            this.btnThemes.Click += new System.EventHandler(this.btnThemes_Click);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnThemes);
            this.Controls.Add(this.btnMulti);
            this.Controls.Add(this.btnSolo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Menu";
            this.Text = "Menu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSolo;
        private System.Windows.Forms.Button btnMulti;
        private System.Windows.Forms.Button btnThemes;
    }
}