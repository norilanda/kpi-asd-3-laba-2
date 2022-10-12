using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba2
{
    internal class InputOutput
    {
        public static void DisplayState(int[][]state)
        {
            for(int i = 0; i < state.Length; i++)
            {
                for (int j = 0; j < state[i].Length; j++)
                {
                    char curr = '-';
                    if(state[i][j] == 1)
                        curr = 'Q';
                    Console.Write(curr + " "); 

                }
                Console.WriteLine();
            }
        }
    }
}
