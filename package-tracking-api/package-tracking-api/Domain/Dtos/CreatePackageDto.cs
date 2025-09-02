using package_tracking_api.Domain.Models;

namespace package_tracking_api.Domain.Dtos;

public class CreatePackageDto
{
    public required Person Sender { get; set; }
    public required Person Recipient { get; set; }
    
    public bool IsValid()
    {
        return !string.IsNullOrWhiteSpace(Sender.Name) &&
               !string.IsNullOrWhiteSpace(Sender.Address) &&
               !string.IsNullOrWhiteSpace(Sender.Phone) &&
               !string.IsNullOrWhiteSpace(Recipient.Name) &&
               !string.IsNullOrWhiteSpace(Recipient.Address) &&
               !string.IsNullOrWhiteSpace(Recipient.Phone);
    }
}