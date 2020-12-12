using CoreBuenasPracticas.Entities;
using System.Threading.Tasks;

namespace CoreBuenasPracticas.Interfaces
{
    public interface ISecurityRepository : IRepository<Security>
    {
        Task<Security> GetLoginByCredentials(UserLogin login);
    }
}