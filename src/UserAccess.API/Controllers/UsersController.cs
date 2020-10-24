using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using UserAccess.API.Models;
using UserAccess.API.Settings;
using UserAccess.Business;
using UserAccess.Data.Mongo.Models;
using UserAccess.Models;

namespace UserAccess.API.Controllers
{
    /// <inheritdoc cref="UserAccessController"/>
    /// <summary>
    ///     An api controller for a non admin user
    /// </summary>
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [Produces("application/json")]
    public class UsersController : UserAccessController
    {
        #region Variables
        
        private readonly IUserManager _userManager;
        private readonly IOptions<TokenSettings> _tokenSettings;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor
        
        public UsersController(IUserManager userManager, IOptions<TokenSettings> tokenSettings, IMapper mapper)
        {
            _userManager = userManager;
            _tokenSettings = tokenSettings;
            _mapper = mapper;
        }

        #endregion

        #region Action Methods

        [HttpGet]
        [ProducesResponseType(typeof(UserApiModel), 200)]
        public IActionResult Get(string email)
        {
            var user = _userManager.GetUser(email);
            return Ok(_mapper.Map<UserApiModel>(user));
        }
        
        [HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("{id}")]
        [ProducesResponseType(typeof(UserApiModel), 200)]
        public IActionResult GetUserById(string id)
        {
            var user = _userManager.GetUserById(id);
            return Ok(_mapper.Map<UserApiModel>(user));
        }
        
        [AllowAnonymous]
        [HttpPost("authenticate")]
        [ProducesResponseType(typeof(UserApiModel), 200)]
        
        public IActionResult Authenticate([FromBody] UserAuthenticateApiModel user)
        {
            // Check if the user model is valid
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var authenticateUser = _userManager.Authenticate(user.Email, user.Password);
            if (authenticateUser == null) return StatusCode(StatusCodes.Status500InternalServerError);
            
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "${authenticateUser.FirstName} ${authenticateUser.LastName}"),
                new Claim(JwtRegisteredClaimNames.Email, authenticateUser.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, authenticateUser.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, authenticateUser.LastName),
                new Claim(JwtRegisteredClaimNames.Sub, authenticateUser.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            claims.AddRange(authenticateUser.Roles.Select(role => new Claim(ClaimTypes.Role, role)));
            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.Value.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_tokenSettings.Value.Issuer, _tokenSettings.Value.Issuer, claims,
                expires: DateTime.Now.AddMinutes(_tokenSettings.Value.Expiration), signingCredentials: credentials);
            var tokenString = new TokenApiModel {Token = new JwtSecurityTokenHandler().WriteToken(token)};

            return Ok(new
            {
                UserId = authenticateUser.Id,
                UserName = authenticateUser.FirstName,
                Token = tokenString
            });
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserApiModel), 200)]
        public IActionResult CreateUser([FromBody] CreateUserApiModel apiModel)
        {
            // Check if the user model is valid
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Create the user
            var result = _userManager?.CreateUser(apiModel.FirstName, apiModel.LastName, apiModel.Email, apiModel.Password);
            if (result == null) return StatusCode(StatusCodes.Status500InternalServerError);
            
            // Return the newly created user's model
            return Ok(new UserApiModel
            {
                Id = result.Id,
                FirstName = result.FirstName,
                LastName = result.LastName,
                Email = result.Email
            });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        public IActionResult Put([FromRoute(Name = "id")] string id, [FromBody] UserUpdateApiModel model)
        {
            // Check if the user model is valid
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            // Add the id to the model
            var submittedUser = _mapper.Map<User>(model);
            submittedUser.Id = id;
            // Update the user
            var result = _userManager.Update(_mapper.Map<User>(submittedUser));
            
            // Return Ok(200)
            return Ok();
        }
        
        [HttpPut("{id}/roles")]
        [ProducesResponseType(200)]
        public IActionResult Put([FromRoute(Name = "id")] string id, [FromBody] List<string> roles)
        {
            // Update the user
            var result = _userManager.UpdateUserRoles(id, roles);
            
            // Return Ok(200)
            return result
                ? StatusCode(StatusCodes.Status204NoContent)
                : StatusCode(StatusCodes.Status500InternalServerError);
        }

        #endregion

    }
}