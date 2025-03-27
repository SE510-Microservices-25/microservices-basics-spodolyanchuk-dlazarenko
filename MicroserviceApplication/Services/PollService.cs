using MicroserviceApplication.Dtos;
using MicroserviceApplication.Models;
using MicroserviceApplication.Repositories;

namespace MicroserviceApplication.Services;

public class PollService : IPollService
{
    private readonly IPollRepository _repository;
    
    public PollService(IPollRepository repository)
    {
        _repository = repository;
    }

    public async Task<ICollection<PollDto>> GetAllPollsAsync()
    {
        var polls = await _repository.GetAllPollsAsync();

        var pollDtos = polls.Select(pollOrigin => new PollDto()
        {
            Id = pollOrigin.Id,
            Question = pollOrigin.Question,
            UserId = pollOrigin.UserId,
            ExpirationDate = pollOrigin.ExpirationDate,
            Options = pollOrigin.Options.Select(option => option.OptionText).ToList()
        }).ToList();
        
        return pollDtos;
    }

    public async Task<PollDto?> GetPollByIdAsync(uint id)
    {
        var poll = await _repository.GetPollByIdAsync(id);

        if (poll is null) return null;
        
        var pollDto = new PollDto {
            Id = poll.Id,
            Question = poll.Question,
            UserId = poll.UserId,
            ExpirationDate = poll.ExpirationDate,
            Options = poll.Options.Select(option => option.OptionText).ToList()
        };

        return pollDto;
    }

    public async Task<PollDto> CreatePollAsync(CreatePollDto dto)
    {
        var poll = new Poll
        {
            Question = dto.Question,
            UserId = dto.UserId,
            ExpirationDate = dto.ExpirationDate,
        };

        poll.Options = dto.Options.Select(optionStr => new Option
        {
            OptionText = optionStr,
            Poll = poll
        }).ToList();

        var createdPoll = await _repository.CreatePollAsync(poll);
        
        return new PollDto {
            Id = createdPoll.Id,
            Question = createdPoll.Question,
            UserId = createdPoll.UserId,
            ExpirationDate = createdPoll.ExpirationDate,
            Options = createdPoll.Options.Select(option => option.OptionText).ToList()
        };
    }

    public async Task<PollDto?> UpdatePollAsync(UpdatePollDto dto)
    {
        var poll = new Poll
        {
            Question = dto.Question,
            UserId = dto.UserId,
            ExpirationDate = dto.ExpirationDate,
        };

        poll.Options = dto.Options.Select(optionStr => new Option
        {
            OptionText = optionStr,
            Poll = poll
        }).ToList();

        var updatePoll = await _repository.UpdatePollAsync(poll);

        if (updatePoll is null) return null;
        
        return new PollDto {
            Id = updatePoll.Id,
            Question = updatePoll.Question,
            UserId = updatePoll.UserId,
            ExpirationDate = updatePoll.ExpirationDate,
            Options = updatePoll.Options.Select(option => option.OptionText).ToList()
        };
    }

    public async Task<PollDto?> DeletePollAsync(uint id)
    {
        var deletedPoll = await _repository.DeletePollAsync(id);

        if (deletedPoll is null) return null;
        
        return new PollDto {
            Id = deletedPoll.Id,
            Question = deletedPoll.Question,
            UserId = deletedPoll.UserId,
            ExpirationDate = deletedPoll.ExpirationDate,
            Options = deletedPoll.Options.Select(option => option.OptionText).ToList()
        };
    }
}