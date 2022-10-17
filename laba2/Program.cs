using laba2;
using System.Diagnostics;

int N = 15;
int maxSizeOfSolutionToDisplay = 50;
int algorithmChosen = 2;    // 1 - IDS, 2 - A*

InputOutput.GetInput(out N, out algorithmChosen, ref maxSizeOfSolutionToDisplay);

State state = State.GenerateInitialState(N); //GenerateInitialState GenerateInitialStateDifferentRows
if (N <= maxSizeOfSolutionToDisplay)
    InputOutput.DisplayState(state);
Console.WriteLine("is goal = "+state.IsGoal());
Console.WriteLine();

//State state1 = new State(new int[4] { 3, 1, 2, 0 });
//InputOutput.DisplayState(state1);
//Console.WriteLine(state1.IsGoal());

//State correctState = new State( new int[4] { 1, 3, 0, 2 });
//InputOutput.DisplayState(correctState.StateToMatrix());
//Console.WriteLine(correctState.IsGoal());

Stopwatch stopwatch = new Stopwatch();
stopwatch.Start();
SolutionTree tree = new SolutionTree(N, state);
bool isSolutionFound = false;
if(algorithmChosen == 1)
    isSolutionFound = tree.IDS();
if (algorithmChosen == 2)
    isSolutionFound = tree.AStar();
stopwatch.Stop();
TimeSpan ts = stopwatch.Elapsed;

InputOutput.DisplayResult(isSolutionFound, tree, ts, maxSizeOfSolutionToDisplay);
