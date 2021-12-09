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
        protected const int MaxPinsInFrame = 10;
        protected int Pins { get; set; }

        public int Score { get; protected set; }
        public List<Roll> Rolls { get; protected set; }
        public bool Strike { get; protected set; }
        public bool Spare { get; protected set; }
        public bool FrameCompleted { get; protected set; }
        public bool FinalFrame { get; protected set; }
        public Frame PreviousFrame { get; protected set; }

        public Frame()
        {
            Rolls = new List<Roll>();
        }

        protected virtual void UpdatePreviousFrames()
        {
            if (PreviousFrame == null)
                return;
            // The bonus for a previous spare frame is the number of pins knocked down by the next roll.
            if (PreviousFrame.Spare && Rolls.Count > 0)
            {
                PreviousFrame.AddScoreBonus(Rolls[0].PinsKnocked);
            } else if (PreviousFrame.Strike) {
                PreviousFrame.AddScoreBonus(Score);
            }
            // The bonus for a previous strike frame is the value of pins knocked down by the next two rolls.
            if (PreviousFrame.Strike)
                PreviousFrame.PreviousFrame?.AddScoreBonus(MaxPinsInFrame);
        }

        private void ValidateFrame()
        {
            if (FrameCompleted)
                throw new InvalidFrameException("Frame exhausted, has been completed");

            if ((!FinalFrame && Rolls.Count > 2) || (FinalFrame && Rolls.Count > 3))
                throw new InvalidFrameException("Frame exhausted, max rolls reached");

            if ((!FinalFrame && Rolls.Count > 1 && Strike) || (FinalFrame && Rolls.Count > 3 && Strike))
                throw new InvalidFrameException("Frame exhausted, strike occured");

            if (Pins < 0)
                throw new InvalidFrameException("Frame error, pins knocked that dont exist");
        }

        protected void CompleteFrame()
        {
            FrameCompleted = true;
            UpdatePreviousFrames();
        }

        protected virtual void ProcessRoll(Roll roll)
        {
            if (roll.PinsKnocked == MaxPinsInFrame)
            {
                Strike = true;
                Pins = MaxPinsInFrame;
                CompleteFrame();
            }
            else if (Pins == 0)
            {
                Spare = true;
                CompleteFrame();
            }
            else if (Rolls.Count == 2)
            {
                CompleteFrame();
            }
        }

        public Frame(Frame previousFrame, bool finalFrame)
        {
            Rolls = new List<Roll>();
            PreviousFrame = previousFrame;
            FinalFrame = finalFrame;
            Pins = MaxPinsInFrame;
        }

        public void AddScoreBonus(int scoreBonus)
        {
            Score += scoreBonus;
        }

        public void AddRoll(Roll roll)
        {

            Rolls.Add(roll);
            Pins -= roll.PinsKnocked;
            Score += roll.PinsKnocked;

            ValidateFrame();
            ProcessRoll(roll);
        }
    }
}
