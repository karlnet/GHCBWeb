using GHCBWeb.Data;
using GHCBWeb.Data.Entities;
using GHCBWeb.Filters;
using GHCBWeb.Infrastructure;
using GHCBWeb.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace GHCBWeb.Controllers
{
    [RoutePrefix("api/accounts")]
    public class AccountsController : BaseApiController
    {
        public AccountsController(IGHCBRepository repo)
            : base(repo)
        {
        }
        

        private async Task<FogToken> FogCheck(CreateUserBindingModel model)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(MyAPPs.FogBaseURL);
                MyAPPs.setRequest(client);

                HttpResponseMessage response = await client.PostAsJsonAsync(MyAPPs.FogLoginURL, new { username = model.Username, password = model.Password });

                if (response.IsSuccessStatusCode)
                {
                   return await response.Content.ReadAsAsync<FogToken> ();                  
                }

                return null ;
            }

        }
        
        private async Task<FogToken> FogAddUser(CreateUserBindingModel model,string url)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(MyAPPs.FogBaseURL);
                MyAPPs.setRequest(client);
             
                HttpResponseMessage response = await client.PostAsJsonAsync(url,
                    new { username = model.Username, password = model.Password, verification_code=model.verification_code });

                if (response.IsSuccessStatusCode)
                {
                   
                    return await response.Content.ReadAsAsync<FogToken>();
                }

                return null;
            }

        }

        ////[Authorize(Roles = "Admin")]
        //[Route("users")]
        //public IHttpActionResult GetUsers()
        //{
        //    return Ok(this.AppUserManager.Users.ToList().Select(u => this.TheModelFactory.Create(u)));
        //}
        //[Authorize(Roles = "Admin")]
        //[Route("user/{id:guid}", Name = "GetUserById")]
        //public async Task<IHttpActionResult> GetUser(string Id)
        //{
        //    var user = await this.AppUserManager.FindByIdAsync(Id);

        //    if (user != null)
        //    {
        //        return Ok(this.TheModelFactory.Create(user));
        //    }

        //    return NotFound();

        //}
        //[Authorize(Roles = "Admin")]
        //[Route("user/{username}")]
        //public async Task<IHttpActionResult> GetUserByName(string username)
        //{
        //    var user = await this.AppUserManager.FindByNameAsync(username);

        //    if (user != null)
        //    {
        //        return Ok(this.TheModelFactory.Create(user));
        //    }

        //    return NotFound();

        //}
        //[HttpGet]
        //[AllowAnonymous]
        //[Route("ConfirmEmail", Name = "ConfirmEmailRoute")]
        //public async Task<IHttpActionResult> ConfirmEmail(string userId = "", string code = "")
        //{
        //    if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(code))
        //    {
        //        ModelState.AddModelError("", "User Id and Code are required");
        //        return BadRequest(ModelState);
        //    }

        //    IdentityResult result = await this.AppUserManager.ConfirmEmailAsync(userId, code);

        //    if (result.Succeeded)
        //    {
        //        return Ok();
        //    }
        //    else
        //    {
        //        return GetErrorResult(result);
        //    }
        //}
        ////[Authorize]
        ////[Route("ChangePassword")]
        ////public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        ////{
        ////    if (!ModelState.IsValid)
        ////    {
        ////        return BadRequest(ModelState);
        ////    }

        ////    IdentityResult result = await this.AppUserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);

        ////    if (!result.Succeeded)
        ////    {
        ////        return GetErrorResult(result);
        ////    }

        ////    return Ok();
        ////}
        ////[GHCBAuthorizeAttribute]
        [Route("Reset")]
        [HttpPost]
        public async Task<IHttpActionResult> ResetPassword(CreateUserBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            FogToken t = await FogAddUser(model, MyAPPs.FogResetPasswordURL);

            if (null == t)
                return BadRequest(ModelState);

            var user = await AppUserManager.FindByNameAsync(model.Username);
            if (null == user)
                return BadRequest();

            string resetToken = await AppUserManager.GeneratePasswordResetTokenAsync(user.Id);
            IdentityResult result = await AppUserManager.ResetPasswordAsync(user.Id, resetToken, model.Password);
            if (null == result)
                return BadRequest();

            t = await FogCheck(model);

            if (null == t)
                return BadRequest(ModelState);
            
            if (!user.UserToken.Equals(t.user_token)|| !user.UserId.Equals(t.user_id))
            {
                user.UserToken = t.user_token;
                user.UserId = t.user_id;
                IdentityResult x = await AppUserManager.UpdateAsync(user);

                if (null == x)
                    return BadRequest();
            }

            UserToken u = CreateToken(model.Username, model.Password);
            u.fog_user_id = t.user_id;
            u.fog_user_token = t.user_token;

            return Ok(u);
            
        }

        [Route("Login")]
        [HttpPost]
        public async Task<IHttpActionResult> Login(CreateUserBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            FogToken t = await FogCheck(model);

            if (null == t)
                return BadRequest(ModelState);

            var user = await AppUserManager.FindByNameAsync(model.Username);

            if (null == user)
                return BadRequest();

            if (!user.UserToken.Equals(t.user_token) || !user.UserId.Equals(t.user_id))
            {
                user.UserToken = t.user_token;
                user.UserId = t.user_id;
                IdentityResult x = await AppUserManager.UpdateAsync(user);

                if (null == x)
                    return BadRequest();
            }

            UserToken u = CreateToken(model.Username, model.Password);
            u.fog_user_id = t.user_id;
            u.fog_user_token = t.user_token;

            return Ok(u);
        }

        //[Authorize(Roles = "Admin")]
        //[Route("user/{id:guid}")]
        //public async Task<IHttpActionResult> DeleteUser(string id)
        //{

        //    //Only SuperAdmin or Admin can delete users (Later when implement roles)

        //    var appUser = await this.AppUserManager.FindByIdAsync(id);

        //    if (appUser != null)
        //    {
        //        IdentityResult result = await this.AppUserManager.DeleteAsync(appUser);

        //        if (!result.Succeeded)
        //        {
        //            return GetErrorResult(result);
        //        }

        //        return Ok();

        //    }

        //    return NotFound();

        //}

        //[Authorize(Roles = "Admin")]
        //[Route("user/{id:guid}/roles")]
        //[HttpPut]
        //public async Task<IHttpActionResult> AssignRolesToUser([FromUri] string id, [FromBody] string[] rolesToAssign)
        //{

        //    var appUser = await this.AppUserManager.FindByIdAsync(id);

        //    if (appUser == null)
        //    {
        //        return NotFound();
        //    }

        //    var currentRoles = await this.AppUserManager.GetRolesAsync(appUser.Id);

        //    var rolesNotExists = rolesToAssign.Except(this.AppRoleManager.Roles.Select(x => x.Name)).ToArray();

        //    if (rolesNotExists.Count() > 0)
        //    {

        //        ModelState.AddModelError("", string.Format("Roles '{0}' does not exixts in the system", string.Join(",", rolesNotExists)));
        //        return BadRequest(ModelState);
        //    }

        //    IdentityResult removeResult = await this.AppUserManager.RemoveFromRolesAsync(appUser.Id, currentRoles.ToArray());

        //    if (!removeResult.Succeeded)
        //    {
        //        ModelState.AddModelError("", "Failed to remove user roles");
        //        return BadRequest(ModelState);
        //    }

        //    IdentityResult addResult = await this.AppUserManager.AddToRolesAsync(appUser.Id, rolesToAssign);

        //    if (!addResult.Succeeded)
        //    {
        //        ModelState.AddModelError("", "Failed to add user roles");
        //        return BadRequest(ModelState);
        //    }

        //    return Ok();
        //}
        //[Authorize(Roles = "Admin")]
        //[Route("user/{id:guid}/assignclaims")]
        //[HttpPut]
        //public async Task<IHttpActionResult> AssignClaimsToUser([FromUri] string id, [FromBody] List<ClaimBindingModel> claimsToAssign)
        //{

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var appUser = await this.AppUserManager.FindByIdAsync(id);

        //    if (appUser == null)
        //    {
        //        return NotFound();
        //    }

        //    foreach (ClaimBindingModel claimModel in claimsToAssign)
        //    {
        //        if (appUser.Claims.Any(c => c.ClaimType == claimModel.Type))
        //        {

        //            await this.AppUserManager.RemoveClaimAsync(id, ExtendedClaimsProvider.CreateClaim(claimModel.Type, claimModel.Value));
        //        }

        //        await this.AppUserManager.AddClaimAsync(id, ExtendedClaimsProvider.CreateClaim(claimModel.Type, claimModel.Value));
        //    }

        //    return Ok();
        //}

        //[Authorize(Roles = "Admin")]
        //[Route("user/{id:guid}/removeclaims")]
        //[HttpPut]
        //public async Task<IHttpActionResult> RemoveClaimsFromUser([FromUri] string id, [FromBody] List<ClaimBindingModel> claimsToRemove)
        //{

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var appUser = await this.AppUserManager.FindByIdAsync(id);

        //    if (appUser == null)
        //    {
        //        return NotFound();
        //    }

        //    foreach (ClaimBindingModel claimModel in claimsToRemove)
        //    {
        //        if (appUser.Claims.Any(c => c.ClaimType == claimModel.Type))
        //        {
        //            await this.AppUserManager.RemoveClaimAsync(id, ExtendedClaimsProvider.CreateClaim(claimModel.Type, claimModel.Value));
        //        }
        //    }

        //    return Ok();
        //}
        //[AllowAnonymous]
        [Route("create")]
        public async Task<IHttpActionResult> CreateUser(CreateUserBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            FogToken t = await FogAddUser(model, MyAPPs.FogRegisterURL);

            if (null == t)
                return BadRequest(ModelState);

            t = await FogCheck(model);

            if (null == t)
                return BadRequest(ModelState);

            var user = new ApplicationUser()
            {
                UserName = model.Username,
                Email = "1@2.3",
                //FirstName = createUserModel.FirstName,
                //LastName = createUserModel.LastName,
                Level = 3,
                JoinDate = DateTime.Now.Date,
                UserToken = t.user_token,
                UserId = t.user_id

            };

            IdentityResult addUserResult = await this.AppUserManager.CreateAsync(user, model.Password);

            if (!addUserResult.Succeeded)
            {
                return GetErrorResult(addUserResult);
            }

          

            //string code = await this.AppUserManager.GenerateEmailConfirmationTokenAsync(user.Id);

            //var callbackUrl = new Uri(Url.Link("ConfirmEmailRoute", new { userId = user.Id, code = code }));

            //await this.AppUserManager.SendEmailAsync(user.Id,
            //                                        "Confirm your account",
            //                                        "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

            //Uri locationHeader = new Uri(Url.Link("GetUserById", new { id = user.Id }));

            UserToken u = CreateToken(model.Username, model.Password);
            u.fog_user_id = t.user_id;
            u.fog_user_token = t.token;

            return Ok(u);
        }
        [Route("add")]
        public async Task<IHttpActionResult> AddUser(CreateUserBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //FogToken t = await FogAddUser(model, MyAPPs.FogRegisterURL);

            //if (null == t)
            //    return BadRequest(ModelState);

            //FogToken t = await FogCheck(model);

            //if (null == t)
            //    return BadRequest(ModelState);

            var user = new ApplicationUser()
            {
                UserName = model.Username,
                Email = "1@2.3",
                //FirstName = createUserModel.FirstName,
                //LastName = createUserModel.LastName,
                Level = 3,
                JoinDate = DateTime.Now.Date,
                UserToken = "none",
                UserId = "none"

            };

            IdentityResult addUserResult = await this.AppUserManager.CreateAsync(user, model.Password);

            if (!addUserResult.Succeeded)
            {
                return GetErrorResult(addUserResult);
            }



            //string code = await this.AppUserManager.GenerateEmailConfirmationTokenAsync(user.Id);

            //var callbackUrl = new Uri(Url.Link("ConfirmEmailRoute", new { userId = user.Id, code = code }));

            //await this.AppUserManager.SendEmailAsync(user.Id,
            //                                        "Confirm your account",
            //                                        "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

            //Uri locationHeader = new Uri(Url.Link("GetUserById", new { id = user.Id }));

            UserToken u = CreateToken(model.Username, model.Password);
            //u.fog_user_id = t.user_id;
            //u.fog_user_token = t.token;

            return Ok(u);
        }
        private UserToken CreateToken(string u,string p)
        {
            var encoding = Encoding.GetEncoding("iso-8859-1");
            return new UserToken() { user_token = Convert.ToBase64String(encoding.GetBytes(u + ":" + p)) };            
        }
        private class UserToken {
            public string user_token { set; get; }
            public string fog_user_token { set; get; }
            public string fog_user_id { set; get; }
        }

        private class FogToken
        {
            public string user_token { set; get; }
            public string user_id { set; get; }
            public string token { set; get; }
            public string result { set; get; }
        }
}
}
