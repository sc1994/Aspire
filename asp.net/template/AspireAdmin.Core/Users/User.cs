using Aspire.FreeSql.Provider;

namespace AspireAdmin.Core.Users
{
    using Aspire.Users;

    public class User : AuditEntity, IUser
    {
        public string Account { get; set; }
        public string Name { get; set; }
        public string[] Roles { get; set; }
        public string Icon { get; set; }
        public string Password { get; set; }
    }
}
