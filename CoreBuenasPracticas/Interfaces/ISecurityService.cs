using CoreBuenasPracticas.Entities;
using System.Threading.Tasks;

namespace CoreBuenasPracticas.Interfaces
{
    public interface ISecurityService
    {
        Task<Security> GetLoginByCredentials(UserLogin userLogin);
        Task RegisterUser(Security userLogin);
    }
}