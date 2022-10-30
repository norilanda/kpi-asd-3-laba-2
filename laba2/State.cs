using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba2
{
    internal class State
    {
        public int[] board;
        public State(int N)
        {
            board = new int[N]; 
        }
        public State(int[] board)
        {
            this.board = board;
        }
        public State(State state)
        {
            this.board = new int[state.board.Length];
            Array.Copy(state.board, this.board, state.board.Length);
        }
        
        public bool IsGoal()  //check if all the queens are in correct places
        {
            for (int j = 1; j< board.Length;j++)    // check columns from the end
            {
                for(int k=j-1;k>=0;k--) // with every column before j one
                {
                    if(board[j]== board[k])  //if in the same row
                        return false;
                    int diff = j - k;   //how far the column k is from column j
                    if (board[j] - diff == board[k] || board[j] + diff == board[k])   //check diagonals
                        return false;
                }
            }
            return true;
        }
        public bool IsSafe(int row, int columnIndex)   //checks if new queen does not break the rules in compare with previous located
        {
            for (int k = columnIndex - 1; k >= 0; k--)
            {
                if (row == board[k])  //if in the same row
                    return false;
                int diff = columnIndex - k;
                if (row - diff == board[k] || row + diff == board[k])   //check diagonals
                    return false;
            }
            return true;
        }
        public void PlaceQueen(int row, int columnIndex)
        {
            board[columnIndex] = row;
        }
        public int F2(int depth)
        {
            int sum = 0;   //h(n)         
            int[] numberOfQueenInRow = new int[board.Length];
            for(int i=0; i<board.Length; i++)
            {
                numberOfQueenInRow[board[i]]++;
            }
            for(int i=0; i< numberOfQueenInRow.Length;i++)  //calculate attacking pairs in rows
            {
                if (numberOfQueenInRow[i] > 1)
                    sum += CalculateCombinations(numberOfQueenInRow[i]);
            }
            for (int j = board.Length-1; j > 0; j--)    // check columns from the end
            {
                for (int k = j - 1; k >= 0; k--) // check with in column before j
                {                    
                    int diff = j - k;   //how far the column k is from column j
                    if (board[j] - diff == board[k])   //check primary diagonals
                        sum++;
                    if (board[j] + diff == board[k])    //check secondary diagonals
                        sum++;
                }
            }
            sum += depth;   //g(n) + h(n)
            return sum;
        }
        private static int CalculateCombinations(int n)  //calculate combinations C(n, r)
        {
            const int pairOfQueens = 2;
            return n*(n-1) / pairOfQueens;             
        }
        public static State GenerateInitialState(int N)
        {
            State state = new State(N);
            Random rnd = new Random();
            for (int col = 0; col < N; col++)
            {
                state.board[col] = rnd.Next(0, N);
            }
            return state;
        }
        public int[][] StateToMatrix()
        {
            int[][] matrix = new int[board.Length][];
            for (int i = 0; i < board.Length; i++)
                matrix[i] = new int[board.Length];
            for(int i=0; i< board.Length;i++)
            {
                matrix[board[i]][i] = 1;
            }
            return matrix;
        }
    }
}
