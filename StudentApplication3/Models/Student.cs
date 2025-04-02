//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StudentApplication3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MinLength(3, ErrorMessage = "Name should be at least 3 characters long.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name should contain only alphabets.")]
        public string Name { get; set; }



        public int? Age { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
    ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "DOB is required.")]

        public DateTime? DOB { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Hobbies are required.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Hobbies should contain only alphabets.")]
        [MinLength(4, ErrorMessage = "Hobbies should be at least 4 characters long.")]
        public string Hobbies { get; set; }

        [Required(ErrorMessage = "CDAC Center is required.")]
        public string CDAC_Center { get; set; }

        public int? center_id { get; set; }

        [Url(ErrorMessage = "Invalid Image URL.")]
        public string Image_Url { get; set; }

        public virtual center center { get; set; }
    }
}
