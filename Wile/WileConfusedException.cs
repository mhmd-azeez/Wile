using System;

namespace Wile
{
    public class WileConfusedException : Exception
    {
        public WileConfusedException(int line, int character, string message) : base (message)
        {
            Line = line;
            Character = character;
        }

        public int Line { get; }
        public int Character { get; }
    }
}
