﻿using Microsoft.AspNetCore.Identity;

namespace QuizzAppFilRouge.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string HandleBy { get; set; }

    }
}
