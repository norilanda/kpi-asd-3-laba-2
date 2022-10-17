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
        public static void GetInput(out int N, out int algo, ref int maxSizeSolutionToDisplay)
        {
            Console.Write("N = ");
            N = Convert.ToInt32(Console.ReadLine());
            Console.Write("Choose the algorithm: IDS - 1, A* - 2:  ");
            algo = Convert.ToInt32(Console.ReadLine());
            Console.Write("Do you want to display the board? yes - 1, no - 2: ");
            int answer = Convert.ToInt32(Console.ReadLine());
            if (answer == 1)
                maxSizeSolutionToDisplay = N;
            if (answer == 2)
                maxSizeSolutionToDisplay = 0;
        }
        public static void DisplayState(State state)
        {
            int[][] stateMatrix = state.StateToMatrix();          
            for (int i = 0; i < stateMatrix.Length; i++)
            {
                for (int j = 0; j < stateMatrix[i].Length; j++)
                {
                    char curr = '-';
                    if (stateMatrix[i][j] == 1)
                        curr = 'Q';
                    Console.Write(curr + " ");

                }
                Console.WriteLine();
            }
        }

        public static void DisplayResult(bool result, SolutionTree tree, TimeSpan ts, int maxSizeOfSolutionToDisplay = 20)
        {
            if (result)
            {
                State solution = tree.Solution;
                int iterations = tree.Iterations;
                int totalNodesCreated = tree.TotalNodesCreated;
                int avaregeNodesSaved = tree.avaregeNodesSaved();
                if (solution.board.Length <= maxSizeOfSolutionToDisplay)
                    InputOutput.DisplayState(solution);
                Console.WriteLine("Iterations = " + iterations + ";" );
                Console.WriteLine("Total Nodes Created = " + totalNodesCreated + ";");
                Console.WriteLine("Avarege Nodes Saved = " + avaregeNodesSaved + ";");
            }
            Console.WriteLine("is goal = " + result);
            Console.WriteLine("Elapsed Time is {0:00}:{1:00}:{2:00}.{3}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
        }
    }
}
