# C# Program Explanation

## Overview

This C# program demonstrates the use of an interface (`INumberAdderService`) to decouple the logic of adding numbers from the specific implementation, following the Dependency Injection (DI) principle. It uses two implementations of the service:

- **NumberAdderService**: A real service that adds two numbers.
- **MockNumberAdderService**: A mock service used for testing purposes that mimics the real service while tracking the values passed in.

### Key Components

### 1. `INumberAdderService` Interface
This interface defines a contract for a service that adds two numbers. It provides a method signature for `AddNumbers`, which takes two integers as input and returns their sum.

### 2. `NumberAdderService`
This class implements the `INumberAdderService` interface and defines the real logic for adding two numbers. The `AddNumbers` method simply returns the sum of the two integers passed to it.

### 3. `Bill` Class
The `Bill` class represents an object that relies on a number-adding service to calculate the total cost of two bill items. It uses **Dependency Injection (DI)** by requiring an implementation of `INumberAdderService` in its constructor. This makes the class flexible, as it can work with any implementation of `INumberAdderService`.

- **Constructor**: The constructor takes an `INumberAdderService` object and assigns it to the private field `numberAddingService`.
- **CalculateTotal**: This method adds two bill items together using the `AddNumbers` method from the injected service.

### 4. `MockNumberAdderService`
This is a mock implementation of `INumberAdderService` used for unit testing. It simulates the behavior of the real service but also captures the input values passed to `AddNumbers` so they can be verified in tests.

- **AddCalledWithNumber1** and **AddCalledWithNumber2**: These properties store the values of the two numbers passed to the `AddNumbers` method. These are used to verify that the method was called with the correct arguments during testing.

### 5. `Program` Class
This is the entry point of the program where two scenarios are demonstrated:
- **Real Service**: A `Bill` object is created using the `NumberAdderService`, and the total of two bill items (42 and 58) is calculated and printed.
- **Mock Service**: A `Bill` object is created using the `MockNumberAdderService`. The inputs (49 and 51) are passed, and the mock tracks these inputs for verification. The test outputs the result and ensures the mock was called with the correct values.

### Conclusion
This program showcases how **Dependency Injection (DI)** can be applied to decouple logic from specific implementations, making code more flexible and easier to test. The use of a mock object in this example demonstrates the benefit of testing without relying on real implementations.

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
