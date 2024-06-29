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
            shownPairs.Clear(); // Reset the shown pairs for a new game
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
            SubmitButton.IsEnabled = true;
            EndButton.IsEnabled = true;
        }

        private void GenerateQuestion()
        {
            if (shownPairs.Count == allPairs.Count)
            {
                InitializePairs();
            }

            string operation = ((ComboBoxItem)OperationSelector.SelectedItem).Content.ToString();
            do
            {
                if (operation == "÷")
                {
                    number2 = random.Next(1, 13); // Ensure number2 is never 0 for division
                    number1 = number2 * random.Next(1, 13); // Ensure number1 is a multiple of number2
                }
                else
                {
                    number1 = random.Next(0, 13);
                    number2 = random.Next(0, 13);
                }

                if (operation == "-" && number1 < number2)
                {
                    var temp = number1;
                    number1 = number2;
                    number2 = temp;
                }

            } while (shownPairs.Contains($"{number1},{number2}"));

            shownPairs.Add($"{number1},{number2}");
            QuestionText.Text = $"{number1} {operation} {number2} = ?";
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            string operation = ((ComboBoxItem)OperationSelector.SelectedItem).Content.ToString();

            if (operation == "÷")
            {
                if (double.TryParse(AnswerInput.Text, out double answer))
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
            else
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
        }

        private bool CheckAnswer(double answer)
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
                    return Math.Abs(answer - (double)number1 / number2) < 0.001; // Allow a small tolerance for floating-point comparison
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
                MessageBox.Show($"Time's up! Your final score is {score}");
                EndGame();
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame();
        }

        private void EndButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Game ended! Your final score is {score}");
            EndGame();
        }

        private void EndGame()
        {
            timer.Stop();
            SubmitButton.IsEnabled = false;
            StartButton.IsEnabled = true;
            EndButton.IsEnabled = false;
            // Reset the score, time left, and clear the question
            score = 0;
            timeLeft = 60;
            ScoreText.Text = $"Score: {score}";
            TimerText.Text = $"Time Left: {timeLeft}";
            QuestionText.Text = string.Empty;
            AnswerInput.Clear();
            ResultText.Text = string.Empty;
        }
    }
}
