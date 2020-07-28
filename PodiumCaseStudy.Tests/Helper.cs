using PodiumCaseStudy.Models;

namespace PodiumCaseStudy.Tests
{
    public static class Helper
    {
        public static MortgageProductQuery StandardMortgageProductQueryModel = new MortgageProductQuery
        {
            Deposit = 50000,
            PropertyValue = 100000
        };

        public static MortgageProductQuery HighLtvMortgageProductQueryModel = new MortgageProductQuery
        {
            Deposit = 15000,
            PropertyValue = 100000
        };

        public static string RegisterUrl = "/Register";
        public static string MortgageUrl = "/Mortgage/GetAvailableMortgages";
    }
}
