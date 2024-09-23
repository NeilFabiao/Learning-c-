// ConsoleApplication_Final/Calculator/UnitTest1.cs
using System.IO;
using Newtonsoft.Json;
using Xunit;

public class BillTests
{
    // Class representing a test case with two costs and the expected total.
    public class TestCase
    {
        public int Cost1 { get; set; } // First bill item cost.
        public int Cost2 { get; set; } // Second bill item cost.
        public int ExpectedTotal { get; set; } // Expected total after adding both costs.
    }

    // File path to the JSON file containing test cases.
    private string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testCases.json");

    // Method to load test cases from a JSON file.
    private TestCase[] LoadTestCases(string filePath)
    {
        // Read the JSON file content.
        string json = File.ReadAllText(filePath);
        
        // Deserialize the JSON content into an array of TestCase objects.
        var testCases = JsonConvert.DeserializeObject<TestCase[]>(json);

        // Throw an exception if deserialization fails.
        if (testCases == null)
        {
            throw new InvalidDataException("Deserialized test cases are null.");
        }

        return testCases;
    }

    // Test to ensure CalculateTotal calls AddNumbers with the correct parameters.
    [Fact]
    public void CalculateTotal_ShouldCallAddNumbers_WithCorrectParameters()
    {
        // Arrange: Create a mock service and Bill instance.
        var mockObj = new MockNumberAdderService();
        var bill = new Bill(mockObj);

        // Act: Call CalculateTotal with two test values (19, 30).
        bill.CalculateTotal(19, 30);

        // Assert: Verify that the mock service was called with the correct parameters.
        Assert.Equal(19, mockObj.AddCalledWithNumber1); // Expect 19 to be passed as the first number.
        Assert.Equal(30, mockObj.AddCalledWithNumber2); // Expect 30 to be passed as the second number.
    }

    // Test to verify CalculateTotal using test cases from a JSON file.
    [Fact]
    public void CalculateTotal_ShouldMatchExpectedTotal_WithJSONTestCases()
    {
        // Arrange: Load test cases from the JSON file.
        var testCases = LoadTestCases(filePath);

        // Iterate through each test case.
        foreach (var testCase in testCases)
        {
            // Create a new mock service and Bill instance for each test case.
            var mockObj = new MockNumberAdderService();
            var bill = new Bill(mockObj);

            // Act: Calculate the total using the test case values.
            int total = bill.CalculateTotal(testCase.Cost1, testCase.Cost2);

            // Assert: Check if the result matches the expected total.
            Assert.Equal(testCase.ExpectedTotal, total);
            
            // Verify that the mock service was called with the correct input values.
            Assert.Equal(testCase.Cost1, mockObj.AddCalledWithNumber1);
            Assert.Equal(testCase.Cost2, mockObj.AddCalledWithNumber2);
        }
    }
}