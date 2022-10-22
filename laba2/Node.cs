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
        // memory : 
        public static int memoryUsageInBytes = 0;
        public static void InitializeMemoryUsage(int N)
        {
            memoryUsageInBytes += sizeof(int)*N + sizeof(int) + sizeof(int)*3;
        }

        private State state;    //current state of the board; indicies represent rows, numbers represent columns of a board
        private Node? parent;
        private int depth;  //how many states are from initial one        
        private int[] action;   //[0] - column number, [1] - old row, [2] - new row
        
        public Node(State currState, Node? parent, int depth, int[] action)
        {
            this.state = new State(currState);
            this.depth = depth;
            this.parent = parent;
            this.action = action;
        }

        public State getState => state; 
        public int Depth => depth;  
        public Node? Parent => parent;
        public int[] Action => action;
    }
}
