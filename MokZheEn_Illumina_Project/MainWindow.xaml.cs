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
        private readonly Random _random = new Random();
        private int _number1;
        private int _number2;
        private int _score;
        private int _timeLeft;
        private readonly DispatcherTimer _timer;
        private HashSet<string> _shownPairs;
        private List<string> _allPairs;

        public MainWindow()
        {
            InitializeComponent();
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += Timer_Tick;
            InitializePairs();
            ToggleButtons(false); // Ensure buttons are disabled initially
        }

        private void InitializePairs()
        {
            _allPairs = new List<string>();
            for (int i = 0; i <= 12; i++)
            {
                for (int j = 0; j <= 12; j++)
                {
                    _allPairs.Add($"{i},{j}");
                }
            }
            _shownPairs = new HashSet<string>();
        }

        private void StartNewGame()
        {
            _score = 0;
            _timeLeft = 60;
            _shownPairs.Clear();
            UpdateScoreAndTime();
            StartGame();
            _timer.Start();
            StartButton.IsEnabled = false;
        }

        private void StartGame()
        {
            GenerateQuestion();
            ClearInputs();
            ToggleButtons(true);
        }

        private void GenerateQuestion()
        {
            if (_shownPairs.Count == _allPairs.Count)
            {
                InitializePairs();
            }

            string operation = ((ComboBoxItem)OperationSelector.SelectedItem).Content.ToString();
            do
            {
                GenerateNumbers(operation);
            } while (_shownPairs.Contains($"{_number1},{_number2}"));

            _shownPairs.Add($"{_number1},{_number2}");
            QuestionText.Text = $"{_number1} {operation} {_number2} = ?";
        }

        private void GenerateNumbers(string operation)
        {
            if (operation == "÷")
            {
                _number2 = _random.Next(1, 13); // Ensure number2 is never 0 for division
                _number1 = _number2 * _random.Next(1, 13); // Ensure number1 is a multiple of number2
            }
            else
            {
                _number1 = _random.Next(0, 13);
                _number2 = _random.Next(0, 13);
            }

            if (operation == "-" && _number1 < _number2)
            {
                SwapNumbers();
            }
        }

        private void SwapNumbers()
        {
            int temp = _number1;
            _number1 = _number2;
            _number2 = temp;
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            string operation = ((ComboBoxItem)OperationSelector.SelectedItem).Content.ToString();
            bool isValidInput = false;
            bool isCorrect = false;

            if (operation == "÷")
            {
                if (double.TryParse(AnswerInput.Text, out double divisionAnswer))
                {
                    isValidInput = true;
                    isCorrect = CheckAnswer(divisionAnswer);
                }
            }
            else
            {
                if (int.TryParse(AnswerInput.Text, out int integerAnswer))
                {
                    isValidInput = true;
                    isCorrect = CheckAnswer(integerAnswer);
                }
            }

            if (isValidInput)
            {
                UpdateScore(isCorrect);
                ResultText.Text = isCorrect ? "Correct!" : "Incorrect!";
                StartGame();
            }
            else
            {
                ResultText.Text = "Please enter a valid number.";
                AnswerInput.Focus();
                AnswerInput.SelectAll();
            }
        }

        private void UpdateScore(bool isCorrect)
        {
            if (isCorrect)
            {
                _score++;
            }
            else if (_score > 0)
            {
                _score--;
            }
            ScoreText.Text = $"Score: {_score}";
        }

        private bool CheckAnswer(double answer)
        {
            string operation = ((ComboBoxItem)OperationSelector.SelectedItem).Content.ToString();
            return operation switch
            {
                "+" => answer == _number1 + _number2,
                "-" => answer == _number1 - _number2,
                "×" => answer == _number1 * _number2,
                "÷" => Math.Abs(answer - (double)_number1 / _number2) < 0.001, // Allow a small tolerance for floating-point comparison
                _ => false
            };
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _timeLeft--;
            TimerText.Text = $"Time Left: {_timeLeft}";
            if (_timeLeft == 0)
            {
                MessageBox.Show($"Time's up! Your final score is {_score}");
                EndGame();
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame();
        }

        private void EndButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Game ended! Your final score is {_score}");
            EndGame();
        }

        private void EndGame()
        {
            _timer.Stop();
            ToggleButtons(false);
            ResetGame();
        }

        private void UpdateScoreAndTime()
        {
            ScoreText.Text = $"Score: {_score}";
            TimerText.Text = $"Time Left: {_timeLeft}";
        }

        private void ClearInputs()
        {
            ResultText.Text = string.Empty;
            AnswerInput.Clear();
            AnswerInput.Focus();
        }

        private void ToggleButtons(bool isGameActive)
        {
            SubmitButton.IsEnabled = isGameActive;
            EndButton.IsEnabled = isGameActive;
            StartButton.IsEnabled = !isGameActive;
        }

        private void ResetGame()
        {
            _score = 0;
            _timeLeft = 60;
            UpdateScoreAndTime();
            QuestionText.Text = string.Empty;
            AnswerInput.Clear();
            ResultText.Text = string.Empty;
        }
    }
}
