using System;

namespace laba13
{
    [Serializable]
    public class Exam : Challenge
    {
        public string nameSubject { get; set; }
        public int numberOfQuestions { get; set; }

        [NonSerialized]
        public int numOfCountExamsOnSession = 5;

        public Exam() : base() { }

        public Exam(string nameSubject, int numberOfQuestions, string nameStudent) : base(nameStudent)
        {
            this.nameSubject = nameSubject;
            this.numberOfQuestions = numberOfQuestions;
        }

        public override string ToString()
        {
            return $"Название предмета: {nameSubject}, количество вопросов: {numberOfQuestions}, имя студента: {studentName}";
        }
    }
}
