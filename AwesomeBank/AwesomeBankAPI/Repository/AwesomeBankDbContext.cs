using AwesomeBankAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeBankAPI.Repository
{
    public class AwesomeBankDbContext : DbContext
    {
        public AwesomeBankDbContext(DbContextOptions<AwesomeBankDbContext> options) : base(options)
        { 
        
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

    }
}
