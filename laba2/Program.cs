using laba2;
using System.Diagnostics;

int N = 8;
int maxSizeOfSolutionToDisplay = 20;
int algorithmChosen = 2;    // 1 - IDS, 2 - A*
int howToCreateState = 2;   //  1 - enter, 2 - generate

InputOutput.GetInput(out N, out algorithmChosen, ref maxSizeOfSolutionToDisplay, out howToCreateState);

State state;
if (howToCreateState == 1)
    state = InputOutput.InputState(N);  //enter state
else
state = State.GenerateInitialState(N); //generate state
Console.WriteLine("Initial state:");
if (N <= maxSizeOfSolutionToDisplay)
    InputOutput.DisplayState(state);
Console.WriteLine("is goal = "+state.IsGoal());
Console.WriteLine();

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
