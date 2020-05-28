using Payroll.Domain.Models;
using System.Collections.Generic;

namespace Payroll.Domain.Repositories
{
    public interface IPersonRepository
    {
        Person GetPerson(int id);
        List<Person> GetGraph();
        int Save(Person person);
    }
}
