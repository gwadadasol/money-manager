﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CategoryService.Domains.Model
{
    public class AppDbContext : DbContext
    {
        public DbSet<CategoryEntity> Categories { get; set; }

        public AppDbContext( DbContextOptions<AppDbContext> opt):base(opt)
        {
            
        }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseSqlite(@"Data Source=C:\Users\phili\src\money-manager\money-manager\database\moneymanager.db;");
        // }

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // { 
        //     modelBuilder.Entity<TransactionEntity>().ToTable("Transactions");
        //     modelBuilder.Entity<CategoryEntity>().ToTable("Categories");
        // }
    }
}
