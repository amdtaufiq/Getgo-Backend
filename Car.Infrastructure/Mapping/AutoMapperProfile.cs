using AutoMapper;
using Car.Core.DTOs;
using Car.Core.Entities;

namespace Car.Infrastructure.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Mapping Vehicle
            CreateMap<Vehicle, VehicleResponse>();

            CreateMap<CreateVehicleRequest, Vehicle>();

            CreateMap<UpdateVehicleRequest, Vehicle>();

            //Mapping Transaction
            CreateMap<Transaction, TransactionResponse>();

            CreateMap<CreateTransactionRequest, Transaction>();

            CreateMap<UpdateTransactionRequest, Transaction>();

        }
    }
}
