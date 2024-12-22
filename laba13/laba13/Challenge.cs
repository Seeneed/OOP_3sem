using System;

namespace laba13
{
    [Serializable]
    public abstract class Challenge
    {
        public string studentName { get; set; }
        public Challenge() { }
        public Challenge(string studentName)
        {
            this.studentName = studentName;
        }
    }
}
