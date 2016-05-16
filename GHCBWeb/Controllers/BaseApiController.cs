using GHCBWeb.Data;
using GHCBWeb.Data.Entities;
using GHCBWeb.Infrastructure;
using GHCBWeb.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GHCBWeb.Controllers
{
    //public class BaseApiController<T, TModel> : BaseApiController where T : class where TModel : class
    //{
    //    private IGHCBRepository<T> repo;    //= new GHCBRepository();  // 

    //    Func<T, TModel> selector;

    //    public BaseApiController(IGHCBRepository<T> repo)
    //    {
    //        this.repo = repo;
    //        this.repo.setDbContext(this.AppDbContext);
    //    }

    //    protected IGHCBRepository<T> TheRepository
    //    {
    //        get
    //        {
    //            return repo;
    //        }
    //    }

    //    GET: api/boards
    //    public IEnumerable<TModel> Get()
    //    {
    //        return TheRepository.GetAll().ToList().Select(selector);

    //    }

    //    public IHttpActionResult Get(int id)
    //    {
    //        try
    //        {
    //            T entity = TheRepository.GetById(id);
    //            if (entity != null)
    //            {
    //                return Ok(entity);
    //            }
    //            else
    //            {
    //                return NotFound();
    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            return BadRequest();
    //        }
    //    }







    //}
    public class BaseApiController: ApiController
    {
        private IGHCBRepository repo;    //= new GHCBRepository();  // 


        public BaseApiController(IGHCBRepository repo)
        {
            this.repo = repo;
         
        }

        protected IGHCBRepository TheRepository
        {
            get
            {
                return repo;
            }
        }

        private ModelFactory _modelFactory;

        private ApplicationDbContext _AppDbContext = null;
        private ApplicationUserManager _AppUserManager = null;
        private ApplicationRoleManager _AppRoleManager = null;

        protected ApplicationDbContext AppDbContext
        {
            get
            {
                return _AppDbContext ?? Request.GetOwinContext().Get<ApplicationDbContext>();
            }
        }
        protected ApplicationRoleManager AppRoleManager
        {
            get
            {
                return _AppRoleManager ?? Request.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
        }
        protected ApplicationUserManager AppUserManager
        {
            get
            {
                if (_AppUserManager == null)
                {
                    var context = Request.GetOwinContext();
                    var res = context.GetUserManager<ApplicationUserManager>();

                    _AppUserManager = res;
                }

                return _AppUserManager;

                //return _AppUserManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        public BaseApiController()
        {
        }

        protected ModelFactory TheModelFactory
        {
            get
            {
                if (_modelFactory == null)
                {
                    _modelFactory = new ModelFactory(this.Request, this.AppUserManager);
                }
                return _modelFactory;
            }
        }

    

        protected IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}
