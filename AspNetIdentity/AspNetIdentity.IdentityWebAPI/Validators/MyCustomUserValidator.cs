using AspNetIdentity.IdentityWebAPI.Infrastructure;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AspNetIdentity.IdentityWebAPI.Validators
{
    public class MyCustomUserValidator : UserValidator<ApplicationUser>
    {
        List<string> AllowedEmailDomains = new List<string>() { "gmail.com", "yahoo.com", "outlook.com", "hotmail.com" };

        public MyCustomUserValidator(ApplicationUserManager applicationUserManager) : base(applicationUserManager)
        {

        }

        public override async Task<IdentityResult> ValidateAsync(ApplicationUser user)
        {
            IdentityResult identityResult = await base.ValidateAsync(user);

            var emailDomain = user.Email.Split('@')[1];

            if (!AllowedEmailDomains.Contains(emailDomain.ToLower()))
            {
                var errors = identityResult.Errors.ToList();

                errors.Add(String.Format("Email domain '{0}' is not allowed", emailDomain));

                identityResult = new IdentityResult(errors);
            }

            return identityResult;
        }
    }
}