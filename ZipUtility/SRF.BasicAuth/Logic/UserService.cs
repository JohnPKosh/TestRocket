using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SRF.BasicAuth.Models;

namespace SRF.BasicAuth.Logic
{
  public interface IUserService
  {
    Task<UserModel> Authenticate(string username, string password);
    Task<IEnumerable<UserModel>> GetAll();
  }

  public class UserService : IUserService
  {
    // users hardcoded for simplicity, store in a db with hashed passwords in production applications
    private List<UserModel> _users = new List<UserModel>
    {
        new UserModel { Id = 1, FirstName = "Test", LastName = "User", Username = "stanleyjobson", Password = "swordfish" }
    };

    public async Task<UserModel> Authenticate(string username, string password)
    {
      var user = await Task.Run(() => _users.SingleOrDefault(x => x.Username == username && x.Password == password));

      // return null if user not found
      if (user == null)
        return null;

      // authentication successful so return user details without password
      return user.WithoutPassword();
    }

    public async Task<IEnumerable<UserModel>> GetAll()
    {
      return await Task.Run(() => _users.WithoutPasswords());
    }
  }
}