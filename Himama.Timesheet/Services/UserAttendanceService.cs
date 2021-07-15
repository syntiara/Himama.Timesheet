using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Himama.Timesheet.Data;
using Himama.Timesheet.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace Himama.Timesheet.Services
{
    public class UserAttendanceService : IUserAttendanceService
    {
        private readonly DBContext _context;


        public UserAttendanceService(DBContext context)
        {
            _context = context;
        }

        public async Task<UserAttendance> GetAttendance(int id)
        {
            return await _context.UserAttendance.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IList<UserAttendance>> GetAttendanceByUserId(int userId)
        {
            return await _context.UserAttendance.Include(x => x.User).OrderBy(x => x.Id)
                .Where(m => m.UserId == userId).ToListAsync();
        }

        public async Task<User> GetUser(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<User> GetUserAttendance(int userId)
        {
            return await _context.Users.Where(x => x.Id == userId).Include(x => x.UserAttendance).FirstOrDefaultAsync();
        }
    }
}
