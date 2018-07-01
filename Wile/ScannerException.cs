using System;
using System.Collections.Generic;
using System.Text;

namespace Wile
{
    public class ScannerException : Exception
    {
        public ScannerException(int line, string message) : base (message)
        {
            Line = line;
        }

        public int Line { get; }
    }
}
