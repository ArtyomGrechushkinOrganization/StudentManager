using Microsoft.EntityFrameworkCore;

namespace DatabaseA.Data;


public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }
}

