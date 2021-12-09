using System;
using System.Collections.Generic;
using System.Text;

namespace PiTechnicalInterview
{

    public class Game
    {

        private List<Roll> _rolls;
        private List<Frame> _frames;
        public List<Frame> Frames
        {
            get
            {
                return _frames;
            }
        }

        private void LogFrames(List<Frame> frames)
        {
            foreach(var frame in frames)
            {
                Console.WriteLine(frame.Score);
            }
        }

        public Game()
        {
            _rolls = new List<Roll>();
        }

        public void Roll(int pinsKnocked)
        {
            _rolls.Add(new Roll(pinsKnocked));
        }

        public int Score()
        {
            _frames = new List<Frame>();
            Frame currentFrame = new Frame(null, false);
            _frames.Add(currentFrame);

            foreach (var roll in _rolls)
            {
                //frames have a limit of two rolls, except for frame 10.
                if (currentFrame.FrameCompleted)
                {
                    //instantiate finalFrame to if creating the 10th frame
                    if (_frames.Count==9)
                        currentFrame = new FinalFrame(currentFrame); 
                    else
                        currentFrame = new Frame(currentFrame, false);
                    _frames.Add(currentFrame);
                }
                currentFrame.AddRoll(roll);
            }

            LogFrames(_frames);

            int total = 0;
            foreach(var frame in Frames)
            {
                total += frame.Score;
            }
            return total;
        }

    }
}
