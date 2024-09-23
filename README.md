# C# Program for Bill Calculation Service

## Overview

This C# project showcases the application of **Dependency Injection (DI)** to decouple logic from implementation, facilitating easier testing and maintainability. The program includes a service for adding numbers, a mock service for testing, and a `Bill` class that depends on the number-adding service to calculate the total cost.

## Folder Structure

The project contains the following key components:

- **Calculator**:
  - Contains the main source code for the application, including interfaces, service implementations, and the `Bill` class.
  
- **Calculator.Tests**:
  - Holds unit tests for the application, focusing on verifying the behavior of the `Bill` class and its interaction with the number-adding service.
  
- **ConsoleApplication_Final.sln**:
  - The solution file that can be opened in Visual Studio or VS Code to work with the project.

## Key Components

### 1. `INumberAdderService` Interface
This interface defines the contract for a service that adds two numbers. It contains the method `AddNumbers` which takes two integers as input and returns their sum.

### 2. `NumberAdderService`
The class that implements `INumberAdderService`, containing the actual logic for adding two numbers.

### 3. `Bill` Class
A class representing a bill calculator. It uses **Dependency Injection (DI)** to inject an implementation of `INumberAdderService` into its constructor and calculates the total of two bill items.

### 4. `MockNumberAdderService`
This is a mock implementation used for testing. It mimics the behavior of the real service but captures input values for validation in the unit tests.

### 5. `Program` Class
The entry point of the program. It demonstrates the functionality using both the real and mock services.

## Unit Testing with xUnit

The project employs the xUnit framework for unit testing, focusing on testing the `Bill` class. The tests are designed to validate the correctness of the bill calculation and the interaction between the `Bill` class and `INumberAdderService`.

### Key Tests

- **CalculateTotal_ShouldCallAddNumbers_WithCorrectParameters**:
  Verifies that the `Bill` class passes the correct parameters to the `AddNumbers` method.

- **CalculateTotal_ShouldMatchExpectedTotal_WithJSONTestCases**:
  Loads test cases from a JSON file to validate the `Bill` class against multiple input-output combinations.

## JSON-Driven Testing

The tests are enhanced by using a `testCases.json` file containing various test scenarios. This approach allows easy addition of test cases by modifying the JSON file rather than altering the test code directly.

## Usage

1. Clone the repository.
2. Open the solution in Visual Studio or Visual Studio Code.
3. Build the solution and run the tests to validate the behavior.

### References ###

0. The people I collaborated with during the project (Chris)

1. Microsoft Docs, [Dependency Injection in .NET](https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection)

2. [Mocking in Unit Tests](https://xunit.net/docs/mocking)

3. [xUnit Documentation](https://xunit.net/)

4. Github and various resources online

5. GPT-4-turbo, for guidance and assistance throughout the development process

### You made it this far for some Gifs ###

<p align="center">
  <img src="img1.gif" alt="Project Overview">
</p>



