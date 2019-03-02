using System;
using FluentAssert;

namespace Core.Test
{
    public class GameAssertContext
    {
        string input;
        int testCellRow = -1, testCellColumn = -1;
        public GameAssertContext(string input)
        {
            var linedString = input.ToLinedString();

            var testAliveCellChar = 'T';
            var testDeadCellChar = 't';
            for (var row = 0; row < linedString.Length; ++row)
            {
                var rowString = linedString[row];
                for (int column = 0; column < rowString.Length; ++column)
                {
                    if (testAliveCellChar == rowString[column])
                    {
                        testCellRow = row;
                        testCellColumn = column;
                    }
                    else if (testDeadCellChar == rowString[column])
                    {
                        testCellRow = row;
                        testCellColumn = column;
                    }
                }
            }

            if (testCellRow == -1 || testCellColumn == -1)
            {
                throw new Exception("test cell char T should be exist.");
            }

            input = input.Replace('T', Game.LivingCellChar);
            input = input.Replace('t', Game.DeadCellChar);

            this.input = input;
        }

        public int CountLivingNeighbours()
        {
            var game = new Game(input);
            return game.CountLivingNeighbours(testCellRow, testCellColumn);
        }

        public void ShouldDie()
        {
            var game = new Game(input);
            game.GenerateNext();
            game.IsDead(testCellRow, testCellColumn).ShouldBeTrue();
        }

        public void ShouldAlive()
        {
            var game = new Game(input);
            game.GenerateNext();

        }
    }
}
