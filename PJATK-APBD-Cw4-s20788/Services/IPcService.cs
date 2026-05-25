using PJATK_APBD_Cw4_s20788.DTOs;

namespace PJATK_APBD_Cw4_s20788.Services;

public interface IPcService
{
    Task<List<PcResponseDto>> GetAllAsync();
    Task<List<PcComponentResponseDto>?> GetComponentsByPcIdAsync(int id);
    Task<PcResponseDto> CreateAsync(PcRequestDto dto);
    Task<PcResponseDto?> UpdateAsync(int id, PcRequestDto dto);
    Task<bool> DeleteAsync(int id);
}