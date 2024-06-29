# Flash Card Game

This is a simple Flash Card Game developed using C# and WPF. The game helps users practice arithmetic operations (addition, subtraction, multiplication, and division) with randomly generated pairs of numbers between 0 and 12.

## Features

- Practice arithmetic operations: addition (+), subtraction (-), multiplication (×), and division (÷).
- For subtraction, ensures the first number is always larger than or equal to the second number.
- For division, ensures both numbers are non-zero, the first number is divisible by the second number, and the result is an integer.
- Track which number combinations have been shown to ensure all 169 pairs are eventually displayed.
- Simple and intuitive user interface.
- Scoring system: award one point for correct answers, subtract one point for incorrect answers.
- Countdown timer for one minute of play.
- Immediate feedback on correct or incorrect answers.
- End game button to manually stop the game, display the final score, and reset the game.

## Getting Started

### Prerequisites

- Visual Studio 2022
- .NET Core 3.1 or later

### Installation

1. **Clone the repository:**

   ```bash
   git clone https://github.com/mokzheen/MokZheEn_Illumina_Project.git
   cd flash-card-game
