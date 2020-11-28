using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace aoc_core
{
    public abstract class AdventPuzzle
    {
        public AocInput Input { get; private set; }
        public AdventPuzzle()
        {
            Input = new AocInput();
        }

        public abstract string SolveFirstPuzzle();

        public abstract string SolveSecondPuzzle();

        public void Solve(Puzzle puzzle = Puzzle.Both)
        {
            if(!Input.IsSet)
                throw new InvalidDataException("Missing puzzle input. Input should be loaded with Input.Load before executing solve.");

            Stopwatch watch = new Stopwatch();

            string toClipBoard = null;

            if(puzzle == Puzzle.First || puzzle ==  Puzzle.Both)
                toClipBoard = SolveSingle("# FIRST PUZZLE", SolveFirstPuzzle);
            if(puzzle == Puzzle.Second || puzzle == Puzzle.Both)
                toClipBoard = SolveSingle("# SECOND PUZZLE", SolveSecondPuzzle);
                 
            if (toClipBoard != null)
            {
                Console.WriteLine("Copy the result? Y/N");
                var key = Console.ReadLine();

                if (key.Equals("Y", StringComparison.CurrentCultureIgnoreCase))
                    TextCopy.ClipboardService.SetText(toClipBoard);
            }
            else
            {
                Console.ReadLine();
            }

        }

        private string SolveSingle(string name, Func<string> SolveFunction)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            try
            {
                Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine(name);
                Console.ResetColor();

                string solution = SolveFunction();
                TimeSpan secondsElapsed = watch.Elapsed;

                PrettyPrintResults(secondsElapsed, solution);
                return solution;

            }
            catch (Exception e)
            {
                PrettyPrintError(e.Message);
                return null;
            }
            finally
            {
                watch.Stop();
                Console.WriteLine();
                Console.ResetColor();
            }
        }

        private void PrettyPrintResults(TimeSpan duration, string result)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($"Duration: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(duration.TotalSeconds.ToString());

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($"Solution: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(result);
        }

        private void PrettyPrintError(string error)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("Error: ");
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(error);
        }
    }

    public enum Puzzle
    {
        First,
        Second,
        Both
    }


}
