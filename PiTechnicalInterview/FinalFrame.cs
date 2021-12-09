using System;
using System.Collections.Generic;
using System.Text;

namespace PiTechnicalInterview
{
    // In the tenth frame a player who rolls a spare or strike is allowed to roll the extra balls to complete the frame.
    // However no more than three balls can be rolled in tenth frame.

    /// <summary>
    /// FinalFrame overloads the regular frame rules in bowling to allow a third roll in the final frame.
    /// </summary>
    public class FinalFrame : Frame
    {
        
        public FinalFrame(Frame previousFrame) : base(previousFrame, true) { }

        protected override void ValidateFrame()
        {
            if (Rolls.Count > 3)
                throw new InvalidFrameException("Frame exhausted, max rolls reached");

            if ( Rolls.Count > 3 && Strike)
                throw new InvalidFrameException("Frame exhausted, strike occured");
        }

        // Handle adding the score of a third roll to the previous frame.
        protected override void UpdatePreviousFrames()
        {
            if(PreviousFrame != null && Rolls.Count > 2)
            {
                PreviousFrame.AddScoreBonus(MaxPinsInFrame);
            }
            else
            {
                base.UpdatePreviousFrames();
            }
        }
        // override ProcessRoll to allow support for a third roll in the frame.
        protected override void ProcessRoll(Roll roll)
        {
            if (roll.PinsKnocked == MaxPinsInFrame)
            {
                Strike = true;
                Pins = MaxPinsInFrame;
                if (Rolls.Count == 1)
                    UpdatePreviousFrames();
                else if (Rolls.Count > 2)
                {
                    CompleteFrame();
                }
            }
            else if (Rolls.Count == 3)
            {
                CompleteFrame();
            }
        }

    }
}
