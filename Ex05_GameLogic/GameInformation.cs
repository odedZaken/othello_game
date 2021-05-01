using System;
using System.Collections.Generic;
using System.Text;

namespace Ex05_GameLogic
{
    public static class GameInformation
    {
        public enum eDirection
        {
            Up,
            UpRight,
            Right,
            DownRight,
            Down,
            DownLeft,
            Left,
            UpLeft,
        }

        public enum eWinner
        {
            Tie,
            Player1,
            Player2,
        }

        private static int m_PlayerOneScore = 0;
        private static int m_PlayerTwoScore = 0;
        private static Player m_Player1 = new Player('X');
        private static Player m_Player2 = new Player('O');
        private static bool m_IsTwoPlayersMode = true;
        private static int m_SizeOfGameBoard;
        private static Board m_MainBoard;

        public static Board MainBoard
        {
            get { return m_MainBoard; }
        }

        public static void InitializeGame(bool i_IsTwoPlayerMode, int i_SizeOfGameBoard)
        {
            m_IsTwoPlayersMode = i_IsTwoPlayerMode;
            m_SizeOfGameBoard = i_SizeOfGameBoard;
            m_MainBoard = new Board(i_SizeOfGameBoard);
        }

        public static int PlayerOneScore
        {
            get { return m_PlayerOneScore; }
            set { m_PlayerOneScore = value; }
        }

        public static int PlayerTwoScore
        {
            get { return m_PlayerTwoScore; }
            set { m_PlayerTwoScore = value; }
        }



       
        public static string GetMoveFromComputer()
        {
            bool isPlayerOneTurn = false;
            char[] computerMoveString = new char[2];
            List<NodeForGame> listOfValidMoveForComputer = MakeValidMovesList(isPlayerOneTurn);
            Random computerTurn = new Random();
            int moveIndex = computerTurn.Next(listOfValidMoveForComputer.Count);
            NodeForGame chosenNode = listOfValidMoveForComputer[moveIndex];
            computerMoveString[0] = (char)(chosenNode.ColumnNumber + 'A');
            computerMoveString[1] = (char)(chosenNode.LineNumber + '0' + 1);
            string toReturn = new string(computerMoveString);
            return toReturn;
        }

        public static bool CheckMoveValidation(string i_StrinputFromUserDuringTheGame, bool i_IsPlayerOneTurn)
        {
            bool isValidMove = false;
            NodeForGame nodeOfTheUserMove = StringOfMoveToNodeForGame(i_StrinputFromUserDuringTheGame);
            List<NodeForGame> newValidMovesList = MakeValidMovesList(i_IsPlayerOneTurn);
           
            for (int i = 0; i < newValidMovesList.Count; i++)
            {
                if (newValidMovesList[i].LineNumber == nodeOfTheUserMove.LineNumber && newValidMovesList[i].ColumnNumber == nodeOfTheUserMove.ColumnNumber)
                {
                    isValidMove = true;
                }
            }
            return isValidMove;
        }

        public static NodeForGame StringOfMoveToNodeForGame(string i_StringOfMove)
        {
            int lineForBoard;
            int columnForBoard;
            if (i_StringOfMove.Length == 2)
            {
                lineForBoard = i_StringOfMove[1] - '0' - 1;
            }
            else
            {
                lineForBoard = i_StringOfMove[2] - '0' - 1 + 10;
            }
            columnForBoard = (i_StringOfMove[0] - 'A');
            NodeForGame theNewNodeToReturn = new NodeForGame(lineForBoard, columnForBoard);
            return theNewNodeToReturn;
        }

        public static void ChangeBoardAccordingToMove(string i_MoveFromUserToPlay, bool i_IsPlayerOneTurn)
        {
            Player currentPlayer;
            if (i_IsPlayerOneTurn)
            {
                currentPlayer = m_Player1;
            }
            else
            {
                currentPlayer = m_Player2;
            }
            List<NodeForGame> NodeToChangeList = new List<NodeForGame>(2);
            NodeForGame TheValidMoveFromUser = StringOfMoveToNodeForGame(i_MoveFromUserToPlay);

            NodeToChangeList.Add(TheValidMoveFromUser);
            CheckEightDirectionForMove(NodeToChangeList, currentPlayer, TheValidMoveFromUser, false);
            ChangBoardMatrix(NodeToChangeList, currentPlayer.m_PlayerSymbol);
        }

