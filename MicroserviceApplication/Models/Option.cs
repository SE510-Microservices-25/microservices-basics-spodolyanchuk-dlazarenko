namespace MicroserviceApplication.Models;

public class Option
{
    public uint Id { get; set; }
    public uint PollId { get; set; }
    public required string OptionText { get; set; }

    public Poll Poll { get; set; } = null!;
}
