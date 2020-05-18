using Payroll.Domain.Models;

namespace Payroll.Domain.Repositories
{
    public interface IPersonRepository
    {
        Person GetPerson(int id);
    }
}