        public static void ChangBoardMatrix(List<NodeForGame> i_NodeToChangeList, char i_ChangeToThisSymbol)
        {
            for (int k = 0; k < i_NodeToChangeList.Count; k++)
            {
                int i_Line = i_NodeToChangeList[k].LineNumber;
                int j_Column = i_NodeToChangeList[k].ColumnNumber;
                m_MainBoard.BoardMatrix[i_Line, j_Column] = i_ChangeToThisSymbol;
               
            }
        }

        public static bool IsEndOfGame()
        {
            bool isGameContinue = true;
            List<NodeForGame> ListOfValidMoveForPlayer1 = MakeValidMovesList(true);
            List<NodeForGame> ListOfValidMoveForPlayer2 = MakeValidMovesList(false);
            if (ListOfValidMoveForPlayer1.Count == 0 && ListOfValidMoveForPlayer2.Count == 0)
            {
                isGameContinue = false;
            }
            return !isGameContinue;
        }

        public static bool IsPlayerHaveNoAvailableMoves(bool i_IsPlayerOneTurn)
        {
            bool noAvailableMoves = false;
            List<NodeForGame> ListOfValidMoveForPlayer = MakeValidMovesList(i_IsPlayerOneTurn);
            if(ListOfValidMoveForPlayer.Count == 0)
            {
                noAvailableMoves = true;
            }
            return noAvailableMoves;
        }

        public static eWinner CheckWhoWon(ref int i_BlackCoinsCounter,ref int i_WhiteCoinsCounter)
        {
            eWinner winningPlayerIndex = eWinner.Tie;
            for (int i = 0; i < m_MainBoard.BoardSize; i++)
            {
                for (int j = 0; j < m_MainBoard.BoardSize; j++)
                {
                    if (m_MainBoard.BoardMatrix[i, j] == 'X')
                    {
                        i_BlackCoinsCounter++;
                    }
                    else if (m_MainBoard.BoardMatrix[i, j] == 'O')
                    {
                        i_WhiteCoinsCounter++;
                    }
                }
            }
            if (i_WhiteCoinsCounter > i_BlackCoinsCounter)
            {
                winningPlayerIndex = eWinner.Player2;
            }
            else if (i_WhiteCoinsCounter < i_BlackCoinsCounter)
            {
                winningPlayerIndex = eWinner.Player1;
            }
            else
            {
                winningPlayerIndex = eWinner.Tie;
            }
            return winningPlayerIndex;
        }

        public static List<NodeForGame> MakeValidMovesList(bool i_IsPlayerOneTurn)
        {
            Player playerInMove = m_Player2;
            List<NodeForGame> validMoveList = new List<NodeForGame>(2);
            if (i_IsPlayerOneTurn)
            {
                playerInMove = m_Player1;
            }
            for (int i = 0; i < m_MainBoard.BoardSize; i++)
            {
                for (int j = 0; j < m_MainBoard.BoardSize; j++)
                {
                    if (m_MainBoard.BoardMatrix[i, j] == playerInMove.m_PlayerSymbol)
                    {
                        NodeForGame currentNodeToCheck = new NodeForGame(i, j);
                        CheckEightDirectionForMove(validMoveList, playerInMove, currentNodeToCheck, true);
                    }
                }
            }
            return validMoveList;
        }

        public static void CheckEightDirectionForMove(List<NodeForGame> io_OptionForMoveList, Player i_PlayerinMove, NodeForGame i_CurrentNodeToCheck, bool i_IsCheckOnly)
        {
            for (eDirection i = eDirection.Up; i <= eDirection.UpLeft; i++)
            {
                CheckCourseOfMove(i, i_PlayerinMove, i_CurrentNodeToCheck, io_OptionForMoveList, i_IsCheckOnly);
            }
        }

