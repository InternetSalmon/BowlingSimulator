using System;
using System.Collections.Generic;
using System.Text;

namespace PiTechnicalInterview
{

    public class InvalidFrameException : Exception
    {
        public InvalidFrameException(string message) : base(message) 
        {
        }
    }

    /// <summary>
    /// Each game of bowling consists of 10 frames, each frame has a maximum of two rolls
    /// In the tenth frame a player who rolls a spare or strike is allowed to roll the extra balls to complete the frame. However no more than three balls can be rolled in tenth frame.
    /// </summary>
    public class Frame
    {
        private const int MaxPinsInFrame = 10;
        private int Pins { get; set; }

        public int Score { get; private set; }
        public int Rolls { get; private set; }
        public bool Strike { get; private set; }
        public bool Spare { get; private set; }
        public bool FinalFrame { get; private set; }
        public Frame PreviousFrame { get; private set; }

        public Frame(Frame previousFrame, bool finalFrame)
        {
            PreviousFrame = previousFrame;
            FinalFrame = finalFrame;
            Pins = MaxPinsInFrame;
        }

        public void AddRoll(int pinsKnocked)
        {
            Rolls++;
            if ((!FinalFrame && Rolls > 2) || (FinalFrame && Rolls > 3))
                throw new InvalidFrameException("Frame exhausted, two rolls reached");

            if ((!FinalFrame && Rolls > 1 && Strike) || (FinalFrame && Rolls > 2 && Strike))
                throw new InvalidFrameException("Frame exhausted, strike occured");

            Pins -= pinsKnocked;
            if(Pins < 0)
                throw new InvalidFrameException("Frame error, pins knocked that dont exist");

            if (pinsKnocked == MaxPinsInFrame)
            {
                Strike = true;
                Pins = MaxPinsInFrame;
            }
            else if (Pins == 0)
            {
                Spare = true;
            }
            Score += pinsKnocked;
        }
    }
}
