using laba2;
using System.Diagnostics;

int N = 8;
State state = State.GenerateInitialState(N); //GenerateInitialState GenerateInitialStateDifferentRows
//InputOutput.DisplayState(state);
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
//bool result = tree.IDS();
bool result = tree.AStar();
stopwatch.Stop();
TimeSpan ts = stopwatch.Elapsed;

InputOutput.DisplayResult(result, tree, ts);
