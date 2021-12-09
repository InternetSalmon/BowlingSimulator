using PiTechnicalInterview;
using System;
using Xunit;

namespace PiTechnicalInterviewTests
{
    public class GameTests
    {
        private Game game;

        public GameTests()
        {
            game = new Game();
        }

        [Fact]
        public void Game_GutterGame()
        {
            for (int i = 0; i < 20; i++)
                game.Roll(0);

            Assert.Equal(0, game.Score());
        }

        [Fact]
        public void Game_PerfectGame()
        {
            for (int i = 0; i < 12; i++)
            {
                game.Roll(10); //strike
            }
            Assert.Equal(300, game.Score());
        }

        /// <summary>
        /// Scoring a strike, total for frame depends on next frame.
        /// Score in subsequent frame is added onto the 10 you got for the strike.
        /// </summary>
        [Theory]
        [InlineData(new int[] { 10, 4, 4 }, 18, 26)] //strike + 4 + 4
        [InlineData(new int[] { 10, 10, 10 }, 30, 60)] //strike + strike + strike
        [InlineData(new int[] { 10, 2, 8 }, 20, 30)] //strike + 2 + spare
        public void Game_StrikesFirstFrame(int[] rolls,int expectedFrame1, int expectedScore)
        {
            foreach(var pinsKnocked in rolls)
                game.Roll(pinsKnocked);
            Assert.Equal(expectedScore, game.Score());
            Assert.Equal(expectedFrame1, game.Frames[0].Score);
        }

        [Fact]
        public void Game_SinglePinGame()
        {
            for (int i = 0; i < 20; i++)
                game.Roll(1);
            Assert.Equal(20, game.Score());
        }

        [Fact]
        public void Game_SpareInFrame()
        {
            game.Roll(8);
            game.Roll(2); //8 + 2 = spare
            game.Roll(7);

            Assert.Equal(17, game.Score());
        }
        [Fact]
        public void Game_RolledSpare()
        {
            game.Roll(8);
            game.Roll(2); //8 + 2 = spare
            game.Roll(7);

            Assert.Equal(17, game.Score());
        }

        [Fact]
        public void Game_RolledTwoStrikes()
        {
            game.Roll(10);
            game.Roll(10);
            game.Roll(7);
            Assert.Equal(37, game.Score());
        }

        [Fact]
        public void Game_ThreeFramesWithSpares()
        {
            game.Roll(8);
            game.Roll(2); // 8 + 2 = spare
            game.Roll(7); // knocks 7, 3 remaining
            game.Roll(3); // knock 3 = spare
            game.Roll(3); // 3 knocked, 7 remaining
            game.Roll(4); // 4 knocked, 3 remaining 
            Assert.Equal(37, game.Score());
        }

        [Theory]
        [InlineData(new int[] { 6, 3, 10, 9, 0, 7, 2, 10, 9, 0, 7, 3, 8, 1, 10, 9, 1, 10}, 141)]
        [InlineData(new int[] { 3, 4, 5, 4, 10, 3, 6, 10, 10, 3, 2, 9, 1, 6, 4, 0, 0 }, 113)]
        [InlineData(new int[] { 2, 0, 3, 2, 4, 1, 0, 3, 3, 3, 10, 2, 1, 0, 5, 3, 3, 0, 1 }, 49)]
        public void Game_CompleteGameIntegratonTest(int[] rolls, int expectedScore)
        {
            foreach (var pinsKnocked in rolls)
                game.Roll(pinsKnocked);
            Assert.Equal(expectedScore, game.Score());
        }

    }
}
