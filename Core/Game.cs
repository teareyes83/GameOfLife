using System;
using System.Linq;
using System.Text;

namespace Core
{
    public class Game
    {
        public const char DeadCellChar = '.';
        public const char LivingCellChar = '*';

        char[,] grid;

        public Game(string initString)
        {
            Initialize(initString.ToLinedString());
        }

        public Game(string[] initLinedStrings)
        {
            Initialize(initLinedStrings);
        }

        void Initialize(string[] initLinedStrings)
        {
            if (initLinedStrings.Length < 1)
            {
                return;
            }

            if (initLinedStrings[0].Length < 1)
            {
                return;
            }

            grid = new char[initLinedStrings.Length, initLinedStrings[0].Length];
            for (var row = 0; row < initLinedStrings.Length; ++row)
            {
                var rowString = initLinedStrings[row];
                for (int column = 0; column < rowString.Length; ++column)
                {
                    grid[row, column] = rowString[column];
                }
            }
        }

        public void GenerateNext()
        {
            var rowLength = grid.GetLength(0);
            var columnLength = grid.GetLength(1);
            char[,] nextGenerationGrid = new char[rowLength, columnLength];

            for (int row = 0; row < rowLength; ++row)
            {
                for (int column = 0; column < columnLength; ++column)
                {
                    var isAlive = IsAlive(row, column);
                    var isDead = IsDead(row, column);
                    var livingNeighbours = CountLivingNeighbours(row, column);

                    if(isAlive && livingNeighbours < 2)
                    {
                        nextGenerationGrid[row, column] = DeadCellChar;
                    }
                    else if (isAlive && livingNeighbours == 2)
                    {
                        nextGenerationGrid[row, column] = LivingCellChar;
                    }
                    else if (isAlive && livingNeighbours == 3)
                    {
                        nextGenerationGrid[row, column] = LivingCellChar;
                    }
                    else if (isAlive && livingNeighbours > 3)
                    {
                        nextGenerationGrid[row, column] = DeadCellChar;
                    }
                    else if (isDead && livingNeighbours == 3)
                    {
                        nextGenerationGrid[row, column] = LivingCellChar;
                    }
                    else
                    {
                        nextGenerationGrid[row, column] = grid[row, column];
                    }
                }
            }

            grid = nextGenerationGrid;
        }

        public int CountLivingNeighbours(int row, int column)
        {
            int[,] cellsToCheck = new int[,]{
                {row - 1, column - 1},
                {row - 1, column},
                {row - 1, column + 1},
                {row, column + 1},
                {row + 1, column + 1},
                {row + 1, column},
                {row + 1, column - 1},
                {row, column - 1},
            };

            int livingNeighbours = 0;

            for (int i = 0; i < cellsToCheck.GetLength(0); i++)
            {
                int rowToCheck = cellsToCheck[i, 0];
                int columnTocheck = cellsToCheck[i, 1];

                if (IsInTheGrid(rowToCheck, columnTocheck) && IsAlive(rowToCheck, columnTocheck))
                {
                    livingNeighbours++;
                }

            }

            return livingNeighbours;
        }

        bool IsInTheGrid(int row, int col)
        {
            return row >= 0 && col >= 0 && row < grid.GetLength(0) && col < grid.GetLength(1);
        }

        public bool IsAlive(int row, int column)
        {
            return grid[row, column] == LivingCellChar;
        }

        public bool IsDead(int row, int column)
        {
            return grid[row, column] == DeadCellChar;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (var row = 0; row < grid.GetLength(0); ++row)
            {
                for (int column = 0; column < grid.GetLength(1); ++column)
                {
                    stringBuilder.Append(grid[row, column]);
                }

                if(row < grid.GetLength(0) - 1)
                {
                    stringBuilder.AppendLine();
                }
            }

            return stringBuilder.ToString();
        }
    }
}
