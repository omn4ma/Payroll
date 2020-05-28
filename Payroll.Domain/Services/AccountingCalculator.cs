using Payroll.Domain.Models;
using Payroll.Domain.Repositories;
using System;

namespace Payroll.Domain.Services
{
    public class AccountingCalculator
    {
        private readonly IPersonRepository reposity;

        public AccountingCalculator(IPersonRepository reposity)
        {
            this.reposity = reposity;
        }

        public decimal CalculateSalaryFor(int personId, DateTime date)
        {
            var person = reposity.GetPerson(personId);

            var result = CalculateSalary(date, person);

            return result.Salary;
        }

        public decimal CalculateTotalSalary(DateTime calculationDate)
        {
            var persons = reposity.GetGraph();
            var total = 0m;

            foreach (var person in persons)
            {
                var result = CalculateSalary(calculationDate, person);

                total += result.Salary;
                total += result.StaffSalarySum;
                total += result.LowLevelsStaffSalarySum;
            }

            return total;
        }

        private SalaryCalculationResult CalculateSalary(DateTime date, Person person)
        {
            var result = new SalaryCalculationResult();

            if (person.Staff.Count > 0)
            {
                foreach (var collaborator in person.Staff)
                {
                    var staffSalary = CalculateSalary(date, collaborator);

                    result.StaffSalarySum += staffSalary.Salary;
                    result.LowLevelsStaffSalarySum += staffSalary.StaffSalarySum + staffSalary.LowLevelsStaffSalarySum;
                }
            }

            if (person.DateOfEmployment <= date)
            {
                result.Salary = person.Rate 
                    + BountyCalculationRules.GetBonusForWorkExpirience(person.Position, person.DateOfEmployment, person.Rate, date) 
                    + BountyCalculationRules.GetBonusForStaffSalary(person.Position, result.StaffSalarySum, result.StaffSalarySum + result.LowLevelsStaffSalarySum);
            }

            return result;
        }

        protected struct SalaryCalculationResult
        {
            public decimal Salary { get; set; }
            public decimal StaffSalarySum { get; set; }
            public decimal LowLevelsStaffSalarySum { get; set; }
        }
    }
}
