// ConsoleApplication_Final/Calculator/Program.cs
using System;

// Interface for basic number operations
public interface INumberAdderService {
    int AddNumbers(int number1, int number2);
}

// Real service implementation
public class NumberAdderService : INumberAdderService {
    public int AddNumbers(int number1, int number2) {
        return number1 + number2;
    }
}

// Bill class depending on INumberAdderService
public class Bill {
    private readonly INumberAdderService _numberAddingService;

    public Bill(INumberAdderService numberAddingService) {
        _numberAddingService = numberAddingService;
    }

    // Calculates the total bill
    public int CalculateTotal(int billItemCost1, int billItemCost2) {
        if (billItemCost1 < 0 || billItemCost2 < 0)
            throw new ArgumentException("Bill item costs cannot be negative.");
        
        return _numberAddingService.AddNumbers(billItemCost1, billItemCost2);
    }
}

// Mock service for unit testing
public class MockNumberAdderService : INumberAdderService {
    public int? AddCalledWithNumber1 { get; private set; }
    public int? AddCalledWithNumber2 { get; private set; }

    // Property to allow setting the return value
    public int ReturnFromAddNumbers { get; set; } = 100; // Default to 100

    public int AddNumbers(int number1, int number2) {
        AddCalledWithNumber1 = number1;
        AddCalledWithNumber2 = number2;
        return ReturnFromAddNumbers; // Return the value set by the property
    }
}

// Main program
class Program {
    static void Main(string[] args) {
        // Using real service
        Bill bill = new Bill(new NumberAdderService());
        int total = bill.CalculateTotal(42, 58); 
        Console.WriteLine($"Total using real service: {total}"); // Expected: 100

        // Using mock service
        MockNumberAdderService mockService = new MockNumberAdderService();
        Bill mockBill = new Bill(mockService);
        int mockTotal = mockBill.CalculateTotal(49, 51); 
        Console.WriteLine($"Total using mock service: {mockTotal}"); // Expected: 100

        // Change the return value for different test scenarios
        mockService.ReturnFromAddNumbers = 150; // Set a different return value
        int modifiedTotal = mockBill.CalculateTotal(49, 51); 
        Console.WriteLine($"Modified total using mock service: {modifiedTotal}"); // Expected: 150

        // Verify mock was called with correct values
        Console.WriteLine($"Mock Add Called With Number1: {mockService.AddCalledWithNumber1}"); // Expected: 49
        Console.WriteLine($"Mock Add Called With Number2: {mockService.AddCalledWithNumber2}"); // Expected: 51
    }
}
