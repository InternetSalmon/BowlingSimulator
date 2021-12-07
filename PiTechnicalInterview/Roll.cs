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
            if (pinsKnocked > 10 || pinsKnocked < 0)
                throw new ArgumentException("Pins knocked greater than 10");
            PinsKnocked = pinsKnocked;
        }
    }
}
