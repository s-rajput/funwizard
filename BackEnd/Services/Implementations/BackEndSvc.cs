using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using Polly.Timeout;
using System.Net.Http;
using BackEnd.Config;
using BackEnd.Models;
using System.Text.RegularExpressions;

//for unit testing secure content
[assembly: System.Runtime.CompilerServices.InternalsVisibleToAttribute("BackEnd.Tests")]

namespace BackEnd.Services.Implementations
{
    public class BackEndSvc : IBackEnd
    {
        IFacebook _facebook;
        private readonly ConfigSettings _urls;
        readonly AsyncRetryPolicy<HttpResponseMessage> _httpRetryPolicy;
        readonly AsyncTimeoutPolicy _timeoutPolicy;


        public BackEndSvc(IFacebook facebook)
        {
            _facebook = facebook;

        }


        public async Task<FacebookUserData> GetFacebookUser(string ClientToken)
        {

            try
            {
                return await _facebook.GetFacebookUser(ClientToken);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public int GetLargestNumber(int[] Numbers) => Numbers.Max();

        public int GetSmallestNumber(int[] Numbers) => Numbers.Min();

        public string RemoveDupesPreferred(string GivenString)
         =>   String.Join("", GivenString.Distinct()); 

        public string RemoveDupesTwo(string GivenString) 
        => new String(GivenString.ToCharArray().Distinct().ToArray()); 
         

        public string RemoveDupesLeastPreffered(string GivenString)
        {
            // --- Removes duplicate chars using string concats. ---
            // Store encountered letters in this string.
            string table = "";

            // Store the result in this string.
            string result = "";

            // Loop over each character.
            foreach (char value in GivenString)
            {
                // See if character is in the table.
                if (table.IndexOf(value) == -1)
                {
                    // Append to the table and the result.
                    table += value;
                    result += value;
                }
            }
            return result;
        }

        public bool AnagramCheck(string FirstString, string SecondStrings)
        {
            var charsA = FirstString.ToLower().Where(c => c >= 'a' && c <= 'z').ToArray();
            var charsB = SecondStrings.ToLower().Where(c => c >= 'a' && c <= 'z').ToArray();
            return charsA.OrderBy(c => c).SequenceEqual(charsB.OrderBy(c => c));
        }

        public bool IsValidAUMobileNumber(string MobileNumber)
        {
            var str = MobileNumber.StartsWith("00") ? 
                                MobileNumber.Substring(2) : MobileNumber.StartsWith("0") ? 
                                            MobileNumber.Substring(1) : MobileNumber;
            
            var Matches = (str.Length == 9 || str.Length == 11) ? 
                                (str.Length == 9 ? Regex.IsMatch(str, @"^[4]\d{8}$") : 
                                    Regex.IsMatch(str, @"^[6]\d{10}$")) : false;
            
            return Matches;
        }

    }
}
