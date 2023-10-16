using Gym_Management.IRespository;
using Gym_Management.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Serialization;

namespace Gym_Management.Respository
{
    public class TrainerRespository : IGymRespository<Trainer>
    {
        private readonly GymContext context;

        public TrainerRespository(GymContext context)
        {
            this.context = context;
         
        }
        public async Task<List<Trainer>> GetAll()
        {
         
            return await context.trainers.ToListAsync();
            
        }

        public async Task<Trainer?> GetById(int Id)
        {
            return await context.trainers.FirstOrDefaultAsync(x => x.Id == Id);

        }
        public async Task<Trainer> Create(Trainer model)
        {
           await context.trainers.AddAsync(model);
          await context.SaveChangesAsync();
            return model;

        }
        public async Task<Trainer?> Update(int Id, Trainer model)
        {
           var data = await context.trainers.FirstOrDefaultAsync(x => x.Id == Id);
            if(data== null)
            {
                return null;
            }
            data.PhoneNumber = model.PhoneNumber;
            data.Address = model.Address;
            data.Email = model.Email;
            data.Age = model.Age;
            await context.SaveChangesAsync();
            return model;

        }
        public async Task<Trainer?> Delete(int Id)
        {
            var data = await context.trainers.FirstOrDefaultAsync(x => x.Id == Id);
            if (data == null)
            {
                return null;
            }
            context.trainers.Remove(data);
            await context.SaveChangesAsync();
            return data;


        }

        public Task<List<SelectListItem>> GetGendersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<SelectListItem>> GetTrainersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<SelectListItem>> GetMembershipsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<SelectListItem>> GetTimingsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
