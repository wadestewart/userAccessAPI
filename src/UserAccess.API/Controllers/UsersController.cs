using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserAccess.API.Models;
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
        private readonly IUserManager _userManager;
        private readonly IMapper _mapper;

        public UsersController(IUserManager userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        
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
    }
}