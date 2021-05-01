using System;
using System.Collections.Generic;
using System.Text;

namespace Ex05_GameLogic
{
    
    public class Board
    {
        
        private int m_SizeOfBoard;
        private char[,] m_GameBoardMatrix;
        
        public Board(int i_DesiredMatrixSize)
        {
            BoardSize = i_DesiredMatrixSize;
            m_GameBoardMatrix = new Char[i_DesiredMatrixSize, i_DesiredMatrixSize];

            m_GameBoardMatrix[i_DesiredMatrixSize / 2, i_DesiredMatrixSize / 2] = 'X';
            m_GameBoardMatrix[i_DesiredMatrixSize / 2, i_DesiredMatrixSize / 2 - 1] = 'O';
            m_GameBoardMatrix[i_DesiredMatrixSize / 2 - 1, i_DesiredMatrixSize / 2] = 'O';
            m_GameBoardMatrix[i_DesiredMatrixSize / 2 - 1, i_DesiredMatrixSize / 2 - 1] = 'X';

        }
        


        public int BoardSize
        {
            get { return m_SizeOfBoard; }
            set { m_SizeOfBoard = value; }           
        }

        public char[,] BoardMatrix
        {
            get { return m_GameBoardMatrix; }
            set { m_GameBoardMatrix = value; }
        }
        
    }    
}
