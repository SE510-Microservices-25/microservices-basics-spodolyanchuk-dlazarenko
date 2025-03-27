using MicroserviceApplication.Data;
using MicroserviceApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceApplication.Repositories;

public class PollRepository : IPollRepository
{
    private readonly AppDbContext _db;
    
    public PollRepository(AppDbContext db)
    {
        _db = db;
    }
    public async Task<ICollection<Poll>> GetAllPollsAsync()
    {
        var polls = await _db.Polls
            .Include(p => p.Options)
            .ToListAsync();
        
        return polls;
    }

    public async Task<Poll?> GetPollByIdAsync(uint id)
    {
        var poll = await _db.Polls
            .Include(p => p.Options)
            .FirstOrDefaultAsync(p => p.Id == id);

        return poll;
    }

    public async Task<Poll> CreatePollAsync(Poll poll)
    {
        await _db.Polls.AddAsync(poll);
        await _db.SaveChangesAsync();
        
        return poll;
    }

    public async Task<Poll?> UpdatePollAsync(Poll poll)
    {
        var pollOrigin = await GetPollByIdAsync(poll.Id);

        if (pollOrigin is null) return null;
        
        pollOrigin.Options = poll.Options;
        pollOrigin.Question = poll.Question;
        pollOrigin.ExpirationDate = poll.ExpirationDate;
            
        await _db.SaveChangesAsync();
        
        return pollOrigin;
    }

    public async Task<Poll?> DeletePollAsync(uint id)
    {
        var pollToDelete = await GetPollByIdAsync(id);

        if (pollToDelete is null) return null;

        _db.Polls.Remove(pollToDelete);
        await _db.SaveChangesAsync();

        return pollToDelete;
    }
}