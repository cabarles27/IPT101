using Cabarles_IPT.Framework.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Cabarles_IPT.Framework.DbContextFactory
{
    public class PosDbContextFactory
    {
        private readonly string _connectionString;

        public PosDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public PosDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<PosDbContext>();
            optionsBuilder.UseSqlServer(_connectionString, sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null);
            });
            return new PosDbContext(optionsBuilder.Options);
        }
    }
}
