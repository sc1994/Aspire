using Aspire.FreeSql.Provider;

namespace AspireAdmin.Core.Users
{
    using Aspire.Users;

    public class User : AuditEntity, IUser
    {
        public string Account { get; set; }
        public string Name { get; set; }
        public string Roles { get; set; }
        public string Password { get; set; }
    }

    public class UserRole : AuditEntity, IUserRole
    {
        public string RoleName { get; set; }
    }
}
