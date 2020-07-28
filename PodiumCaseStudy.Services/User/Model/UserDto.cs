﻿using System;

namespace PodiumCaseStudy.Services.User
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string Firstname { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
