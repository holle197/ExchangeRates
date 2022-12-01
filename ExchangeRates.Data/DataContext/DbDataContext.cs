using ExchangeRates.Data.ApiCallsManagers;
using ExchangeRates.Data.Currencies;
using ExchangeRates.Data.ExchangeRates;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates.Data.DataContext
{
    public class DbDataContext : DbContext
    {
        public DbDataContext(DbContextOptions<DbDataContext> options) : base(options)
        {

        }

        public DbSet<ApiCallManager> ApiCallManager { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<DailyRate> DailyRates { get; set; }
        public DbSet<ExchangeRate> ExchangeRates { get; set; }



    }
}
