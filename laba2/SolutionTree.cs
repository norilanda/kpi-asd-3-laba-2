using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba2
{
    internal class SolutionTree
    {
        /*  class for algorithms implementation  */

        /* some variables for statistic */
        int iterations;
        int totalNodesCreated;

        /* attributes for solving the problem */
        int N;
        Node root;

        /*   Constructor   */
        public SolutionTree(int N, int[] initialState)
        {
            this.N = N;
            this.iterations = 0;
            this.totalNodesCreated = 0;
            root = new Node(initialState, null, 0); //create the first (root) node of the tree
        }

        /*   algorithms   */

        private List<Node> Expand(Node node)
        {
            List<Node> successors = new List<Node>();

            return successors;
        }
    }
}
