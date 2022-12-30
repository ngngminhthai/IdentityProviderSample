using BookStore.Data.Entities;
using System.Linq;

namespace BookStore.Data.Repositories
{
    public class UserRepository
    {
        private readonly BookStoreDBContext _context;

        public UserRepository(BookStoreDBContext context)
        {
            _context = context;
        }

        public User GetUserWithUsernameAndPassword(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Name == username && u.PassWord == password);
            return user;
        }
    }
}
