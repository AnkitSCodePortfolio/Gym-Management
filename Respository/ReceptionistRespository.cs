using Gym_Management.IRespository;
using Gym_Management.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gym_Management.Respository
{
    public class ReceptionistRespository : IGymRespository<Receptionist>
    {
        public ReceptionistRespository()
        {
        }

        public Task<Receptionist> Create(Receptionist model)
        {
            throw new NotImplementedException();
        }

        public Task<Receptionist?> Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Receptionist>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Receptionist?> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<SelectListItem>> GetGendersAsync()
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

        public Task<List<SelectListItem>> GetTrainersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Receptionist?> Update(int Id, Receptionist model)
        {
            throw new NotImplementedException();
        }
    }
}
