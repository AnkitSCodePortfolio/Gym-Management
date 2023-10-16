using Gym_Management.IRespository;
using Gym_Management.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Gym_Management.Respository
{
    public class CustomerRespository : IGymRespository<Customer>
    {
        private readonly GymContext context;
      

        public CustomerRespository(GymContext context)
        {
            this.context = context;

        }
        public async Task<List<Customer>> GetAll()
        {
              return  await context.customer.ToListAsync();
              
        }

        public async Task<Customer?> GetById(int Id)
        {
            return await context.customer.FirstOrDefaultAsync(x => x.Id == Id);
        }
        public async Task<Customer> Create(Customer model)
        {
            await context.customer.AddAsync(model);
            await context.SaveChangesAsync();
            return model;
        }
        public async Task<Customer?> Update(int Id, Customer model)
        {
            var data =await context.customer.FirstOrDefaultAsync(x => x.Id == Id);
            if(data == null)
            {
                return null;
            }
            data.Email = model.Email;
            data.Address = model.Address;
            data.PhoneNumber = model.PhoneNumber;
            data.Age = model.Age;
            await context.SaveChangesAsync();
            return model;
        }
        public async Task<Customer?> Delete(int Id)
        {
            var data = await context.customer.FirstOrDefaultAsync(x => x.Id == Id);
            if (data == null)
            {
                return null;
            }
            context.customer.Remove(data);
            await context.SaveChangesAsync();
            return data;

        }
        public async Task<List<SelectListItem>> GetGendersAsync()
        {
            return await context.Gender.Select(g => new SelectListItem
            {
                Value = g.Id.ToString(),
                Text = g.Name
            }).ToListAsync();
        }

        public async Task<List<SelectListItem>> GetTrainersAsync()
        {
            return await context.trainers.Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.Name
            }).ToListAsync();
        }

        public async Task<List<SelectListItem>> GetMembershipsAsync()
        {
            return await context.members.Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.Name
            }).ToListAsync();
        }

        public async Task<List<SelectListItem>> GetTimingsAsync()
        {
            return await context.timings.Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.Name
            }).ToListAsync();
        }

    }
}
