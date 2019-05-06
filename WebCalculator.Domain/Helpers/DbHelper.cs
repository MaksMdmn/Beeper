using System.Configuration;

namespace WebCalculator.Domain.Helpers
{
    public static class DbHelper
    {
        public static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
    }
}