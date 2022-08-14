

using Microsoft.Extensions.Configuration;

namespace Project.Infrastructure.Persistence.Query
{
    public class SqlQueries
    {

        static IConfiguration _configuration = new ConfigurationBuilder()
            .AddXmlFile("SqlQueries.xml", true, true)
            .Build();
        public static string FilterRecord { get { return _configuration["FilterRecord"]; } }
        public static string DeleteRecord { get { return _configuration["DeleteRecord"]; } }
        public static string GetAllStation { get { return _configuration["GetAllStation"]; } }
        public static string AllResult { get { return _configuration["AllResult"]; } }
        public static string Additional { get { return _configuration["Additional"]; } }
        public static string ReturnPopular { get { return _configuration["ReturnPopular"]; } }
        public static string FilterByMonth { get { return _configuration["FilterByMonth"]; } }




    }
}
