using MicroserviceApplication.Dtos;

namespace MicroserviceApplication.Services;

public interface IPollService
{
    Task<ICollection<PollDto>> GetAllPollsAsync();
    Task<PollDto?> GetPollByIdAsync(uint id);
    Task<PollDto> CreatePollAsync(CreatePollDto dto);
    Task<PollDto?> UpdatePollAsync(UpdatePollDto dto);
    Task<PollDto?> DeletePollAsync(uint id);
}