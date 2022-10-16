using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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

        public static void DisplayResult(bool result, SolutionTree tree, TimeSpan ts)
        {
            if (result)
            {
                State solution = tree.Solution;
                int iterations = tree.Iterations;
                int totalNodesCreated = tree.TotalNodesCreated;
                int avaregeNodesSaved = tree.avaregeNodesSaved();
               // InputOutput.DisplayState(solution);
                Console.WriteLine("Iterations = " + iterations + ";" );
                Console.WriteLine("Total Nodes Created = " + totalNodesCreated + ";");
                Console.WriteLine("Avarege Nodes Saved = " + avaregeNodesSaved + ";");
            }
            Console.WriteLine("is goal = " + result);
            Console.WriteLine("Elapsed Time is {0:00}:{1:00}:{2:00}.{3}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
        }
    }
}
