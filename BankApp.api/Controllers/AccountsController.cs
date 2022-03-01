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
        
        public AccountsController(BankDbContext _db, IAccountService _accountService, IMapper _mapper )
        {
            this.mapper = _mapper;
            this.db = _db;
            this.accountService = _accountService;
        }
        [HttpGet]
        public async Task<ActionResult<List<DisplayAccountDto>>> Get()
        {
            
            var accList = await db.Accounts.ToListAsync();
            var accdto = mapper.Map<IEnumerable<DisplayAccountDto>>(accList);
            return Ok(accdto);
        }

        [HttpGet("{id}")]
        public ActionResult<DisplayAccountDto> GetAccount(string id,string password )
        {
            id = id.Trim();
            password = password.Trim();
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
                return NotFound("Enter a Valid AccountId");
            }
        }

        
        [HttpPost("login"),AllowAnonymous]
        public ActionResult<string> LoginAccount(LoginDto loginDto)
        {
            loginDto.accountId = loginDto.accountId.Trim();
            loginDto.password = loginDto.password.Trim();
            try
            {   if (accountService.AccountValidator(loginDto.accountId, loginDto.password))
                {
                    var account = accountService.FindAccount(loginDto.accountId);
                    string token = accountService.CreateToken(account);
                    return Ok(token);
                }
                else
                {
                    return BadRequest("Invalid accountId or Password");
                }
            }
            catch (InvalidId)
            {
                return BadRequest("Invalid accountId or password");
            }
            catch(InvalidBankId)
            {
                return BadRequest("Enter a valid BankId");
            }
        }

        [HttpPut("Deposit")]
        public ActionResult<DisplayAccountDto> Deposit(DepositAmountDto depositDto)
        {
            
            if(!ModelState.IsValid)
            {
                return BadRequest("please check the data enter");
            }
            
            depositDto.accountId = depositDto.accountId.Trim();
            depositDto.currencyCode = depositDto.currencyCode.Trim();
            depositDto.password = depositDto.password.Trim();
            if (User.FindFirstValue("accountId") != depositDto.accountId)
            {
                return Unauthorized("You aren't allowed to deposit or access this account");
            }
            float amount = 0;
            try
            {
                amount=Convert.ToSingle(depositDto.amount);
                if (amount < 0)
                {
                    return BadRequest("Enter valid amount");
                }
            }
            catch
            {
                
                return BadRequest("Enter Valid Amount");
            }
            try
            {
                
                float balance = accountService.Deposit(amount, depositDto.accountId, depositDto.password, depositDto.currencyCode);
                var account = accountService.FindAccount(depositDto.accountId);
                return Ok(mapper.Map<DisplayAccountDto>(account));
            }
            catch(InvalidId)
            {
                return BadRequest("Invalid accountId or Password");
            }
            catch(InvalidPin)
            {
                return BadRequest("Invalid accountId or Password");
            }
            catch (InvalidCurrencyCode)
            {
                return BadRequest("This currency is not accepted by the bank");
            }
        }

        [HttpPut("Withdrawl")]
        public ActionResult<DisplayAccountDto> Withdrawl(WithDrawlDto withDrawlDto)
        {
            withDrawlDto.accountId = withDrawlDto.accountId.Trim();
            withDrawlDto.password = withDrawlDto.password.Trim();
            if (User.FindFirstValue("accountId") != withDrawlDto.accountId)
            {
                return Unauthorized("You aren't allowed to withdrawl or access this account");
            }
            float amount = 0;
            try
            {
                amount = Convert.ToSingle(withDrawlDto.amount);
                if (amount < 0)
                {
                    return BadRequest("Enter valid amount");
                }
            }
            catch
            {

                return BadRequest("Enter Valid Amount");
            }
            try
            {
                float balance = accountService.Withdrawl(amount, withDrawlDto.accountId, withDrawlDto.password);
                var account = accountService.FindAccount(withDrawlDto.accountId);

                return Ok(mapper.Map<DisplayAccountDto>(account));
            }
            catch(InvalidCurrencyCode)
            {
                return BadRequest("Please check the currecy code");
            }
            catch(NotEnoughBalance)
            {
                return BadRequest("Your account doesn't have enough balance");
            }
            catch(InvalidId)
            {
                return BadRequest("Invalid accountId or Password");
            }
            catch(InvalidPin)
            {
                return BadRequest("Invalid accountId or Password");
            }
        }

        [HttpPost("Transfer")]
        public  ActionResult<GetTransactionDto> Transfer (TransferAmountDto transferDto)
        {
            transferDto.currencycode = transferDto.currencycode.Trim();
            transferDto.reciverAccountId = transferDto.reciverAccountId.Trim();
            transferDto.senderAccountId = transferDto.senderAccountId.Trim();
            transferDto.senderPassword = transferDto.senderPassword.Trim();
            transferDto.transactionservice = transferDto.transactionservice.ToLower().Trim();
            TransactionService transactionService;
            switch (transferDto.transactionservice)
            {
                case "imps" :
                    transactionService = TransactionService.IMPS;
                    break;
                case "rtgs":
                    transactionService = TransactionService.RTGS;
                    break;
                default:
                    return BadRequest("Enter valid Trasnaction Service");
            }
            float amount = 0;
            try
            {
                amount = Convert.ToSingle(transferDto.amount);
                if (amount < 0)
                {
                    return BadRequest("Enter valid amount");
                }
            }
            catch
            {

                return BadRequest("Enter Valid Amount");
            }
            if (User.FindFirstValue("accountId") != transferDto.senderAccountId)
            {
                return Unauthorized("You aren't allowed to access this account");
            }
            try
            {
                string transactionId = accountService.Transfer(transferDto.senderAccountId, transferDto.reciverAccountId, transferDto.senderPassword, amount, transactionService);
                Transaction transaction = accountService.FindTransaction(transactionId);
                if (transaction != null)
                {
                    var getTransactionDto = mapper.Map<GetTransactionDto>(transaction);
                    if((int)transaction.type==0)
                    {
                        getTransactionDto.type = "Self Debited";
                    }
                    else if ((int)transaction.type == 1)
                    {
                        getTransactionDto.type = "Self Credited";
                    }
                    else if ((int)transaction.type == 2)
                    {
                        getTransactionDto.type = "Transfer";
                    }
                    return Ok(mapper.Map<GetTransactionDto>(transaction));
                }
                else
                    return BadRequest("Enter valid TransactionId");
            }
            catch (NotEnoughBalance)
            {
                return BadRequest("the Account doesn't have enough Balance");
            }
            catch (InvalidReceiver)
            {
                return BadRequest("Enter a Valid Receiver Id");
            }
            catch (InvalidId)
            { return BadRequest("Invalid accountId or Password"); }
            
        }

        [HttpGet("GetTransactions/{accountId}")]
        public ActionResult<IEnumerable<GetTransactionDto>> GetTranactions(string accountId)
        {
            if (User.FindFirstValue("accountId") != accountId)
            {
                return Unauthorized("You aren't allowed to view or acess this account's transactions");
            }
            try
            {
                var transactionList = accountService.GetTransaction(accountId);
                List<GetTransactionDto> transactionListDto = new();
                foreach (var i in transactionList)
                {
                    transactionListDto.Add(new GetTransactionDto
                    {
                        transactionId = i.Item6,
                        amount = i.Item1,
                        sourceAccountId = i.Item2,
                        receiveraccountId = i.Item3,
                        on = i.Item5,
                        type = i.Item4 == 0 ? "Self Debited" : i.Item4 == 1 ? "Self Credited" : i.Item3==null ? "debit - Transfer" :"credit - Transfer",
                        
                    }); ;
                }
                return transactionListDto;
            }
            catch (InvalidId)
            {
                return BadRequest("Ivalid Id");
            }
            
        }
    }
}