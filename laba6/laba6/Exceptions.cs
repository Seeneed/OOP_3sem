using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Globalization;

namespace laba6
{
    public class Errors : Exception
    {
        public Errors(string message) : base(message)
        { }
    }
    public class TestException : Errors
    {
        public int Value { get; }
        public TestException(string message, int value) : base(message)
        {
            Value = value;
        }
    }
    public class ExamException : Errors
    {
        public int Value { get; }
        public ExamException(string message, int value) : base(message)
        {
            Value = value;
        }
    }
    public class QuestionException : Errors
    {
        public int Value { get; }
        public QuestionException(string message, int value) : base(message)
        {
            Value = value;
        }
    }
    public class InvalidExceptions : Exception
    {
        public int Value { get; }
        public InvalidExceptions(string message, int value) : base(message)
        {
            Value = value;
        }
    }
}
