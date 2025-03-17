using MvcDemo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace MvcDemo.Data
{
    public class ApplicationDbContext:DbContext{
public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){

}
    public DbSet<Person>Person{get;set;}

    public DbSet<Employee>Employee{get;set;}
    
    }
}