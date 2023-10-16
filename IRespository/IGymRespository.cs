using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gym_Management.IRespository
{
    public interface IGymRespository<T>
    {
        Task<List<T>> GetAll();
        Task<T?> GetById(int Id);
        Task<T> Create(T model);
        Task<T?> Update(int Id, T model);
        Task<T?> Delete(int Id);
        Task<List<SelectListItem>> GetGendersAsync();
        Task<List<SelectListItem>> GetTrainersAsync();
        Task<List<SelectListItem>> GetMembershipsAsync();
        Task<List<SelectListItem>> GetTimingsAsync();
    }
}
