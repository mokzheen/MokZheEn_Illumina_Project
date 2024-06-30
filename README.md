# Flash Card Game

This is a simple Flash Card Game developed using C# and WPF. The game helps users practice arithmetic operations (addition, subtraction, multiplication, and division) with randomly generated pairs of numbers between 0 and 12.

## Features

- Practice arithmetic operations: addition (+), subtraction (-), multiplication (×), and division (÷).
- Option to select a random operation mode.
- For subtraction, ensures the first number is always larger than or equal to the second number.
- For division, ensures both numbers are non-zero, the first number is divisible by the second number, and the result is an integer.
- Track which number combinations have been shown to ensure all pairs are eventually displayed.
- Simple and intuitive user interface.
- Sound effects for correct and incorrect answers.
- Scoring system: award one point for correct answers, subtract one point for incorrect answers (score does not go below zero).
- Countdown timer for one minute of play.
- Displays the user's current score and highest score.
- Progress bar to visualize the remaining time.
- Instructions displayed on the UI for ease of use.

## How to Play
1. Start the Game:
   -Click on the "Start Game" button to begin a new game session.
   
3.  Select an Operation:
   -Choose an arithmetic operation from the dropdown menu. The available options are addition (+), subtraction (-), multiplication (×), division (÷), and random.
   
4. Answer the Questions:
   -A question will be displayed based on the selected operation.
   -Enter the correct answer in the input box and click "Submit" to check your answer.
   
5. Game Feedback:
   -The game will provide immediate feedback on whether the answer was correct or incorrect.
   -Correct answers will increment the score, while incorrect answers will decrement the score (unless the score is zero).
   
6. End the Game:
   -The game lasts for 60 seconds. You can also click "End Game" to stop the game early.
   -The final score will be displayed, and if it is higher than the previous highest score, it will be updated.
   
7. Instructions:
   -Instructions are displayed at the top of the game window for quick reference.

### Game Controls

- Start Game Button: Starts a new game session.
- End Game Button: Ends the current game session early.
- Submit Button: Submits the entered answer for the current question.

### Project Files

- MainWindow.xaml: Contains the UI layout and elements.
- MainWindow.xaml.cs: Contains the logic for generating questions, handling user input, tracking scores, and managing the game timer.
