using Domain.Entities.UserEntity;

namespace Domain.Entities;

public class Transaction
{
    public User OriginUser { get; set; }
    public User DestinationUser { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateTime { get; set; }
    public string Message { get; set; }
    public Receipt Receipt { get; set; }
}