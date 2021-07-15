using System.Collections.Generic;
using System.Threading.Tasks;
using Himama.Timesheet.Data.Entity;

namespace Himama.Timesheet.Services
{
    public interface IUserAttendanceService
    {
        public Task<UserAttendance> GetAttendance(int id);

        public Task<IList<UserAttendance>> GetAttendanceByUserId(int userId);

        public Task<User> GetUser(int id);

        public Task<User> GetUserAttendance(int userId);
    }
}
