using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Himama.Timesheet.Data;
using Himama.Timesheet.Data.Entity;
using Himama.Timesheet.Data.Models;
using Himama.Timesheet.Services;
using Microsoft.AspNetCore.Mvc;

namespace Himama.Timesheet
{
    public class UsersController : Controller
    {
        private readonly IUserAttendanceService _service;
        private readonly DBContext _context;

        public UsersController(IUserAttendanceService service, DBContext context)
        {
            _service = service;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IList<User>> SearchUser(string name)
        {
            IList<User> users = new List<User>();
            if (!String.IsNullOrEmpty(name))
            {
                users = await _service.SearchUserByName(name);
            }

            return users;
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var user = await _service.GetUserAttendance(id);
                if (user == null)
                {
                    return NotFound();
                }

                return View((UserDTO)user);
            }
            catch (Exception)
            {
                return null;
            }
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind] UserWDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entityEntry = _context.Users.Add(model);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Details), new { id = entityEntry.Entity });
                }
                return View(model);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
