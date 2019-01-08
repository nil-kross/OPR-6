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
            this.rowsCountTextBox = new System.Windows.Forms.TextBox();
            this.startButton = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.inputTabPage = new System.Windows.Forms.TabPage();
            this.saddleTabPage = new System.Windows.Forms.TabPage();
            this.paretoTabPage = new System.Windows.Forms.TabPage();
            this.clearCostTabPage = new System.Windows.Forms.TabPage();
            this.grid = new System.Windows.Forms.DataGridView();
            this.graphButton = new System.Windows.Forms.Button();
            this.panel = new System.Windows.Forms.Panel();
            this.colsCountTextBox = new System.Windows.Forms.TextBox();
            this.buildButton = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // gameModeSwitchButton
            // 
            this.gameModeSwitchButton.BackColor = System.Drawing.Color.White;
            this.gameModeSwitchButton.Location = new System.Drawing.Point(37, 3);
            this.gameModeSwitchButton.Name = "gameModeSwitchButton";
            this.gameModeSwitchButton.Size = new System.Drawing.Size(21, 23);
            this.gameModeSwitchButton.TabIndex = 1;
            this.gameModeSwitchButton.Text = "X";
            this.gameModeSwitchButton.UseVisualStyleBackColor = false;
            this.gameModeSwitchButton.Click += new System.EventHandler(this.OnGameModeSwitchButtonClick);
            // 
            // rowsCountTextBox
            // 
            this.rowsCountTextBox.BackColor = System.Drawing.Color.Yellow;
            this.rowsCountTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rowsCountTextBox.Location = new System.Drawing.Point(7, 3);
            this.rowsCountTextBox.Name = "rowsCountTextBox";
            this.rowsCountTextBox.Size = new System.Drawing.Size(30, 21);
            this.rowsCountTextBox.TabIndex = 2;
            this.rowsCountTextBox.Text = "0";
            this.rowsCountTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.rowsCountTextBox.Leave += new System.EventHandler(this.OnRowsTextBoxLeave);
            // 
            // startButton
            // 
            this.startButton.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.startButton.BackColor = System.Drawing.Color.White;
            this.startButton.Location = new System.Drawing.Point(215, 3);
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
            this.tabControl.Controls.Add(this.clearCostTabPage);
            this.tabControl.Location = new System.Drawing.Point(3, 32);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(528, 20);
            this.tabControl.TabIndex = 5;
            this.tabControl.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.OnTabControlSelectedIndexChanged);
            // 
            // inputTabPage
            // 
            this.inputTabPage.Location = new System.Drawing.Point(4, 22);
            this.inputTabPage.Name = "inputTabPage";
            this.inputTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.inputTabPage.Size = new System.Drawing.Size(520, 0);
            this.inputTabPage.TabIndex = 0;
            this.inputTabPage.Text = "Исходные данные";
            this.inputTabPage.UseVisualStyleBackColor = true;
            // 
            // saddleTabPage
            // 
            this.saddleTabPage.Location = new System.Drawing.Point(4, 22);
            this.saddleTabPage.Name = "saddleTabPage";
            this.saddleTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.saddleTabPage.Size = new System.Drawing.Size(520, 0);
            this.saddleTabPage.TabIndex = 1;
            this.saddleTabPage.Text = "Седловая точка";
            this.saddleTabPage.UseVisualStyleBackColor = true;
            // 
            // paretoTabPage
            // 
            this.paretoTabPage.Location = new System.Drawing.Point(4, 22);
            this.paretoTabPage.Name = "paretoTabPage";
            this.paretoTabPage.Size = new System.Drawing.Size(520, 0);
            this.paretoTabPage.TabIndex = 2;
            this.paretoTabPage.Text = "Парето";
            this.paretoTabPage.UseVisualStyleBackColor = true;
            // 
            // clearCostTabPage
            // 
            this.clearCostTabPage.Location = new System.Drawing.Point(4, 22);
            this.clearCostTabPage.Name = "clearCostTabPage";
            this.clearCostTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.clearCostTabPage.Size = new System.Drawing.Size(520, 0);
            this.clearCostTabPage.TabIndex = 3;
            this.clearCostTabPage.Text = "Чистая стратегия";
            this.clearCostTabPage.UseVisualStyleBackColor = true;
            // 
            // grid
            // 
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Location = new System.Drawing.Point(5, 172);
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(286, 194);
            this.grid.TabIndex = 6;
            this.grid.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.OnGridCellLeave);
            // 
            // graphButton
            // 
            this.graphButton.BackColor = System.Drawing.Color.White;
            this.graphButton.Location = new System.Drawing.Point(271, 3);
            this.graphButton.Name = "graphButton";
            this.graphButton.Size = new System.Drawing.Size(95, 23);
            this.graphButton.TabIndex = 7;
            this.graphButton.Text = "Граф. решение";
            this.graphButton.UseVisualStyleBackColor = false;
            this.graphButton.Click += new System.EventHandler(this.OnGraphButtonClick);
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.Color.LightCoral;
            this.panel.Controls.Add(this.colsCountTextBox);
            this.panel.Controls.Add(this.rowsCountTextBox);
            this.panel.Controls.Add(this.graphButton);
            this.panel.Controls.Add(this.tabControl);
            this.panel.Controls.Add(this.gameModeSwitchButton);
            this.panel.Controls.Add(this.buildButton);
            this.panel.Controls.Add(this.startButton);
            this.panel.Location = new System.Drawing.Point(5, 3);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(498, 53);
            this.panel.TabIndex = 8;
            this.panel.Click += new System.EventHandler(this.OnRowsTextBoxLeave);
            // 
            // colsCountTextBox
            // 
            this.colsCountTextBox.Location = new System.Drawing.Point(58, 4);
            this.colsCountTextBox.Name = "colsCountTextBox";
            this.colsCountTextBox.Size = new System.Drawing.Size(30, 20);
            this.colsCountTextBox.TabIndex = 8;
            this.colsCountTextBox.Text = "0";
            this.colsCountTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colsCountTextBox.Leave += new System.EventHandler(this.OnColsTextBoxLeave);
            // 
            // buildButton
            // 
            this.buildButton.BackColor = System.Drawing.Color.White;
            this.buildButton.Location = new System.Drawing.Point(94, 3);
            this.buildButton.Name = "buildButton";
            this.buildButton.Size = new System.Drawing.Size(115, 23);
            this.buildButton.TabIndex = 3;
            this.buildButton.Text = "Построить таблицу";
            this.buildButton.UseVisualStyleBackColor = false;
            this.buildButton.Click += new System.EventHandler(this.OnBuildButtonClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Turquoise;
            this.ClientSize = new System.Drawing.Size(919, 378);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.grid);
            this.Name = "MainForm";
            this.Text = "ОПР 6";
            this.Resize += new System.EventHandler(this.OnResize);
            this.tabControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button gameModeSwitchButton;
        private System.Windows.Forms.TextBox rowsCountTextBox;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage saddleTabPage;
        private System.Windows.Forms.TabPage paretoTabPage;
        private System.Windows.Forms.TabPage inputTabPage;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.Button graphButton;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Button buildButton;
        private System.Windows.Forms.TextBox colsCountTextBox;
        private System.Windows.Forms.TabPage clearCostTabPage;
    }
}

