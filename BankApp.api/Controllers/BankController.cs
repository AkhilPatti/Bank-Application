using AutoMapper;
using BankApp.api.DTOs.BankDtos;
using BankApp.api.Dtos.AccountDtos;
using BankApp.Models.Exceptions;
using BankApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BankApp.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BankApp.api.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "staff")]
    
    public class BankController : ControllerBase
    {

        
        private readonly IMapper mapper;
        private IAccountService accountService;
        
        public IBankService bankService;
        public BankController( IAccountService _accountService, IMapper _mapper, IBankService _bankService)
        {
            this.mapper = _mapper;
            this.accountService = _accountService;
            
            this.bankService = _bankService;
        }
        [HttpPost("Login"),AllowAnonymous]
        public ActionResult<string> StaffLogin(StaffLoginDto loginDto)
        {
            loginDto.staffId = loginDto.staffId.Trim();
            loginDto.password = loginDto.password.Trim();
            try
            {

                if (bankService.AuthenticateBankStaff(loginDto.staffId, loginDto.password))
                {    
                    var token = bankService.CreateToken(loginDto.staffId);
                 
                 
                 return Ok(token);
                }
                else
                    return BadRequest("Enter a Valid Password");
            }
            catch (InvalidStaff)
            {
                return BadRequest("Enter a Valid StaffId ");
            }
            
        }
        [HttpPost("CreateAccount")]
        public ActionResult<string> CreateAccount(CreateAccountDto accountDto)
        {
            
         if (User.FindFirstValue("bankId")!=accountDto.bankId)
            {
                return Unauthorized("You are unAuthorized to create an Account in this bank");
            }
            try
            {
                string accountId = bankService.CreateAccount(accountDto.accountHolderName, accountDto.password, accountDto.phoneNumber, accountDto.bankId);
                Account account = accountService.FindAccount(accountId);
                var token = accountService.CreateToken(account);
                return Ok(accountId);
            }
            catch (InvalidBankId)
            {
                return BadRequest("Enter a Valid BankId");
            }
        }

        [HttpDelete("Delete")]
        public ActionResult<string> DeleteAccount(string accountId)
        {
            
            try
            {
                if (bankService.DeleteAccount(accountId, User.FindFirstValue("bankId")))
                    return Ok("Deleted");
            }
            catch(AccountNotFound)
            {
                NotFound("there exists no account with given accountId");
            }
            catch(InvalidBankId)
            {
                return Unauthorized("You are not allowed to delete this account");
            }
                return BadRequest("Check your details");
        }
        [HttpPut("UpdateSameAccountCharges")]
        public ActionResult<GetBankDto> UpdateSameAccountCharges(string bankId, UpdateSameAccountChargesDto updateChargesDto)
        {
            bankId = bankId.Trim();
            if (User.FindFirstValue("bankId") != bankId)
            {
                return Unauthorized();
            }
            try
            {
                bankService.UpdateSameAccountCharges(updateChargesDto.newSameAccountImpsCharge, updateChargesDto.newSameAccountRtgsCharge, bankId);
                var bank = bankService.FindBank(bankId);
                return mapper.Map<GetBankDto>(bank);
            }
            catch (InvalidBankId)
            {
                return BadRequest("Enter a Valid BankId");
            }
        }
        [HttpPut("UpdateOtherAccountCharge")]
        public ActionResult<GetBankDto> UpdateOtherAccountCharges(string bankId, UpdateOtherAccountChargesDto updateChargesDto)
        {
            bankId = bankId.Trim();
            if (User.FindFirstValue("bankId") != bankId)
            {
                return Unauthorized();
            }
            try
            {
                bankService.UpdateSameAccountCharges(updateChargesDto.newOtherAccountImpsCharge, updateChargesDto.newOtherAccountRtgsCharge, bankId);
                var bank = bankService.FindBank(bankId);
                return mapper.Map<GetBankDto>(bank);
            }
            catch(InvalidBankId)
            {
                return BadRequest("Enter a Valid BankId");
            }
        }

       
    }
}
