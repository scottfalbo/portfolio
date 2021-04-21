using Microsoft.Data.Sqlite;
using Portfolio.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace PortfolioTests
{
    public abstract class Mock : IDisposable
    {
        private readonly SqliteConnection _connection;
        protected readonly PortfolioDbContext _db;

        /// <summary>
        /// Constructor to create a mock database based on PortfolioDbContext.
        /// </summary>
        public Mock()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            _db = new PortfolioDbContext(
                new DbContextOptionsBuilder<PortfolioDbContext>()
                    .UseSqlite(_connection)
                    .Options);

            _db.Database.EnsureCreated();
        }

        /// <summary>
        /// Self destruct method for the database after use.
        /// </summary>
        public void Dispose()
        {
            _db?.Dispose();
            _connection?.Dispose();
        }


    }
}
