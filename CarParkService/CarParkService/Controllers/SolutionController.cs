using CarPark.Service;
using CarParkService.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarParkService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolutionController : ControllerBase
    {
        private readonly CarPack carpack;

        public SolutionController(CarPack _carpack)
        {
            carpack = _carpack;
        }

        [HttpPost("GetAmount")]
        public async Task<int> Solution(string E, string L)
        {
            int result = (int)(await carpack.GetAmountToPay(E, L));

            return result;
        }

        [HttpGet("GetAlltickets")]
        public List<PackingTicket> AllTickets()
        {
            return carpack.GetAlltickets();
        }
    }
}
