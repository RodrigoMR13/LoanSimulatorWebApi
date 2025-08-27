using Application.Dtos.Responses;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Mapping
{
    public class LoanSimulationProfile : Profile
    {
        public LoanSimulationProfile()
        {
            CreateMap<LoanSimulation, GetSimulationByIdResponse>()
                .ForMember(dest => dest.Value, opt =>
                    opt.MapFrom(src => src.Simulations.First().Installments.Sum(i => i.AmortizationValue)))
                .ForMember(dest => dest.Installments, opt =>
                    opt.MapFrom(src => src.Simulations.First().Installments.Count))
                .ForMember(dest => dest.TotalValueInstallments, opt =>
                    opt.MapFrom(src => src.Simulations.First().Installments.Sum(i => i.InstallmentValue)));
        }
    }
}
