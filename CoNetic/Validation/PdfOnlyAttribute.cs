using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace CoNetic.Validation
{    
    public class PdfOnlyAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                if (file.ContentType != "application/pdf" && Path.GetExtension(file.FileName).ToLower() != ".pdf")
                {
                    return new ValidationResult("Only PDF files are allowed.");
                }
            }
            return ValidationResult.Success;
        }
    }

}
