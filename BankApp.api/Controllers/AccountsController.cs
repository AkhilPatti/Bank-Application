using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using BankApp.Models;
using BankApp.Models.Exceptions;
using BankApp.api.Dtos.AccountDtos;
using BankApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Security.Cryptography;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;

namespace BankApp.api.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles="user")]
    public class AccountsController : ControllerBase
    {
        private readonly IMapper mapper;
        private BankDbContext db;
        private IAccountService accountService;
        private IConfiguration configuration;
        public AccountsController(BankDbContext _db, IAccountService _accountService, IMapper _mapper, IConfiguration _configuration)
        {
            this.mapper = _mapper;
            this.db = _db;
            this.accountService = _accountService;
            this.configuration = _configuration;

        }
        [HttpGet]
        public async Task<ActionResult<List<DisplayAccountDto>>> Get()
        {
            var accList = await db.Accounts.ToListAsync();
            var accdto = mapper.Map<IEnumerable<DisplayAccountDto>>(accList);//mapper.Map<Account>(accountDto)
            return Ok(accdto);
        }

        [HttpGet("{id}")]
        public ActionResult<DisplayAccountDto> GetAccount(string id,string password )
        {
            try
            {
                
                if (accountService.AccountValidator(id, password))
                {
                    Account account = accountService.FindAccount(id);
                    var accountDto = mapper.Map<DisplayAccountDto>(account);
                    return Ok(accountDto);
                }
                else
                {
                    return BadRequest("Enter a Valid Password");
                }
  
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost("register"),AllowAnonymous]
        public ActionResult<string> RegisterAccount([FromBody] CreateAccountDto accountDto)
        {
            try
            {
                string accountId = accountService.CreateAccount(accountDto.accountHolderName,accountDto.password, accountDto.phoneNumber, accountDto.bankId);
                Account account = accountService.FindAccount(accountId);
                var token = accountService.CreateToken(account);
                return Ok(token);
            }
            catch(InvalidBankId)
            {
                return BadRequest("Enter a Valid BankId");
            }
        }
        
        [HttpPost("login"),AllowAnonymous]
        public ActionResult<string> LoginAccount(LoginDto loginDto)
        {
            try
            {   if (accountService.AccountValidator(loginDto.accountId, loginDto.password))
                {
                    var account = accountService.FindAccount(loginDto.accountId);
                    string token = accountService.CreateToken(account);
                    return Ok(token);
                }
                else
                {
                    return BadRequest("Enter a Valid Password");
                }
            }
            catch (InvalidId)
            {
                return BadRequest("Enter a Valid UserName");
            }
            catch(InvalidBankId)
            {
                return BadRequest("Enter a valid BankId");
            }
        }

        [HttpPut("Deposit")]
        public ActionResult<DisplayAccountDto>  Deposit(DepositAmountDto depositDto)
        {
            try
            {
                float balance = accountService.Deposit(depositDto.amount, depositDto.accountId, depositDto.password, depositDto.currencyCode);
                var account = accountService.FindAccount(depositDto.accountId);
                return Ok(mapper.Map<DisplayAccountDto>(account));
            }
            catch(InvalidId)
            {
                return BadRequest("Enter a Valid AccountId");
            }
            catch(InvalidPin)
            {
                return BadRequest("Enter a Valid Password");
            }
        }

        [HttpPut("WithDrawl")]
        public ActionResult<DisplayAccountDto> WithDrawl(WithDrawlDto withDrawlDto)
        {
            float balance = accountService.WithDraw(withDrawlDto.amount, withDrawlDto.accountId, withDrawlDto.password );
            var account = accountService.FindAccount(withDrawlDto.accountId);
            return Ok(mapper.Map<DisplayAccountDto>(account));
        }

        [HttpPost("Transfer")]
        public ActionResult<Transaction> Transfer (TransferAmountDto transferDto)
        {
            try
            {
                string transactionId = accountService.Transfer(transferDto.senderAccountId, transferDto.reciverAccountId, transferDto.senderPassword, transferDto.amount, transferDto.transactionservice);
                Transaction transaction = accountService.FindTransaction(transactionId);
                if (transaction != null)
                    return Ok(transaction);
                else
                    return BadRequest("Enter valid Detials");
            }
            catch(NotEnoughBalance)
            {
                return BadRequest("the Account doesn't have enough Balance");
            }
        }

        [HttpGet("GetTransactions/{accountId}")]
        public ActionResult<IEnumerable<GetTransactionDto>> GetTrasnactions(string accountId)
        {
            var transactionList = accountService.GetTransaction(accountId);
            List<GetTransactionDto> transactionListDto = new() ;
            foreach(var i in transactionList)
            {
                transactionListDto.Add(new GetTransactionDto
                {
                    tranactionId = i.Item6,
                    amount = i.Item1,
                    sourceAccountId = i.Item2,
                    receiveraccountId = i.Item3,
                    on = i.Item5,
                    type = (TransactionType)i.Item4
                });
            }
            return transactionListDto;
        }
        /*private string CreateToken(Account account)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role,"user")
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken
            (
                claims: claims,
                signingCredentials: cred,
                expires: DateTime.Now.AddDays(1));
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }*/
    }
}