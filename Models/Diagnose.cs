using System.ComponentModel.DataAnnotations;

namespace ReactHospital.Models;

public class Diagnose
{
    [Key] public int Id { get; set; }

    public string? Body { get; set; }

    public DateOnly? Date { get; set; }
}