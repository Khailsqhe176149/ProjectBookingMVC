using ProjectBookingMVC.Models;

namespace ProjectBookingMVC.Repository.RepUser
{
    public class UserRepository : IUserRepository
    {
        public User Login(User user) => UserDAO.Instance.Login(user);

        public User Register(User user) => UserDAO.Instance.Register(user);
    }
}
