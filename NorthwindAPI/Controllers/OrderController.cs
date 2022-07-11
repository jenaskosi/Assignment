using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NorthwindAPI.Model;
using NorthwindAPI.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IDapper _dapper;
      
        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger, IDapper dapper)
        {
            _logger = logger;
            _dapper = dapper;
        }

        [HttpGet(nameof(GetOrderSummary))]
        public Task<List<OrderModel>> GetOrderSummary(string CustomerId, int? EmployeeId, DateTime StartDate, DateTime EndDate)
        {
            var dbparams = new DynamicParameters();
            if (EmployeeId.HasValue)
                dbparams.Add("@EmployeeId", EmployeeId.Value);
            else
                dbparams.Add("@EmployeeId", null);

            if (string.IsNullOrEmpty(CustomerId))
                dbparams.Add("@CustomerId", CustomerId);
            else
                dbparams.Add("@CustomerId", null);

            dbparams.Add("@StartDate", StartDate);
            dbparams.Add("@EndDate", EndDate);

            var result = Task.FromResult(_dapper.GetAll<OrderModel>("pr_GetOrderSummary", dbparams,
                   commandType: CommandType.StoredProcedure));

            return result;

        }
    }
}
