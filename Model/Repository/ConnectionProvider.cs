using System.Data;
using Microsoft.Extensions.Options;

namespace Model.Repository
{
    public class ConnectionProvider(IOptions<DbSettings> dbSettings, IDbConnection dbConnection) : IConnectionProvider
    {
        public IDbConnection GetConnection()
        {
            dbConnection.ConnectionString = dbSettings.Value.ConnectionString;
            return dbConnection;
        }
    }

    public interface IConnectionProvider
    {
        public IDbConnection GetConnection();
    }
}
