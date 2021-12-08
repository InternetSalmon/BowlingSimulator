using PiTechnicalInterview;
using System;
using Xunit;

namespace PiTechnicalInterviewTests
{
    public class FrameTests
    {

        [Fact]
        public void Frame_IncompleteFrame()
        {
            var frame = new Frame(null, false);
            frame.AddRoll(5);
            Assert.False(frame.Strike);
            Assert.False(frame.Spare);
            Assert.False(frame.FrameCompleted);
            Assert.Equal(5, frame.Score);
        }

        [Fact]
        public void Frame_HasStrike()
        {
            var frame = new Frame(null, false);
            frame.AddRoll(10);
            Assert.True(frame.Strike);
            Assert.False(frame.Spare);
            Assert.True(frame.FrameCompleted);
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
            Assert.True(frame.FrameCompleted);
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
            Assert.True(frame.FrameCompleted);
            Assert.Equal(0, frame.Score);
        }

        [Fact]
        public void Frame_HasTwoStrikes()
        {
            var frame1 = new Frame(null, false);
            var frame2 = new Frame(frame1, false);
            frame1.AddRoll(10);
            frame2.AddRoll(10);
            Assert.Equal(20, frame1.Score);
            Assert.Equal(10, frame2.Score);
        }

        [Fact]
        public void Frame_HasThreeStrikes()
        {
            var frame1 = new Frame(null, false);
            var frame2 = new Frame(frame1, false);
            var frame3 = new Frame(frame2, false);
            frame1.AddRoll(10);
            frame2.AddRoll(10);
            frame3.AddRoll(10);
            Assert.Equal(30, frame1.Score);
            Assert.Equal(20, frame2.Score);
            Assert.Equal(10, frame3.Score);
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
