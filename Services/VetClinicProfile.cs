using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.Dto;
using VetClinic.Models;

namespace VetClinic.Services
{
    public class VetClinicProfile : Profile
    {
        public VetClinicProfile()
        {
            CreateMap<Animal, AnimalDto>().ReverseMap();
            CreateMap<Race, RaceDto>().ReverseMap();
            CreateMap<Pet, GetPetDto>().ReverseMap();
            CreateMap<Case, GetCaseDto>().ReverseMap();
            CreateMap<Case, WriteCaseDto>().ReverseMap();
            CreateMap<Control, ControlDto>().ReverseMap();
            CreateMap<Owner, OwnerDto>().ReverseMap();
            CreateMap<Pet, GetPetDto>().ReverseMap();
            CreateMap<PetService, PetServiceDto>().ReverseMap();
            CreateMap<Race, RaceDto>().ReverseMap();  
            CreateMap<Therapy, TherapyDto>().ReverseMap(); 
            CreateMap<Therapy, WriteTherapyDto>().ReverseMap();
            CreateMap<User, GetUserDto>().ReverseMap()
            .ForMember(r => r.Role, o => o.Ignore());
            CreateMap<Vet, VetDto>().ReverseMap();  
            CreateMap<Xray, XrayDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();
            CreateMap<User, UserRegisterDto>().ReverseMap();
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<PetWithOwner, PetWithOwnerDto>().ReverseMap();
            CreateMap<Pet, WritePetDto>().ReverseMap();
            CreateMap<User, WriteUserDto>().ReverseMap();
            CreateMap<CasePetService, GetCasePetServiceDto>().ReverseMap();
            CreateMap<VetCase, GetVetCaseDto>().ReverseMap();
            CreateMap<CasePetService, WriteCasePetServiceDto>().ReverseMap();
            CreateMap<VetCase, WriteVetCaseDto>().ReverseMap();
            CreateMap<PaymentModel, StripeChargeDto>().ReverseMap();
        }      
    }
}
