using AspNetIdentity.IdentityWebAPI.Infrastructure;
using AspNetIdentity.IdentityWebAPI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AspNetIdentity.IdentityWebAPI.Controllers
{
    public class BaseApiController : ApiController
    {
        private ModelFactory _modelFactory;
        private ApplicationUserManager _applicationUserManager = null;

        protected ApplicationUserManager AppUserManager
        {
            get { return _applicationUserManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
        }

        protected ModelFactory TheModelFactory
        {
            get
            {
                if(_modelFactory == null)
                {
                    _modelFactory = new ModelFactory(this.Request, this.AppUserManager);
                }

                return _modelFactory;
            }
        }

        public BaseApiController()
        {

        }

        protected IHttpActionResult GetErrorResult(IdentityResult identityResult)
        {
            if(identityResult == null)
            {
                return InternalServerError();
            }

            if (!identityResult.Succeeded)
            {
                if(identityResult.Errors != null)
                {
                    foreach(string error in identityResult.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }        
    }
}