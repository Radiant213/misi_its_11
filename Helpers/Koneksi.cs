using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace WebApplication1.Helpers
{
    public class Koneksi
    {
        private readonly IConfiguration _configuration;
        public Koneksi(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public SqlConnection GetConnection()
        {
            var connectionString =_configuration.GetConnectionString("DefaultConnection");
            return new SqlConnection(connectionString);
        }
    }
}
