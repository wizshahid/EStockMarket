using System.ComponentModel.DataAnnotations;

namespace EStockMarket.Company.Domain.Entities;
public class CompanyModel
{
    public Guid Id { get; set; }

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

    public decimal LatestStockPrice { get; set; }

    public string CreatedBy { get; set; } = null!;
}
