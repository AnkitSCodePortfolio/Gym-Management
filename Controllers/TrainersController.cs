using AutoMapper;
using Gym_Management.DTO;
using Gym_Management.IRespository;
using Gym_Management.Models;
using Gym_Management.Respository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Gym_Management.Controllers
{
    public class TrainersController : Controller
    {
        private readonly IGymRespository<Trainer> respository;
        private readonly ILogger<TrainerRespository> logger;
        private readonly IMapper mapper;
        private readonly GymContext context;

        public TrainersController(IGymRespository<Trainer> respository, ILogger<TrainerRespository> logger,IMapper mapper,GymContext context)
        {
            this.respository = respository;
            this.logger = logger;
            this.mapper = mapper;
            this.context = context;
        }

        [HttpGet]
        public  IActionResult Home()
        {
            try
            {
                
                return View("~/Views/Common/Home.cshtml");
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
        public IActionResult CreateTrainer()
        {
            var gender = context.Gender.ToList();
            var trainer = new TrainerDTO();
            trainer.Dateofjoin = DateTime.Today;
            trainer.Gender = gender.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name 
            }).ToList();
            return View("~/Views/Admin/Create_Trainer.cshtml",trainer);
        }

        [HttpPost]
         public async Task<IActionResult> CreateTrainer( TrainerDTO trainers)
         {
            try
            { 
                var data = mapper.Map<Trainer>(trainers);
                var response = await respository.Create(data);
                TrainerDTO trainer = new TrainerDTO();
                trainer.Gender = GetGenders();
                trainer.Dateofjoin = DateTime.Today;
               
                return View("~/Views/Admin/Create_Trainer.cshtml");
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
        public async Task<IActionResult> DisplayTrainer()
        {
            try
            {
                var data = await respository.GetAll();
                var trainers = mapper.Map<List<TrainerDTO>>(data);
                foreach (var trainer in trainers)
                {
                    trainer.GenderName = GetGenderbyId(trainer.GenderId);
                }
                TempData["IsBusyIndicator"] = "none";
                int pageNumber = 1;
                int pageSize = 10;
                int totalItems = trainers.Count();
                int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
                var pagedTrainers = trainers.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

                Page<TrainerDTO> pageData = new Page<TrainerDTO>()
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = totalPages,
                    Data = pagedTrainers
                };

                return View("~/Views/Common/Display_Trainer.cshtml", pageData);
            }
            catch (Exception ex)
            {
                string msg = "Database Error:";

                msg += ex.Message;
                logger.LogInformation(" Failed to execute. " + ex.Message + ex.StackTrace);
                return View("~/Views/Common/NotFound.cshtml");
              
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var response = await respository.GetAll();
                return Ok(mapper.Map<List<TrainerDTO>>(response));
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
        public async Task<IActionResult> GetById( int Id)
        {
            try
            {
                var response = await respository.GetById(Id);
                if (response == null)
                {
                    return NotFound();
                }

                return Ok(mapper.Map<TrainerDTO>(response));
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
        public async Task<IActionResult> Create([FromBody] TrainerRequestDTO trainer)
        {
            try
            {
                var data = mapper.Map<Trainer>(trainer);
                data = await respository.Create(data);
                var request = mapper.Map<TrainerDTO>(data);
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
        public async Task<IActionResult> Update( TrainerUpdateRequest trainerupdate, int Id)
        {
            try
            {

                var data = mapper.Map<Trainer>(trainerupdate);
                data = await respository.Update(Id, data);
                if (data == null)
                {
                    NotFound();
                }
                var result = mapper.Map<TrainerUpdateRequest>(data);
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
        public async Task<IActionResult> Delete( int Id)
        {
            try
            {
                var data = await respository.Delete(Id);
                if (data == null)
                {
                    return NotFound();
                }
                var result = mapper.Map<TrainerDTO>(data);
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
        public string GetGenderbyId(int Id)
        {
            var gender = context.Gender.FirstOrDefault(x => x.Id == Id);
            var data = mapper.Map<Genders>(gender);
            return data.Name;

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

    }

}
