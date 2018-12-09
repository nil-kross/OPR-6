namespace Lomtseu
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
            this.grid = new System.Windows.Forms.DataGridView();
            this.mSwitchButton = new System.Windows.Forms.Button();
            this.nTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // grid
            // 
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Location = new System.Drawing.Point(0, 40);
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(240, 150);
            this.grid.TabIndex = 0;
            // 
            // mSwitchButton
            // 
            this.mSwitchButton.Location = new System.Drawing.Point(12, 11);
            this.mSwitchButton.Name = "mSwitchButton";
            this.mSwitchButton.Size = new System.Drawing.Size(60, 23);
            this.mSwitchButton.TabIndex = 1;
            this.mSwitchButton.Text = "(<=>) m x";
            this.mSwitchButton.UseVisualStyleBackColor = true;
            // 
            // nTextBox
            // 
            this.nTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nTextBox.Location = new System.Drawing.Point(78, 12);
            this.nTextBox.Name = "nTextBox";
            this.nTextBox.Size = new System.Drawing.Size(52, 21);
            this.nTextBox.TabIndex = 2;
            this.nTextBox.Text = "0";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(295, 255);
            this.Controls.Add(this.nTextBox);
            this.Controls.Add(this.mSwitchButton);
            this.Controls.Add(this.grid);
            this.Name = "MainForm";
            this.Text = "ОПР 6";
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.Button mSwitchButton;
        private System.Windows.Forms.TextBox nTextBox;
    }
}

