using System.ComponentModel.DataAnnotations;

namespace ComputerComponentsApi.DTOs;

public class PcResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public double Weight { get; set; }
    public int Warranty { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Stock { get; set; }
}

public class PcCreateRequestDto
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = null!;

    [Range(0.01, double.MaxValue)]
    public double Weight { get; set; }

    [Range(1, int.MaxValue)]
    public int Warranty { get; set; }

    public DateTime CreatedAt { get; set; }

    [Range(0, int.MaxValue)]
    public int Stock { get; set; }
}

public class PcUpdateRequestDto : PcCreateRequestDto
{
}

public class PcWithComponentsResponseDto : PcResponseDto
{
    public List<PcComponentResponseDto> Components { get; set; } = new();
}