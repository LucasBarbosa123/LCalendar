using LCalendar.Dtos;
using LCalendar.Models;
using Microsoft.AspNetCore.Mvc;

namespace LCalendar.Utils;

public static class ClientsUtils
{
    public static ClientInfo ConvertToClientInfo(this Client client)
    {
        var newClientInfo = new ClientInfo(client.Name);
        return newClientInfo;
    }
}