using System.Collections.Generic;

namespace Himama.Timesheet.Data.Entity
{
    public class User
    {
        public int Id { get; private set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public IList<UserAttendance> UserAttendance { get; set; } = new List<UserAttendance>();
    }
}
