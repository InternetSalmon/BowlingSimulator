﻿using System;
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
        public bool FrameCompleted { get; private set; }
        public bool FinalFrame { get; private set; }
        public Frame PreviousFrame { get; private set; }


        private void UpdatePreviousFrames()
        {
            if (PreviousFrame == null || !(PreviousFrame.Strike || PreviousFrame.Spare))
                return;
            // The bonus for a previous spare frame is the number of pins knocked down by the next roll.
            PreviousFrame.AddScoreBonus(Score);
            // The bonus for a previous strike frame is the value of pins knocked down by the next two rolls.
            if (PreviousFrame.Strike)
                PreviousFrame.PreviousFrame?.AddScoreBonus(MaxPinsInFrame);
        }

        private void ValidateFrame()
        {
            if (FrameCompleted)
                throw new InvalidFrameException("Frame exhausted, has been completed");

            if ((!FinalFrame && Rolls > 2) || (FinalFrame && Rolls > 3))
                throw new InvalidFrameException("Frame exhausted, max rolls reached");

            if ((!FinalFrame && Rolls > 1 && Strike) || (FinalFrame && Rolls > 3 && Strike))
                throw new InvalidFrameException("Frame exhausted, strike occured");

            if (Pins < 0)
                throw new InvalidFrameException("Frame error, pins knocked that dont exist");
        }

        private void CompleteFrame()
        {
            FrameCompleted = true;
            UpdatePreviousFrames();
        }

        public Frame(Frame previousFrame, bool finalFrame)
        {
            PreviousFrame = previousFrame;
            FinalFrame = finalFrame;
            Pins = MaxPinsInFrame;
        }

        public void AddScoreBonus(int scoreBonus)
        {
            Score += scoreBonus;
        }

        public void AddRoll(int pinsKnocked)
        {

            Rolls++;
            Pins -= pinsKnocked;
            Score += pinsKnocked;

            ValidateFrame();

            if (pinsKnocked == MaxPinsInFrame)
            {
                Strike = true;
                Pins = MaxPinsInFrame;
                if(!FinalFrame)
                    CompleteFrame();
                else if(FinalFrame)
                {
                    if (Rolls == 1)
                        UpdatePreviousFrames();
                    else if (Rolls > 2)
                    {
                        PreviousFrame.AddScoreBonus(MaxPinsInFrame);
                        FrameCompleted = true;
                    }
                }
            }
            else if (Pins == 0 && !FinalFrame)
            {
                Spare = true;
                CompleteFrame();
            }
            else if((Rolls == 2 && !FinalFrame) || (Rolls == 3 && FinalFrame))
            {
                CompleteFrame();
            }
        }
    }
}
