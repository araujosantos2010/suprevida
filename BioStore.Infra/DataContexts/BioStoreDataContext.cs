using System;
using System.Data;
using System.Data.SqlClient;
using BioStore.Shared;

namespace BioStore.Infra.DataContexts
{
    public class BioStoreDataContext : IDisposable
    {
        public SqlConnection Connection { get; set; }

        public BioStoreDataContext()
        {
            Connection = new SqlConnection(Settings.ConnectionString);
            Connection.Open();
        }

        public void Dispose()
        {
            if (Connection.State != ConnectionState.Closed)
                Connection.Close();
        }
    }
}