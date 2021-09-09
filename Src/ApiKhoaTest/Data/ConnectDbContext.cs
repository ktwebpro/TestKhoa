
using ApiKhoaTest.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace TestKhoa.Data
{
    public class ConnectDbContext : DbContext
    {
        public ConnectDbContext(DbContextOptions<ConnectDbContext> options)
            : base(options)
        {
        }
        public DbSet<Account> Account{ get; set; }
        public DbSet<Role> Role{ get; set; }
        public DbSet<AccountRole> AccountRole { get; set; }

    }
}
