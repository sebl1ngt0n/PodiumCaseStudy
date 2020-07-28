using System;

namespace PodiumCaseStudy.Services.Mortgage.Model
{
    public class MortgageProductQueryDto
    {
        public Guid UserId { get; set; }

        public int PropertyValue { get; set; }

        public int Deposit { get; set; }
    }
}
