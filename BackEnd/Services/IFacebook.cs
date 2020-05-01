using BackEnd.Models;
using System.Threading.Tasks;

namespace BackEnd.Services
{
    public interface IFacebook
    {

        Task<FacebookUserData> GetFacebookUser(string ClientToken);
    }
}
