﻿using System.ComponentModel.DataAnnotations;

namespace CineMagic.DTOs
{
    public class UserCredentials
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
