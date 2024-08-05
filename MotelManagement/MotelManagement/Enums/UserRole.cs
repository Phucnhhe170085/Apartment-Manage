using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotelManagement.Enums
{
    public enum UserRole
    {
        RENTER,
        OWNER,
        SECURITY,
        ADMIN,
        UNKNOWN
    }
    public class Role
    {
        public int Value { get; set; }

        public string Name { get; set; }

        public static readonly Role Unknown = new Role(0, "Unknown");
        public static readonly Role Renter = new Role(1, "Renter");
        public static readonly Role Owner = new Role(2, "Owner");
        public static readonly Role Security = new Role(3, "Security");
        public static readonly Role Admin = new Role(4, "Admin");

        public Role(int value, string name) { Value = value; Name = name; }

        public static readonly Dictionary<UserRole, Role> Roles = new Dictionary<UserRole, Role>()
        {
            {UserRole.UNKNOWN, Unknown },
            {UserRole.RENTER, Renter },
            {UserRole.OWNER, Owner },
            {UserRole.SECURITY, Security },
            {UserRole.ADMIN, Admin },
        };

        public static Role GetRole(UserRole role)
        {
            return Roles[role];
        }

        public static Role GetRole(int Value)
        {
            foreach (Role role in Roles.Values)
            {
                if(role.Value == Value) return role;
            }
            return Role.Admin;
        }

    }
}
