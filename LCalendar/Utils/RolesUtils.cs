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
}