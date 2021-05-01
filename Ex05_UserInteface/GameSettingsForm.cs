using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Ex05_GameLogic;

namespace Ex05_UserInteface
{
    public partial class GameSettingsForm : Form
    {
        private int m_CountBoardSizeClicks;
        private int m_SizeOfBoard;
        private bool m_IsTwoPlayerMode;

        public GameSettingsForm()
        {
            InitializeComponent();            
        }

        private void GameSettingsForm_Load(object sender, EventArgs e)
        {
            m_CountBoardSizeClicks = 1;
            m_SizeOfBoard = 6;
        }

        private void DoWhenClicked(object sender, EventArgs e)
        {
            m_CountBoardSizeClicks += 1;
            Button boardSizeButton = sender as Button;
            if (m_CountBoardSizeClicks % 4 == 1) 
            {
                boardSizeButton.Text = "Board size: 6x6 (Click to increase)";
                m_SizeOfBoard = 6;
            }
            else if (m_CountBoardSizeClicks % 4 == 2)
            {
                boardSizeButton.Text = "Board size: 8x8 (Click to increase)";
                m_SizeOfBoard = 8;
            }
            else if (m_CountBoardSizeClicks%4==3)
            {
                boardSizeButton.Text = "Board size: 10x10 (Click to increase)";
                m_SizeOfBoard = 10;
            }
            else
            {
                boardSizeButton.Text = "Board size: 12x12 (Click to increase)";
                m_SizeOfBoard = 12;
            }
        }

        private void PlayComputerButton_Click(object sender, EventArgs e)
        {
            m_IsTwoPlayerMode = false;
            executeNewGame();
        }

        private void PlayTwoPlayersButton_Click(object sender, EventArgs e)
        {
            m_IsTwoPlayerMode = true;
            executeNewGame();
        }

        private void executeNewGame()
        {
            this.Hide();
            GameplayForm newGameplayForm = new GameplayForm(m_SizeOfBoard, m_IsTwoPlayerMode);
            newGameplayForm.ShowDialog();
        }
    }
}
