using System;

namespace task17
{
    public class WrongThreadException : Exception
    {
        public WrongThreadException() { }
        public WrongThreadException(string message) : base(message) { }
        public WrongThreadException(string message, Exception inner) : base(message, inner) { }
    }
}
