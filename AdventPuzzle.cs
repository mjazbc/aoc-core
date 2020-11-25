using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Util
{
    public abstract class AdventPuzzle
    {
        private readonly string _inputPath = "../../../input.txt";
        protected AocInput Input { get; private set; }
        protected string inputText;
        public AdventPuzzle()
        {
            this.Input = new AocInput(_inputPath);
        }

        public AdventPuzzle(string inputPath)
        {
            _inputPath = inputPath;
        }

        public T[] ReadInputArray<T>()
        {
            return File.ReadAllLines(_inputPath).Select(l => (T)Convert.ChangeType(l, typeof(T))).ToArray();
        }
        public T ReadInputText<T>()
        {
            return (T)Convert.ChangeType(File.ReadAllText(_inputPath), typeof(T));
        }

        public abstract string SolveFirstPuzzle();

        public abstract string SolveSecondPuzzle();

        public void Solve(Puzzle puzzle = Puzzle.Both)
        {

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
