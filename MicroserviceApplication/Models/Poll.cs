namespace MicroserviceApplication.Models;

public class Poll
{
    public uint Id { get; set; }
    public required string Question { get; set; }
    public uint UserId { get; set; }
    public DateTime ExpirationDate { get; set; }

    public ICollection<Option> Options { get; set; } = null!;
}