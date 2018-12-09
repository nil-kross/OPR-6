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
            this.gameModeSwitchButton = new System.Windows.Forms.Button();
            this.mTextBox = new System.Windows.Forms.TextBox();
            this.startButton = new System.Windows.Forms.Button();
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
            // gameModeSwitchButton
            // 
            this.gameModeSwitchButton.BackColor = System.Drawing.Color.WhiteSmoke;
            this.gameModeSwitchButton.Location = new System.Drawing.Point(48, 10);
            this.gameModeSwitchButton.Name = "gameModeSwitchButton";
            this.gameModeSwitchButton.Size = new System.Drawing.Size(70, 23);
            this.gameModeSwitchButton.TabIndex = 1;
            this.gameModeSwitchButton.Text = "(<=>) M x 2";
            this.gameModeSwitchButton.UseVisualStyleBackColor = false;
            this.gameModeSwitchButton.Click += new System.EventHandler(this.OnGameModeSwitchButtonClick);
            // 
            // mTextBox
            // 
            this.mTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.mTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.mTextBox.Location = new System.Drawing.Point(12, 12);
            this.mTextBox.Name = "mTextBox";
            this.mTextBox.Size = new System.Drawing.Size(30, 21);
            this.mTextBox.TabIndex = 2;
            this.mTextBox.Text = "0";
            this.mTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mTextBox.Leave += new System.EventHandler(this.OnMTextBoxLeave);
            // 
            // startButton
            // 
            this.startButton.BackColor = System.Drawing.Color.WhiteSmoke;
            this.startButton.Location = new System.Drawing.Point(164, 10);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 3;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = false;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCoral;
            this.ClientSize = new System.Drawing.Size(295, 255);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.mTextBox);
            this.Controls.Add(this.gameModeSwitchButton);
            this.Controls.Add(this.grid);
            this.Name = "MainForm";
            this.Text = "ОПР 6";
            this.Resize += new System.EventHandler(this.OnResize);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.Button gameModeSwitchButton;
        private System.Windows.Forms.TextBox mTextBox;
        private System.Windows.Forms.Button startButton;
    }
}

