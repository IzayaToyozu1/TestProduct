﻿using Microsoft.EntityFrameworkCore;
using TestProduct.Model;

namespace TestProduct.DB
{
    public class ApplicationContext: DbContext
    {
        private readonly string _connectionString = @"Data Source=172.16.24.85\K21;User Id=SuperUser;Password=fate;Initial Catalog=dbase1;MultipleActiveResultSets=True;TrustServerCertificate=True";
        public virtual DbSet<RequestRepair> RequestRepair { get; set; }
        public virtual DbSet<User> Users { get; set; }
        //public virtual DbSet<Person> Persons { get; set; } = null!;

        //public virtual DbSet<Position> Positions { get; set; } = null!;
        /// <summary>
        /// database connection
        /// </summary>
        /// <param name="connectionString"> example || @"Data Source=172.16.24.85\K21;User Id=SuperUser;Password=fate;Initial Catalog=dbase1;MultipleActiveResultSets=True;TrustServerCertificate=True" || </param>
        public ApplicationContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlServer(_connectionString);
    }
}
