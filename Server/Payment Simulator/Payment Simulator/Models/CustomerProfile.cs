using AutoMapper;
using System;

namespace Payment_Simulator.Models
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDTO>();
        }
    }
}
