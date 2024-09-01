using Dapper;

namespace Model.Repository
{
    public class DapperRepository<T>(IConnectionProvider connectionProvider) : IRepository<T>
    {
        public IConnectionProvider DapperConnectionProvider { get; } = connectionProvider;

        public virtual IEnumerable<T> Get(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new InvalidDataException("Query can not be empty.");
            }

            try
            {
                using var connection = DapperConnectionProvider.GetConnection();
                return connection.Query<T>(query);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Query can not be executed.", e);
            }
        }
    }
}
