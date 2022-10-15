using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba2
{
    internal class SolutionTree
    {/*  class for algorithms implementation  */


        /* some variables for statistic */
        int iterations;
        int totalNodesCreated;

        /* attributes for solving the problem */
        private int N;
        private Node root;
        private State solution; //solution state
        
        public State Solution { get => solution; }
        enum Indicator
        {
            goal,   //solution is found
            cutoff, //the solution may exist but not at this depth
            failure //the solution is not exist
        }
        /*   Constructor   */
        public SolutionTree(int N, State initialState)
        {
            this.N = N;
            this.iterations = 0;
            this.totalNodesCreated = 0;
            root = new Node(initialState, null, 0); //create the first (root) node of the tree
        }

        /*   algorithms   */

        private List<Node> Expand(ref Node node)
        {
            List<Node> successors = new List<Node>();
            //if(node.Depth < 0)
            //{
            //    for(int i = 0; i < N; i++)
            //    {
            //        State newState = new State(node.getState);
            //        newState.PlaceQueen(i, 0);   //node.Depth is a column index
            //    }
            //}
            for(int row = 0; row < N; row++)
            {
                if(node.getState.IsSafe(row, node.Depth))   //if new position is safe then create a new state with placed queen here
                {
                    State newState = new State(node.getState);
                    newState.PlaceQueen(row, node.Depth);   //node.Depth is a column index
                    successors.Add(new Node(newState, node, node.Depth + 1));
                }                    
            }
            return successors;
        }

        private Indicator DLS(Node node, int limit)  //Depth-Limited-Search
        {
            bool cutoff_occurred = false;
            //InputOutput.DisplayState(node.getState);
            //Console.WriteLine();
            if (node.getState.IsGoal())
            {
                solution = new State(node.getState);
                return Indicator.goal;
            }
            if (node.Depth == limit)
                return Indicator.cutoff;
            List<Node> successors = Expand(ref node);          
            for (int i = 0; i < successors.Count; i++)
            {
                Indicator result = DLS(successors[i], limit);
                if (result == Indicator.cutoff)
                    cutoff_occurred = true;
                else if(result != Indicator.failure)
                    return result;
            }
            if (cutoff_occurred)
                return Indicator.cutoff;
            else
                return Indicator.failure;
        }

        public bool IDS()   //Iterative-Deepening-Search
        {
            for (int i=0; i<=N; i++)
            {
                Indicator result = DLS(root, i);
                if (result == Indicator.goal)
                    return true;
                else if (result == Indicator.failure)
                    return false;
            }
            return false;
        }
    }
}
