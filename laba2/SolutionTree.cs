using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Diagnostics;

namespace laba2
{
    internal class SolutionTree
    {/*  class for algorithms implementation  */

        const long bytesInOneGB = 1024 * 1024 * 1024;
        const long msIn30Minutes = 1800000;
        Stopwatch innerTimer;

        /* some variables for statistic */
        private long iterations; //how many iterations of the algotithm were made
        long nodesSaved;   //how many nodes the program holds at the same time
        private long totalNodesCreated;  //how many nodes have been created during the algorithm working
        private List<int[]> path;   //stores all actions

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
            this.path = new List<int[]>();
            this.nodesSaved = 1;
            
            Node.InitializeMemoryUsage(N);
            root = new Node(initialState, null, 0, new int[] {-1, -1, -1}); //create the first (root) node of the tree

            innerTimer = new Stopwatch();
            innerTimer.Start();
        }
        /*  getters/setters */
        public long Iterations => iterations;
        public long TotalNodesCreated => totalNodesCreated;
        public List<int[]> Path => path;
        public long NodesSaved => nodesSaved;   

        /*   algorithms   */

        private List<Node> Expand(ref Node node)
        {
            int currentDepth = node.Depth;            
            List<Node> successors = new List<Node>();
            if (currentDepth == N)    //if node cannot have successors
                return successors;
            int currentRow = node.getState.board[currentDepth];
            for (int row = 0; row < N; row++)
            {               
                //if(node.getState.IsSafe(row, node.Depth))   //if new position is safe then create a new state with placed queen here
                //{
                State newState = new State(node.getState);
                newState.PlaceQueen(row, currentDepth);   //node.Depth is a column index
                successors.Add(new Node(newState, node, currentDepth + 1, new int[] { currentDepth, currentRow, row }));
                //}
            }
            return successors;
        }        
        private Indicator DLS(Node node, int limit, ref long currNodesInMemory)  //Depth-Limited-Search
        {
            iterations++;
            bool cutoff_occurred = false;
            if (node.getState.IsGoal()) //check if node has the goal state
            {
                solution = new State(node.getState);
                getPath(node);
                return Indicator.goal;
            }
            if (node.Depth == limit)    //if depth == limit stop search
            {
                currNodesInMemory--;
                return Indicator.cutoff; 
            }
            List<Node> successors = Expand(ref node);

            /*statistic*/
            totalNodesCreated += successors.Count;  //statistic
            currNodesInMemory += successors.Count;
            if (currNodesInMemory > nodesSaved) nodesSaved = currNodesInMemory;
            if (currNodesInMemory * Node.memoryUsageInBytes > bytesInOneGB || innerTimer.ElapsedMilliseconds > msIn30Minutes) //memory&time restriction
                return Indicator.failure;
            /*statistic*/

            for (int i = 0; i < successors.Count; i++)  //for every node in successors
            {
                Indicator result = DLS(successors[i], limit, ref currNodesInMemory);
                if (result == Indicator.cutoff)
                    cutoff_occurred = true;
                else if(result != Indicator.failure)    //if is goal
                    return result;
            }
            currNodesInMemory--;
            if (cutoff_occurred)
                return Indicator.cutoff;
            else
                return Indicator.failure;
        }

        public bool IDS()   //Iterative-Deepening-Search
        {
            for (int i=0; i<=N; i++)
            {
                long currNodeSaved = 0;
                Indicator result = DLS(root, i, ref currNodeSaved);                              
                if (result == Indicator.goal)
                    return true;
                else if (result == Indicator.failure)
                    return false;
            }
            return false;
        }

        public bool AStar()
        {
            PriorityQueue<Node, int> openList = new PriorityQueue<Node, int>(); //stores nodes that should be expanded         
            openList.Enqueue(root, root.getState.F2()); //add initial node to opneList
            while(openList.Count > 0)   //while open list is not empty
            {
                iterations++;
                Node current = openList.Dequeue(); //get node with min value F2
                if(current.getState.IsGoal())
                {
                    solution = new State(current.getState);
                    getPath(current);
                    return true;
                }
                List<Node> successors = Expand(ref current);
                totalNodesCreated += successors.Count;
                if (openList.Count * Node.memoryUsageInBytes > bytesInOneGB || innerTimer.ElapsedMilliseconds > msIn30Minutes)//memory&time restriction
                    return false;
                    for (int i = 0; i < successors.Count; i++)
                {
                    openList.Enqueue(successors[i], successors[i].getState.F2());
                }
                if (openList.Count > nodesSaved)
                    nodesSaved = openList.Count;                
            }
            return false;
        }
        private void getPath(Node node) //returns path (actions) to final state
        {
            while(node.Parent != null)
            {
                path.Add(node.Action);
                node = node.Parent;
            }
        }       
    }
}
