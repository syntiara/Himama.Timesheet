using System;
using Himama.Timesheet.Data.Entity;

namespace Himama.Timesheet.Data.Models
{
    public class AttendanceDTO
    {
        public static implicit operator AttendanceDTO(UserAttendance attendance)
        {
            var userDTO = new AttendanceDTO
            {
                Id = attendance?.Id,
                UserId = attendance?.UserId,
                ClockIn = attendance?.ClockIn,
                ClockOut = attendance?.ClockOut,
                IsClockedIn = attendance?.ClockIn != null && attendance?.ClockOut == null,
                IsClockedOut = attendance?.ClockIn != null && attendance?.ClockOut != null
            };

            return userDTO;
        }
        public int? Id { get; set; }

        public int? UserId { get; set; }

        public DateTime? ClockIn { get; set; }

        public DateTime? ClockOut { get; set; }

        public bool IsClockedIn { get; set; }

        public bool IsClockedOut { get; set; }
    }
}
