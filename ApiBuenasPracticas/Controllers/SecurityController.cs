using ApiBuenasPracticas.Response;
using AutoMapper;
using CoreBuenasPracticas.DTOs;
using CoreBuenasPracticas.Entities;
using CoreBuenasPracticas.Enumerations;
using CoreBuenasPracticas.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiBuenasPracticas.Controllers
{
    [Authorize(Roles =(nameof(RolType.Administrator)))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        public readonly ISecurityService _securityService;
        private readonly IMapper _mapper;
        public SecurityController(ISecurityService securityService, IMapper mapper)
        {
            _securityService = securityService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Security(SecurityDTO securityDTO)
        {
            var security = _mapper.Map<Security>(securityDTO);
            await _securityService.RegisterUser(security);
            securityDTO = _mapper.Map<SecurityDTO>(security);
            var response = new ApiResponse<SecurityDTO>(securityDTO);
            return Ok(response);
        }
    }
}
