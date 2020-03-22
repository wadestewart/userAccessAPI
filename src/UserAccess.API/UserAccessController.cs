using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace UserAccess.API
{
    /// <inheritdoc />
    /// <summary>
    /// An abstract controller class that contains shared functionality
    /// for all UserAccess controllers
    /// </summary>
    public abstract class UserAccessController : Controller
    {
        #region Properties

        /// <summary>
        /// The ID of th User
        /// </summary>
        protected string UserId => User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).ToList().FirstOrDefault()?.Value;

        #endregion
    }
}