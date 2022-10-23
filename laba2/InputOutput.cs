using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace laba2
{
    internal class InputOutput
    {
        public static void GetInput(out int N, out int algo, ref int maxSizeSolutionToDisplay, out int howToCreateState)
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
            Console.Write("Enter state or generate? enter - 1, generate - 2: ");
            howToCreateState = Convert.ToInt32(Console.ReadLine());
        }
        public static State InputState(int N)
        {
            int[] board = new int[N];
            Console.Write("Enter initial state: ");
            string answer = Console.ReadLine();
            answer = Regex.Replace(answer, "[{}.,;]", string.Empty);
            string[] indices = answer.Split(' ');
            try
            {
                for (int i = 0; i < N; i++)
                {
                    board[i] = Int32.Parse(indices[i]) - 1;
                }
                return new State(board);
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
                for (int i = 0; i < N; i++)
                    board[i] = 0;
                return new State(board);
            }
        }
        public static void DisplayState(State state)
        {
            Console.Write("[");
            for (int i=0; i < state.board.Length; i++)
                Console.Write((int)(state.board[i]+1) + " ");
            Console.WriteLine("]");
            int[][] stateMatrix = state.StateToMatrix();

            const int maxSize = 50;
            if (stateMatrix.Length < maxSize)
            {
                string horizontalLine = "";
                for (int i = 0; i < stateMatrix.Length; i++)
                    horizontalLine += "|---";
                Console.WriteLine(horizontalLine + "|");
                for (int i = 0; i < stateMatrix.Length; i++)
                {
                    for (int j = 0; j < stateMatrix[i].Length; j++)
                    {
                        char curr = ' ';
                        if (stateMatrix[i][j] == 1)
                            curr = 'Q';
                        Console.Write("| " + curr + " ");

                    }
                    Console.Write("|\n");
                    Console.WriteLine(horizontalLine + "|");
                }
            }
            else
            {
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
            
        }

        public static void DisplayResult(bool result, SolutionTree tree, TimeSpan ts, int maxSizeOfSolutionToDisplay = 20)
        {
            
            State solution = tree.Solution;
            long iterations = tree.Iterations;
            long totalNodesCreated = tree.TotalNodesCreated;
            long avaregeNodesSaved = tree.NodesSaved;
            List<int[]> path = tree.Path;
            Console.WriteLine("-------------------------------------------");
            if (result)
            {
                if (solution.board.Length <= maxSizeOfSolutionToDisplay)
                {
                    Console.WriteLine("Movements: ");
                    int k = 1;
                    for(int i = path.Count-1; i >= 0 ; i--)
                    {
                        Console.WriteLine(k+") column " + (int)(path[i][0] + 1) + ": from row "+ (int)(path[i][1]+1) + " to row " + (int)(path[i][2]+1) +";");
                        k++;
                    }
                    Console.WriteLine("\nFinal state: ");
                    InputOutput.DisplayState(solution);
                    Console.WriteLine();
                }
            }
            else
                Console.WriteLine("FAILED");
            Console.WriteLine("Iterations = " + iterations + ";" );
            Console.WriteLine("Total Nodes Created = " + totalNodesCreated + ";");
            Console.WriteLine("Maximum Nodes Saved = " + avaregeNodesSaved + ";");
            
            Console.WriteLine("is goal = " + result);
            Console.WriteLine("Elapsed Time is {0:00}:{1:00}:{2:00}.{3}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
        }
    }
}
