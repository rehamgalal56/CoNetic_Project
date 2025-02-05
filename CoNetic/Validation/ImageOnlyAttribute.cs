using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace CoNetic.Validation
{    
    public class ImageOnlyAttribute : ValidationAttribute
    {
        private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png" };
        private readonly string[] _allowedMimeTypes = { "image/jpeg", "image/png" };

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                string extension = Path.GetExtension(file.FileName).ToLower();
                if (!_allowedExtensions.Contains(extension) || !_allowedMimeTypes.Contains(file.ContentType))
                {
                    return new ValidationResult("Only JPG, JPEG, and PNG images are allowed.");
                }
            }
            return ValidationResult.Success;
        }
    }

}
