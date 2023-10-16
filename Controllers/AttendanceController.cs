using AutoMapper;
using Gym_Management.IRespository;
using Gym_Management.Models;
using Gym_Management.Respository;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Management.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly IGymRespository<Attendance> respository;
        private readonly ILogger<AttendanceRespository> logger;
        private readonly IMapper mapper;

        public AttendanceController(IGymRespository<Attendance> respository, ILogger<AttendanceRespository> logger, IMapper mapper)
        {
            this.respository = respository;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Attendance()
        {
            try
            {
                var data = await respository.GetAll();
                var attendance = mapper.Map<List<AttendanceDTO>>(data);
                TempData["IsBusyIndicator"] = "none";
                int pageNumber = 1;
                int pageSize = 10;
                int totalItems = attendance.Count();
                int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
                var pagedAttendance = attendance.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

                Page<AttendanceDTO> pageData = new Page<AttendanceDTO>()
                {
                    PageSize = pageSize,
                    TotalPages = totalPages,
                    PageNumber = pageNumber,
                    Data = pagedAttendance
                };
                return View("~/Views/Common/Attendance.cshtml", pageData);
            }
            catch (Exception ex)
            {
                string msg = "Database Error:";
                msg += ex.Message;
                logger.LogInformation(" Failed to execute. " + ex.Message + ex.StackTrace);
                return View("~/Views/Common/NotFound.cshtml");
                throw new Exception(msg);
            }
        }
    }
}
//: 'Invalid column name 'Date'.
//Invalid column name 'DisplayType'.'
