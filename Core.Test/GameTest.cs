using NUnit.Framework;
using FluentAssert;
using System;
namespace Core.Test
{
    public class GameTest
    {
        [Test]
        public void FoundsNoLivingNeighboursInAnEmptyGrid()
        {
            TestCellOn(@"...
                        .T.
                        ...").CountLivingNeighbours().ShouldBeEqualTo(0);
        }

        [Test]
        public void FoundsOneLivingNeighbour()
        {
            TestCellOn(@"...
                         .T.
                         *..").CountLivingNeighbours().ShouldBeEqualTo(1);
        }


        [Test]
        public void FoundsTwoLivingNeighbour()
        {
            TestCellOn(@"...
                         .T.
                         **.").CountLivingNeighbours().ShouldBeEqualTo(2);
        }


        [Test]
        public void FoundsThreeLivingNeighbour()
        {
            TestCellOn(@"...
                         .T.
                         ***").CountLivingNeighbours().ShouldBeEqualTo(3);
        }

        [Test]
        public void FoundsFourLivingNeighbour()
        {
            TestCellOn(@"...
                         *T.
                         ***").CountLivingNeighbours().ShouldBeEqualTo(4);
        }

        [Test]
        public void FoundsFiveLivingNeighbour()
        {
            TestCellOn(@"*..
                         *T.
                         ***").CountLivingNeighbours().ShouldBeEqualTo(5);
        }

        [Test]
        public void FoundsSixLivingNeighbour()
        {
            TestCellOn(@"*..
                         *T*
                         ***").CountLivingNeighbours().ShouldBeEqualTo(6);
        }


        [Test]
        public void FoundsSevenLivingNeighbour()
        {
            TestCellOn(@"*.*
                         *T*
                         ***").CountLivingNeighbours().ShouldBeEqualTo(7);
        }

        [Test]
        public void FoundsEightLivingNeighbour()
        {
            TestCellOn(@"***
                         *T*
                         ***").CountLivingNeighbours().ShouldBeEqualTo(8);
        }

        [Test]
        public void FoundsThreeLivingNeighboursForTheTopLeftCornerCell()
        {
            TestCellOn(@"T*.
                         **.
                         ...").CountLivingNeighbours().ShouldBeEqualTo(3);
        }

        [Test]
        public void FoundsThreeLivingNeighboursForTheBottomRightCornerCell()
        {
            TestCellOn(@"...
                         **.
                         T*.").CountLivingNeighbours().ShouldBeEqualTo(3);
        }

        [Test]
        public void ACellWithFewerThanTwoNeighboursDies()
        {
            TestCellOn(@"T..
                         ...
                         ...").ShouldDie();

            TestCellOn(@"T*.
                         ...
                         ...").ShouldDie();
        }

        [Test]
        public void ACellWithTwoNeighboursLives()
        {
            TestCellOn(@"T*.
                         *..
                         ...").ShouldAlive();
            
            TestCellOn(@"T*.
                         .*.
                         ...").ShouldAlive();
        }

        [Test]
        public void ACellWithThreeNeighboursLives()
        {
            TestCellOn(@"T*.
                         **.
                         ...").ShouldAlive();
        }


        [Test]
        public void ACellWithMoreThanThreeNeighboursDies()
        {
            TestCellOn(@"***
                         *T.
                         ...").ShouldDie();

            TestCellOn(@"***
                         *T*
                         ...").ShouldDie();

            TestCellOn(@"***
                         *T*
                         ***").ShouldDie();
        }

        public GameAssertContext TestCellOn(string input)
        {
            var gameAssertContext = new GameAssertContext(input);
            return gameAssertContext;
        }

        [TestCase(@"........
                    ....*...
                    ...**...
                    ........",
                  @"........
                    ...**...
                    ...**...
                    ........")]
        public void TestInputToOutput(string input, string ouput)
        {
            var game = new Game(input);
            game.GenerateNext();
            game.ToString().ToLinedString().ShouldBeEqualTo(ouput.ToLinedString());
        }
    }
}
