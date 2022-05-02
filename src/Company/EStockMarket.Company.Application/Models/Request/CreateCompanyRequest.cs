using System.ComponentModel.DataAnnotations;

namespace EStockMarket.Company.Application.Models.Request;
public class CreateCompanyRequest
{
    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string Code { get; set; } = null!;

    [Required]
    public string CEO { get; set; } = null!;

    [Required]
    public string Website { get; set; } = null!;

    [Range(100000000, double.MaxValue)]
    public decimal TurnOver { get; set; }
}

public class UpdateCompanyRequest : CreateCompanyRequest
{
    public Guid Id { get; set; }
}
