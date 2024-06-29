using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace FlashCardGame
{
    public partial class MainWindow : Window
    {
        private Random random = new Random();
        private int number1;
        private int number2;
        private int score;
        private int timeLeft;
        private DispatcherTimer timer;
        private HashSet<string> shownPairs;
        private List<string> allPairs;

        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            InitializePairs();
        }

        private void InitializePairs()
        {
            allPairs = new List<string>();
            for (int i = 0; i <= 12; i++)
            {
                for (int j = 0; j <= 12; j++)
                {
                    allPairs.Add($"{i},{j}");
                }
            }
            shownPairs = new HashSet<string>();
        }

        private void StartNewGame()
        {
            score = 0;
            timeLeft = 60;
            shownPairs.Clear();
            ScoreText.Text = $"Score: {score}";
            TimerText.Text = $"Time Left: {timeLeft}";
            StartGame();
            timer.Start();
        }

        private void StartGame()
        {
            GenerateQuestion();
            ResultText.Text = string.Empty;
            AnswerInput.Clear();
            AnswerInput.Focus();
        }

        private void GenerateQuestion()
        {
            if (shownPairs.Count == allPairs.Count)
            {
                InitializePairs();
            }

            do
            {
                number1 = random.Next(0, 13);
                number2 = random.Next(0, 13);
            } while (shownPairs.Contains($"{number1},{number2}"));

            shownPairs.Add($"{number1},{number2}");
            string operation = ((ComboBoxItem)OperationSelector.SelectedItem).Content.ToString();
            QuestionText.Text = $"{number1} {operation} {number2} = ?";
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(AnswerInput.Text, out int answer))
            {
                if (CheckAnswer(answer))
                {
                    score++;
                    ResultText.Text = "Correct!";
                }
                else
                {
                    score--;
                    ResultText.Text = "Incorrect!";
                }
                ScoreText.Text = $"Score: {score}";
                StartGame();
            }
            else
            {
                ResultText.Text = "Please enter a valid number.";
            }
        }

        private bool CheckAnswer(int answer)
        {
            string operation = ((ComboBoxItem)OperationSelector.SelectedItem).Content.ToString();
            switch (operation)
            {
                case "+":
                    return answer == number1 + number2;
                case "-":
                    return answer == number1 - number2;
                case "×":
                    return answer == number1 * number2;
                case "÷":
                    return number2 != 0 && answer == number1 / number2;
                default:
                    return false;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timeLeft--;
            TimerText.Text = $"Time Left: {timeLeft}";
            if (timeLeft == 0)
            {
                timer.Stop();
                MessageBox.Show($"Time's up! Your final score is {score}");
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame();
        }
    }

    public class Card
    {
        public string Suit { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            return $"{Value} of {Suit}";
        }
    }
}
