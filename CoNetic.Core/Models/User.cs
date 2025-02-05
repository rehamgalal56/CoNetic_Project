using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;



namespace CoNetic.Core.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FullName { get; set; }

        public string? Image { get; set; }

        public string? JobRole { get; set; }

        public int Gender { get; set; }
        public DateOnly? BirthDate { get; set; } 
        public string ?AboutMe { get; set; }
        public string ?Country { get; set; }
        public string ?City { get; set; }
        public string ?linkedIn { get; set; }
        public string ?Github { get; set; }
        public string ?Protofilo { get; set; }
            

        public string? CVFile { get; set; }
        public string? VerificationCode { get; set; }
        public List<Skill> skills { get; set; } = new List<Skill>();
        public List<Experience>experiences { get; set; } = new List<Experience>();
        
    }
}
