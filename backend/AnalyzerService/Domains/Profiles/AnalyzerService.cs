using AnalyzerService.Domains.Dtos;
using AnalyzerService.Domains.Model;
using AutoMapper;

namespace AnalyzerService.Domains.Profiles
{
    public class AnalyzerServiceProfile : Profile
    {
        public AnalyzerServiceProfile()
        {
            // Source --> Target
            CreateMap<TransactionEntity, TransactionDto>()
            .ForMember(dto => dto.Amount, map => map.MapFrom(entity => entity.AmountCad))
            .ForMember(dto => dto.Date, map => map.MapFrom(entity => entity.TransactionDate))
            .ForMember(dto => dto.Account, map => map.MapFrom(entity => entity.AccountNumber));

            CreateMap<TransactionDto,TransactionEntity>()
            .ForMember(entity => entity.AmountCad, map => map.MapFrom(dto => dto.Amount))
            .ForMember(entity => entity.TransactionDate, map => map.MapFrom(dto => dto.Date))
            .ForMember(entity => entity.AccountNumber, map => map.MapFrom(dto => dto.Account));
        }
    }
}