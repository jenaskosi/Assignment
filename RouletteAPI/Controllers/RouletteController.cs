using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RouletteAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RouletteController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };


        private readonly ILogger<RouletteController> _logger;

        public RouletteController(ILogger<RouletteController> logger)
        {
            _logger = logger;
        }

        private readonly IDapper _dapper;

        public RouletteController(IDapper dapper)
        {
            _dapper = dapper;
        }
        [HttpGet(nameof(PlaceBet))]
        public async Task<bool> PlaceBet(int myBet)
        {
            var rng = new Random();
            //the number returned by the wheel
            int generatedNumber = rng.Next(1, 36);

            //compare the bet to the number returned by the wheel and return the results async 
            return await Task.FromResult<bool>(generatedNumber == myBet);

        }


        public IActionResult GetBets()
        {
            var bets = _dapper.GetAll();

            return Ok(bets);
        }

        [HttpPost]
        public IActionResult PlaceBet(Bets bet)
        {
            var placeBet = _dapper.Insert(bet);
            _dapper.SaveChanges();

            return Ok(placeBet);
        }
        [HttpGet]

        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
