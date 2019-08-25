using Microsoft.EntityFrameworkCore;
using ZwajApp.Api.Models;
namespace ZwajApp.Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Value> Values { get; set; }
        public DbSet<User>Users  { get; set; }
    }
}