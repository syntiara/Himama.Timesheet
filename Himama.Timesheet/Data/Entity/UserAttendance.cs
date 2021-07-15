using System;

namespace Himama.Timesheet.Data.Entity
{
    public class UserAttendance
    {
        public int Id { get; private set; }

        public int UserId { get; set; }

        public DateTime? ClockIn { get; set; }

        public DateTime? ClockOut { get; set; }

        public User User { get; set; }

    }
}