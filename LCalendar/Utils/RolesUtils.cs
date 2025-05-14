using LCalendar.Dtos;
using LCalendar.Models;

namespace LCalendar.Utils;

public static class RolesUtils
{
    public static RoleInfo ConvertToRoleInfo(this Role role)
    {
        var roleInfo = new RoleInfo();
        roleInfo.Name = role.Name;

        return roleInfo;
    }
    
    public static string GetRoleNameById(int id, AppDbContext dbContext)
    {
        var role =  dbContext.Roles.Where(r => r.Id == id).FirstOrDefault();
        return (role?.Name ?? "");
    }
}