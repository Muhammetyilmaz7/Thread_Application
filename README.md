# Threaded Classification

## Author
Muhammet Sait Yılmaz  
Student Number: 211229018  
Date: 04.01.2024  
GitHub: [Muhammetyilmaz7](https://github.com/Muhammetyilmaz7)

## Description
This C# program is a multithreaded application that classifies prime numbers, even numbers, and odd numbers using thread processing.

## How to Run
1. Clone the repository.
2. Open the project in your preferred C# development environment.
3. Build and run the program.

## Program Flow
1. The program generates a list of numbers from 1 to 1000000.
2. Divides the list into 4 equal parts.
3. Creates threads to process each part concurrently.
4. Classifies numbers into prime, even, and odd categories using separate thread-safe bags.
5. Sorts and prints the results.

## Usage
- The `App` function initializes the necessary data structures and threads.
- The `ProcessNumbers` function classifies numbers into different categories.
- The `IsPrime` function checks if a number is prime.

## Results
- Sorted lists of prime, even, and odd numbers are displayed in the console.

## Additional Notes
- Thread priorities are set, but note that the operating system may not guarantee their order.
- The elapsed time for the program's execution is also displayed.
