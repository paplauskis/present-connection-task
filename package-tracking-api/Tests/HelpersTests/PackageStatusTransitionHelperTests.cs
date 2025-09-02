using package_tracking_api.Domain.Models;
using package_tracking_api.Helpers;

namespace Tests.HelpersTests;

public class PackageStatusTransitionHelperTests
{
    [Fact]
    public void GetValidStatusChanges_Should_Return_Correct_Statuses()
    {
        var createdValid = PackageStatusTransitionHelper.GetValidStatusChanges(PackageStatus.Created);
        var sentValid = PackageStatusTransitionHelper.GetValidStatusChanges(PackageStatus.Sent);
        var returnedValid = PackageStatusTransitionHelper.GetValidStatusChanges(PackageStatus.Returned);
        var acceptedValid = PackageStatusTransitionHelper.GetValidStatusChanges(PackageStatus.Accepted);
        var cancelledValid = PackageStatusTransitionHelper.GetValidStatusChanges(PackageStatus.Cancelled);

        Assert.Equal(new[] { PackageStatus.Sent, PackageStatus.Cancelled }, createdValid);
        Assert.Equal(new[] { PackageStatus.Accepted, PackageStatus.Returned, PackageStatus.Cancelled }, sentValid);
        Assert.Equal(new[] { PackageStatus.Sent, PackageStatus.Cancelled }, returnedValid);
        Assert.Empty(acceptedValid);
        Assert.Empty(cancelledValid);
    }
    
    [Theory]
    [InlineData(PackageStatus.Created, PackageStatus.Sent, true)]
    [InlineData(PackageStatus.Created, PackageStatus.Cancelled, true)]
    [InlineData(PackageStatus.Created, PackageStatus.Accepted, false)]
    [InlineData(PackageStatus.Sent, PackageStatus.Accepted, true)]
    [InlineData(PackageStatus.Sent, PackageStatus.Returned, true)]
    [InlineData(PackageStatus.Sent, PackageStatus.Cancelled, true)]
    [InlineData(PackageStatus.Sent, PackageStatus.Created, false)]
    [InlineData(PackageStatus.Accepted, PackageStatus.Created, false)]
    [InlineData(PackageStatus.Cancelled, PackageStatus.Sent, false)]
    public void CanChangeStatus_Should_Return_Correct_Result(PackageStatus current, PackageStatus target, bool expected)
    {
        var result = PackageStatusTransitionHelper.CanChangeStatus(current, target);

        Assert.Equal(expected, result);
    }
}