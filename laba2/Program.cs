using laba2;

int N = 4;
//int[] state = State.GenerateInitialState(N);
//int[][] mtr = State.StateToMatrix(state);
//InputOutput.DisplayState(mtr);
//Console.WriteLine(State.IsGoal(state));

//int[] correctState = new int[4] { 1, 3, 0, 2};
//InputOutput.DisplayState(State.StateToMatrix(correctState));
//Console.WriteLine(State.IsGoal(correctState));

SolutionTree tree = new SolutionTree(N);