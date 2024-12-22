using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba5
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
            FinalExam finalExam = new FinalExam("Экзамены", "Кирилл", SubjectType.Math, 4);
            Question question = new Question("Влад", SubjectType.ASD, 5);

            controller.addChallenge(test1);
            controller.addChallenge(exam1);
            controller.addChallenge(finalExam);
            controller.addChallenge(question);

            controller.printAllChallenges();
            controller.countChallenge();
            controller.FindExamsBySubject(SubjectType.OOP);
            controller.CountTestsByNumberOfQuestions(40);
        }
    }
}