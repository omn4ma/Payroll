using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Payroll.Domain.Models;
using Payroll.WebApplication.Models;
using System;
using System.Collections.Generic;

namespace Payroll.WebApplication.Controllers
{
    [Route("api/[controller]")]
    public class DictionaryController : Controller
    {
        private readonly IMapper mapper;

        public DictionaryController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [HttpGet("[action]")]
        public IEnumerable<PositionView> Position()
        {
            var positions = Enum.GetValues(typeof(Position));
            return mapper.Map<List<PositionView>>(positions);
        }
    }
}
