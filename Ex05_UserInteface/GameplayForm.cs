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
    public partial class GameplayForm : Form
    {
        private bool m_IsPlayerOneTurn = true;
        private bool m_IsTwoPlayerMode;
        private Button[,] m_ButtonMatrix;
        private int m_SizeOfBoard;
        private readonly int sr_PixelsSpace = 1;
        private const string k_PlayerOneColor = "Red";
        private const string k_PlayerTwoColor = "Yellow";

        public GameplayForm(int i_SizeOfBoard, bool i_IsTwoPlayerMode)
        {
            m_ButtonMatrix = new Button[i_SizeOfBoard, i_SizeOfBoard];
            m_SizeOfBoard = i_SizeOfBoard;
            m_IsTwoPlayerMode = i_IsTwoPlayerMode;
            InitializeComponent();

        }

        private void GameplayForm_Load(object sender, EventArgs e)
        {
            int currentScreenWidth = Screen.PrimaryScreen.Bounds.Width;
            int currentScreenHeight = Screen.PrimaryScreen.Bounds.Height;
            
            bool isNoAvailableMoves = false;
            GameInformation.InitializeGame(m_IsTwoPlayerMode, m_SizeOfBoard);
            this.Text = "Othello - " + k_PlayerOneColor + "'s turn";
            InitializeButtonMatrix();
            updateBoard(ref isNoAvailableMoves);
            this.Location = new Point((currentScreenWidth - this.Width) / 2, (currentScreenHeight - this.Height) / 2);
        }


        public void GameManager(string i_PlayerMove)
        {
            bool isNoAvailableMoves = false;
            makePlayerMove(i_PlayerMove, ref isNoAvailableMoves);
            while (!m_IsTwoPlayerMode && !m_IsPlayerOneTurn && !isNoAvailableMoves)
            {
                i_PlayerMove = GameInformation.GetMoveFromComputer();
                makePlayerMove(i_PlayerMove, ref isNoAvailableMoves);
            }
            if (m_IsPlayerOneTurn)
            {
                this.Text = "Othello - " + k_PlayerOneColor+"'s turn";
            }
            else
            {
                this.Text = "Othello - " + k_PlayerTwoColor + "'s turn";
            }
        }

        
        private void makePlayerMove(string i_PlayerMove, ref bool i_IsNoAvailableMoves)
        {
            GameInformation.ChangeBoardAccordingToMove(i_PlayerMove, m_IsPlayerOneTurn);
            m_IsPlayerOneTurn = !m_IsPlayerOneTurn;
            updateBoard(ref i_IsNoAvailableMoves);
            if (i_IsNoAvailableMoves)
            {
                if (CheckIfEndGame())
                {
                    this.Close();
                }
                else if (m_IsPlayerOneTurn)
                {
                    MessageBox.Show(k_PlayerOneColor +" has no available moves! Your turn will be skipped!");
                }
                else
                {
                    MessageBox.Show(k_PlayerTwoColor+ " has no available moves! Your turn will be skipped!");
                }
                m_IsPlayerOneTurn = !m_IsPlayerOneTurn;
                updateBoard(ref i_IsNoAvailableMoves);
            }
        }

        private bool CheckIfEndGame()
        {
            int playerOneCoinsCounter = 0;
            int playerTwoCoinsCounter = 0;
            bool isEndGame = false;
            GameInformation.eWinner winnerID;
            if (GameInformation.IsEndOfGame())
            {
                isEndGame = true;
                winnerID = GameInformation.CheckWhoWon(ref playerOneCoinsCounter, ref playerTwoCoinsCounter);
                if (winnerID == GameInformation.eWinner.Player1)
                {
                    GameInformation.PlayerOneScore++;
                    showEndGameMessage(k_PlayerOneColor, playerOneCoinsCounter, playerTwoCoinsCounter);
                }
                else if (winnerID == GameInformation.eWinner.Player2)
                {

                    GameInformation.PlayerTwoScore++;
                    showEndGameMessage(k_PlayerTwoColor, playerOneCoinsCounter, playerTwoCoinsCounter);
                }
                else
                {
                    showEndGameMessage("Tie", playerOneCoinsCounter, playerTwoCoinsCounter);
                }
            }
            return isEndGame;
        }
       
        private void showEndGameMessage(string i_WinnerName, int i_playerOneCoinsCounter, int i_playerTwoCoinsCounter)
        {
            string winningMessage = string.Format("{5} Won!! ({1}/{2}) {0}Total wins ("+ k_PlayerOneColor+"/"+ k_PlayerTwoColor+"): ({3}/{4}){0}Would you like another round?", Environment.NewLine, i_playerOneCoinsCounter, i_playerTwoCoinsCounter, GameInformation.PlayerTwoScore, GameInformation.PlayerOneScore, i_WinnerName);
            string tieMessage = string.Format("It's a tie!! ({1},{2}) {0}Would you like another round?", Environment.NewLine, i_playerOneCoinsCounter, i_playerTwoCoinsCounter);
            if (i_WinnerName == "Tie")
            {
                if (MessageBox.Show(tieMessage, "Othello", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    closeWindowAndOpenNew();
                }
                else
                {
                    MessageBox.Show("Thanks for playing!!");
                }
            }
            else if (MessageBox.Show(winningMessage, "Othello", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                closeWindowAndOpenNew();
            }
            else
            {
                MessageBox.Show("Thanks for playing!!");
            }
        }

        private void closeWindowAndOpenNew()
        {
            this.Hide();
            this.Close();
            GameplayForm newGameplayForm = new GameplayForm(m_SizeOfBoard, m_IsTwoPlayerMode);
            newGameplayForm.ShowDialog();
        }

        private bool updateValidMovesHighlight()
        {
            bool isNoAvailableTurn = false;
            List<NodeForGame> newValidMovesList = GameInformation.MakeValidMovesList(m_IsPlayerOneTurn);

            if ((!m_IsTwoPlayerMode && m_IsPlayerOneTurn) || m_IsTwoPlayerMode)
            {
                foreach (NodeForGame gameNode in newValidMovesList)
                {
                    m_ButtonMatrix[gameNode.LineNumber, gameNode.ColumnNumber].BackColor = System.Drawing.Color.Green;
                    m_ButtonMatrix[gameNode.LineNumber, gameNode.ColumnNumber].Enabled = true;
                }
            }
            if (newValidMovesList.Count == 0)
            {
                isNoAvailableTurn = true;
            }
            return isNoAvailableTurn;
        }

        private void updateBoard(ref bool o_IsNoAvailableMoves)
        {
            char[,] gameBoardMatrix = GameInformation.MainBoard.BoardMatrix;
            for (int i = 0; i < m_SizeOfBoard; i++)
            {
                for (int j = 0; j < m_SizeOfBoard; j++)
                {
                    m_ButtonMatrix[i, j].Enabled = false;
                    if (m_ButtonMatrix[i, j].BackColor == System.Drawing.Color.Green)
                    {
                        m_ButtonMatrix[i, j].BackColor = System.Drawing.SystemColors.ActiveCaption;
                    }
                    if (gameBoardMatrix[i, j] == 'X')
                    {
                        m_ButtonMatrix[i, j].BackgroundImage = Properties.Resources.CoinRed;
                        m_ButtonMatrix[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                    }
                    if (gameBoardMatrix[i, j] == 'O')
                    {
                        m_ButtonMatrix[i, j].BackgroundImage = Properties.Resources.CoinYellow;
                        m_ButtonMatrix[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                    }
                }
            }
            o_IsNoAvailableMoves = updateValidMovesHighlight();
        }

        public void InitializeButtonMatrix()
        {
            for (int i = 0; i < m_SizeOfBoard; i++)
            {
                for (int j = 0; j < m_SizeOfBoard; j++)
                {
                    m_ButtonMatrix[i, j] = new Button();
                    m_ButtonMatrix[i, j].Click += new System.EventHandler(this.doWhenClicked);
                    string msg = string.Format("{0}{1}", (char)(j + 'A'), i + 1);
                    m_ButtonMatrix[i, j].Name = msg;
                    if (i == 0 && j == 0)
                    {
                        m_ButtonMatrix[0, 0].Location = new Point(3, 3);
                    }
                    else if (j == 0)
                    {
                        Button lastButton = m_ButtonMatrix[i - 1, 0];
                        m_ButtonMatrix[i, 0].Top = sr_PixelsSpace + lastButton.Top + lastButton.Height;
                        m_ButtonMatrix[i, 0].Left = lastButton.Left;
                    }
                    else
                    {
                        Button lastButton = m_ButtonMatrix[i, j - 1];
                        m_ButtonMatrix[i, j].Left = sr_PixelsSpace + lastButton.Left + lastButton.Width;
                        m_ButtonMatrix[i, j].Top = lastButton.Top;
                    }
                    m_ButtonMatrix[i, j].FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    m_ButtonMatrix[i, j].Size = new Size(50, 50);
                    this.Controls.Add(m_ButtonMatrix[i, j]);
                }
            }
        }

        private void doWhenClicked(object sender, EventArgs e)
        {
            Button senderButton = sender as Button;
            if (senderButton != null && GameInformation.CheckMoveValidation(senderButton.Name, m_IsPlayerOneTurn))
            {
                GameManager(senderButton.Name);
            }
            else
            {
                MessageBox.Show("Invalid move selected, please try again");
            }
        }
    }
}