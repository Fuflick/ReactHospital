using System.ComponentModel.DataAnnotations;

namespace ReactHospital.Models;

public class Procedure
{
    [Key] public int Id { get; set; }
    
    public string? Body { get; set; }

    public decimal? Cost { get; set; }

    public DateOnly? Date { get; set; }
}