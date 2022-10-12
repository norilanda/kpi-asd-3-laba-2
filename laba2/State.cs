using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba2
{
    internal class State
    {
        public static int[] GenerateInitialState(int N)
        {            
            int[] state = new int[N];
            List <int> indicies = new List <int>(Enumerable.Range(0, N).ToList());
            Random rnd = new Random();
            for (int col = 0; col < N; col++)
            {
                int row = rnd.Next(0, indicies.Count);
                state[col] = indicies[row];
                indicies.RemoveAt(row);
            }
            return state;
        }
        public static bool IsGoal(int[] state)  //check if all the queens are in correct places
        {
            for(int j = 1; j<state.Length;j++)
            {
                for(int k=j-1;k>=0;k--)
                {
                    if(state[j]==state[k])  //if in the same row
                        return false;
                    int diff = j - k;   //how far the column k is from column j
                    if (state[j] - diff == state[k] || state[j] + diff == state[k])   //check diagonals
                        return false;
                }
            }
            return true;
        }
        public static int[][] StateToMatrix(int[] state)
        {
            int[][] matrix = new int[state.Length][];
            for (int i = 0; i < state.Length; i++)
                matrix[i] = new int[state.Length];
            for(int i=0; i<state.Length;i++)
            {
                matrix[state[i]][i] = 1;
            }
            return matrix;
        }
    }
}
