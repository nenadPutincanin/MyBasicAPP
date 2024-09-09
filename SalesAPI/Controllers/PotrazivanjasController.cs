using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesAPI.Data;
using SalesAPI.Data.Entities;

namespace SalesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PotrazivanjasController : ControllerBase
    {
        private readonly IPotrazivanjaRepo _potrazivanjaRepo;
        

        public PotrazivanjasController(IPotrazivanjaRepo potrazivanjaRepo)
        {
            _potrazivanjaRepo = potrazivanjaRepo;
        }

        [HttpGet]

        public IActionResult Get()
        {
            return Ok(_potrazivanjaRepo.GetPotrazivanja());
        }
    }
}
