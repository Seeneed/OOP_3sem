using System;

namespace laba5
{
    public enum SubjectType
    {
        OOP,
        Math,
        JS,
        ASD,
        LP
    }

    public struct currentData
    {
        public int day;
        public int month;
        public int year;
        public currentData(int Day, int Month, int Year)
        {
            day = Day;
            month = Month;
            year = Year;
        }
        public override string ToString()
        {
            return $"{day}.{month}.{year}";
        }
    }

    interface ICloneable
    {
        bool DoClone();
        object Clone();
    }

    public abstract class Challenge
    {
        private string student;
        private string subjects;

        public virtual string _student
        {
            get { return student; }
            set { student = value; }
        }

        public virtual string _subjects
        {
            get { return subjects; }
            set { subjects = value; }
        }

        public abstract bool DoClone();

        public Challenge(string Student, string Subject)
        {
            student = Student;
            subjects = Subject;
        }

        public Challenge(string Student)
        {
            student = Student;
            subjects = null;
        }

        public Challenge()
        {
            student = null;
            subjects = null;
        }
    }

    public partial class Test : Challenge
    {
        private int numberOfQuestion;
        private SubjectType subjectType;
        public currentData data;

        public int _numberOfQuestion
        {
            get { return numberOfQuestion; }
            set { numberOfQuestion = value; }
        }

        public SubjectType _subjectType
        {
            get { return subjectType; }
            set { subjectType = value; }
        }

        public virtual string student
        {
            get { return _student; }
            set { _student = value; }
        }

        public override bool DoClone()
        {
            return false;
        }

        public Test(string Student, SubjectType subject, int Number, currentData testData)
        {
            this.student = Student;
            this.subjectType = subject;
            this.numberOfQuestion = Number;
            this.data = testData;
        }

        public override string ToString()
        {
            return $"Тест: Студент: {student}, Предмет: {subjectType}, Количество вопросов: {_numberOfQuestion}, Дата: {data}";
        }
    }

    public class Exam : Challenge, ICloneable
    {
        private int attempt;
        private SubjectType subjectType;

        public int _attempt
        {
            get { return attempt; }
            set { attempt = value; }
        }

        public SubjectType _subjectType
        {
            get { return subjectType; }
            set { subjectType = value; }
        }

        object ICloneable.Clone()
        {
            return new Exam(this._student, this._subjectType, this._attempt);
        }

        bool ICloneable.DoClone()
        {
            return true;
        }

        public override bool DoClone()
        {
            return true;
        }

        public Exam(string Student, SubjectType Subject, int Attempt) : base(Student)
        {
            this.attempt = Attempt;
            this.subjectType = Subject;
        }

        public override string ToString()
        {
            return $"Экзамен: Студент: {_student}, Предмет: {_subjectType}, Количество попыток: {_attempt}";
        }
    }

    public class FinalExam : Exam
    {
        static readonly int numberOfFinallyExams = 4;
        private string exams;

        public FinalExam(string exams, string Student, SubjectType subject, int Number) : base(Student, subject, Number)
        {
            this.exams = exams;
        }

        public override string ToString()
        {
            return $"Выпускные экзамены: Студент: {_student}, Предмет: {_subjectType}, Количество сданных экзаменов: {numberOfFinallyExams}";
        }
    }

    public sealed class Question : Test
    {
        private int _id;

        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

        public Question(string Student, SubjectType subject, int Number) : base(Student, subject, Number, new currentData())
        {
            this.id = Number;
        }

        public void ChangeId()
        {
            _id = 100;
        }

        public override string ToString()
        {
            return $"ID вопроса: {id}, Студент: {_student}, Предмет: {_subjectType}";
        }
    }
    public class Printer
    {
        public string IAmPrinting(Challenge someobj)
        {
            return someobj.ToString();
        }
    }

}
