using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BackEnd.Models;
using BackEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Swashbuckle.AspNetCore.Filters;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FacebookController : ControllerBase
    {
        //inject the service
        private readonly IBackEnd _svc; 

      
        public FacebookController(IBackEnd svc)
        { 
            _svc = svc; 
        }

        /// <summary>
        ///Function to get facebook user information connecting to facebook
        /// </summary>
        [HttpGet]
        [HttpGet]
        public async Task<FacebookUserData> Get(string Token)
        {

            return await _svc.GetFacebookUser(Token);
        }
      
    }
    /// <summary>
    ///swagger description
    /// </summary> 
    public class FacebookExample : IExamplesProvider<FaceBookLogin>
    {
        public FaceBookLogin GetExamples()
        {
            return new FaceBookLogin { AccessToken = "someInvalidToken" };
        }
    }
} 