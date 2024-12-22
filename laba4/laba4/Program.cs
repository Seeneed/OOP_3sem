using System;
using System.Globalization;

namespace lab4
{
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
        public override string ToString()
        {
            return $"Студент: {_student}, Предмет: {_subjects}";
        }
        public override bool Equals(object? obj)
        {
            if (obj is Challenge otherChallenge)
            {
                return this._student == otherChallenge._student && this._subjects == otherChallenge._subjects;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return (_student, _subjects).GetHashCode();
        }
    }
    public class Test : Challenge
    {
        private int numberOfQuestion;
        public int _numberOfQuestion
        {
            get { return numberOfQuestion; }
            set { numberOfQuestion = value; }
        }
        public virtual string student
        {
            get { return _student; }
            set { _student = value; }
        }
        public virtual string subjects
        {
            get { return _subjects; }
            set { _subjects = value; }
        }
        public override bool DoClone()
        {
            return false;
        }
        public Test(string Student, string Subject, int Number) : base(Student, Subject)
        {
            _numberOfQuestion = Number;
        }
        public Test() : base() { }
        public override string ToString()
        {
            return $"Тест: Cтудент: {student}, Предмет: {subjects}, Количество вопросов в тесте: {_numberOfQuestion}";
        }
    }
    public class Exam : Challenge, ICloneable
    {
        private int attempt;
        public int _attempt
        {
            get { return attempt; }
            set { attempt = value; }
        }
        object ICloneable.Clone()
        {
            return new Exam(this._student, this._subjects, this._attempt);
        }
        bool ICloneable.DoClone()
        {
            return true;
        }
        public override bool DoClone()
        {
            return true;
        }
        public Exam(string Student, string Subject, int Attempt) : base(Student, Subject)
        {
            this.attempt = Attempt;
        }
        public override string ToString()
        {
            return $"Экзамен: Студент: {_student}, Предмет: {_subjects}, Количество попыток: {_attempt}";
        }
    }
    public class FinalExam : Exam
    {
        static readonly int numberOfFinallyExams = 4;
        private string exams;
        public FinalExam(string exams, string Student, string Subject, int Number) : base(Student, Subject, Number)
        {
            this.exams = exams;
        }
        public override string ToString()
        {
            return $"Выпускные экзамены: Студент: {_student}, Предметы: {_subjects}, Количество сданных экзаменов: {numberOfFinallyExams}";
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
        public Question(string Student, string Subject, int Number) : base(Student, Subject, Number)
        {
            this.id = Number;
        }
        public Question() : base()
        {
            _id = 1;
        }
        public void ChangeId()
        {
            _id = 100;
        }
        public override string ToString()
        {
            return $"ID вопроса: {id}, Студент: {_student}, Предмет: {_subjects}";
        }
    }
    public class Printer
    {
        public string IAmPrinting(Challenge someobj)
        {
            return someobj.ToString();
        }
    }
    class Programm
    {
        static void Main(string[] args)
        {
            Test test1 = new Test("Денис", "Русский язык", 40);
            Console.WriteLine(test1.ToString());
            Console.WriteLine($"Можно ли склонировать тест №1? {test1.DoClone()}");
            Console.WriteLine();

            Exam exam1 = new Exam("Максим", "Математика", 1);
            Console.WriteLine(exam1.ToString());
            Console.WriteLine($"Можно ли склонировать экзамен через класс? {exam1.DoClone()}");
            ICloneable cloneableExam = exam1;
            Console.WriteLine($"Можно ли склонировать экзамен через интерфейс? {cloneableExam.DoClone()}");
            Console.WriteLine($"Склонированный экзамен: {cloneableExam.Clone()}");
            Console.WriteLine();

            FinalExam finalExam1 = new FinalExam("Экзамены", "Даниил", "Физика", 3);
            Console.WriteLine(finalExam1.ToString());
            Console.WriteLine($"Можно ли склонировать выпускной экзамен №1? {finalExam1.DoClone()}");
            Console.WriteLine();

            Question question1 = new Question("Влад", "ООП", 5);
            Console.WriteLine(question1.ToString());
            question1.ChangeId();
            Console.WriteLine(question1.ToString());
            Console.WriteLine();

            if (exam1 is FinalExam)
            {
                Console.WriteLine("exam1 это выпускной экзамен");
            }
            else
            {
                Console.WriteLine("exam1 не является выпускным экзаменом");
            }

            Challenge challenge1 = exam1 as Challenge;
            if (challenge1 != null)
            {
                Console.WriteLine("exam1 успешно приведён к типу Challenge");
            }
            else
            {
                Console.WriteLine("exam1 не может быть приведён к типу Challenge");
            }
            Console.WriteLine();

            Printer printer = new Printer();
            Console.WriteLine("Полиморфный вывод: ");
            Console.WriteLine(printer.IAmPrinting(test1));
            Console.WriteLine(printer.IAmPrinting(question1));
            Console.WriteLine(printer.IAmPrinting(exam1));
            Console.WriteLine(printer.IAmPrinting(finalExam1));
        }
    }
}
