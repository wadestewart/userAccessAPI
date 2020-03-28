using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserAccess.API.Models;
using UserAccess.Business;
using UserAccess.Data.Mongo.Models;

namespace UserAccess.API.Controllers
{
    /// <inheritdoc cref="UserAccessController"/>
    /// <summary>
    ///     An api controller for a non admin user
    /// </summary>
    [Route("api/[controller]")]
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

        [HttpPost]
        [ProducesResponseType(typeof(UserApiModel), 200)]
        public IActionResult CreateUser([FromBody] UserApiModel model)
        {
            // Check if the user model is valid
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            // Map the request model to the data model to pass to the Create
            var userDataModel = _mapper.Map<UserDataModel>(model);
            
            // Create the user
            var result = _userManager?.CreateUser(userDataModel);
            if (result == null) return StatusCode(StatusCodes.Status500InternalServerError);
            
            // Return the newly created user's model
            return Ok(_mapper.Map<UserApiModel>(result));
        }

        [HttpGet]
        [ProducesResponseType(typeof(UserApiModel), 200)]
        public IActionResult Get(string email)
        {
            var user = _userManager.GetUser(email);
            return Ok(_mapper.Map<UserApiModel>(user));
        }
    }
}