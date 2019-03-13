using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using AftahCloud.Domain.Entities.ApplicationUsers;
using AftahCloudAPI.Infrastructure.SecretsManager;
using AftahCloudAPI.Presentation.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AftahCloudAPI.Presentation.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ManagementController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly SecretManager _secretManager;
        private readonly string trustedClientId;
        private readonly string issuer;

        public ManagementController(UserManager<ApplicationUser> userManager,
          SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILogger<AccountController> logger,
            IConfiguration configuration,
            SecretManager secretManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            _configuration = configuration;
            _secretManager = secretManager;
            issuer = _configuration.GetValue<string>("IdentityServer:Issuer");
            trustedClientId = secretManager.GetSecret("TokenProvider:" + issuer + ":Trusted:ClientId");
        }

        // GET: api/<controller>
        [HttpGet("user/{email}/exists")]
        public async Task<IActionResult> Get(string email)
        {
            if (ValidateJwtToken(Request.Headers.Where(h => h.Key == "Authorization").FirstOrDefault().Value))
            {
                var user = await _userManager.FindByEmailAsync(email);

                if (user != null)
                {
                    return Ok(true);
                }
                else
                {
                    return Ok(false);
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost("user/{email}/reset")]
        public async Task<IActionResult> Post(string email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (ValidateJwtToken(Request.Headers.Where(h => h.Key == "Authorization").FirstOrDefault().Value))
            {
                var user = await _userManager.FindByEmailAsync(email);

                if (user != null)
                {
                    var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var callbackUrl = Url.ResetPasswordCallbackLink(user.Id.ToString(), code, Request.Scheme);
                    //Return the callback URL to reset the password
                    return Json(callbackUrl);
                }
                else
                {
                    return BadRequest("User does not exist");
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost("user/{email}/create")]
        public async Task<IActionResult> PostNewUser(string email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (ValidateJwtToken(Request.Headers.Where(h => h.Key == "Authorization").FirstOrDefault().Value))
            {
                var newUser = new ApplicationUser { UserName = email, Email = email };
                //Password will not work without confirmation via a password reset anyway
                var result = await _userManager.CreateAsync(newUser, "P@ssw0rd12345Bop");

                if (result.Succeeded)
                {
                    _logger.LogInformation("Sending confirmation email to " + email);

                    var user = await _userManager.FindByEmailAsync(email);

                    var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var callbackUrl = Url.ResetPasswordCallbackLink(user.Id.ToString(), code, Request.Scheme);

                    return Json(callbackUrl);
                }
                else
                {
                    return BadRequest(Json(result.Errors));
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        public bool ValidateJwtToken(string token)
        {
            var securityTokens = token.Split(" ");

            if (securityTokens[0] != "Bearer")
            {
                return false;
            }
            else
            {
                var handler = new JwtSecurityTokenHandler();
                var extractedToken = securityTokens[1];
                try
                {

                    var tokenS = handler.ReadToken(extractedToken) as JwtSecurityToken;
                    var expDate = tokenS.ValidTo;

                    //Check expiry date is after the time right now
                    if (expDate > DateTime.UtcNow.AddMinutes(1))
                    {
                        //Validate Issuer
                        if (tokenS.Issuer == issuer)
                        {
                            var clientId = tokenS.Claims.Where(c => c.Type == "client_id").First().Value;
                            var scope = tokenS.Claims.Where(c => c.Type == "scope").First().Value;
                            if (clientId == trustedClientId && scope == "trustedManagement")
                            {
                                return true;
                            }
                        }
                    }

                }
                catch (Exception e)
                {
                    if (e.Message == "IDX10709: JWT is not well formed: '123'.\nThe token needs to be in JWS or JWE Compact Serialization Format. (JWS): 'EncodedHeader.EndcodedPayload.EncodedSignature'. (JWE): 'EncodedProtectedHeader.EncodedEncryptedKey.EncodedInitializationVector.EncodedCiphertext.EncodedAuthenticationTag'.")
                    {
                        return false;
                    }
                    else
                    {
                        _logger.LogError(e.Message, e);
                    }
                }
            }

            return false;

        }
    }
}
