using ComputerComponentsApi.DTOs;
using ComputerComponentsApi.Services;
using Microsoft.AspNetCore.Mvc;
namespace ComputerComponentsApi.Controllers;
[ApiController]
[Route("api/pcs")]
public class PcsController : ControllerBase
{
    private readonly IPcsService _pcsService;
    public PcsController(IPcsService pcsService)
    {
        _pcsService = pcsService;
    }
    [HttpGet]
    public async Task<ActionResult<List<PcResponseDto>>> GetAll()
    {
        var pcs = await _pcsService.GetAllAsync();
        return Ok(pcs);
    }
    [HttpGet("{id:int}/components")]
    public async Task<ActionResult<PcWithComponentsResponseDto>> GetComponents(int id)
    {
        var pc = await _pcsService.GetComponentsByPcIdAsync(id);
        if (pc is null)
        {
            return NotFound($"PC with id {id} was not found.");
        }
        return Ok(pc);
    }
    [HttpPost]
    public async Task<ActionResult<PcResponseDto>> Create([FromBody] PcCreateRequestDto request)
    {
        var createdPc = await _pcsService.CreateAsync(request);
        return CreatedAtAction(
            nameof(GetComponents),
            new { id = createdPc.Id },
            createdPc
        );
    }
    [HttpPut("{id:int}")]
    public async Task<ActionResult<PcResponseDto>> Update(int id, [FromBody] PcUpdateRequestDto request)
    {
        var updatedPc = await _pcsService.UpdateAsync(id, request);
        if (updatedPc is null)
        {
            return NotFound($"PC with id {id} was not found.");
        }
        return Ok(updatedPc);
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var wasDeleted = await _pcsService.DeleteAsync(id);
        if (!wasDeleted)
        {
            return NotFound($"PC with id {id} was not found.");
        }
        return NoContent();
    }
}