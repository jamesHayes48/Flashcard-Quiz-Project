using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Program
{
    class Quiz
    {
        private List<Question> Questions { get; set; }

        public Quiz()
        {
            Questions = new List<Question>();
        }

        public Quiz(List<Question> questions)
        {
            Questions = questions;
        }
        
        public void AddQuestion(Question question)
        {
            Questions.Add(question);
        }
    }


    class Question
    {
        private string QuestionText { get; set; }
        private string[] AnswerArray { get; set; }
        private int CorrectAnswerIndex { get; set; }

        public Question(string questionText, string[] answerArray)
        {
            QuestionText = questionText;
            
            // Assign Correct answer index based on where the star is located
            int i = 0;
            foreach (string answer in answerArray)
            {
                if (answer.StartsWith("*"))
                {
                    CorrectAnswerIndex = i;
                    answerArray[i] = answer.Substring(1);
                }
                i++;
            }
            AnswerArray = answerArray;
        }

        public void DisplayAnswer()
        {
            Console.WriteLine($"Correct Answer: {AnswerArray[CorrectAnswerIndex]}");
        }
    }

    static void Main(string[] args)
    {
        Quiz quiz = new Quiz();
        string filePath = @"C:\\Users\\jhpip\source\repos\Flashcard Quiz Project\Biology-Quiz.txt";
        char delimiter = ';';

        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            while((line = reader.ReadLine()) != null)
            {
                string[] data = line.Split(delimiter);

                // Take the answers from the line input
                string[] answers = data.Skip(1).ToArray();

                Question newQuestion = new Question(data[0], answers);
                quiz.AddQuestion(newQuestion);
            }
        }
    }
}
