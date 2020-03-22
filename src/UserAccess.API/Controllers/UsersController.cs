using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace UserAccess.API.Controllers
{
    /// <inheritdoc cref="UserAccessController"/>
    /// <summary>
    /// An api controller for a non admin user
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UsersController : UserAccessController
    {
        // private readonly IUserManager _userManager;
        private readonly IMapper _mapper;

    }
}