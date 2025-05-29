using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Zoo.Infrastructure;

public class ZooContextFactory : IDesignTimeDbContextFactory<ZooContext>
{
    public ZooContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder()
            .UseSqlServer(args[0])
            .Options;

        return new ZooContext(options);
    }
}