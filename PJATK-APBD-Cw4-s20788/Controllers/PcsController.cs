using Microsoft.AspNetCore.Mvc;
using PJATK_APBD_Cw4_s20788.DTOs;
using PJATK_APBD_Cw4_s20788.Services;

namespace PJATK_APBD_Cw4_s20788.Controllers;

[ApiController]
[Route("api/pcs")]
public class PcsController : ControllerBase
{
    private readonly IPcService _pcService;

    public PcsController(IPcService pcService)
    {
        _pcService = pcService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _pcService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id:int}/components")]
    public async Task<IActionResult> GetComponents(int id)
    {
        var result = await _pcService.GetComponentsByPcIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(PcRequestDto dto)
    {
        var result = await _pcService.CreateAsync(dto);
        return Created($"/api/pcs/{result.Id}", result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, PcRequestDto dto)
    {
        var result = await _pcService.UpdateAsync(id, dto);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _pcService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}