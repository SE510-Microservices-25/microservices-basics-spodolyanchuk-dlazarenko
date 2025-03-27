using MicroserviceApplication.Dtos;
using MicroserviceApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceApplication.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PollController : ControllerBase
{
    private readonly IPollService _service;
    
    public PollController(IPollService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPolls()
    {
        var polls = await _service.GetAllPollsAsync();
        return Ok(polls);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPollById([FromRoute] uint id)
    {
        var poll = await _service.GetPollByIdAsync(id);
        if (poll is null)
            return NotFound();
        
        return Ok(poll);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePoll([FromBody] CreatePollDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdPoll = await _service.CreatePollAsync(dto);
        
        return CreatedAtAction(nameof(GetPollById), new { id = createdPoll.Id }, createdPoll);
    }

    [HttpPut]
    public async Task<IActionResult> UpdatePoll([FromBody] UpdatePollDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var updatedPoll = await _service.UpdatePollAsync(dto);

        if (updatedPoll is null) return NotFound();
        
        return Ok(updatedPoll);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePoll([FromRoute] uint id)
    {
        var poll = await _service.DeletePollAsync(id);
        if (poll is null)
            return NotFound();
        
        return Ok();
    }
}