using System;
using System.Collections.Generic;
using System.Text;

namespace PiTechnicalInterview
{

    public class Roll
    {
        public int PinsKnocked { get; private set; }

        public Roll(int pinsKnocked)
        {
            if (pinsKnocked > 10)
                throw new ArgumentException("Pins knocked greater than 10");
            PinsKnocked = pinsKnocked;
        }
    }

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
            Frame currentFrame = new Frame(false);
            Frame previousFrame = currentFrame;
            _frames.Add(currentFrame);

            foreach (var roll in _rolls)
            {
                //frames have a limit of two rolls, except for frame 10.
                //increment frame if exhausted.
                if (currentFrame.Rolls == 2)
                {
                    //if previous frame was a strike add frame bonus
                    if(previousFrame.Strike)
                    previousFrame = currentFrame;
                    currentFrame = new Frame(_frames.Count==8); //set finalFrame to true if creating the 10th frame
                    _frames.Add(currentFrame);
                }
             
                currentFrame.AddRoll(roll.PinsKnocked);
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
