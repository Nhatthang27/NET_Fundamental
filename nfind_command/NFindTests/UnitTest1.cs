using Xunit;
using Xunit.Abstractions;

namespace Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        // Arrange
        string[] args = new string[] { "/v", "/c", "/n" };

        // Act
        var options = Program.BuildOption(args);

        //log the options
        Console.WriteLine("THANGNGNGN");

        // Assert
        Assert.NotNull(options);
        Assert.True(options.FindDontContain);
        Assert.True(options.CountMode);

    }
}