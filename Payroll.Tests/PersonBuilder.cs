using Payroll.Domain.Models;
using System;
using System.Collections.Generic;

namespace Payroll.Tests
{
    public static class PersonBuilder
    {
        public static Person Create(Position position, int years)
        {
            return new Person
            {
                Id = 1,
                DateOfEmployment = DateTime.Today.AddYears(-years),
                Staff = new List<Person>(),
                Head = null,
                Position = position,
                Rate = 100
            };
        }

        public static Person AddStaff(this Person head, Position position, int years)
        {
            var person = Create(position, years);
            person.Head = head;
            head.Staff.Add(person);

            return head;
        }

        public static Person AddStaff(this Person head, Person person)
        {
            person.Head = head;
            head.Staff.Add(person);

            return head;
        }
    }
}
