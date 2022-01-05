using AutoMapper;
using TransactionApplication.Contracts.Requests;
using TransactionApplication.Contracts.Responses;
using TransactionApplication.Domain.Models.Input;
using TransactionApplication.Domain.Models.Output;

namespace TransactionApplication.Api.Mapping
{
    public class ContractDomainMappingProfile : Profile
    {
        public ContractDomainMappingProfile()
        {
            CreateMap<MakeWithdrawalRequest, MakeWithdrawalInput>();
            CreateMap<MakeWithdrawalOutput, MakeWithdrawalResponse>();

            CreateMap<MakeDepositRequest, MakeDepositInput>();
            CreateMap<MakeDepositOutput, MakeDepositResponse>();
        }
    }
}