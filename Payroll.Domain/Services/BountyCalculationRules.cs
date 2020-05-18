using Payroll.Domain.Models;
using System;

namespace Payroll.Domain.Services
{
    internal static class BountyCalculationRules
    {
        public static decimal GetBonusForStaffSalary(Position position, decimal salaryOfFirstLevelSubordinates, decimal salaryOfAlltLevelsSubordinates)
        {
            switch (position)
            {
                case Position.Employee:
                    return 0m;
                case Position.Manager:
                    return salaryOfFirstLevelSubordinates * GetStaffSalaryRate(position);
                case Position.Sales:
                    return salaryOfAlltLevelsSubordinates * GetStaffSalaryRate(position);
                default:
                    throw new NotImplementedException();
            }
        }

        public static decimal GetBonusForWorkExpirience(Position position, DateTime dateOfEmployment, decimal baseSalary, DateTime calculationDate)
        {
            var yearsWorking = DateTimeHelper.TotalYears(dateOfEmployment, calculationDate);
            var calculatedRate = GetWorkExpirienceRate(position) * yearsWorking;
            var maxRate = GetWorkExpirienceMaxRate(position);

            return baseSalary * Math.Min(calculatedRate, maxRate);
        }

        private static decimal GetWorkExpirienceRate(Position position)
        {
            switch (position)
            {
                case Position.Employee:
                    return 0.03m;
                case Position.Manager:
                    return 0.05m;
                case Position.Sales:
                    return 0.01m;
                default:
                    throw new NotImplementedException();
            }
        }

        private static decimal GetWorkExpirienceMaxRate(Position position)
        {
            switch (position)
            {
                case Position.Employee:
                    return 0.3m;
                case Position.Manager:
                    return 0.4m;
                case Position.Sales:
                    return 0.35m;
                default:
                    throw new NotImplementedException();
            }
        }

        private static decimal GetStaffSalaryRate(Position position)
        {
            switch (position)
            {
                case Position.Manager:
                    return 0.005m;
                case Position.Sales:
                    return 0.003m;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
