using AutoMapper;
using BankApp.api.DTOs.BankDtos;
using BankApp.api.Dtos.AccountDtos;
using BankApp.Models.Exceptions;
using BankApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApp.Models;

namespace BankApp.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly IMapper mapper;
        private BankDbContext db;
        private IAccountService accountService;
        private IConfiguration configuration;
        public IBankService bankService;
        public BankController(BankDbContext _db, IAccountService _accountService, IMapper _mapper, IConfiguration _configuration, IBankService _bankService)
        {
            this.mapper = _mapper;
            this.db = _db;
            this.accountService = _accountService;
            this.configuration = _configuration;
            this.bankService = _bankService;
        }
        [HttpGet("{staffId}")]
        public ActionResult StaffLogin(StaffLoginDto loginDto)
        {
            try
            {
                if (bankService.AuthenticateBankStaff(loginDto.staffId, loginDto.password))
                    return Ok("logged in Successfully");
                else
                    return BadRequest("Enter a Valid Password");
            }
            catch (InvalidStaff)
            {
                return BadRequest("Enter a Valid StaffId ");
            }
        }
        [HttpPost("CreateAccount")]
        public ActionResult<string> CreateAccount([FromBody] CreateAccountDto accountDto)
        {
            try
            {
                string accountId = bankService.CreateAccount(accountDto.accountHolderName, accountDto.password, accountDto.phoneNumber, accountDto.bankId);
                Account account = accountService.FindAccount(accountId);
                var token = accountService.CreateToken(account);
                return Ok(token);
            }
            catch (InvalidBankId)
            {
                return BadRequest("Enter a Valid BankId");
            }
        }
        [HttpPut("UpdateSameAccountCharges")]
        public ActionResult<GetBankDto> UpdateSameAccountCharges(string bankId, UpdateSameAccountChargesDto updateChargesDto)
        {
            bankService.UpdateSameAccountCharges(updateChargesDto.newSameAccountImpsCharge, updateChargesDto.newSameAccountRtgsCharge,bankId);
            var bank = bankService.FindBank(bankId);
            return mapper.Map<GetBankDto>(bank);
        }
        [HttpPut("UpdateOtherAccountCharge")]
        public ActionResult<GetBankDto> UpdateOtherAccountCharges(string bankId, UpdateOtherAccountChargesDto updateChargesDto)
        {
            bankService.UpdateSameAccountCharges(updateChargesDto.newOtherAccountImpsCharge, updateChargesDto.newOtherAccountRtgsCharge, bankId);
            var bank = bankService.FindBank(bankId);
            return mapper.Map<GetBankDto>(bank);
        }

        [HttpPut("{transacionId}")]
        public ActionResult<GetTransactionDto> RevertTrasanction (string transactionId,[FromBody]StaffLoginDto loginDto)
        {
            try
            {
                var isreverted = bankService.RevertTransaction(transactionId);
                if (isreverted)
                {
                    return Ok("the transaction is reverted");
                }
            }
            catch(InvalidTransactionId)
            {
                return BadRequest("Enter Valid Transaction Id");
            }
            catch(InvalidId)
            {
                return BadRequest("Enter a Valid Sender Id");
            }
            catch(InvalidReceiver)
            {
                return BadRequest("Enter a Valid Receiver Id");
            }
            return Ok();
        }
    }
}
