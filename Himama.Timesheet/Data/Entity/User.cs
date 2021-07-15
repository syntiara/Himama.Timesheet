using System.Collections.Generic;
using Himama.Timesheet.Data.Models;

namespace Himama.Timesheet.Data.Entity
{
    public class User
    {
        private static string ConvertToTitleCase(string value) => string.IsNullOrEmpty(value) ? string.Empty
                                                            : char.ToUpper(value[0]) + value.Substring(1).ToLower();
        public static implicit operator User(UserWDTO userWDTO)
        {
            var user = new User
            {
                FirstName = ConvertToTitleCase(userWDTO.FirstName),
                LastName = ConvertToTitleCase(userWDTO.LastName),
                Email = userWDTO.Email.ToLowerInvariant()
            };

            return user;
        }

        public int Id { get; private set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public IList<UserAttendance> UserAttendance { get; set; } = new List<UserAttendance>();
    }
}
