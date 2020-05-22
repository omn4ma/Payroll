using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Payroll.Domain.Services;
using System;

namespace Payroll.WebApplication.Controllers
{
    [Route("api/[controller]")]
    public class SalaryController : Controller
    {
        private readonly IMapper mapper;
        private readonly AccountingCalculator calculator;

        public SalaryController(IMapper mapper, AccountingCalculator calculator)
        {
            this.mapper = mapper;
            this.calculator = calculator;
        }

        [HttpGet("[action]")]
        public decimal Calculate(CalculationQuery query)
        {
            var result = calculator.CalculateSalaryFor(query.PersonId, query.Date.DateTime);
            return result;
        }

        public class CalculationQuery
        {
            public int PersonId { get; set; }
            public DateTimeOffset Date { get; set; }
        }
    }
}
