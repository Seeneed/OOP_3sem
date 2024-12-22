using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;

namespace laba6
{
    public abstract class Session
    {
        public abstract void addChallenge(Challenge challenge);
        public abstract void removeChallenge(Challenge challenge);
        public abstract void printAllChallenges();
        public abstract int countChallenges();
        public abstract int CountTestsByNumberOfQuestions(int numberOfQuestions);
        public abstract List<Exam> findExam(SubjectType subjectType);
        public abstract List<Challenge> getChallenges();
        public abstract void setChallenges(List<Challenge> newChallenges);

    }
    public class AllChallenges : Session
    {
        private List<Challenge> challenges = new List<Challenge>();

        public override void addChallenge(Challenge challenge)
        {
            challenges.Add(challenge);
        }

        public override void removeChallenge(Challenge challenge)
        {
            challenges.Remove(challenge);
        }

        public override void printAllChallenges()
        {
            foreach (Challenge challenge in challenges)
            {
                Console.WriteLine(challenge.ToString());
            }
        }

        public override int countChallenges()
        {
            return challenges.Count;
        }

        public override List<Exam> findExam(SubjectType subjectType)
        {
            List<Exam> exams = new List<Exam>();
            foreach (var challenge in challenges)
            {
                if (challenge is Exam exam && exam._subjectType == subjectType)
                {
                    exams.Add(exam);
                }
            }
            return exams;
        }

        public override int CountTestsByNumberOfQuestions(int numberOfQuestions)
        {
            int count = 0;
            foreach (var challenge in challenges)
            {
                if (challenge is Test test && test._numberOfQuestion == numberOfQuestions)
                {
                    count++;
                }
            }
            return count;
        }
        public override List<Challenge> getChallenges()
        {
            return challenges;
        }
        public override void setChallenges(List<Challenge> newChallenges)
        {
            challenges = newChallenges;
        }

    }
    public class sessionController
    {
        private Session session;

        public sessionController(Session session)
        {
            this.session = session;
        }

        public void addChallenge(Challenge challenge)
        {
            session.addChallenge(challenge);
        }

        public void removeChallenge(Challenge challenge)
        {
            session.removeChallenge(challenge);
        }

        public void printAllChallenges()
        {
            var allChallenges = ((AllChallenges)session).getChallenges();
            Console.WriteLine("Список всех испытаний:");
            foreach (var challenge in allChallenges)
            {
                Console.WriteLine(challenge.ToString());
            }
        }

        public void FindExamsBySubject(SubjectType subjectType)
        {
            int count = 0;
            var exams = session.findExam(subjectType);
            Console.WriteLine($"Экзамены по предмету {subjectType}:");
            foreach (var exam in exams)
            {
                Console.WriteLine(exam.ToString());
                count++;
            }
            Console.WriteLine($"Общее количество экзаменов по предмету {subjectType}: {count}");
        }

        public void countChallenge()
        {
            Console.WriteLine($"Общее количество испытаний в сессии: {session.countChallenges()}");
        }
        public void CountTestsByNumberOfQuestions(int numberOfQuestions)
        {
            Console.WriteLine($"Количество тестов по количеству вопросов: {session.CountTestsByNumberOfQuestions(numberOfQuestions)}");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            AllChallenges session = new AllChallenges();
            sessionController controller = new sessionController(session);

            Test test1 = new Test("Денис", SubjectType.JS, 40, new currentData(14, 1, 2024));
            Exam exam1 = new Exam("Даниил", SubjectType.OOP, 1);
            FinalExam finalExam1 = new FinalExam("Экзамены", "Кирилл", SubjectType.Math, 4);
            Question question1 = new Question("Влад", SubjectType.ASD, 5);

            controller.addChallenge(test1);
            controller.addChallenge(exam1);
            controller.addChallenge(finalExam1);
            controller.addChallenge(question1);

            controller.printAllChallenges();
            controller.countChallenge();
            controller.FindExamsBySubject(SubjectType.OOP);
            controller.CountTestsByNumberOfQuestions(40);

            Console.WriteLine();

            try
            {
                Test test2 = new Test("Денис", SubjectType.JS, 70, new currentData(19, 10, 2024));
                controller.addChallenge(test2);
            }
            catch (TestException t2)
            {
                Console.WriteLine($"Ошибка: {t2.Message}. Значение: {t2.Value}");
            }

            try
            {
                Exam exam2 = new Exam("Кирилл", SubjectType.LP, 6);
                controller.addChallenge(exam2);
            }
            catch (ExamException ex2)
            {
                Console.WriteLine($"Ошибка: {ex2.Message}. Значение: {ex2.Value}");
            }

            try
            {
                Question question2 = new Question("Артём", SubjectType.ASD, 120);
                controller.addChallenge(question2);
            }
            catch (QuestionException q2)
            {
                Console.WriteLine($"Ошибка: {q2.Message}. Значение: {q2.Value}");
            }

            Console.WriteLine();

            static void studentMark(int mark)
            {
                Debug.Assert(mark >= 0 && mark <= 10, "Оценка должна быть в диапазоне от 0 до 10");
                if(mark < 0 || mark > 10)
                {
                    throw new InvalidExceptions("Оценка не входит в диапазон значений", mark);
                }
                Console.WriteLine($"Студент получил оценку {mark}");
            }

            static void divisionOfNumbers(int a, int b)
            {
                if (b == 0)
                {
                    throw new DivideByZeroException("Нельзя делить на ноль");
                }
                Console.WriteLine($"Деление чисел: {a / b}");
            }

            static void arrayInvalid()
            {
                int[] numbers = { 1, 2, 3 };
                numbers[8] = 10;
            }

            static void errorOfObject()
            {
                object obj = null;
                if (obj == null)
                {
                    Console.WriteLine("Объект равен null и вызов не возможен");
                }
                else
                {
                    Console.WriteLine(obj.ToString());
                }
            }

            static void checkCorrectName(string name)
            {
                if (name == null || name.Length < 2)
                {
                    throw new Exception("Имя содержит меньше 2 символов");
                }
                else
                {
                    Console.WriteLine($"Имя: {name}");
                }
            }

            try
            {
                try
                {
                    try
                    {
                        studentMark(12);
                    }
                    catch (InvalidExceptions ex1)
                    {
                        Console.WriteLine($"Ошибка: {ex1.Message}. Значение: {ex1.Value}");
                        throw;
                    }
                }
                catch(InvalidExceptions ex2)
                {
                    Console.WriteLine($"Вторичная обработка исключения: {ex2.Message}");
                }
                try
                {
                    divisionOfNumbers(2, 0);
                }
                catch (DivideByZeroException ex2)
                {
                    Console.WriteLine($"Деление на 0: {ex2.Message}");
                }

                try
                {
                    arrayInvalid();
                }
                catch (IndexOutOfRangeException ex3)
                {
                    Console.WriteLine($"Обращение к элементу массива по несуществующему индексу: {ex3.Message}");
                }

                try
                {
                    errorOfObject();
                }
                catch (NullReferenceException ex4)
                {
                    Console.WriteLine($"Нулевой указатель на объект: {ex4.Message}");
                }

                try
                {
                    checkCorrectName("S");
                }
                catch (Exception ex5)
                {
                    Console.WriteLine($"Ошибка: {ex5.Message}");
                }
            }
            finally
            {
                Console.WriteLine("Программа выполнена");
            }
        }
    }
}
