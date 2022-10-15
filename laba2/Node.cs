﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba2
{
    internal class Node
    {
        /*  class for building searching solution tree  */
        private State state;    //current state of the board; indicies represent rows, numbers represent columns of a board
        private Node? parent;    //parent Node for the current node;
        private int depth;  //how many states are from initial one

        public Node(State currState, Node? parent, int depth)
        {
            this.state = new State(currState);
            this.parent = parent;
            this.depth = depth;
        }

        public State getState
        {
            get => state;            
        }
        public int Depth
        {
            get => depth;
        }
    }
}
