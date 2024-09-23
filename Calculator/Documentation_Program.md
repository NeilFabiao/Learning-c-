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
