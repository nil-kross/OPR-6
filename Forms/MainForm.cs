using Lomtseu.GamesTheory;
using Lomtseu.Tables;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Lomtseu {
    public partial class MainForm : Form {
        private static Size defaultSizeValue = new Size(2, 5);

        private IArrayFromGridParser arrayFromGridParser = new ArrayFromGridParser();
        private ISaddlePointResolver saddlePointResolver = new SaddlePointResolver();
        private IParetoPointsResolver paretoPointsResolver = new ParetoPointsResolver();
        private Byte rowsCountValue = 0;
        private Byte colsCountValue = 0;
        private Boolean isBuilded = false;
        private Boolean isStarted = false;
        private Boolean isRowsCountChanged = false;
        private Boolean isModeChanged = false;
        private GameModes gameMode;
        private GameModes selectedGameMode;
        private Table inputTable;
        private Table saddleTable;
        private Table paretoTable;
        private Double[][] normalizedArray;
        private Double[][] strategiesArray;
        private Double[][] paretoArray;
        private StrategiesTable strategiesTable;

        protected Byte RowsCount {
            get { return this.rowsCountValue; }
            set { this.rowsCountValue = value; }
        }

        protected Byte ColsCount {
            get { return this.colsCountValue; }
            set { this.colsCountValue = value; }
        }

        protected Nullable<Byte> InputRowsCount {
            get {
                Byte? rowsCountValue = null;

                {
                    Byte numberValue;

                    if (Byte.TryParse(this.rowsCountTextBox.Text, out numberValue)) {
                        rowsCountValue = numberValue;
                    }
                }

                return rowsCountValue;
            }
            set {
                this.rowsCountTextBox.Text = value.ToString();
            }
        }

        protected Nullable<Byte> InputColsCount {
            get {
                Byte? colsCountValue = null;

                {
                    Byte numberValue;

                    if (Byte.TryParse(this.colsCountTextBox.Text, out numberValue)) {
                        colsCountValue = numberValue;
                    }
                }

                return colsCountValue;
            }
            set {
                this.colsCountTextBox.Text = value.ToString();
            }
        }

        public MainForm() {
            this.InitializeComponent();

            this.rowsCountValue = (Byte)MainForm.defaultSizeValue.Height;
            this.colsCountValue = (Byte)MainForm.defaultSizeValue.Width;

            this.UpdateLayout();
            this.OnGameModeChange(GameModes.TwoPerM);
            this.InputRowsCount = this.RowsCount;
            this.InputColsCount = this.ColsCount;
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
        }

        protected void UpdateLayout() {
            this.ResizeLayout();

            this.rowsCountTextBox.BackColor = this.isRowsCountChanged ? Color.Yellow : Color.White;
            this.buildButton.BackColor = this.isModeChanged || this.isRowsCountChanged ? Color.Yellow : Color.White;
            this.startButton.Enabled = this.isBuilded;
            this.graphButton.Enabled = this.isStarted;

            this.tabControl.TabPages.Clear();
            if (this.isBuilded) {
                this.tabControl.TabPages.Add(this.inputTabPage);
            }
            if (this.isStarted) {
                this.tabControl.TabPages.Add(this.saddleTabPage);
                this.tabControl.TabPages.Add(this.paretoTabPage);
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
            var rowsCountValue = this.InputRowsCount;
            var colsCountValue = this.InputColsCount;

            this.InputRowsCount = colsCountValue;
            this.InputColsCount = rowsCountValue;
        }

        private void OnMTextBoxLeave(Object sender, EventArgs e) {
            this.isRowsCountChanged = this.InputRowsCount == this.RowsCount;

            this.UpdateLayout();
        }

        private void OnBuildButtonClick(Object sender, EventArgs e) {
            this.RowsCount = this.InputRowsCount.Value;
            this.ColsCount = this.InputColsCount.Value;

            this.isModeChanged = false;
            this.isBuilded = true;
            
            {
                Table table = new Table(this.RowsCount, this.ColsCount)
                    .ForEach((cell, r, c) => new TextCell(r.ToString()));

                this.grid.Load(table);
                this.inputTable = table;
            }

            this.UpdateLayout();
            this.tabControl.SelectTab(this.inputTabPage.Name);
        }

        private void OnStartButtonClick(Object sender, EventArgs e) {
            Double[][] array = null;
            var rowsValue = this.grid.Rows.Count - 1;
            var colsValue = this.grid.Columns.Count;

            if (this.tabControl.SelectedTab != this.inputTabPage) {
                this.tabControl.SelectTab(this.inputTabPage.Name);
                this.grid.Load(this.inputTable);
            }
            array = this.arrayFromGridParser.Parse(this.grid);
            
            if (array != null) {
                Matrix strategiesMatrix = null;
                Matrix normalizedMatrix = null;

                // Сохраняем inputTable
                this.inputTable = this.grid.Save();
                this.strategiesArray = array;
                strategiesMatrix = new Matrix(this.strategiesArray);
                normalizedMatrix = this.Normalize(this.strategiesArray);
                this.normalizedArray = normalizedMatrix.ToArray();
                // Седловая точка
                {
                    var saddlePointModel = this.saddlePointResolver.Resolve(strategiesMatrix);

                    this.grid.Load(saddlePointModel.Table);
                    this.saddleTable = saddlePointModel.Table;
                }
                // Парето
                {
                    var model = this.paretoPointsResolver.Resolve(normalizedMatrix);

                    this.paretoArray = model.Array;
                    this.paretoTable = !(strategiesMatrix.RowsCount == normalizedMatrix.RowsCount && strategiesMatrix.ColsCount == normalizedMatrix.ColsCount) 
                                            ? model.Table.Rotate()
                                            : model.Table;
                }
            }
            this.isStarted = true;

            this.UpdateLayout();

            this.tabControl.SelectTab(this.saddleTabPage.Name);
        }

        private void OnGraphButtonClick(Object sender, EventArgs e) {
            // Нормализуем массив стратегий:
            {
                // TODO: нормализация массива

                // DEBUG
                this.normalizedArray = new Double[2][];
                this.normalizedArray[0] = new Double[7] { -6, -1, 1, 4, 7, 4, 3 };
                this.normalizedArray[1] = new Double[7] { 7, -2, 6, 3, -2, -5, 7 };
            }

            (new GraphForm(new StrategiesTable(this.normalizedArray, Direction.Max))).ShowDialog();
        }

        public Matrix Normalize(Double[][] array) {
            Matrix matrix = new Matrix(array);

            if (matrix.RowsCount != 2) {
                if (matrix.ColsCount == 2) {
                    return matrix.Rotate();
                } else {
                    throw new Exception("One of matrix dimensions should equals 2!");
                }
            }

            return matrix;
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