using System.ComponentModel.DataAnnotations;

namespace Himama.Timesheet.Data.Models
{
    public class UserWDTO
    {
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "Exceeds character length of 50")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Exceeds character length of 50")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [StringLength(50, ErrorMessage = "Exceeds character length of 50")]
        public string Email { get; set; }
    }
}
