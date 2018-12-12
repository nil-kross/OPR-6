using Lomtseu.GamesTheory;
using Lomtseu.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lomtseu
{
    public partial class MainForm : Form
    {
        private const Int32 defaultMValue = 5;

        private Boolean isBuilded = false;
        private Boolean isStarted = false;
        private Boolean isMChanged = false;
        private Boolean isModeChanged = false;
        private Int32 mValue;
        private GameModes gameMode;
        private GameModes selectedGameMode;
        private Table inputTable;
        private Table saddleTable;
        private Table paretoTable;

        protected Int32 M {
            get {
                var mValue = -1;
                var isMValid = false;

                if (Int32.TryParse(this.mTextBox.Text, out mValue)) {
                    if (mValue > 0) {
                        isMValid = true;
                    }
                }

                if (!isMValid) {
                    this.mTextBox.Text = this.mValue.ToString();
                    this.isMChanged = false;
                    mValue = this.M;
                }

                return mValue;
            }
            set {
                var newValue = value > 0 ? value : this.mValue;

                this.isMChanged = false;
                this.mValue = newValue;
            }
        }

        public MainForm()
        {
            InitializeComponent();

            this.mValue = MainForm.defaultMValue;

            this.UpdateLayout();
            this.OnGameModeChange(GameModes.MxN);
            this.mTextBox.Text = MainForm.defaultMValue.ToString();
            //var a = (new GraphForm()).ShowDialog(); // DEBUG
        }

        protected void ResizeLayout()
        {
            var height = 40;

            this.tabControl.Location = new Point(0, height);
            this.tabControl.Size = new Size(this.ClientSize.Width, this.tabControl.Height);
            this.grid.Location = new Point(0, (this.tabControl.Location.Y + this.tabControl.Size.Height));
            this.grid.Size = new Size(this.ClientSize.Width, this.ClientSize.Height - (this.tabControl.Location.Y + this.tabControl.Size.Height));
        }

        protected void UpdateLayout()
        {
            this.ResizeLayout();

            this.mTextBox.BackColor = this.isMChanged ? Color.Yellow : Color.White;
            this.buildButton.BackColor = this.isModeChanged || this.isMChanged ? Color.Yellow : Color.White;
            this.startButton.Enabled = this.isBuilded;

            this.tabControl.TabPages.Clear();
            if (this.isBuilded)
            {
                this.tabControl.TabPages.Add(inputTabPage);
            }
            if (this.isStarted)
            {
                this.tabControl.TabPages.Add(saddleTabPage);
                this.tabControl.TabPages.Add(paretoTabPage);
            }
        }

        protected void UpdateSelectedGameMode(GameModes gameMode) {
            var labelString = "";
            var labelXValue = 0;
            var inputXValue = 0;

            if (gameMode == GameModes.MxN) {
                labelString = "(<=>) M x 2";
                labelXValue = 12;
                inputXValue = 88;
            } else {
                labelString = "2 x M (<=>)";
                labelXValue = 48;
                inputXValue = 12;
            }

            this.gameModeSwitchButton.Text = labelString;
            this.gameModeSwitchButton.Location = new Point(labelXValue, this.gameModeSwitchButton.Location.Y);
            this.mTextBox.Location = new Point(inputXValue, this.mTextBox.Location.Y);
        }

        private void OnGameModeChange(GameModes gameMode) {
            this.isModeChanged = this.gameMode != gameMode;
            this.selectedGameMode = gameMode;

            this.UpdateSelectedGameMode(this.selectedGameMode);
            this.UpdateLayout();
        }

        private void OnResize(object sender, EventArgs e)
        {
            this.ResizeLayout();
        }

        private void OnGameModeSwitchButtonClick(Object sender, EventArgs e) {
            this.OnGameModeChange(this.selectedGameMode == GameModes.MxN ? GameModes.NxM : GameModes.MxN);
        }

        private void OnMTextBoxLeave(Object sender, EventArgs e) {
            this.isMChanged = this.mValue != this.M;

            this.UpdateLayout();
        }

        private void OnBuildButtonClick(Object sender, EventArgs e) {
            var rowsValue = 0;
            var colsValue = 0;

            this.isModeChanged = false;
            this.isBuilded = true;
            this.M = this.M;

            this.gameMode = selectedGameMode;
            if (this.gameMode == GameModes.MxN) {
                rowsValue = this.M;
                colsValue = 2;
            } else {
                rowsValue = 2;
                colsValue = this.M;
            }

            {
                Table table = new Table(rowsValue, colsValue).ForEach((cell, r, c) => new TextCell(r.ToString()));

                this.grid.Load(table);
                this.inputTable = table;
            }

            this.UpdateLayout();
        }

        private void OnStartButtonClick(Object sender, EventArgs e) {
            Double[][] array;
            var rowsValue = this.grid.Rows.Count - 1;
            var colsValue = this.grid.Columns.Count;

            array = new Double[rowsValue][];

            for (var r = 0; r < rowsValue; r++)
            {
                array[r] = new Double[colsValue];
            }
            {
                var r = 0;

                foreach (DataGridViewRow row in this.grid.Rows) {
                    var c = 0;

                    if (r < this.grid.Rows.Count - 1) {
                        foreach (DataGridViewTextBoxCell col in row.Cells) {
                            array[r][c] = Double.Parse(col.Value as String);

                            c++;
                        }
                    }
                    r++;
                }
            }
            // Сохраняем inputTable
            {
                // TODO: сохранять таблицу в inputTable
            }
            // Седловая точка
            {
                Point? point = null;
                Double[] minByRowArray = new Double[rowsValue];
                Double[] maxByColArray = new Double[colsValue];

                for (var r = 0; r < rowsValue; r++) {
                    minByRowArray[r] = array[r].Min();
                }
                for (var c = 0; c < colsValue; c++) {
                    var maxValue = Double.MinValue;

                    for (var r = 0; r < rowsValue; r++) {
                        maxValue = Math.Max(array[r][c], maxValue);
                    }
                    maxByColArray[c] = maxValue;
                }

                {
                    Table table = null;
                    var maxByMinByRowValue = minByRowArray.Max();
                    var minByMaxByColValue = maxByColArray.Min();
                    var maxByRowsCountValue = 0;
                    var maxByRowsIndexValue = -1;
                    var minByColsCountValue = 0;
                    var minByColsIndexValue = -1;

                    {
                        var i = 0;

                        foreach (var minByRowValue in minByRowArray) {
                            if (minByRowValue == maxByMinByRowValue) {
                                maxByRowsCountValue++;
                                maxByRowsIndexValue = i;
                            }
                            i++;
                        }
                    }
                    {
                        var i = 0;

                        foreach (var maxByColValue in maxByColArray) {
                            if (maxByColValue == minByMaxByColValue) {
                                minByColsCountValue++;
                                minByColsIndexValue = i;
                            }
                            i++;
                        }
                    }

                    table = new Table(rowsValue + 1, colsValue + 1).ForEach(c => new TextCell(""));
                    if (maxByRowsCountValue == 1 && minByColsCountValue == 1) {
                        point = new Point(minByColsIndexValue, maxByRowsIndexValue);
                    }

                    for (var r = 0; r < rowsValue; r++) {
                        for (var c = 0; c < colsValue; c++) {
                            var isSaddle = (point != null && (r == point?.Y && c == point?.X));

                            table[r, c] = new TextCell(array[r][c].ToString()) {
                                Fore = isSaddle ? Color.DarkGreen : Color.Black,
                            };
                        }
                    }
                    for (var r = 0; r < rowsValue; r++) {
                        var isMaxMin = minByRowArray[r] == maxByMinByRowValue;
                        var textString = String.Format("{0}{1}", minByRowArray[r], isMaxMin ? " *" : "");

                        table[r, colsValue] = new TextCell(textString) {
                            Fore = isMaxMin ? Color.Red : Color.Black
                        };
                    }
                    for (var c = 0; c < colsValue; c++) {
                        var isMinMax = maxByColArray[c] == minByMaxByColValue;
                        var textString = String.Format("{0}{1}", maxByColArray[c], isMinMax ? " *" : "");

                        table[rowsValue, c] = new TextCell(textString) {
                            Fore = isMinMax ? Color.Red : Color.Black
                        };
                    }

                    this.grid.Load(table);
                    this.saddleTable = table;
                }

            }

            this.UpdateLayout();
        }
    }
}