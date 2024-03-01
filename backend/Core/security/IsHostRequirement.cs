using Core.Interface.Infrastructure.Database;
using Core.Interface.security;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Core.security
{
    public class IsHostRequirement : IAuthorizationRequirement
    {
    }

    public class IsHostRequirementHandler : AuthorizationHandler<IsHostRequirement>
    {
        private readonly IUserAccessor _userAccessor;
        private readonly IAuthRespository _authRespo;
        private readonly IHttpContextAccessor _httpContext;

        public IsHostRequirementHandler(IUserAccessor userAccessor, IAuthRespository authRespo, IHttpContextAccessor httpContext)
        {
            _userAccessor = userAccessor;
            _authRespo = authRespo;
            _httpContext = httpContext;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsHostRequirement requirement)
        {
            string userId = _userAccessor.GetUserId();

            if (userId == null) return Task.CompletedTask;

            Guid tripId = Guid.Parse(_httpContext.HttpContext?.Request.RouteValues.SingleOrDefault(x => x.Key == "id").Value?.ToString());

            bool hostInCommuTrip = _authRespo.IsHostInCommuTrip(userId, tripId).Result;

            if (hostInCommuTrip)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}