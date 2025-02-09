using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OidcServer.Helpers;
using OidcServer.Models;
using OidcServer.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace OidcServer.Controllers
{
    public class AuthorizeController : Controller
    {
        private readonly IUserRepository _userRepository;

        private readonly ICodeItemRepository _codeItemRepository;

        public AuthorizeController(IUserRepository userRepository, ICodeItemRepository codeItemRepository)
        {
            _userRepository = userRepository;
            _codeItemRepository = codeItemRepository;
        }

        public IActionResult Index(AuthenticationRequestModel authenticationRequestModel)
        {
            return View(authenticationRequestModel);
        }

        [HttpPost]
        public IActionResult Authorize(AuthenticationRequestModel authenticationRequestModel, string username, string[] scopes)
        {
            var user = _userRepository.FindByUsername(username);
            if (user == null)
            {
                ModelState.AddModelError("Username", "Invalid username");
                return View("UserNotFound", authenticationRequestModel);
            }

            string code = GeneratedCode();
            var model = new CodeFlowResponseViewModel
            {
                Code = code,
                State = authenticationRequestModel.State,
                RedirectUri = authenticationRequestModel.RedirectUri
            };
            _codeItemRepository.Add(code, new CodeItem
            {
                AuthenticationRequest = authenticationRequestModel,
                User = username,
                Scopes = scopes
            });

            return View("SubmitForm", model);
        }

        [Route("oauth/token")]
        [HttpPost]
        public IActionResult ReturnTokens(string grant_type, string code, string redirect_uri)
        {
            if (grant_type != "authorization_code") return BadRequest();

            var codeItem = _codeItemRepository.FindByCode(code);
            if (codeItem == null || codeItem.AuthenticationRequest.RedirectUri != redirect_uri)
            {
                return BadRequest();
            }
            _codeItemRepository.Delete(codeItem);

            var jwk = JwkLoader.LoadFromDefault();

            var model = new AuthenticationResponseModel()
            {
                AccessToken = GenerateAccessToken(codeItem.User, string.Join(' ', codeItem.Scopes), codeItem.AuthenticationRequest.ClientId, codeItem.AuthenticationRequest.Nonce, jwk),
                TokenType = "Bearer",
                ExpiresIn = 3600,
                RefreshToken = GeneratedRefreshToken(),
                IdToken = GenerateIdToken(codeItem.User, codeItem.AuthenticationRequest.ClientId, codeItem.AuthenticationRequest.Nonce, jwk)
            };
            return Json(model);
        }

        private static string GeneratedRefreshToken()
        {
            return GeneratedCode();
        }

        static Random random = new();
        private static string GeneratedCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, 32)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private string GenerateAccessToken(string userId, string scope, string audience, string nonce, JsonWebKey jsonWebKey)
        {
            // access_token can be the same as id_token, but here we might have different values for expirySeconds so we use 2 different functions

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, userId),
                new("scope", scope)
            };
            var idToken = JwtGenerator.GenerateJWTToken(
                20 * 60,
                "https://localhost:7000/",
                audience,
                nonce,
                claims,
                jsonWebKey
                );

            return idToken;
        }

        private string GenerateIdToken(string userId, string audience, string nonce, JsonWebKey jsonWebKey)
        {
            // https://openid.net/specs/openid-connect-core-1_0.html#IDToken
            // we can return some claims defined here: https://openid.net/specs/openid-connect-core-1_0.html#StandardClaims
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, userId)
            };

            var idToken = JwtGenerator.GenerateJWTToken(
                20 * 60,
                "https://localhost:7000/",
                audience,
                nonce,
                claims,
                jsonWebKey
                );


            return idToken;
        }
    }
}

