using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApp.Services;

namespace BankApp.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    
    public class BankController : ControllerBase
    {
        BankDbContext db;
        public BankController(BankDbContext db)
        {
            this.db = db;
        }
        [HttpGet]
        public int Get()
        {
            return db.Banks.Count();
        }
    }
}
