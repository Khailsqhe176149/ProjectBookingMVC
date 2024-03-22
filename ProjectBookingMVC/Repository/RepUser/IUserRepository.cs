using ProjectBookingMVC.Models;

namespace ProjectBookingMVC.Repository.RepUser
{
    public interface IUserRepository
    {
        User Register(User user);
        User Login(User user);

    }
}
