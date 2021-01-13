using AspNetIdentity.IdentityWebAPI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Routing;

namespace AspNetIdentity.IdentityWebAPI.Models
{
    public class ModelFactory
    {
        private UrlHelper _urlHelper;
        private ApplicationUserManager _applicationUserManager;

        public ModelFactory(HttpRequestMessage request, ApplicationUserManager applicationUserManager)
        {
            _urlHelper = new UrlHelper(request);
            _applicationUserManager = applicationUserManager;
        }

        public UserReturnModel Create(ApplicationUser applicationUser)
        {
            return new UserReturnModel()
            {
                Url = _urlHelper.Link("GetUserById",new { id = applicationUser.Id }),
                Id =applicationUser.Id,
                UserName = applicationUser.UserName,
                FullName = string.Format("{0} {1}",applicationUser.FirstName, applicationUser.LastName),
                Email = applicationUser.Email,
                EmailConfirmed = applicationUser.EmailConfirmed,
                Level = applicationUser.Level,
                JoinDate = applicationUser.JoinDate,
                Roles = _applicationUserManager.GetRolesAsync(applicationUser.Id).Result,
                Claims = _applicationUserManager.GetClaimsAsync(applicationUser.Id).Result
            };
        }
    }

    public class UserReturnModel
    {
        public string Url { get; set; }
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public int Level { get; set; }
        public DateTime JoinDate { get; set; }
        public IList<string> Roles { get; set; }
        public IList<System.Security.Claims.Claim> Claims { get; set; }
    }
}