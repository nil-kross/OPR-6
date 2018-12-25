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

namespace Lomtseu {
    public partial class MainForm : Form {
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
        private Double[][] normalizedArray;
        private Double[][] strategiesArray;
        private Double[][] paretoArray;

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

        public MainForm() {
            this.InitializeComponent();

            this.mValue = MainForm.defaultMValue;

            this.UpdateLayout();
            this.OnGameModeChange(GameModes.MPerTwo);
            this.mTextBox.Text = MainForm.defaultMValue.ToString();

    }

        protected void ResizeLayout() {
            var x0 = 7;
            var x1 = 58;
            var panelPoint = new Point(0, 0);
            var panelSize = new Size(this.ClientSize.Width, this.panel.Height);
            var gridPoint = new Point(0, panelPoint.Y + panelSize.Height);
            var gridSize = new Size(this.ClientSize.Width, this.ClientSize.Height - gridPoint.Y);
            var tabControlSize = new Size(this.ClientSize.Width, this.tabControl.Height);
            var tabControlPoint = new Point(0, panelSize.Height - tabControlSize.Height);
            var nButtonPoint = new Point(this.selectedGameMode == GameModes.TwoPerM ? x0 : x1, this.nButton.Location.Y);
            var mTextBoxPoint = new Point(this.selectedGameMode == GameModes.TwoPerM ? x1 : x0, this.mTextBox.Location.Y);

            if (this.panel.Location != panelPoint) {
                this.panel.Location = panelPoint;
            }
            if (this.panel.Size != panelSize) {
                this.panel.Size = panelSize;
            }
            if (this.grid.Location != gridPoint) {
                this.grid.Location = gridPoint;
            }
            if (this.grid.Size != gridSize) {
                this.grid.Size = gridSize;
            }
            if (this.tabControl.Size != tabControlSize) {
                this.tabControl.Size = tabControlSize;
            }
            if (this.tabControl.Location != tabControlPoint) {
                this.tabControl.Location = tabControlPoint;
            }
            if (this.nButton.Location != nButtonPoint) {
                this.nButton.Location = nButtonPoint;
            }
            if (this.mTextBox.Location != mTextBoxPoint) {
                this.mTextBox.Location = mTextBoxPoint;
            }
        }

        protected void UpdateLayout() {
            this.ResizeLayout();

            this.mTextBox.BackColor = this.isMChanged ? Color.Yellow : Color.White;
            this.buildButton.BackColor = this.isModeChanged || this.isMChanged ? Color.Yellow : Color.White;
            this.startButton.Enabled = this.isBuilded;
            this.graphButton.Enabled = this.isStarted;

            this.tabControl.TabPages.Clear();
            if (this.isBuilded) {
                this.tabControl.TabPages.Add(inputTabPage);
            }
            if (this.isStarted) {
                this.tabControl.TabPages.Add(saddleTabPage);
                this.tabControl.TabPages.Add(paretoTabPage);
            }
        }

        private void OnGameModeChange(GameModes gameMode) {
            this.isModeChanged = this.gameMode != gameMode;
            this.selectedGameMode = gameMode;

            this.UpdateLayout();
        }

        private void OnResize(Object sender, EventArgs e) {
            this.ResizeLayout();
        }

        private void OnGameModeSwitchButtonClick(Object sender, EventArgs e) {
            this.OnGameModeChange(this.selectedGameMode == GameModes.MPerTwo ? GameModes.TwoPerM : GameModes.MPerTwo);
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
            if (this.gameMode == GameModes.MPerTwo) {
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

            this.tabControl.SelectTab(this.inputTabPage.Name);
        }

        private void OnStartButtonClick(Object sender, EventArgs e) {
            Double[][] array;
            var rowsValue = this.grid.Rows.Count - 1;
            var colsValue = this.grid.Columns.Count;

            array = new Double[rowsValue][];

            {
                this.tabControl.SelectTab(this.inputTabPage.Name);
                this.grid.Load(this.inputTable);

                for (var r = 0; r < rowsValue; r++) {
                    array[r] = new Double[colsValue];
                }
                {
                    var r = 0;

                    foreach (DataGridViewRow row in this.grid.Rows) {
                        if (r < this.grid.RowCount - 1) {
                            var c = 0;

                            if (r < this.grid.Rows.Count - 1) {
                                foreach (DataGridViewTextBoxCell col in row.Cells) {
                                    array[r][c] = Double.Parse(col.Value as String);

                                    c++;
                                }
                            }
                        }

                        r++;
                    }
                }
            }
            // Сохраняем inputTable
            {
                this.inputTable = this.grid.Save();
                this.strategiesArray = array;
                this.normalizedArray = this.DeleteNonPareto(this.strategiesArray);
                this.paretoArray = this.DeleteNonPareto(this.normalizedArray);
            }
            this.isStarted = true;

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
                            var isSaddle = (point != null && (r == point.Value.Y && c == point.Value.X));

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
            // Парето
            {
                for (var r = 0; r < rowsValue; r++) {
                    for (var c = 0; c < colsValue; c++) {
                        // TO DO
                    }
                }
            }

            this.UpdateLayout();

            this.tabControl.SelectTab(this.saddleTabPage.Name);
        }

        private void OnGraphButtonClick(object sender, EventArgs e) {
            // Нормализуем массив стратегий:
            {
                // TODO: нормализация массива

                // DEBUG
                this.normalizedArray = new Double[2][];
                this.normalizedArray[0] = new double[7] { -6, -1, 1, 4, 7, 4, 3 };
                this.normalizedArray[1] = new double[7] { 7, -2, 6, 3, -2, -5, 7 };
            }

            (new GraphForm(new StrategiesTable(this.normalizedArray, Direction.Max))).ShowDialog();
        }

        public Double[][] Normalize(Double[][] array) {
            Double[][] normArray = new Double[2][];

            if (true) {
                throw new NotImplementedException();
            }

            return normArray;
        }

        public Double[][] DeleteNonPareto(Double[][] array) {
            Double[][] paretoArray = null;

            {
                var lengthValue = array[0].Count();
                var isDominatedArray = new Boolean[lengthValue];

                for (var f = 0; f < lengthValue - 1; f++) {
                    for (var s = f + 1; s < lengthValue; s++) {
                        var isDominated = true;

                        for (var k = 0; k < 2; k++) {
                            isDominated = isDominated && (array[f][k] < array[s][k]);
                        }

                        isDominatedArray[s] = isDominatedArray[s] && isDominated;
                    }
                }

                {
                    var newLengthValue = 0;

                    for (var p = 0; p < array[0].Length; p++) {
                        newLengthValue += !isDominatedArray[p] ? 1 : 0;
                    }

                    paretoArray = new Double[2][];
                    for (var c = 0; c < 2; c++) {
                        paretoArray[c] = new Double[newLengthValue];
                    }

                    {
                        var i = 0;

                        for (var r = 0; r < array[0].Length; r++) {
                            if (!isDominatedArray[r]) {
                                for (var c = 0; c < 2; c++) {
                                    paretoArray[c][i] = array[c][r];
                                }
                                i++;
                            }
                        }
                    }
                }
            }

            return paretoArray;
        }

        private void OnTabControlSelectedIndexChanged(Object sender, TabControlCancelEventArgs e) {

            if (this.tabControl.SelectedTab == this.inputTabPage) {
                this.grid.Load(this.inputTable);
            }
            if (this.tabControl.SelectedTab == this.saddleTabPage) {
                this.grid.Load(this.saddleTable);
            }
            if (this.tabControl.SelectedTab == this.paretoTabPage) {
                this.grid.Load(this.paretoTable);
            }
        }
    }
}