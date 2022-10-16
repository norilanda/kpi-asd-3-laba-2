using laba2;

int N = 4;
State state = State.GenerateInitialState(N); //GenerateInitialState GenerateInitialStateDifferentRows
InputOutput.DisplayState(state);
Console.WriteLine("is goal = "+state.IsGoal());

//State state1 = new State(new int[4] { 3, 1, 2, 0 });
//InputOutput.DisplayState(state1);
//Console.WriteLine(state1.IsGoal());

//State correctState = new State( new int[4] { 1, 3, 0, 2 });
//InputOutput.DisplayState(correctState.StateToMatrix());
//Console.WriteLine(correctState.IsGoal());

SolutionTree tree = new SolutionTree(N, state);
//bool result = tree.IDS();
bool result = tree.AStar();
if (result)
{
    State solution = tree.Solution;
    int iterations = tree.Iterations;
    int totalNodesCreated = tree.TotalNodesCreated;
    InputOutput.DisplayState(solution);
    Console.WriteLine("Iterations = " + iterations + "; Total Nodes Created = " + totalNodesCreated);
}
Console.WriteLine("is goal = " + result);
