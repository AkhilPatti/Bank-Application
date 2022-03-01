using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using BankApp.api.DTOs.BankDtos;
using BankApp.api.Dtos.AccountDtos;
using BankApp.Models;
using System.Threading.Tasks;

namespace BankApp.api.Mappers
{
    public class AccountMapper : Profile
    {
        public AccountMapper()
        {
            CreateMap <Account,CreateAccountDto> ().ReverseMap();
            CreateMap<DisplayAccountDto ,LoginDto>().ReverseMap();
            CreateMap<Account, DisplayAccountDto>().ReverseMap();
            CreateMap<GetBalanceDTO,Account>();
            CreateMap<GetTransactionDto, Transaction>().ReverseMap();
            CreateMap<GetBankDto, Bank>().ReverseMap();
            
        }
    }
}
