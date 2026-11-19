using Microsoft.EntityFrameworkCore;

namespace Database.Data;


public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }
}

