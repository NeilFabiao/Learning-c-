//Calculator.Tests/UnitTest1.cs
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Xunit;
using Moq; // Import Moq

public class BillTests
{
    public class TestCase
    {
        public int Cost1 { get; set; }
        public int Cost2 { get; set; }
        public int ExpectedTotal { get; set; }
    }

    private string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testCases.json");

    private static TestCase[] LoadTestCases(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Test cases file not found at {filePath}");
        }

        string json = File.ReadAllText(filePath);
        var testCases = JsonConvert.DeserializeObject<TestCase[]>(json);
        
        if (testCases == null)
        {
            throw new InvalidDataException("Deserialized test cases are null.");
        }

        return testCases;
    }

    [Fact]
    public void CalculateTotal_ShouldCallAddNumbers_WithCorrectParameters()
    {
        var mockService = new Mock<INumberAdderService>(); // Create a mock of INumberAdderService

        // Setup the mock to track parameters
        mockService.Setup(m => m.AddNumbers(It.IsAny<int>(), It.IsAny<int>()))
                   .Returns(0); // Return value does not matter here

        var bill = new Bill(mockService.Object);

        // Call the method under test
        bill.CalculateTotal(19, 30);

        // Verify that the mock was called with the correct parameters
        mockService.Verify(m => m.AddNumbers(19, 30), Times.Once);
    }

    public static IEnumerable<object[]> GetTestCases()
    {
        var testCases = LoadTestCases(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testCases.json"));
        foreach (var testCase in testCases)
        {
            yield return new object[] { testCase.Cost1, testCase.Cost2, testCase.ExpectedTotal };
        }
    }

    [Theory]
    [MemberData(nameof(GetTestCases))]
    public void CalculateTotal_ShouldMatchExpectedTotal(int cost1, int cost2, int expectedTotal)
    {
        var mockService = new Mock<INumberAdderService>(); // Create a mock of INumberAdderService

        // Setup the mock to return the expected total based on input parameters
        mockService.Setup(m => m.AddNumbers(cost1, cost2)).Returns(expectedTotal);

        var bill = new Bill(mockService.Object);

        // Call the method under test
        int total = bill.CalculateTotal(cost1, cost2);

        // Assert that the total matches the expected value
        Assert.Equal(expectedTotal, total);
        // Verify that the mock was called with the correct parameters
        mockService.Verify(m => m.AddNumbers(cost1, cost2), Times.Once);
    }
}
