using Moq;
using NUnit.Framework;
using Payroll.Domain.Models;
using Payroll.Domain.Repositories;
using Payroll.Domain.Services;
using Payroll.Tests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    public class AccountingCalculatorTests
    {
        [Test]
        [TestCaseSource(typeof(TestsDataFactory), nameof(TestsDataFactory.OnePersonByDateCases))]
        public void CalculateSalary_ForOnePerson_ChechEquals(Person person, DateTime calculationDate, decimal expectedSalary)
        {
            var calculator = GetCalculator(person);

            var salary = calculator.CalculateSalaryFor(person.Id, calculationDate);

            Assert.AreEqual(expectedSalary, salary);
        }

        [Test]
        [TestCaseSource(typeof(TestsDataFactory), nameof(TestsDataFactory.BonusForExpirienceLimits))]
        public void CalculateSalary_BonusForExpirienceLimits_ChechEquals(Person person, DateTime calculationDate, decimal expectedSalary)
        {
            var calculator = GetCalculator(person);

            var salary = calculator.CalculateSalaryFor(person.Id, calculationDate);

            Assert.AreEqual(expectedSalary, salary);
        }

        [Test]
        [TestCaseSource(typeof(TestsDataFactory), nameof(TestsDataFactory.WithMinStaffCases))]
        public void CalculateSalary_WithMinStaffCases_ChechEquals(Person person, DateTime calculationDate, decimal expectedSalary)
        {
            var calculator = GetCalculator(person);

            var salary = calculator.CalculateSalaryFor(person.Id, calculationDate);

            Assert.AreEqual(expectedSalary, salary);
        }

        [Test]
        [TestCaseSource(typeof(TestsDataFactory), nameof(TestsDataFactory.CalculateTotalCases))]
        public void CalculateSalary_TotalCases_ChechEquals(List<Person> persons, DateTime calculationDate, decimal expectedSalary)
        {
            var calculator = GetCalculator(persons);

            var salary = calculator.CalculateTotalSalary(calculationDate);

            Assert.AreEqual(expectedSalary, salary);
        }

        private static AccountingCalculator GetCalculator(Person person)
        {
            var repository = new Mock<IPersonRepository>();
            repository.Setup(x => x.GetPerson(It.IsAny<int>())).Returns(person);
            var calculator = new AccountingCalculator(repository.Object);
            return calculator;
        }

        private static AccountingCalculator GetCalculator(List<Person> persons)
        {
            var repository = new Mock<IPersonRepository>();
            repository.Setup(x => x.GetGraph()).Returns(persons);
            var calculator = new AccountingCalculator(repository.Object);
            return calculator;
        }
    }

    public class TestsDataFactory
    {
        public static IEnumerable OnePersonByDateCases
        {
            get
            {
                yield return new TestCaseData(PersonBuilder.Create(Position.Employee, 1), DateTime.Today, 103m);
                yield return new TestCaseData(PersonBuilder.Create(Position.Employee, 2), DateTime.Today, 106m);
                yield return new TestCaseData(PersonBuilder.Create(Position.Employee, 1), DateTime.Today.AddDays(-1), 100m);
                yield return new TestCaseData(PersonBuilder.Create(Position.Employee, 0), DateTime.Today.AddDays(-1), 0m);
                yield return new TestCaseData(PersonBuilder.Create(Position.Manager, 1), DateTime.Today, 105m);
                yield return new TestCaseData(PersonBuilder.Create(Position.Manager, 2), DateTime.Today, 110m);
                yield return new TestCaseData(PersonBuilder.Create(Position.Manager, 1), DateTime.Today.AddDays(-1), 100m);
                yield return new TestCaseData(PersonBuilder.Create(Position.Manager, 0), DateTime.Today.AddDays(-1), 0m);
                yield return new TestCaseData(PersonBuilder.Create(Position.Sales, 1), DateTime.Today, 101m);
                yield return new TestCaseData(PersonBuilder.Create(Position.Sales, 2), DateTime.Today, 102m);
                yield return new TestCaseData(PersonBuilder.Create(Position.Sales, 1), DateTime.Today.AddDays(-1), 100m);
                yield return new TestCaseData(PersonBuilder.Create(Position.Sales, 0), DateTime.Today.AddDays(-1), 0m);
            }
        }

        public static IEnumerable WithMinStaffCases
        {
            get
            {
                yield return new TestCaseData(
                    PersonBuilder.Create(Position.Manager, 0).AddStaff(Position.Manager, 0)
                    , DateTime.Today, 100.5m);
                yield return new TestCaseData(
                    PersonBuilder.Create(Position.Sales, 0).AddStaff(Position.Employee, 0)
                    , DateTime.Today, 100.3m);
                yield return new TestCaseData(
                    PersonBuilder.Create(Position.Manager, 0).AddStaff(PersonBuilder.Create(Position.Manager, 0).AddStaff(Position.Employee, 0))
                    , DateTime.Today, 100.5025m);
                yield return new TestCaseData(
                    PersonBuilder.Create(Position.Sales, 0).AddStaff(PersonBuilder.Create(Position.Manager, 0).AddStaff(Position.Employee, 0))
                    , DateTime.Today, 100.6015m);
            }
        }

        public static IEnumerable BonusForExpirienceLimits
        {
            get
            {
                yield return new TestCaseData(PersonBuilder.Create(Position.Employee, 100), DateTime.Today, 130m);
                yield return new TestCaseData(PersonBuilder.Create(Position.Manager, 100), DateTime.Today, 140m);
                yield return new TestCaseData(PersonBuilder.Create(Position.Sales, 100), DateTime.Today, 135m);
            }
        }

        public static IEnumerable CalculateTotalCases
        {
            get
            {
                yield return new TestCaseData(new Person[] {
                    PersonBuilder.Create(Position.Employee, 1),
                    PersonBuilder.Create(Position.Employee, 2)
                }.ToList(), DateTime.Today, 209m);
            }
        }
    }
}