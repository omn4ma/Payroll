using Payroll.Domain.Models;
using System;
using System.Collections.Generic;

namespace Payroll.WebApplication.Models
{
    public class PersonView
    {
        public int Id { get; set; }
        public DateTime DateOfEmployment { get; set; }
        public Position Position { get; set; }
        public int Rate { get; set; }
        public List<PersonView> Staff { get; set; }
        public string Name { get; set; }
    }
}
