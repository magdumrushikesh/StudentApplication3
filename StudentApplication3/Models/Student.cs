using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentApplication3.Models
{
    public partial class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MinLength(3, ErrorMessage = "Name should be at least 3 characters long.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name should contain only alphabets.")]
        public string Name { get; set; }



        public int? Age { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DOB { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Hobbies are required.")]
        public string Hobbies { get; set; }

        [Required(ErrorMessage = "CDAC Center is required.")]
        public string CDAC_Center { get; set; }

        public int? center_id { get; set; }

        [Url(ErrorMessage = "Invalid Image URL.")]
        public string Image_Url { get; set; }

        public virtual center center { get; set; }
    }
}
