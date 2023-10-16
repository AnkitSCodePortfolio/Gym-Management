using AutoMapper;
using Gym_Management.IRespository;
using Gym_Management.Models;
using Gym_Management.Respository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gym_Management.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IGymRespository<Payment> respository;
        private readonly ILogger<PaymentRespository> logger;
        private readonly IMapper mapper;
        private readonly GymContext context;

        public PaymentController(IGymRespository<Payment> respository,ILogger<PaymentRespository> logger,IMapper mapper,GymContext context)
        {
            this.respository = respository;
            this.logger = logger;
            this.mapper = mapper;
            this.context = context;
        }

        [HttpPost]
        [HttpGet]
        public IActionResult Payment()
        {
            try
            {
                TempData["IsBusyIndicator"] = "none";
                var name = context.customer.ToList();
                var member =context.members.ToList();
                var goal = context.Goals.ToList();
                var method = context.PayMethods.ToList();
                var payment = new PaymentDTO();
                var customer = context.customer.FirstOrDefault();
                if (customer != null)
                {
                    payment.CustomerName = customer.Name;
                }
                payment.PaymentDate = DateTime.Now;

                payment.Membership = member.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name,
                }).ToList();
                payment.Goal = goal.Select(x => new SelectListItem
                {
                    Value= x.Id.ToString(),
                    Text = x.Name
                }).ToList();
                payment.PaymentMethod = method.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToList();


                return View("~/Views/Admin/Payment.cshtml",payment);
            }
            catch (Exception ex)
            {
                string msg = "Database Error:";
                msg += ex.Message;
                logger.LogInformation(" Failed to execute. " + ex.Message + ex.StackTrace);
                return View("~/Views/Common/NotFound.cshtml");
                throw new Exception(msg);
            }
//data lana hai sare dropdowns ke
        }
        [HttpGet]
        public async Task<IActionResult> PaymentSummary()
        {
            try
            {
                var data = await respository.GetAll();
                var paysummary = mapper.Map<List<PaymentDTO>>(data);
                TempData["IsBusyIndicator"] = "none";
                int pageNumber = 1;
                int pageSize = 10;
                int totalItems = paysummary.Count();
                int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
                var pagedpayment = paysummary.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

                Page<PaymentDTO> pageData = new Page<PaymentDTO>()
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = totalPages,
                    Data = paysummary
                };

                return View("~/Views/Common/PaymentSummary.cshtml", pageData);
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

        //Invalid column name 'MembershipId'.
       // Invalid column name 'PayMethodId'.
//Invalid column name 'PaymentDate'.
//Invalid column name 'PaymentMethodId'.'

    }
}
