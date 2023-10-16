using AutoMapper;
using Gym_Management.DTO;
using Gym_Management.IRespository;
using Gym_Management.Mapper;
using Gym_Management.Models;
using Gym_Management.Respository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace Gym_Management.Controllers
{
  
    public class CustomerController : Controller
    {
        private readonly IGymRespository<Customer> respository;
        private readonly IMapper mapper;
        private readonly ILogger<CustomerRespository> logger;
        private readonly GymContext context;

        public CustomerController(IGymRespository<Customer> respository,IMapper mapper,ILogger<CustomerRespository> logger,GymContext context)
        {
            this.respository = respository;
            this.mapper = mapper;
            this.logger = logger;
            this.context = context;
        }

       [HttpGet]
       public async Task<IActionResult> GetAll()
       {
            try
            {
                var response = await respository.GetAll();
                return Ok(mapper.Map<List<CustomerDTO>>(response));
            }
            catch (Exception ex)
            {
                string msg = "Database Error:";
                msg += ex.Message;
                logger.LogInformation(" Failed to execute. " + ex.Message + ex.StackTrace);
                throw new Exception(msg);
            }

       }
        [HttpGet]
       public async Task<IActionResult>GetById(int Id)
       {
            try
            {
                var response = await respository.GetById(Id);
                if(response == null)
                {
                    return NotFound();
                }

                return Ok(mapper.Map<CustomerDTO>(response));
            }
            catch (Exception ex)
            {
                string msg = "Database Error:";
                msg += ex.Message;
                logger.LogInformation(" Failed to execute. " + ex.Message + ex.StackTrace);
                throw new Exception(msg);
            }
       }
        [HttpPost]
       public async Task<IActionResult> Create([FromBody] CustomerRequestDTO customer)
       {
            try
            {
                var data = mapper.Map<Customer>(customer);
                data = await respository.Create(data);
                var request = mapper.Map<CustomerDTO>(data);
                return CreatedAtAction(nameof(GetById), new { Id = request.Id }, data);
            }
            catch (Exception ex)
            {
                string msg = "Database Error:";
                msg += ex.Message;
                logger.LogInformation(" Failed to execute. " + ex.Message + ex.StackTrace);
                throw new Exception(msg);
            }
       }
        [HttpPut]
        public async Task<IActionResult> Update( CustomerUpdateRequest customerupdate, int Id)
        {
            try
            {

                var data =  mapper.Map<Customer>(customerupdate);
                data = await respository.Update(Id, data);
                if(data == null)
                {
                    NotFound();
                }
                var result = mapper.Map<CustomerUpdateRequest>(data);
                return Ok(result);


            }
            catch (Exception ex)
            {
                string msg = "Database Error:";
                msg += ex.Message;
                logger.LogInformation(" Failed to execute. " + ex.Message + ex.StackTrace);
                throw new Exception(msg);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                var data = await respository.Delete(Id);
                if(data == null)
                {
                    return NotFound();
                }
               var result = mapper.Map<CustomerDTO>(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                string msg = "Database Error:";
                msg += ex.Message;
                logger.LogInformation(" Failed to execute. " + ex.Message + ex.StackTrace);
                throw new Exception(msg);
            }
        }

        [HttpGet]
        public async Task<IActionResult> DisplayCustomer()
        {
            try
            {
                var data = await respository.GetAll();
                var customer = mapper.Map<List<CustomerDTO>>(data);
                foreach (var cust in customer)
                {
                    cust.Gendername = GetGenderbyId(cust.GenderId);
                    cust.Membershipname = GetMembershipById(cust.MembershipId);
                    cust.Timingname = GetTimingById(cust.TimingsId);
                    cust.Trainername = GetTrainerById(cust.TrainerId);
                }
                TempData["IsBusyIndicator"] = "none";
                int pageNumber = 1;
                int pageSize = 10;
                int totalItems = customer.Count();
                int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
                var pagedCustomers = customer.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

                Page<CustomerDTO> pageData = new Page<CustomerDTO>()
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = totalPages,
                    Data = pagedCustomers
                };
                return View("~/Views/Common/Display_Customer.cshtml", pageData);
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
          [HttpGet]
          public IActionResult CreateCustomer()
          {
              var genderdata = context.Gender.ToList();
              var member = context.members.ToList();
              var timing = context.timings.ToList();
              var trainers = context.trainers.ToList();
              var customer = new CustomerDTO();
              customer.Dateofjoin = DateTime.Today;
              customer.Gender = genderdata.Select(x => new SelectListItem
              {
                  Value = x.Id.ToString(),
                  Text = x.Name,
              }).ToList();
              customer.Membership = member.Select(x => new SelectListItem
              {
                  Value = x.Id.ToString(),
                  Text = x.Name,
              }).ToList();
              customer.Timings = timing.Select(x => new SelectListItem
              {
                  Value = x.Id.ToString(),
                  Text = x.Name,
              }).ToList();
              customer.Trainer = trainers.Select(x => new SelectListItem
              {
                  Value = x.Id.ToString(),
                  Text = x.Name,
              }).ToList();

              return View("~/Views/Admin/Create_Customer.cshtml", customer);
          }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCustomer(CustomerDTO cust)
        {
            try
              {
                var data = mapper.Map<Customer>(cust);
                var response = await respository.Create(data);
                CustomerDTO customer = new CustomerDTO();
                customer.Membership = GetMemberships();
                customer.Timings = GetTimings();
                customer.Gender = GetGenders();
                customer.Dateofjoin = DateTime.Today;
                customer.Trainer = GetTrainers();

                return View("~/Views/Admin/Create_Customer.cshtml", customer);
              }  
               catch (Exception ex)
               {
                    string msg = "Database Error:";
                    msg += ex.Message;
                    logger.LogInformation("Failed to execute. " + ex.Message + ex.StackTrace);
                    return View("~/Views/Common/NotFound.cshtml");
                }

        }
   
         private List<SelectListItem> GetGenders()
         {
             var genderdata = context.Gender.ToList();
             return genderdata.Select(x => new SelectListItem
             {
                 Value = x.Id.ToString(),
                 Text = x.Name,
             }).ToList();
         }

        //Method to Get name by fetching it's Id and display in view
        public string GetGenderbyId(int Id)
        {
            var gender = context.Gender.FirstOrDefault(x => x.Id == Id);
            var data = mapper.Map<Genders>(gender);
            return data.Name;

        }
        public string GetMembershipById(int Id)
        {
            var member = context.members.FirstOrDefault(x => x.Id == Id);
            var data = mapper.Map<Membership>(member);
            return data.Name;
        }
        public string GetTimingById(int Id)
        {
            var timing = context.timings.FirstOrDefault(x => x.Id == Id);
            var data = mapper.Map<Timings>(timing);
            return data.Name;

        }
        public string GetTrainerById(int Id)
        {
            var trainer = context.trainers.FirstOrDefault(x => x.Id == Id);
            var data = mapper.Map<Trainer>(trainer);
            return data.Name;
        }
         private List<SelectListItem> GetTimings()
         {
             var timing = context.timings.ToList();
             return timing.Select(x => new SelectListItem
             {
                 Value = x.Id.ToString(),
                 Text = x.Name,
             }).ToList();
         }
         private List<SelectListItem> GetMemberships()
         {
             var member = context.members.ToList();
             return member.Select(x => new SelectListItem
             {
                 Value = x.Id.ToString(),
                 Text = x.Name,
             }).ToList();
         }
         private List<SelectListItem> GetTrainers()
         {
             var trainers = context.trainers.ToList();
             return trainers.Select(x => new SelectListItem
             {
                 Value = x.Id.ToString(),
                 Text = x.Name,
             }).ToList();
         }
    }
}