        public static void CheckCourseOfMove(eDirection i_CurrentDirection, Player i_PlayerInMove, NodeForGame i_CurrentNodeToCheck, List<NodeForGame> io_OptionForCoursesList, bool i_IsCheckOnly)
        {
            List<NodeForGame> optionNodesToChange = new List<NodeForGame>(2);
            bool noOppositeSymbolInRoute = true;
            char opponentSymbol = 'X';
            if (i_PlayerInMove.m_PlayerSymbol == 'X')
            {
                opponentSymbol = 'O';
            }

            int i_Line = i_CurrentNodeToCheck.LineNumber;
            int j_Column = i_CurrentNodeToCheck.ColumnNumber;
            if (GetNextNodeInRoute(ref i_Line, ref j_Column, i_CurrentDirection, m_MainBoard.BoardSize))
            {
                CheckingForOppositesymbol(m_MainBoard, opponentSymbol, i_IsCheckOnly, ref optionNodesToChange, ref noOppositeSymbolInRoute, i_CurrentDirection, ref i_Line, ref j_Column);
            }

            if (m_MainBoard.BoardMatrix[i_Line, j_Column] == '\0' && (!noOppositeSymbolInRoute) && i_IsCheckOnly)
            {
                NodeForGame newValidOption = new NodeForGame(i_Line, j_Column);
                io_OptionForCoursesList.Add(newValidOption);
            }

            if (m_MainBoard.BoardMatrix[i_Line, j_Column] == i_PlayerInMove.m_PlayerSymbol && (!noOppositeSymbolInRoute) && (!i_IsCheckOnly))
            {
                AddNewSmallListToBigList(io_OptionForCoursesList, optionNodesToChange, io_OptionForCoursesList.Count);
            }
        }

        public static bool GetNextNodeInRoute(ref int io_Line, ref int io_Column, eDirection i_CurrentDirection, int i_MatrixSize)
        {
            int original_i = io_Line;
            int original_j = io_Column;
            bool isOutOfMatrix = true;
            if ((i_CurrentDirection == eDirection.Up) || (i_CurrentDirection == eDirection.UpLeft) || (i_CurrentDirection == eDirection.UpRight))
            {
                io_Line--;
            }
            if ((i_CurrentDirection == eDirection.Down) || (i_CurrentDirection == eDirection.DownLeft) || (i_CurrentDirection == eDirection.DownRight))
            {
                io_Line++;
            }
            if ((i_CurrentDirection == eDirection.Right) || (i_CurrentDirection == eDirection.UpRight) || (i_CurrentDirection == eDirection.DownRight))
            {
                io_Column++;
            }
            if ((i_CurrentDirection == eDirection.Left) || (i_CurrentDirection == eDirection.DownLeft) || (i_CurrentDirection == eDirection.UpLeft))
            {
                io_Column--;
            }
            if ((io_Line < i_MatrixSize && io_Line >= 0) && (io_Column < i_MatrixSize && io_Column >= 0))
            {
                isOutOfMatrix = false;
            }
            else
            {
                io_Line = original_i;
                io_Column = original_j;
            }
            return !isOutOfMatrix;
        }

        public static void AddNewSmallListToBigList(List<NodeForGame> i_TheBigList, List<NodeForGame> i_TheSmallList, int i_IndexInBigList)
        {
            for (int i = 0; i < i_TheSmallList.Count; i++)
            {
                i_TheBigList.Add(i_TheSmallList[i]);
                i_IndexInBigList++;
            }
        }

      

        public static void CheckingForOppositesymbol(Board i_MainBoard, char i_OpponentSymbol, bool i_IsCheckOnly, ref List<NodeForGame> io_OptionNodesToChange, ref bool io_NoOppositeSymbolInRoute, eDirection i_CurrentDirection, ref int io_Line, ref int io_Column)
        {
            while (i_MainBoard.BoardMatrix[io_Line, io_Column] == i_OpponentSymbol)
            {
                if (!i_IsCheckOnly)
                {
                    NodeForGame newNodeForOptionalChange = new NodeForGame(io_Line, io_Column);
                    io_OptionNodesToChange.Add(newNodeForOptionalChange);
                }
                io_NoOppositeSymbolInRoute = false;
                if (GetNextNodeInRoute(ref io_Line, ref io_Column, i_CurrentDirection, i_MainBoard.BoardSize))
                {
                    continue;
                }
                else
                {
                    break;
                }
            }
        }
    }
}