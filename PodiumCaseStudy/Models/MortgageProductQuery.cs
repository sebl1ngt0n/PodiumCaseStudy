using System;

namespace PodiumCaseStudy.Models
{
    public class MortgageProductQuery
    {
        public Guid UserId { get; set; }

        public int PropertyValue { get; set; }

        public int Deposit { get; set; }
    }
}
