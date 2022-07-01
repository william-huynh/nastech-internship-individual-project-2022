using ClothesShop.SharedVMs;
using ClothesShop.SharedVMs.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ClothesShop.API.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly IList<Role> _roles;

        public AuthorizeAttribute(params Role[] roles)
        {
            _roles = roles ?? new Role[] { };
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Skip authoriztion if action is decorated with [AllowAnonymous] attribute
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous) return;

            // Authorization
            var user = (UserDto)context.HttpContext.Items["User"];

            if (user == null || _roles.Any() && !_roles.Contains(user.Role))
                // Not logged in
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(
                    new
                    {
                        controller = "Home",
                        action = "Error",
                    }));
                //context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
    }
}
