using CoreBuenasPracticas.Enumerations;

namespace CoreBuenasPracticas.Entities
{
    public class Security : BaseEntity
    {
        public string User { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public RolType Role { get; set; }
    }
}
