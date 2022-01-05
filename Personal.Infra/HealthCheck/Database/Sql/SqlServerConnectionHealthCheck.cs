using System.Data.Common;
using System.Data.SqlClient;

namespace Personal.Infra.HealthCheck.Database.Sql
{
    public class SqlServerConnectionHealthCheck : DbConnectionHealthCheck
    {
        public SqlServerConnectionHealthCheck(string connectionString) : base(connectionString)
        {
        }

        protected override DbConnection CreateConnection(string connectionString)
            => new SqlConnection(connectionString);
    }
}