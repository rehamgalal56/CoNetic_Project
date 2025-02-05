using CoNetic.Core.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CoNetic.DTOs
{
    public class AddInformationDTO
    {

        [Required(ErrorMessage = "The JobRole field is required.")]
        [JsonPropertyName("jobRole")]
        public string JobRole { get; set; }

        [Required(ErrorMessage = "The Gender field is required.")]
       
        public int Gender { get; set; }

        [Required(ErrorMessage = "The BirthDate field is required.")]
        [SwaggerSchema(Format = "date", Description = "Date format: YYYY-MM-DD")]
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly BirthDate { get; set; }

        [Required(ErrorMessage = "The Phone field is required.")]
     
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "The AboutMe field is required.")]
       
        public string AboutMe { get; set; }

        //[Required(ErrorMessage = "The Country field is required.")]

        // public string Country { get; set; }

        // [Required(ErrorMessage = "The City field is required.")]
        //public string City { get; set; }
        [Required]
        public List<ExperienceDTO> Experiences { get; set; } = new List<ExperienceDTO>();

        [Required]
        public List<SkillDTO> Skills { get; set; } = new List<SkillDTO>();

    }
}
