using Gym_Management.IRespository;
using Gym_Management.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace Gym_Management.Respository
{
    public class AttendanceRespository : IGymRespository<Attendance>
    {
        private readonly GymContext context;

        public AttendanceRespository(GymContext context)
        {
            this.context = context;
        }
        public async Task<List<Attendance>> GetAll()
        {
            return await context.employeestatus.ToListAsync();
        }

        public async Task<Attendance?> GetById(int Id)
        {
            return await context.employeestatus.FirstOrDefaultAsync( x => x.Id == Id);
        }
        public async Task<Attendance> Create(Attendance model)
        {
            var data = await context.employeestatus.AddAsync(model);
            await context.SaveChangesAsync();
            return model;

        }
        public async Task<Attendance?> Update(int Id, Attendance model)
        {
            var data = await context.employeestatus.FirstOrDefaultAsync(x => x.Id == Id);
            if (data == null)
            {
                return null;
            }
           data.Timing = model.Timing;
           data.Status = model.Status;
            await context.SaveChangesAsync();
            return model;
        }
        public async Task<Attendance?> Delete(int Id)
        {
           var data= await context.employeestatus.FirstOrDefaultAsync(x => x.Id == Id);
            if(data == null)
            {
                return null;
            }
            context.employeestatus.Remove(data);
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
