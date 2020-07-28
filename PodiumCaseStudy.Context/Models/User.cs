using System;

namespace PodiumCaseStudy.Context
{
    public class User
    {
        public Guid Id { get; set; }

        public string Firstname { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
