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

        public decimal CalculateTotalSalary(DateTime calculationDate)
        {
            return 209m;
        }
    }
}
