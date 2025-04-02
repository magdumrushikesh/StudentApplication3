

using System;

namespace StudentApplication3.DTO
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? Age { get; set; }
        public string Email { get; set; }
        public DateTime? DOB { get; set; }

        public string Gender { get; set; }

        public string Hobbies { get; set; }
        public string CDAC_Center { get; set; }
    }
}