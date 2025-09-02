using package_tracking_api.Data.Context;
using package_tracking_api.Data.Repositories;
using package_tracking_api.Domain.Dtos;
using package_tracking_api.Domain.Models;
using Tests.Utilities;

namespace Tests.RepositoryTests;

public class PackageRepositoryTests
{
    private PackageRepository GetRepository()
    {
        var context = DbContextGenerator.CreateInMemoryDbContext();
        return new PackageRepository(context);
    }
    
    [Fact]
    public async Task GetAllAsync_Should_Return_All_Packages()
    {
        // Arrange
        var repo = GetRepository();

        var package1 = new Package
        {
            TrackingNumber = "P090202T97",
            Sender = new Person { Name = "SenderName", Address = "SenderAddress", Phone = "111" },
            Recipient = new Person { Name = "RecipientName", Address = "RecipientAddress", Phone = "222" }
        };

        var package2 = new Package
        {
            TrackingNumber = "P090201A23",
            Sender = new Person { Name = "Jon", Address = "Street 1", Phone = "2754742" },
            Recipient = new Person { Name = "Bob", Address = "unknown", Phone = "7094323" }
        };

        await repo.AddAsync(package1);
        await repo.AddAsync(package2);

        // Act
        var result = await repo.GetAllAsync();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains(result, p => p.TrackingNumber == "P090202T97");
        Assert.Contains(result, p => p.TrackingNumber == "P090201A23");
    }

    [Fact]
    public async Task GetByTrackingNumberAsync_Should_Return_Correct_Package()
    {
        // Arrange
        var repo = GetRepository();

        var package = new Package
        {
            TrackingNumber = "P090201A23",
            Sender = new Person { Name = "SenderName", Address = "SenderAddress", Phone = "111" },
            Recipient = new Person { Name = "RecipientName", Address = "RecipientAddress", Phone = "222" }
        };

        await repo.AddAsync(package);

        // Act
        var result = await repo.GetByTrackingNumberAsync("P090201A23");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("P090201A23", result.TrackingNumber);
        Assert.NotNull(result.Sender);
        Assert.Equal("SenderName", result.Sender.Name);
        Assert.Equal("SenderAddress", result.Sender.Address);
        Assert.Equal("111", result.Sender.Phone);
        Assert.NotNull(result.Recipient);
        Assert.Equal("RecipientName", result.Recipient.Name);
        Assert.Equal("RecipientAddress", result.Recipient.Address);
        Assert.Equal("222", result.Recipient.Phone);
    }
    
    [Theory]
    [InlineData(PackageStatus.Created, null, null, 2)]
    [InlineData(null, "Jon", null, 1)]
    [InlineData(PackageStatus.Sent, null, null, 0)]
    [InlineData(PackageStatus.Created, "SenderName", "RecipientName", 1)]
    [InlineData(null, null, null, 2)]
    public async Task GetFilteredPackagesAsync_Should_Filter(
        PackageStatus? status, 
        string? senderName, 
        string? recipientName,
        int expectedCount)
    {
        // Arrange
        var repo = GetRepository();

        var package1 = new Package
        {
            TrackingNumber = "P090202T97",
            Sender = new Person { Name = "SenderName", Address = "SenderAddress", Phone = "111" },
            Recipient = new Person { Name = "RecipientName", Address = "RecipientAddress", Phone = "222" }
        };

        var package2 = new Package
        {
            TrackingNumber = "P090201A23",
            Sender = new Person { Name = "Jon", Address = "Street 1", Phone = "2754742" },
            Recipient = new Person { Name = "Bob", Address = "unknown", Phone = "7094323" }
        };

        await repo.AddAsync(package1);
        await repo.AddAsync(package2);

        var filter = new PackageFilterDto
        {
            Status = status,
            SenderName = senderName,
            RecipientName = recipientName,
        };

        // Act
        var result = await repo.GetFilteredPackagesAsync(filter);

        // Assert
        Assert.Equal(expectedCount, result.Count);
    }
}