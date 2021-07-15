using System;
using System.Linq;
using System.Threading.Tasks;
using Himama.Timesheet.Data;
using Himama.Timesheet.Data.Entity;
using Himama.Timesheet.Data.Models;
using Himama.Timesheet.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Himama.Timesheet.Controllers
{
    public class UsersAttendanceController : ErrorController
    {
        private readonly DBContext _context;
        private readonly IUserAttendanceService _service;

        public UsersAttendanceController(IUserAttendanceService service, DBContext context)
        {
            _context = context;
            _service = service;
        }


        // GET: UsersAttendance/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest();
                }

                var userAttendance = await _service.GetAttendanceByUserId(id);

                if (!userAttendance.Any())
                {
                    return ShowNotFound("uh...oh! It looks like you don't have any attendance");
                }

                var attendanceDTO = userAttendance.Select(x => (AttendanceDTO)x).ToList();

                return View(attendanceDTO);
            }
            catch (Exception)
            {
                return ShowServerError();
            }
        }

        public async Task<IActionResult> CreateTimeSheet(int userId, bool isClockIn)
        {
            try
            {
                var user = await _service.GetUser(userId);

                if (user == null)
                {
                    return ShowNotFound("Sorry! I can't find you...");
                }

                var userAttendance = new UserAttendance
                {
                    UserId = user.Id,
                };

                if (isClockIn)
                {
                    userAttendance.ClockIn = DateTime.Now;
                    _context.UserAttendance.Add(userAttendance);
                }

                else
                {
                    userAttendance = await _context.UserAttendance.OrderBy(x => x.Id).LastOrDefaultAsync(x => x.UserId == userId);
                    if (userAttendance.ClockIn == null || userAttendance.ClockOut.HasValue)
                    {
                        return ShowNotFound("Umm! Looks like you are not clocked in yet");
                    }

                    userAttendance.ClockOut = DateTime.Now;
                    _context.UserAttendance.Update(userAttendance);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction($"{nameof(Details)}", "Users", new { id = userId });
            }
            catch (Exception)
            {
                return ShowServerError();
            }
        }

        public async Task<IActionResult> Edit(AttendanceWDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _service.GetUser(model.UserId);
                    if (user == null)
                    {
                        return ShowNotFound();
                    }
                    var userAttendance = await _service.GetAttendance(model.Id);

                    if (userAttendance == null)
                    {
                        return ShowNotFound("uh...oh! No timesheet record found");
                    }

                    userAttendance.ClockIn = model.ClockIn;
                    userAttendance.ClockOut = model.ClockOut;

                    _context.Update(userAttendance);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction($"{nameof(Details)}", "Users", new { id = model.UserId });
            }
            catch (Exception)
            {
                return ShowServerError(); ;
            }
        }
    }
}
