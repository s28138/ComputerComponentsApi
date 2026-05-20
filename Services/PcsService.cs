using ComputerComponentsApi.Data;
using ComputerComponentsApi.DTOs;
using ComputerComponentsApi.Models;
using Microsoft.EntityFrameworkCore;
namespace ComputerComponentsApi.Services;
public class PcsService : IPcsService
{
    private readonly AppDbContext _dbContext;
    public PcsService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<PcResponseDto>> GetAllAsync()
    {
        return await _dbContext.PCs
            .AsNoTracking()
            .OrderBy(pc => pc.Id)
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
    public async Task<PcWithComponentsResponseDto?> GetComponentsByPcIdAsync(int id)
    {
        return await _dbContext.PCs
            .AsNoTracking()
            .Where(pc => pc.Id == id)
            .Select(pc => new PcWithComponentsResponseDto
            {
                Id = pc.Id,
                Name = pc.Name,
                Weight = pc.Weight,
                Warranty = pc.Warranty,
                CreatedAt = pc.CreatedAt,
                Stock = pc.Stock,
                Components = pc.PCComponents
                    .OrderBy(pcComponent => pcComponent.ComponentCode)
                    .Select(pcComponent => new PcComponentResponseDto
                    {
                        Amount = pcComponent.Amount,
                        Component = new ComponentResponseDto
                        {
                            Code = pcComponent.Component.Code,
                            Name = pcComponent.Component.Name,
                            Description = pcComponent.Component.Description,
                            Manufacturer = new ComponentManufacturerResponseDto
                            {
                                Id = pcComponent.Component.ComponentManufacturer.Id,
                                Abbreviation = pcComponent.Component.ComponentManufacturer.Abbreviation,
                                FullName = pcComponent.Component.ComponentManufacturer.FullName,
                                FoundationDate = pcComponent.Component.ComponentManufacturer.FoundationDate
                            },
                            Type = new ComponentTypeResponseDto
                            {
                                Id = pcComponent.Component.ComponentType.Id,
                                Abbreviation = pcComponent.Component.ComponentType.Abbreviation,
                                Name = pcComponent.Component.ComponentType.Name
                            }
                        }
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync();
    }
    public async Task<PcResponseDto> CreateAsync(PcCreateRequestDto request)
    {
        var pc = new PC
        {
            Name = request.Name,
            Weight = request.Weight,
            Warranty = request.Warranty,
            CreatedAt = request.CreatedAt,
            Stock = request.Stock
        };

        _dbContext.PCs.Add(pc);
        await _dbContext.SaveChangesAsync();

        return MapToResponseDto(pc);
    }
    public async Task<PcResponseDto?> UpdateAsync(int id, PcUpdateRequestDto request)
    {
        var pc = await _dbContext.PCs
            .FirstOrDefaultAsync(pc => pc.Id == id);
        if (pc is null)
        {
            return null;
        }
        pc.Name = request.Name;
        pc.Weight = request.Weight;
        pc.Warranty = request.Warranty;
        pc.CreatedAt = request.CreatedAt;
        pc.Stock = request.Stock;
        await _dbContext.SaveChangesAsync();
        return MapToResponseDto(pc);
    }
    public async Task<bool> DeleteAsync(int id)
    {
        var pc = await _dbContext.PCs
            .FirstOrDefaultAsync(pc => pc.Id == id);
        if (pc is null)
        {
            return false;
        }
        _dbContext.PCs.Remove(pc);
        await _dbContext.SaveChangesAsync();
        return true;
    }
    private static PcResponseDto MapToResponseDto(PC pc)
    {
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
}