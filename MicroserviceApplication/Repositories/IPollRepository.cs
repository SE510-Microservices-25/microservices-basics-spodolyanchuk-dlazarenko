using MicroserviceApplication.Dtos;
using MicroserviceApplication.Models;

namespace MicroserviceApplication.Repositories;

public interface IPollRepository
{
    Task<ICollection<Poll>> GetAllPollsAsync();
    Task<Poll?> GetPollByIdAsync(uint id);
    Task<Poll> CreatePollAsync(Poll poll);
    Task<Poll?> UpdatePollAsync(Poll poll);
    Task<Poll?> DeletePollAsync(uint id);
}