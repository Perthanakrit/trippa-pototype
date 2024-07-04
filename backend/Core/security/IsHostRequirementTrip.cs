using Core.Interface.Infrastructure.Database;
using Core.Interface.security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Core.security
{
    public class IsHostRequirementTrip : IAuthorizationRequirement
    {
    }

    public class IsHostRequirementTripHandler : AuthorizationHandler<IsHostRequirementTrip>
    {
        private readonly IUserAccessor _userAccessor;
        private readonly IAuthRespository _authRespo;
        private readonly IHttpContextAccessor _httpContext;

        public IsHostRequirementTripHandler(IUserAccessor userAccessor, IAuthRespository authRespo, IHttpContextAccessor httpContext)
        {
            _userAccessor = userAccessor;
            _authRespo = authRespo;
            _httpContext = httpContext;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsHostRequirementTrip requirement)
        {
            string userId = _userAccessor.GetUserId();

            if (userId == null) return Task.CompletedTask;

            Guid tripId = Guid.Parse(_httpContext.HttpContext?.Request.RouteValues.SingleOrDefault(x => x.Key == "id").Value?.ToString());

            bool hostInTrip = _authRespo.IsHostInCommuTrip(userId, tripId).Result;

            if (hostInTrip)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}