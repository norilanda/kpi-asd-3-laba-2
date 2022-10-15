using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba2
{
    internal class InputOutput
    {
        public static void DisplayState(State state)
        {
            int[][] stateMatrix = state.StateToMatrix();
            for (int i = 0; i < stateMatrix.Length; i++)
            {
                for (int j = 0; j < stateMatrix[i].Length; j++)
                {
                    char curr = '-';
                    if(stateMatrix[i][j] == 1)
                        curr = 'Q';
                    Console.Write(curr + " "); 

                }
                Console.WriteLine();
            }
        }
    }
}
