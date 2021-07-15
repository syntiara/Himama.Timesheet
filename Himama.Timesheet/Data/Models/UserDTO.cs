using System.Linq;
using Himama.Timesheet.Data.Entity;

namespace Himama.Timesheet.Data.Models
{
    public class UserDTO
    {
        public static implicit operator UserDTO(User user)
        {
            var userDTO = new UserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email.ToLowerInvariant(),
                LastUserAttendance = user.UserAttendance.Any() ? user.UserAttendance?.LastOrDefault() : null
            };

            return userDTO;
        }

        public int Id { get; private set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public AttendanceDTO LastUserAttendance { get; set; }
    }
}
