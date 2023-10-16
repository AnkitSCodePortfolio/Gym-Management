
using Gym_Management.Models;
using AutoMapper;

namespace Gym_Management.Mapper
{
    public class GymAutoMapperProfile:Profile
    {
      public GymAutoMapperProfile() 
      {        
          CreateMap<Attendance,AttendanceDTO>().ReverseMap();
          CreateMap<Customer, CustomerDTO>().ReverseMap();
          CreateMap<Login, LoginDTO>().ReverseMap();
          CreateMap<Payment, PaymentDTO>().ReverseMap();
          CreateMap<Receptionist, ReceptionistDTO>().ReverseMap();
          CreateMap<Register, RegisterDTO>().ReverseMap();
          CreateMap<Trainer, TrainerDTO>().ReverseMap();
        
        }
    }
}
