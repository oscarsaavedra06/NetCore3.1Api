using CoreBuenasPracticas.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreBuenasPracticas.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUser(int id);
        Task<IEnumerable<User>> GetUsers();
    }
}