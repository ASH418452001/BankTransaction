using BankTransaction.DTO.bankTranactionDTO;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using AutoMapper;
using BankTransaction.Model.entities;

namespace BankTransaction.Profiles
{
    public class MappingbankTransactionProfile : Profile
    {

        public MappingbankTransactionProfile()
        {
            CreateMap<bankTransaction, CreatebankTransactionDTO>().ReverseMap();
            CreateMap<bankTransaction, UPDATEbankTransactionDTO>().ReverseMap();
            CreateMap<GETbankTransaction, bankTransaction>().ReverseMap();
        }
    }
}
