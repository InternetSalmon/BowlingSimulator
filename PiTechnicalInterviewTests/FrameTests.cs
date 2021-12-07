using PiTechnicalInterview;
using System;
using Xunit;

namespace PiTechnicalInterviewTests
{
    public class FrameTests
    {

        [Fact]
        public void Frame_HasStrike()
        {
            var frame = new Frame(null, false);
            frame.AddRoll(10);
            Assert.True(frame.Strike);
            Assert.False(frame.Spare);
            Assert.Equal(10, frame.Score);
        }

        [Fact]
        public void Frame_HasSpare()
        {
            var frame = new Frame(null, false);
            frame.AddRoll(4);
            frame.AddRoll(6);
            Assert.False(frame.Strike);
            Assert.True(frame.Spare);
            Assert.Equal(10, frame.Score);
        }

        [Fact]
        public void Frame_GutterBalls()
        {
            var frame = new Frame(null, false);
            frame.AddRoll(0);
            frame.AddRoll(0);
            Assert.False(frame.Strike);
            Assert.False(frame.Spare);
            Assert.Equal(0, frame.Score);
        }

        [Fact]
        public void Frame_InvalidNumberOfRolls()
        {
            var frame = new Frame(null, false);
            frame.AddRoll(0);
            frame.AddRoll(0);
            Assert.Throws<InvalidFrameException>(() =>
            {
                frame.AddRoll(0);
            });
        }

        [Fact]
        public void Frame_InvalidNumberOfRollsFinalFrame()
        {
            var frame = new Frame(null, true);
            frame.AddRoll(0);
            frame.AddRoll(0);
            frame.AddRoll(0);
            Assert.Throws<InvalidFrameException>(() =>
            {
                frame.AddRoll(0);
            });
        }

        [Fact]
        public void Frame_InvalidNumberOfPinsKnocked()
        {
            var frame = new Frame(null, false);
            frame.AddRoll(4);
            Assert.Throws<InvalidFrameException>(() =>
            {
                frame.AddRoll(8);
            });
        }

        [Fact]
        public void Frame_InvalidNumberOfStrikes()
        {
            var frame = new Frame(null, false);
            frame.AddRoll(10);
            Assert.Throws<InvalidFrameException>(() =>
            {
                frame.AddRoll(10);
            });
        }

        [Fact]
        public void Frame_InvalidNumberOfStrikesFinalFrame()
        {
            var frame = new Frame(null, true);
            frame.AddRoll(10);
            frame.AddRoll(10);
            Assert.Throws<InvalidFrameException>(() =>
            {
                frame.AddRoll(10);
            });
        }

    }
}
