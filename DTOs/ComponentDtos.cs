namespace ComputerComponentsApi.DTOs;

public class PcComponentResponseDto
{
    public int Amount { get; set; }
    public ComponentResponseDto Component { get; set; } = null!;
}

public class ComponentResponseDto
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public ComponentManufacturerResponseDto Manufacturer { get; set; } = null!;
    public ComponentTypeResponseDto Type { get; set; } = null!;
}

public class ComponentManufacturerResponseDto
{
    public int Id { get; set; }
    public string Abbreviation { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public DateOnly FoundationDate { get; set; }
}

public class ComponentTypeResponseDto
{
    public int Id { get; set; }
    public string Abbreviation { get; set; } = null!;
    public string Name { get; set; } = null!;
}