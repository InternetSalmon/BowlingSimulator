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
            for (int i = 0; i < 10; i++)
            {
                //perfect frame
                game.Roll(10); //strike
                game.Roll(10); //strike !
                game.Roll(10); //strike !!
            }
            Assert.Equal(300, game.Score());
        }

        /// <summary>
        /// Scoring a strike, total for frame depends on next frame.
        /// Score in subsequent frame is added onto the 10 you got for the strike.
        /// </summary>
        [Theory]
        [InlineData(new int[] { 10, 4, 4 }, 18)] //strike + 4 + 4
        [InlineData(new int[] { 10, 10, 10 }, 30)] //strike + strike + strike
        [InlineData(new int[] { 10, 2, 8 }, 20)] //strike + 2 + spare
        public void Game_StrikesFirstFrame(int[] rolls, int expected)
        {
            foreach(var pinsKnocked in rolls)
                game.Roll(pinsKnocked);
            Assert.Equal(expected, game.Score());
            Assert.Equal(expected, game.Frames[0].Score);
            Assert.Equal(3, game.Frames[0].Rolls);
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
            Assert.Equal(27, game.Score());
        }

        [Fact]
        public void Game_ThreeFramesWithSpares()
        {
            game.Roll(8);
            game.Roll(2); // 8 + 2 = spare
            game.Roll(7); // knocks 7, 3 remaining
            // end frame one with 17
            // start frame 2 with 3 remaining
            game.Roll(3); // knock 3 = spare
            game.Roll(3); // 3 knocked, 7 remaining
            // end frame 2 with 30
            // start frame 3
            game.Roll(4); // 4 knocked, 3 remaining 
            Assert.Equal(17, game.Score());
        }

    }
}
