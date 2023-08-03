using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        private const int BoardSize = 3;
        private const int ButtonSize = 100;
        private Button[,] buttons = new Button[BoardSize, BoardSize];
        private bool isXTurn;

        public Form1()
        {
            InitializeComponent();
            InitializeGrid();
            Reset();
        }

        private void InitializeGrid()
        {
            for (int i = 0; i < BoardSize; i++)
                for (int j = 0; j < BoardSize; j++)
                {
                    buttons[i, j] = new Button { Location = new Point(i * ButtonSize, j * ButtonSize), Size = new Size(ButtonSize, ButtonSize) };
                    buttons[i, j].Click += Button_Click;
                    buttons[i, j].ForeColor = Color.White;
                    Controls.Add(buttons[i, j]);
                }
        }

        private void Reset()
        {
            isXTurn = true;
            for (int i = 0; i < BoardSize; i++)
                for (int j = 0; j < BoardSize; j++)
                    buttons[i, j].Text = "";
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button.Text != "")
                return;

            button.Text = isXTurn ? "X" : "O";
            isXTurn = !isXTurn;

            if (CheckWin())
            {
                MessageBox.Show($"{(isXTurn ? "O" : "X")} Wins!");
                Reset();
            }
            else if (CheckDraw())
            {
                MessageBox.Show("It is a draw!");
                Reset();
            }

        }
        private bool CheckDraw()
        {
            for (int i = 0; i < BoardSize; i++)
                for (int j = 0; j < BoardSize; j++)
                    if (buttons[i, j].Text == "")
                        return false;
            return true;
        }

        private bool CheckWin()
        {
            for (int i = 0; i < BoardSize; i++)
                if (CheckLine(buttons[i, 0].Text, buttons[i, 1].Text, buttons[i, 2].Text) || CheckLine(buttons[0, i].Text, buttons[1, i].Text, buttons[2, i].Text))
                    return true;

            if (CheckLine(buttons[0, 0].Text, buttons[1, 1].Text, buttons[2, 2].Text) || CheckLine(buttons[2, 0].Text, buttons[1, 1].Text, buttons[0, 2].Text))
                return true;

            return false;
        }

        private bool CheckLine(string cell1, string cell2, string cell3)
        {
            return cell1 == cell2 && cell2 == cell3 && cell1 != "";
        }
    }
}