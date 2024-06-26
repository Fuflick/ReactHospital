using Microsoft.EntityFrameworkCore;
using ReactHospital.Models;
namespace ReactHospital;

public class MyDbContext : DbContext
{

    // Какие таблицы есть в базе данных
    public DbSet<Diagnose> Diagnose { get; set; } = null!;
    public DbSet<Doctor> Doctor { get; set; } = null!;

    public DbSet<DocDiagnose> DocDiagnoses { get; set; } = null!;

    // Метод, реализующий подключение к базе данных
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Строка подключения
        optionsBuilder.UseNpgsql("Host=172.17.0.1;Port=5432;Database=react;Username=postgres;Password=123");
    }
}