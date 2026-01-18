using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Application.Static
{
    public class StaticUserRole
    {
        public static readonly StaticUserRole Admin = new(1, "Admin", "Has full access");
        public static readonly StaticUserRole User = new(2, "User", "Has limited access");
        public static readonly StaticUserRole Manager = new(3, "Manager", "Has limited access");

        public int Id { get; }
        public string Name { get; }
        public string Description { get; }

        private StaticUserRole(int id, string name, string description)
        {
            Id = id; Name = name; Description = description;
        }
        public static StaticUserRole? FromId(int id) =>
        List().SingleOrDefault(r => r.Id == id);

      
        public static StaticUserRole? FromName(string name) =>
            List().SingleOrDefault(r => string.Equals(r.Name, name, StringComparison.OrdinalIgnoreCase));


        public static IEnumerable<StaticUserRole> List() => new[] { Admin, User,Manager };
    }

}
