namespace PJATK_APBD_Cw4_s20788.DTOs;

public class PcComponentResponseDto
{
    public string ComponentCode { get; set; } = null!;
    public string ComponentName { get; set; } = null!;
    public string ComponentType { get; set; } = null!;
    public string Manufacturer { get; set; } = null!;
    public int Amount { get; set; }
}