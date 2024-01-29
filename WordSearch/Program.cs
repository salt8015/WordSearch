using System;
using System.Data;
using System.Text.RegularExpressions;

namespace WordSearch
{
    class Program
    {
        static char[,] Grid = new char[,] {
            {'C', 'P', 'K', 'X', 'O', 'I', 'G', 'H', 'S', 'F', 'C', 'H'},
            {'Y', 'G', 'W', 'R', 'I', 'A', 'H', 'C', 'Q', 'R', 'X', 'K'},
            {'M', 'A', 'X', 'I', 'M', 'I', 'Z', 'A', 'T', 'I', 'O', 'N'},
            {'E', 'T', 'W', 'Z', 'N', 'L', 'W', 'G', 'E', 'D', 'Y', 'W'},
            {'M', 'C', 'L', 'E', 'L', 'D', 'N', 'V', 'L', 'G', 'P', 'T'},
            {'O', 'J', 'A', 'A', 'V', 'I', 'O', 'T', 'E', 'E', 'P', 'X'},
            {'C', 'D', 'B', 'P', 'H', 'I', 'A', 'W', 'V', 'X', 'U', 'I'},
            {'L', 'G', 'O', 'S', 'S', 'B', 'R', 'Q', 'I', 'A', 'P', 'K'},
            {'E', 'O', 'I', 'G', 'L', 'P', 'S', 'D', 'S', 'F', 'W', 'P'},
            {'W', 'F', 'K', 'E', 'G', 'O', 'L', 'F', 'I', 'F', 'R', 'S'},
            {'O', 'T', 'R', 'U', 'O', 'C', 'D', 'O', 'O', 'F', 'T', 'P'},
            {'C', 'A', 'R', 'P', 'E', 'T', 'R', 'W', 'N', 'G', 'V', 'Z'}
        };

        static string[] Words = new string[] 
        {
            "CARPET",
            "CHAIR",
            "DOG",
            "BALL",
            "DRIVEWAY",
            "FISHING",
            "FOODCOURT",
            "FRIDGE",
            "GOLF",
            "MAXIMIZATION",
            "PUPPY",
            "SPACE",
            "TABLE",
            "TELEVISION",
            "WELCOME",
            "WINDOW"
        };

        readonly static int[,] directions = {{1, 0}, {0, 1}, {1, 1}, {1, -1}, {-1, 0},
            {0, -1}, {-1, -1}, {-1, 1}};

        readonly static int rows = 12;
        readonly static int cols = 12;
        readonly static int gridSize = rows * cols;
        static void Main(string[] args)
        {
            Console.WriteLine("Word Search");

            for (int y = 0; y < 12; y++)
            {
                for (int x = 0; x < 12; x++)
                {
                    Console.Write(Grid[y, x]);
                    Console.Write(' ');
                }
                Console.WriteLine("");

            }

            Console.WriteLine("");
            Console.WriteLine("Found Words");
            Console.WriteLine("------------------------------");

            FindWords();

            Console.WriteLine("------------------------------");
            Console.WriteLine("");
            Console.WriteLine("Press any key to end");
            Console.ReadKey();
        }

        private static void FindWords()
        {
            //Find each of the words in the grid, outputting the start and end location of
            //each word, e.g.
            //PUPPY found at(10, 7) to(10, 3)
            foreach (var word in Words)
            {
                for (int direction = 0; direction < directions.GetLength(0); direction++)
                {
                    for (int position = 0; position < gridSize; position++)
                    {
                        var location = FoundLocation(word, direction, position);
                        if (!string.IsNullOrEmpty(location))
                            Console.WriteLine(location);
                    }
                }
            }
        }

        private static string FoundLocation(string word, int direction, int position)
        {
            int startX = position % cols;
            int startY = position / cols;
            int len = word.Length;

            //  check bounds of grid size
            if ((directions[direction, 0] == 1 && (len + startX) > cols)
                    || (directions[direction, 0] == -1 && (len - 1) > startX)
                    || (directions[direction, 1] == 1 && (len + startY) > rows)
                    || (directions[direction, 1] == -1 && (len - 1) > startY))
                return null;

            int endX, endY, i = 0;

            // check cells
            for (i = 0, endY = startY, endX = startX; i < len; i++)
            {
                if (Grid[endY, endX] != 0 && Grid[endY, endX] != word[i])
                {
                    return null;
                }

                if( i != len - 1)
                {
                    endX += directions[direction, 0];
                    endY += directions[direction, 1];
                }
            }
            return $"{word,-10} ({startX},{startY})({endX},{endY})";
        }
    }
}
