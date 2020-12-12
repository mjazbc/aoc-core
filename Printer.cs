using System;

namespace aoc_core
{
    public static class Printer
    {
        public static void Print(char[,] matrix)
        {
            for(int y = 0; y < matrix.GetLength(0); y++){
                for(int x = 0; x < matrix.GetLength(0); x++)
                    Console.Write(matrix[y,x]);

                Console.WriteLine();    
            }

            Console.WriteLine();
        }
    }
}