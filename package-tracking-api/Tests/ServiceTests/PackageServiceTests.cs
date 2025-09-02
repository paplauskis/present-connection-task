using package_tracking_api.Data.Repositories;
using package_tracking_api.Domain.Dtos;
using package_tracking_api.Domain.Models;
using package_tracking_api.Services;
using Tests.Utilities;

namespace Tests.ServiceTests;

public class PackageServiceTests
{
    [Fact]
    public async Task CreatePackageAsync_Should_Add_Package()
    {
        var service = CreateService();

        var dto = CreatePackageDto();

        var result = await service.CreatePackageAsync(dto);

        Assert.NotNull(result);
        Assert.NotNull(result.Sender);
        Assert.Equal("SenderName", result.Sender.Name);
        Assert.Equal("SenderAddress", result.Sender.Address);
        Assert.Equal("111", result.Sender.Phone);
        Assert.NotNull(result.Recipient);
        Assert.Equal("RecipientName", result.Recipient.Name);
        Assert.Equal("RecipientAddress", result.Recipient.Address);
        Assert.Equal("222", result.Recipient.Phone);
        Assert.NotEqual(Guid.Empty, result.Id);
        Assert.False(string.IsNullOrWhiteSpace(result.TrackingNumber));
        Assert.Equal(PackageStatus.Created, result.Status);
        Assert.NotNull(result.StatusHistory);
        Assert.Single(result.StatusHistory);
        Assert.Equal(PackageStatus.Created, result.StatusHistory.First().Status);
    }

    [Fact]
    public async Task GetPackagesAsync_Should_Return_All_Packages()
    {
        var service = CreateService();

        var dto1 = CreatePackageDto();
        var dto2 = CreatePackageDto("h", "f", "123", "name", "0", "876543");

        var createdPackage1 = await service.CreatePackageAsync(dto1);
        var createdPackage2 = await service.CreatePackageAsync(dto2);

        var packages = (await service.GetPackagesAsync()).ToList();

        Assert.Equal(2, packages.Count);

        var package1 = packages.First(p => p.Id == createdPackage1.Id);
        Assert.Equal("SenderName", package1.Sender.Name);
        Assert.Equal("SenderAddress", package1.Sender.Address);
        Assert.Equal("111", package1.Sender.Phone);
        Assert.Equal("RecipientName", package1.Recipient.Name);
        Assert.Equal("RecipientAddress", package1.Recipient.Address);
        Assert.Equal("222", package1.Recipient.Phone);
        Assert.Equal(PackageStatus.Created, package1.Status);
        Assert.Single(package1.StatusHistory);
        Assert.Equal(PackageStatus.Created, package1.StatusHistory.First().Status);

        var package2 = packages.First(p => p.Id == createdPackage2.Id);
        Assert.Equal("h", package2.Sender.Name);
        Assert.Equal("f", package2.Sender.Address);
        Assert.Equal("123", package2.Sender.Phone);
        Assert.Equal("name", package2.Recipient.Name);
        Assert.Equal("0", package2.Recipient.Address);
        Assert.Equal("876543", package2.Recipient.Phone);
        Assert.Equal(PackageStatus.Created, package2.Status);
        Assert.Single(package2.StatusHistory);
        Assert.Equal(PackageStatus.Created, package2.StatusHistory.First().Status);
    }

    [Fact]
    public async Task GetPackageByTrackingNumber_Should_Return_Correct_Package()
    {
        var service = CreateService();

        var dto1 = CreatePackageDto("h", "f", "123", "name", "0", "876543");
        var dto2 = CreatePackageDto();

        var createdPackage1 = await service.CreatePackageAsync(dto1);
        var createdPackage2 = await service.CreatePackageAsync(dto2);

        var returnedPackage = await service.GetPackageByTrackingNumber(createdPackage1.TrackingNumber);

        Assert.NotNull(returnedPackage);
        Assert.Equal("h", returnedPackage.Sender.Name);
        Assert.Equal("f", returnedPackage.Sender.Address);
        Assert.Equal("123", returnedPackage.Sender.Phone);
        Assert.Equal("name", returnedPackage.Recipient.Name);
        Assert.Equal("0", returnedPackage.Recipient.Address);
        Assert.Equal("876543", returnedPackage.Recipient.Phone);
        Assert.Equal(PackageStatus.Created, returnedPackage.Status);
        Assert.Single(returnedPackage.StatusHistory);
        Assert.Equal(PackageStatus.Created, returnedPackage.StatusHistory.First().Status);
        Assert.Equal(createdPackage1.TrackingNumber, returnedPackage.TrackingNumber);
    }
    
    [Theory]
    [InlineData(PackageStatus.Created, null, null, 2)]
    [InlineData(null, "h", null, 1)]
    [InlineData(PackageStatus.Created, "SenderName", "RecipientName", 1)]
    public async Task GetFilteredPackagesAsync_Should_Filter(PackageStatus? status, 
        string? senderName, 
        string? recipientName,
        int expectedCount)
    {
        // Arrange
        var service = CreateService();
        await service.CreatePackageAsync(CreatePackageDto());
        await service.CreatePackageAsync(CreatePackageDto("h", "f", "123", "name", "0", "876543"));

        var filter = new PackageFilterDto
        {
            Status = status,
            SenderName = senderName,
            RecipientName = recipientName,
        };
        
        // Act
        var result = (await service.GetFilteredPackagesAsync(filter)).ToList();

        // Assert
        Assert.Equal(expectedCount, result.Count);
    }
    
    [Theory]
    [InlineData(PackageStatus.Sent)]
    [InlineData(PackageStatus.Cancelled)]
    public async Task UpdatePackageStatusAsync_Should_Update_Status(PackageStatus newStatus)
    {
        var service = CreateService();
    
        var dto = await service.CreatePackageAsync(CreatePackageDto());
    
        var newDto = new PackageUpdateStatusDto
        {
            TrackingNumber = dto.TrackingNumber,
            Status = newStatus
        };
    
        var updatedDto = await service.UpdatePackageStatusAsync(newDto);
    
        Assert.Equal(newStatus, updatedDto.Status);
        Assert.Equal(2, updatedDto.StatusHistory.Count);
    }
    
    private PackageService CreateService()
    {
        var context = DbContextGenerator.CreateInMemoryDbContext();
        var repo = new PackageRepository(context);
        var statusRepo = new PackageStatusRepository(context);
        return new PackageService(repo, statusRepo);
    }

    private CreatePackageDto CreatePackageDto(
        string senderName = "SenderName",
        string senderAddress = "SenderAddress",
        string senderPhone = "111",
        string recipientName = "RecipientName",
        string recipientAddress = "RecipientAddress",
        string recipientPhone = "222")
    {
        return new CreatePackageDto
        {
            Sender = new Person { Name = senderName, Address = senderAddress, Phone = senderPhone },
            Recipient = new Person { Name = recipientName, Address = recipientAddress, Phone = recipientPhone }
        };
    }
}
