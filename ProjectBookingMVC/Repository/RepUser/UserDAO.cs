using ProjectBookingMVC.Models;

namespace ProjectBookingMVC.Repository.RepUser
{
    public class UserDAO
    {
        public static BookingHotelContext context = new BookingHotelContext();
        private static UserDAO instance;
        public static readonly object instanceLock = new object();
        public static UserDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new UserDAO();
                    }
                    return instance;
                }
            }
        }
        internal User Login(User user)
        {
            List<User> users = context.Users.ToList();
            User userLogined = users.SingleOrDefault(x => x.Username == user.Username && x.Password == user.Password);
            if (userLogined == null)
            {
                return null;
            }
            else
            {
                return userLogined;
            }
        }
        internal User Register(User user)
        {
            user.Role = "USER";
            context.Users.Add(user);
            context.SaveChanges();
            return user;
        }

    }
}
