using System;
using System.Collections.Generic;

namespace Payroll.Domain.Models
{
    public class Person
    {
        public int Id { get; set; }
        public DateTime DateOfEmployment { get; set; }
        public Position Position { get; set; }
        public int Rate { get; set; }
        public Person Head { get; set; }
        public List<Person> Staff { get; set; }

        public override string ToString()
        {
            return Position.ToString();
        }
    }
}
