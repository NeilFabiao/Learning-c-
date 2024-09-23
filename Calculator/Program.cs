// ConsoleApplication_Final/Calculator/Program.cs
using System;

// Interface for adding numbers
// Defines a service for adding two numbers together.
public interface INumberAdderService {
    int AddNumbers(int number1, int number2);
}

// Real service implementation
// Implements the number adding service by returning the sum of two numbers.
public class NumberAdderService : INumberAdderService {
    public int AddNumbers(int number1, int number2) {
        return number1 + number2;
    }
}

// Class that uses the number adding service
// This class depends on INumberAdderService to calculate the total of two bill items.
public class Bill {
    private readonly INumberAdderService numberAddingService;

    // Constructor injects an implementation of INumberAdderService.
    public Bill(INumberAdderService numberAddingService) {
        this.numberAddingService = numberAddingService;
    }

    // Calculates the total cost by using the AddNumbers method from the injected service.
    public int CalculateTotal(int billItemCost1, int billItemCost2) {
        return this.numberAddingService.AddNumbers(billItemCost1, billItemCost2);
    }
}

// Mock service for testing
// Simulates the number adding service for unit testing without relying on the real implementation.
public class MockNumberAdderService : INumberAdderService {
    // Stores the values that were passed into AddNumbers for later verification.
    public int? AddCalledWithNumber1 { get; private set; }
    public int? AddCalledWithNumber2 { get; private set; }

    // Mimics the real service's AddNumbers method while capturing the inputs for testing.
    public int AddNumbers(int number1, int number2) {
        AddCalledWithNumber1 = number1;
        AddCalledWithNumber2 = number2;
        return number1 + number2; // Returns the sum, just like the real service, to keep the behavior consistent for testing.
    }
}

// Usage in main program
class Program {
    static void Main(string[] args) {
        // Using the real service
        Bill bill = new Bill(new NumberAdderService());
        int total = bill.CalculateTotal(42, 58); // Calculates total using real values
        Console.WriteLine($"Total using real service: {total}"); // Expected output: 100 (42 + 58)

        // Using the mock service
        MockNumberAdderService mockObj = new MockNumberAdderService();
        Bill mockBill = new Bill(mockObj);
        int mockTotal = mockBill.CalculateTotal(49, 51); // Using mock values
        Console.WriteLine($"Total using mock service: {mockTotal}"); // Expected output: 100 (49 + 51)

        // Assertions for testing
        // Output captured values to verify that the mock was called with correct arguments.
        Console.WriteLine($"Mock Add Called With Number1: {mockObj.AddCalledWithNumber1}"); // Should output 49
        Console.WriteLine($"Mock Add Called With Number2: {mockObj.AddCalledWithNumber2}"); // Should output 51
    }
}

