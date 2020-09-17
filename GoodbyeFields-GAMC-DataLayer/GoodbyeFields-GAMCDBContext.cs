using System;
using Microsoft.EntityFrameworkCore;
using GoodbyeFields_GAMC_Model;

namespace GoodbyeFields_GAMC_DataLayer
{
    public class GoodbyeFieldsGAMCDBContext:DbContext
    {
        public GoodbyeFieldsGAMCDBContext(DbContextOptions<GoodbyeFieldsGAMCDBContext> options) : base(options)
        {

        }

        //public virtual DbSet<Transaction> Transaction { get; set; }
        //public virtual DbSet<Player> Player { get; set; }
        //public virtual DbSet<TransactionType> TransactionType { get; set; }
    }
}
