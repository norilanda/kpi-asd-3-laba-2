using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba2
{
    internal class Node
    {
        /*  class for building searching solution tree  */
        private int[] state;    //current state of the board; indicies represent rows, numbers represent columns of a board
        private Node? parent;    //parent Node for the current node;
        private int depth;  //how many states are from initial one

        public Node(int[] currState, Node? parent, int depth)
        {
            this.state = new int[currState.Length];
            Array.Copy(currState, this.state, currState.Length);
            this.parent = parent;
            this.depth = depth;
        }
    }
}
