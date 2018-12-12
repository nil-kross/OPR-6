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
            this.gameModeSwitchButton = new System.Windows.Forms.Button();
            this.mTextBox = new System.Windows.Forms.TextBox();
            this.buildButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.inputTabPage = new System.Windows.Forms.TabPage();
            this.saddleTabPage = new System.Windows.Forms.TabPage();
            this.paretoTabPage = new System.Windows.Forms.TabPage();
            this.grid = new System.Windows.Forms.DataGridView();
            this.graphButton = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // gameModeSwitchButton
            // 
            this.gameModeSwitchButton.BackColor = System.Drawing.Color.White;
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
            this.mTextBox.BackColor = System.Drawing.Color.Yellow;
            this.mTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.mTextBox.Location = new System.Drawing.Point(12, 12);
            this.mTextBox.Name = "mTextBox";
            this.mTextBox.Size = new System.Drawing.Size(30, 21);
            this.mTextBox.TabIndex = 2;
            this.mTextBox.Text = "0";
            this.mTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mTextBox.Leave += new System.EventHandler(this.OnMTextBoxLeave);
            // 
            // buildButton
            // 
            this.buildButton.BackColor = System.Drawing.Color.White;
            this.buildButton.Location = new System.Drawing.Point(124, 10);
            this.buildButton.Name = "buildButton";
            this.buildButton.Size = new System.Drawing.Size(115, 23);
            this.buildButton.TabIndex = 3;
            this.buildButton.Text = "Построить таблицу";
            this.buildButton.UseVisualStyleBackColor = false;
            this.buildButton.Click += new System.EventHandler(this.OnBuildButtonClick);
            // 
            // startButton
            // 
            this.startButton.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.startButton.BackColor = System.Drawing.Color.White;
            this.startButton.Location = new System.Drawing.Point(245, 10);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(50, 23);
            this.startButton.TabIndex = 4;
            this.startButton.Text = "Старт!";
            this.startButton.UseVisualStyleBackColor = false;
            this.startButton.Click += new System.EventHandler(this.OnStartButtonClick);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.inputTabPage);
            this.tabControl.Controls.Add(this.saddleTabPage);
            this.tabControl.Controls.Add(this.paretoTabPage);
            this.tabControl.Location = new System.Drawing.Point(5, 39);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(294, 20);
            this.tabControl.TabIndex = 5;
            // 
            // inputTabPage
            // 
            this.inputTabPage.Location = new System.Drawing.Point(4, 22);
            this.inputTabPage.Name = "inputTabPage";
            this.inputTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.inputTabPage.Size = new System.Drawing.Size(286, 0);
            this.inputTabPage.TabIndex = 0;
            this.inputTabPage.Text = "Исходные данные";
            this.inputTabPage.UseVisualStyleBackColor = true;
            // 
            // saddleTabPage
            // 
            this.saddleTabPage.Location = new System.Drawing.Point(4, 22);
            this.saddleTabPage.Name = "saddleTabPage";
            this.saddleTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.saddleTabPage.Size = new System.Drawing.Size(286, 0);
            this.saddleTabPage.TabIndex = 1;
            this.saddleTabPage.Text = "Седловая точка";
            this.saddleTabPage.UseVisualStyleBackColor = true;
            // 
            // paretoTabPage
            // 
            this.paretoTabPage.Location = new System.Drawing.Point(4, 22);
            this.paretoTabPage.Name = "paretoTabPage";
            this.paretoTabPage.Size = new System.Drawing.Size(286, 0);
            this.paretoTabPage.TabIndex = 2;
            this.paretoTabPage.Text = "Парето";
            this.paretoTabPage.UseVisualStyleBackColor = true;
            // 
            // grid
            // 
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Location = new System.Drawing.Point(5, 61);
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(286, 194);
            this.grid.TabIndex = 6;
            // 
            // graphButton
            // 
            this.graphButton.BackColor = System.Drawing.Color.White;
            this.graphButton.Location = new System.Drawing.Point(301, 10);
            this.graphButton.Name = "graphButton";
            this.graphButton.Size = new System.Drawing.Size(95, 23);
            this.graphButton.TabIndex = 7;
            this.graphButton.Text = "Граф. решение";
            this.graphButton.UseVisualStyleBackColor = false;
            this.graphButton.Click += new System.EventHandler(this.OnGraphButtonClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCoral;
            this.ClientSize = new System.Drawing.Size(465, 255);
            this.Controls.Add(this.graphButton);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.buildButton);
            this.Controls.Add(this.mTextBox);
            this.Controls.Add(this.gameModeSwitchButton);
            this.Name = "MainForm";
            this.Text = "ОПР 6";
            this.Resize += new System.EventHandler(this.OnResize);
            this.tabControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button gameModeSwitchButton;
        private System.Windows.Forms.TextBox mTextBox;
        private System.Windows.Forms.Button buildButton;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage saddleTabPage;
        private System.Windows.Forms.TabPage paretoTabPage;
        private System.Windows.Forms.TabPage inputTabPage;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.Button graphButton;
    }
}

