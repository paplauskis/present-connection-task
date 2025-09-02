using package_tracking_api.Helpers;

namespace Tests.HelpersTests;

public class TrackingNumberGeneratorTests
{
    [Fact]
    public void Generate_Should_Start_With_P()
    {
        var tn = TrackingNumberGenerator.Generate();
        Assert.StartsWith("P", tn);
    }

    [Fact]
    public void Generate_Should_Have_Correct_Length()
    {
        var tn = TrackingNumberGenerator.Generate();
        Assert.Equal(15, tn.Length);
    }

    [Fact]
    public void Generate_Should_Be_Unique()
    {
        var trackingNumberList = new List<string>();

        for (int i = 0; i < 100; i++)
        {
            trackingNumberList.Add(TrackingNumberGenerator.Generate());
        }
        
        Assert.Equal(trackingNumberList.Count, trackingNumberList.Distinct().Count());
    }
}