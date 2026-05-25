using Microsoft.EntityFrameworkCore;
using PJATK_APBD_Cw4_s20788.Data;
using PJATK_APBD_Cw4_s20788.DTOs;
using PJATK_APBD_Cw4_s20788.Models;

namespace PJATK_APBD_Cw4_s20788.Services;

public class PcService : IPcService
{
    private readonly AppDbContext _context;

    public PcService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<PcResponseDto>> GetAllAsync()
    {
        return await _context.Pcs
            .Select(pc => new PcResponseDto
            {
                Id = pc.Id,
                Name = pc.Name,
                Weight = pc.Weight,
                Warranty = pc.Warranty,
                CreatedAt = pc.CreatedAt,
                Stock = pc.Stock
            })
            .ToListAsync();
    }

    public async Task<List<PcComponentResponseDto>?> GetComponentsByPcIdAsync(int id)
    {
        var pcExists = await _context.Pcs.AnyAsync(pc => pc.Id == id);

        if (!pcExists)
            return null;

        return await _context.PcComponents
            .Where(pcComponent => pcComponent.PcId == id)
            .Select(pcComponent => new PcComponentResponseDto
            {
                ComponentCode = pcComponent.ComponentCode,
                ComponentName = pcComponent.Component.Name,
                ComponentType = pcComponent.Component.ComponentType.Name,
                Manufacturer = pcComponent.Component.ComponentManufacturer.FullName,
                Amount = pcComponent.Amount
            })
            .ToListAsync();
    }

    public async Task<PcResponseDto> CreateAsync(PcRequestDto dto)
    {
        var pc = new Pc
        {
            Name = dto.Name,
            Weight = dto.Weight,
            Warranty = dto.Warranty,
            CreatedAt = dto.CreatedAt,
            Stock = dto.Stock
        };

        _context.Pcs.Add(pc);
        await _context.SaveChangesAsync();

        return new PcResponseDto
        {
            Id = pc.Id,
            Name = pc.Name,
            Weight = pc.Weight,
            Warranty = pc.Warranty,
            CreatedAt = pc.CreatedAt,
            Stock = pc.Stock
        };
    }

    public async Task<PcResponseDto?> UpdateAsync(int id, PcRequestDto dto)
    {
        var pc = await _context.Pcs.FindAsync(id);

        if (pc == null)
            return null;

        pc.Name = dto.Name;
        pc.Weight = dto.Weight;
        pc.Warranty = dto.Warranty;
        pc.CreatedAt = dto.CreatedAt;
        pc.Stock = dto.Stock;

        await _context.SaveChangesAsync();

        return new PcResponseDto
        {
            Id = pc.Id,
            Name = pc.Name,
            Weight = pc.Weight,
            Warranty = pc.Warranty,
            CreatedAt = pc.CreatedAt,
            Stock = pc.Stock
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var pc = await _context.Pcs.FindAsync(id);

        if (pc == null)
            return false;

        _context.Pcs.Remove(pc);
        await _context.SaveChangesAsync();

        return true;
    }
}