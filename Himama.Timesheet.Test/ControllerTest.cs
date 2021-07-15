using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Himama.Timesheet.Controllers;
using Himama.Timesheet.Data;
using Himama.Timesheet.Data.Entity;
using Himama.Timesheet.Data.Models;
using Himama.Timesheet.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Himama.Timesheet.Test
{
    public class ControllerTest
    {
        private readonly int userId = 7;
        private readonly DateTime dateTime = DateTime.Now;
        private Mock<DBContext> dbContextMock;
        private Mock<IUserAttendanceService> serviceMock;
        private UsersController usersController;
        private UsersAttendanceController attendanceController;


        private IList<User> users = new List<User>
        {
            new User { FirstName = "Tiara", LastName = "Patty", Email = "ozodumpat@gamil.com" },
            new User { FirstName = "Jaaira", LastName = "Patrick", Email = "tiapaty@gamil.com"  },
            new User { FirstName = "Mmeso", LastName = "Ezeh", Email = "tiapaty@gamil.com"  },
        };

        private IList<UserAttendance> GetMockUsersAttendance()
        {
            return new List<UserAttendance>
            {
                new UserAttendance { UserId = 7, ClockIn = dateTime, ClockOut = dateTime  },
                new UserAttendance { UserId = 8, ClockIn = dateTime, ClockOut = dateTime  },
                new UserAttendance { UserId = 7, ClockIn = dateTime, ClockOut = dateTime  },
            };
        }

        [Fact]
        public async Task Users_Details_ReturnsViewResult_WithValidModelObject()
        {
            InitTests();

            serviceMock.Setup(repo => repo.GetUserAttendance(It.IsAny<int>()))
                .ReturnsAsync(users.FirstOrDefault());

            usersController = new UsersController(serviceMock.Object, dbContextMock.Object);

            var result = await usersController.Details(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<UserDTO>(
                viewResult.ViewData.Model);
            Assert.Equal(model.Email, users.First().Email);

        }

        [Fact]
        public async Task User_Details_ReturnsNotFoundView()
        {
            InitTests();

            serviceMock.Setup(repo => repo.GetUserAttendance(It.IsAny<int>()))
                .ReturnsAsync((User)null);

            usersController = new UsersController(serviceMock.Object, dbContextMock.Object);

            var result = await usersController.Details(-1);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ErrorViewModel>(
                viewResult.ViewData.Model);

            Assert.Contains("not found", model.ErrorMessage);
        }


        [Fact]
        public async Task UsersAttendance_Details_ReturnsAViewResult_WithValidModelObject()
        {
            InitTests();

            serviceMock.Setup(repo => repo.GetAttendanceByUserId(It.IsAny<int>()))
                .ReturnsAsync(GetMockUsersAttendance().Where(x => x.UserId == userId).ToList());

            attendanceController = new UsersAttendanceController(serviceMock.Object, dbContextMock.Object);


            var result = await attendanceController.Details(userId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IList<AttendanceDTO>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        private void InitTests()
        {
            serviceMock = new Mock<IUserAttendanceService>();
            dbContextMock = new Mock<DBContext>();
        }

    }
}
