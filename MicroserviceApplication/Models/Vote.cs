namespace MicroserviceApplication.Models;

public class Vote
{
    public uint Id { get; set; }
    public uint PollId { get; set; }
    public uint OptionId { get; set; }
    public uint UserId { get; set; }
    public DateTime VoteDate { get; set; }

    public Poll Poll { get; set; } = null!;
    public Option Option { get; set; } = null!;
}