using Gym_Management.IRespository;
using Gym_Management.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Gym_Management.Respository
{
    public class PaymentRespository : IGymRespository<Payment>
    {
        private readonly GymContext context;

        public PaymentRespository(GymContext context)
        {
            this.context = context;
        }

        public async Task<List<Payment>> GetAll()
        {
            return  await context.payment.ToListAsync();
        }

        public async Task<Payment?> GetById(int Id)
        {
           return await context.payment.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<Payment> Create(Payment model)
        {
            await context.payment.AddAsync(model);
            await context.SaveChangesAsync();
            return model;
        }
        public async Task<Payment?> Update(int Id, Payment model)
        {
            var data =await context.payment.FirstOrDefaultAsync(x => x.Id == Id);
            if (data == null) 
            {
                return null;
            }
            data.PhoneNumber = model.PhoneNumber;
            data.PaymentMethod = model.PaymentMethod;
            data.Amount = model.Amount;
            data.goal = model.goal;
            await context.SaveChangesAsync();
            return model;
        }
        public async Task<Payment?> Delete(int Id)
        {
            var model = await context.payment.FirstOrDefaultAsync(x => x.Id == Id);
            if (model == null)
            {
                return null;
            }
            context.payment.Remove(model);
            await context.SaveChangesAsync();
            return model;
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
