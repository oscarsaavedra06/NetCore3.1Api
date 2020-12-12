using CoreBuenasPracticas.Entities;
using CoreBuenasPracticas.Interfaces;
using InfraestructureBuenasPracticas.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace InfraestructureBuenasPracticas.Repositories
{
    public class SecurityRepository : BaseRepository<Security>, ISecurityRepository
    {
        public SecurityRepository(SocialMediaContext context) : base(context)
        {

        }

        public async Task<Security> GetLoginByCredentials(UserLogin login)
        {
            return await _entities.FirstOrDefaultAsync(x => x.User == login.User && x.Password == login.Password);
        }
    }
}
