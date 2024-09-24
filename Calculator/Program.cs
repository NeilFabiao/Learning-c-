//Calculator/Program.cs
using System;
using Moq; // Import Moq

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

// Main program
class Program {
    static void Main(string[] args) {
        // Using real service
        Bill bill = new Bill(new NumberAdderService());
        int total = bill.CalculateTotal(42, 58); 
        Console.WriteLine($"Total using real service: {total}"); // Expected: 100

        // Using mock service
        var mockService = new Mock<INumberAdderService>(); // Create mock of INumberAdderService

        // Setup the mock behavior
        mockService.Setup(m => m.AddNumbers(It.IsAny<int>(), It.IsAny<int>()))
                   .Returns(100); // Default return value

        // Create Bill instance with the mock service
        Bill mockBill = new Bill(mockService.Object);
        int mockTotal = mockBill.CalculateTotal(49, 51); 
        Console.WriteLine($"Total using mock service: {mockTotal}"); // Expected: 100

        // Change the mock's return value for different test scenarios
        mockService.Setup(m => m.AddNumbers(It.IsAny<int>(), It.IsAny<int>()))
                   .Returns(150); // Change return value

        int modifiedTotal = mockBill.CalculateTotal(49, 51); 
        Console.WriteLine($"Modified total using mock service: {modifiedTotal}"); // Expected: 150

        // Verify mock was called with correct values
        mockService.Verify(m => m.AddNumbers(49, 51), Times.Once);
    }
}
