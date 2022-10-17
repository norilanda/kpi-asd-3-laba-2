using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace laba2
{
    internal class SolutionTree
    {/*  class for algorithms implementation  */


        /* some variables for statistic */
        private int iterations; //how many iterations of the algotithm were made
        private List<int> nodesSaved;   //how many nodes the program holds at the same time
        private int totalNodesCreated;  //how many nodes have been created during the algorithm working

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
            this.totalNodesCreated = 1;
            nodesSaved = new List<int>();
            root = new Node(initialState, 0); //create the first (root) node of the tree
        }
        /*  getters/setters */
        public int Iterations => iterations;
        public int TotalNodesCreated => totalNodesCreated;

        /*   algorithms   */

        private List<Node> Expand(ref Node node)
        {
            List<Node> successors = new List<Node>();
            for(int row = 0; row < N; row++)
            {
                if(node.getState.IsSafe(row, node.Depth))   //if new position is safe then create a new state with placed queen here
                {
                    State newState = new State(node.getState);
                    newState.PlaceQueen(row, node.Depth);   //node.Depth is a column index
                    successors.Add(new Node(newState, node.Depth + 1));
                }                    
            }
            return successors;
        }

        private Indicator DLS(Node node, int limit)  //Depth-Limited-Search
        {
            bool cutoff_occurred = false;
            if (node.getState.IsGoal())
            {
                solution = new State(node.getState);
                return Indicator.goal;
            }
            if (node.Depth == limit)
                return Indicator.cutoff;
            List<Node> successors = Expand(ref node);
            totalNodesCreated += successors.Count;
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
                iterations++;
                if (result == Indicator.goal)
                    return true;
                else if (result == Indicator.failure)
                    return false;
            }
            return false;
        }

        public bool AStar()
        {
            PriorityQueue<Node, int> openList = new PriorityQueue<Node, int>();         
            openList.Enqueue(root, root.getState.F2());
            while(openList.Count > 0)
            {
                iterations++;
                Node current = openList.Dequeue(); 
                if(current.getState.IsGoal())
                {
                    solution = new State(current.getState);
                    return true;
                }
                List<Node> successors = Expand(ref current);
                totalNodesCreated += successors.Count;
                for (int i = 0; i < successors.Count; i++)
                {
                    openList.Enqueue(successors[i], successors[i].getState.F2());
                }
                nodesSaved.Add(openList.Count);
            }
            return false;
        }
        public int avaregeNodesSaved()
        {
            int sum = 0;
            for(int i=0;i<nodesSaved.Count;i++)
                sum += nodesSaved[i];
            return sum/iterations;
        }
    }
}
