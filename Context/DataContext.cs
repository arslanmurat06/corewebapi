using advancedwebapi.Models;
using Microsoft.EntityFrameworkCore;

namespace advancedwebapi.Context
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}
        public DbSet<Character> Characters {get;set;}
    }
}