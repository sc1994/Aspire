using Aspire.FreeSql.Provider;

namespace AspireAdmin.Core.Users
{
    using Aspire;

    public class User : AuditEntity, IUserEntity
    {
        public string Account { get; set; }
        public string Name { get; set; }
        public string Roles { get; set; }
        public string Password { get; set; }
    }

    public class UserRole : AuditEntity, IUserRoleEntity
    {
        public string RoleName { get; set; }
    }
}
