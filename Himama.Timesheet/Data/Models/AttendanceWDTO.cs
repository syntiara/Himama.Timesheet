using System;

namespace Himama.Timesheet.Data.Models
{
    public class AttendanceWDTO
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime? ClockIn { get; set; }

        public DateTime? ClockOut { get; set; }

    }
}
