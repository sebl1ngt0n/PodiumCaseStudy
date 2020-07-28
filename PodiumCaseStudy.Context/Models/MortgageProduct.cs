using System;

namespace PodiumCaseStudy.Context
{
    public class MortgageProduct
    {
        public Guid Id { get; set; }

        public string Lender { get; set; }

        public int InterestRate { get; set; }

        public MortgageType MortgageType { get; set; }

        public int LoanToValue { get; set; }
    }
}
