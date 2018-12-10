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

        private Boolean isMChanged = false;
        private Int32 mValue;
        private GameModes gameMode;

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

            this.ResizeLayout();
            this.OnGameModeChange(GameModes.MxN);
            this.mTextBox.Text = MainForm.defaultMValue.ToString();
        }

        protected void ResizeLayout()
        {
            var height = 40;

            this.grid.Location = new Point(0, height);
            this.grid.Size = new Size(this.ClientSize.Width, this.ClientSize.Height - height);
        }

        protected void UpdateGameMode(GameModes gameMode) {
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

            {

            }
        }

        protected void UpdateMTextBox() {
            this.mTextBox.BackColor = this.isMChanged ? Color.LightYellow : Color.White;
        }

        private void OnGameModeChange(GameModes gameMode) {
            this.gameMode = gameMode;
            this.UpdateGameMode(this.gameMode);
        }

        private void OnResize(object sender, EventArgs e)
        {
            this.ResizeLayout();
        }

        private void OnGameModeSwitchButtonClick(Object sender, EventArgs e) {
            this.OnGameModeChange(this.gameMode == GameModes.MxN ? GameModes.NxM : GameModes.MxN);
        }

        private void OnMTextBoxLeave(Object sender, EventArgs e) {
            if (this.mValue != this.M) {
                this.isMChanged = true;
            }
            this.UpdateMTextBox();
        }

        private void startButton_Click(Object sender, EventArgs e) {
            var rowsValue = 0;
            var colsValue = 0;

            this.M = this.M;

            if (this.gameMode == GameModes.MxN) {
                rowsValue = this.M;
                colsValue = 2;
            } else {
                rowsValue = 2;
                colsValue = this.M;
            }

            {
                Table t = new Table(rowsValue, colsValue).ForEach((cell, r, c) => new TextCell("0"));

                this.grid.Load(t);
            }
        }
    }
}