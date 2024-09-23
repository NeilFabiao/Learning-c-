# C# Unit Tests for Bill Calculation

## Overview

This file contains unit tests for the `Bill` class using the xUnit testing framework and mock services. The tests verify that the `Bill` class behaves correctly when calculating totals and interacting with the `INumberAdderService` interface. JSON-based test cases are also utilized to make the tests more dynamic and data-driven.

### Key Components

### 1. `TestCase` Class
This class defines the structure of a test case, which includes:
- **Cost1**: The first cost value for the bill.
- **Cost2**: The second cost value for the bill.
- **ExpectedTotal**: The expected result after adding `Cost1` and `Cost2`.

### 2. `filePath`
The file path to the JSON file, `testCases.json`, which contains predefined test cases. This file is dynamically loaded at runtime to test the `Bill` class with various input values.

### 3. `LoadTestCases`
This method reads the content of the JSON file and deserializes it into an array of `TestCase` objects. If the deserialization fails or returns `null`, the method throws an `InvalidDataException`. This ensures that valid data is always provided to the tests.

### 4.`CalculateTotal_ShouldCallAddNumbers_WithCorrectParameters`
This test checks if the `Bill` class calls the `AddNumbers` method of the injected service (in this case, the `MockNumberAdderService`) with the correct parameters. 
- **Arrange**: The test sets up the `MockNumberAdderService` and `Bill` objects.
- **Act**: It invokes the `CalculateTotal` method with `19` and `30` as arguments.
- **Assert**: The test verifies that `AddNumbers` was called with these exact values, ensuring the correct inputs are being passed.

### 5.`CalculateTotal_ShouldMatchExpectedTotal_WithJSONTestCases`
This test uses external test data stored in a JSON file to validate the correctness of the `Bill` class logic.
- **Arrange**: It loads the test cases from `testCases.json`, which contains multiple input and expected result combinations.
- **Act**: For each test case, the `CalculateTotal` method is called with the input values, and the result is computed.
- **Assert**: The test verifies that the computed total matches the expected value, and that the mock service was called with the correct input values. This ensures the `Bill` class behaves as expected for a variety of inputs.

### JSON-Driven Testing
By using a JSON file to store test cases, this approach makes it easier to add, update, or remove test cases without modifying the source code. This makes the tests more flexible and scalable, as new test scenarios can be added just by updating the `testCases.json` file.
