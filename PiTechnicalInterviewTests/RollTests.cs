using PiTechnicalInterview;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PiTechnicalInterviewTests
{
    public class RollTests
    {
        [Fact]
        public void Roll_ValidRoll()
        {
            var roll = new Roll(10);
            Assert.Equal(10, roll.PinsKnocked);
        }

        [Theory]
        [InlineData(11)]
        [InlineData(-1)]
        public void Roll_InvalidRoll(int pinsKnocked)
        {
            Assert.Throws<ArgumentException>(() => {
                new Roll(pinsKnocked);
            });
        }
    }
}
