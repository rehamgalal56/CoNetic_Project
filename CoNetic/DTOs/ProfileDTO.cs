using CoNetic.Validation;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CoNetic.DTOs
{
    public class ProfileDTO
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

       
        public string JobRole { get; set; }

        [Required(ErrorMessage = "The Gender field is required.")]

        public int Gender { get; set; }

       
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly BirthDate { get; set; }

        public string PhoneNumber { get; set; }

        public string AboutMe { get; set; }

        public string Country { get; set; }

        
        public string City { get; set; }

        public String Image { get; set; }

        public String CVFile { get; set; }
        public List<ExperienceDTO> Experiences { get; set; } = new List<ExperienceDTO>();
       
        public List<SkillDTO> Skills { get; set; } = new List<SkillDTO>();



    }
}
