namespace MicroserviceApplication.Dtos;

public class PollDto
{
    public uint Id { get; set; }
    public required string Question { get; set; }
    public uint UserId { get; set; }
    public DateTime ExpirationDate { get; set; }

    public ICollection<string> Options { get; set; } = null!;
}