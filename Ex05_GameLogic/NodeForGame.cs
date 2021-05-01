using System;
using System.Collections.Generic;
using System.Text;

namespace Ex05_GameLogic
{

    public class NodeForGame
    {
        private int m_LineNumberInBoard;
        private int m_ColumnNumberInBoard;
        
        public NodeForGame(int i_LineNumber, int i_ColumnNumber)
        {
            LineNumber = i_LineNumber;
            ColumnNumber = i_ColumnNumber;
        }

        public int LineNumber
        {
            get
            {
                return m_LineNumberInBoard;
            }
            set
            {
                m_LineNumberInBoard = value;
            }
        }

        public int ColumnNumber
        {
            get
            {
                return m_ColumnNumberInBoard;
            }
            set
            {
                m_ColumnNumberInBoard = value;
            }
        }
    }
}
