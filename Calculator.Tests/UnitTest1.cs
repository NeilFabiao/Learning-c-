// ConsoleApplication_Final/Calculator/UnitTest1.cs
using System.IO;
using Newtonsoft.Json;
using Xunit;
using System.Collections.Generic;

public class BillTests
{
    public class TestCase
    {
        public int Cost1 { get; set; }
        public int Cost2 { get; set; }
        public int ExpectedTotal { get; set; }
        public int MockReturnValue { get; set; } // Add this to allow dynamic return values
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
        var mockObj = new MockNumberAdderService();
        var bill = new Bill(mockObj);

        bill.CalculateTotal(19, 30);

        Assert.Equal(19, mockObj.AddCalledWithNumber1);
        Assert.Equal(30, mockObj.AddCalledWithNumber2);
    }

    public static IEnumerable<object[]> GetTestCases()
    {
        var testCases = LoadTestCases(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testCases.json"));
        foreach (var testCase in testCases)
        {
            yield return new object[] { testCase.Cost1, testCase.Cost2, testCase.ExpectedTotal, testCase.MockReturnValue };
        }
    }

    [Theory]
    [MemberData(nameof(GetTestCases))]
    public void CalculateTotal_ShouldMatchExpectedTotal(int cost1, int cost2, int expectedTotal, int mockReturnValue)
    {
        var mockObj = new MockNumberAdderService { ReturnFromAddNumbers = mockReturnValue }; // Set mock return value
        var bill = new Bill(mockObj);

        int total = bill.CalculateTotal(cost1, cost2);

        Assert.Equal(expectedTotal, total);
        Assert.Equal(cost1, mockObj.AddCalledWithNumber1);
        Assert.Equal(cost2, mockObj.AddCalledWithNumber2);
    }

    [Fact]
    public void CalculateTotal_ShouldThrowException_WhenCostIsNegative()
    {
        var mockObj = new MockNumberAdderService();
        var bill = new Bill(mockObj);

        var exception1 = Assert.Throws<ArgumentException>(() => bill.CalculateTotal(-1, 30));
        Assert.Equal("Bill item costs cannot be negative.", exception1.Message);

        var exception2 = Assert.Throws<ArgumentException>(() => bill.CalculateTotal(19, -2));
        Assert.Equal("Bill item costs cannot be negative.", exception2.Message);
    }
}
