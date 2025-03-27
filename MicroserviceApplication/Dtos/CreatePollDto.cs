namespace MicroserviceApplication.Dtos;

public class CreatePollDto
{
    public required string Question { get; set; }
    public uint UserId { get; set; }
    public DateTime ExpirationDate { get; set; }
    public List<string> Options { get; set; } = new();
}