using ComputerComponentsApi.DTOs;
namespace ComputerComponentsApi.Services;
public interface IPcsService
{
    Task<List<PcResponseDto>> GetAllAsync();
    Task<PcWithComponentsResponseDto?> GetComponentsByPcIdAsync(int id);
    Task<PcResponseDto> CreateAsync(PcCreateRequestDto request);
    Task<PcResponseDto?> UpdateAsync(int id, PcUpdateRequestDto request);
    Task<bool> DeleteAsync(int id);
}