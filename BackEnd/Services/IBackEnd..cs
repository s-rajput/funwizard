
using BackEnd.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BackEnd.Services
{
    public interface IBackEnd
    {
        Task<FacebookUserData> GetFacebookUser(string ClientToken);

        int GetLargestNumber(int[] Numbers);

        int GetSmallestNumber(int[] Numbers);

        string RemoveDupesPreferred(string GivenString);

        string RemoveDupesTwo(string GivenString);
        string RemoveDupesLeastPreffered(string GivenString);

        bool AnagramCheck(string FirstString, string SecondStrings);

        bool IsValidAUMobileNumber(string MobileNumber);

    }
}
