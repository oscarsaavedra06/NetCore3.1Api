using CoreBuenasPracticas.Entities;
using CoreBuenasPracticas.Interfaces;
using System.Threading.Tasks;

namespace CoreBuenasPracticas.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SecurityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Security> GetLoginByCredentials(UserLogin userLogin)
        {

            return await _unitOfWork.SecurityRepository.GetLoginByCredentials(userLogin);

        }

        public async Task RegisterUser(Security userLogin)
        {

            await _unitOfWork.SecurityRepository.Add(userLogin);
            await _unitOfWork.saveChangesAsync();

        }
    }
}
