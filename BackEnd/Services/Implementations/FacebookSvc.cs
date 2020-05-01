using BackEnd.Config;
using BackEnd.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using Polly.Timeout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BackEnd.Services.Implementations
{
    internal class FacebookSvc : IFacebook
    {

        private readonly HttpClient _httpClient;
        private readonly ConfigSettings _urls;
        readonly AsyncRetryPolicy<HttpResponseMessage> _httpRetryPolicy;
        readonly AsyncTimeoutPolicy _timeoutPolicy;


        public FacebookSvc(HttpClient httpClient, IOptions<ConfigSettings> config)
        {
            _httpClient = httpClient;
            _urls = config.Value;

            //handle re-tries for http failures
            _httpRetryPolicy =
                 Policy.HandleResult<HttpResponseMessage>(msg => msg.StatusCode == System.Net.HttpStatusCode.GatewayTimeout)
                 .Or<TimeoutRejectedException>()
                 .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            //set timeout policy
            _timeoutPolicy = Policy.TimeoutAsync(25);

        }

        internal async Task<AppAccessToken> GetFacebookAccessToken()
        {
            // Get app access token

            var AccessTokenURL = ConfigSettings.FaceboookOperations.TokenAccessPath(_urls.FacebookBaseURL, _urls.FacebookAppId, _urls.FacebookAppSecret);

            var AccessTokenResponse = await _httpClient.GetStringAsync(AccessTokenURL);

            return JsonConvert.DeserializeObject<AppAccessToken>(AccessTokenResponse);
        }


        internal async Task<UserAccessTokenValidation> FacebookClientTokenValidation(string ClientToken,string AccessToken)
        {
            //validate user token passed by client
            var AccessValidationURL = ConfigSettings.FaceboookOperations.TokenValidationPath(_urls.FacebookBaseURL, ClientToken, AccessToken);

            var ClientTokenValidationResponse = await _httpClient.GetStringAsync(AccessValidationURL);

            return JsonConvert.DeserializeObject<UserAccessTokenValidation>(ClientTokenValidationResponse);

        }


        internal async Task<FacebookUserData> GetFacebookUserData(string ClientToken)
        {
            //Request user data for internal registeration for brand new users
            var UserInfoURL = ConfigSettings.FaceboookOperations.UserInfo(_urls.FacebookBaseURL, ClientToken);


            var userInfoResponse = await _httpClient.GetStringAsync(UserInfoURL);

            return JsonConvert.DeserializeObject<FacebookUserData>(userInfoResponse);

        }

        public async Task<FacebookUserData> GetFacebookUser(string ClientToken)
        {

            try
            {

                // Get app access token 

                var AccessTokenResponseObj = await GetFacebookAccessToken();

                //validate user token passed by client              

                var ClientTokenValidationResponseObj = await FacebookClientTokenValidation(ClientToken, AccessTokenResponseObj.AccessToken);
               
                if (!ClientTokenValidationResponseObj.Data.IsValid)
                {
                    return null;
                }


                //Request user data for internal registeration for brand new users
                var userInfoObj = await GetFacebookUserData(ClientToken);


                return userInfoObj;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
