using System;
using System.ComponentModel.DataAnnotations;

namespace StudentApplication3.Models
{
    public partial class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Age is required.")]
        [Range(1, 120, ErrorMessage = "Age must be between 1 and 120.")]
        public Nullable<int> Age { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Date of Birth is required.")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(Student), "ValidateDOB")]
        public Nullable<System.DateTime> DOB { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public string Gender { get; set; }

        [StringLength(500, ErrorMessage = "Hobbies cannot exceed 500 characters.")]
        public string Hobbies { get; set; }

        [Required(ErrorMessage = "CDAC Center is required.")]
        [StringLength(150, ErrorMessage = "CDAC Center name cannot exceed 150 characters.")]
        public string CDAC_Center { get; set; }

        // Custom Validation for DOB
        public static ValidationResult ValidateDOB(DateTime? dob, ValidationContext context)
        {
            if (!dob.HasValue)
                return ValidationResult.Success;

            if (dob.Value > DateTime.Now)
            {
                return new ValidationResult("Date of Birth cannot be in the future.");
            }
            return ValidationResult.Success;
        }
    }
}
