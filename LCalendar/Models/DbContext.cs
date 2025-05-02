using LCalendar.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Dutie> Duties { get; set; }
    public DbSet<EmployeeDutie> EmployeeDuties { get; set; }
    public DbSet<Role> Roles { get; set; }
    
    public DbSet<Client> Clients { get; set; }
    
    public DbSet<Apointment> Apointments { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}