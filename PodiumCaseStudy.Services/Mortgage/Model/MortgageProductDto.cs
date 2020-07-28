using System;
using PodiumCaseStudy.Context;

namespace PodiumCaseStudy.Services.Mortgage.Model
{
    public class MortgageProductDto
    {
        public Guid Id { get; set; }

        public string Lender { get; set; }

        public int InterestRate { get; set; }

        public MortgageType MortgageType { get; set; }

        public int LoanToValue { get; set; }
    }
}
