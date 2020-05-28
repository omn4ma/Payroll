using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Payroll.Domain.Models;
using Payroll.Domain.Repositories;
using Payroll.WebApplication.Models;
using System.Collections.Generic;

namespace Payroll.WebApplication.Controllers
{
    [Route("api/[controller]")]
    public class PersonController : Controller
    {
        private readonly IPersonRepository repository;
        private readonly IMapper mapper;

        public PersonController(IPersonRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet("[action]")]
        public IEnumerable<PersonView> Graph()
        {
            var persons = repository.GetGraph();
            return mapper.Map<List<PersonView>>(persons);
        }

        [HttpPut]
        public int SavePerson(PersonView person)
        {
            var model = mapper.Map<Person>(person);
            var id = repository.Save(model);
            return id;
        }
    }
}
