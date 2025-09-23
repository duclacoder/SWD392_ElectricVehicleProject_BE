using EV.Domain.Entities;

namespace EV.Application.Interfaces.ServiceInterfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}