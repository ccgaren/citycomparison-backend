using Microsoft.EntityFrameworkCore;
using System;

namespace CityComparison.EntityFrameworkCore
{
    public class CityComparisonContext : DbContext
    {
        public CityComparisonContext(DbContextOptions<CityComparisonContext> options) : base(options) { }
    }
}
