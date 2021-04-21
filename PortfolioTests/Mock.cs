using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PortfolioTests
{
    public abstract class Mock : IDisposable
    {
        private readonly SqliteConnection _connection;

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
