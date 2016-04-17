using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using GHCBWeb.Models;

namespace GHCBWeb.Controllers
{
    [RoutePrefix("api/appUsers")]
    public class appUsersController : ApiController
    {
        private ghcbDBEntities db = new ghcbDBEntities();

        // GET: api/appUsers
        public IQueryable<appUser> GetappUsers()
        {
            return db.appUsers;
        }

        // GET: api/appUsers/5
        [Authorize]
        [ResponseType(typeof(appUser))]
        public async Task<IHttpActionResult> GetappUser(int id)
        {
            appUser appUser = await db.appUsers.FindAsync(id);
            if (appUser == null)
            {
                return NotFound();
            }

            return Ok(appUser);
        }

        [Route("{id:int}/boards")]
        public IQueryable<board> GetappUserBoards(int id)
        {
            return db.userboards.Include(u=>u.board)
                .Where(u=>u.userId== id)
                .Select(u=> u.board);
        }



        // PUT: api/appUsers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutappUser( appUser appuser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            appUser appuserinDB = db.appUsers.Where(u => u.userid == appuser.userid).FirstOrDefault();
            if (appuserinDB!=null)
            {
                if (appuser.token != null) appuserinDB.token = appuser.token;
                if (appuser.password != null) appuserinDB.password = appuser.password;
            }
            else {

                return BadRequest();
            }

            db.Entry(appuserinDB).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
                appUser.usertoken.AddOrUpdate(appuserinDB.token, appuserinDB.Id, (key, oldvalue) => oldvalue = appuserinDB.Id);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!appUserExists(id))
                //{
                //    return NotFound();
                //}
                //else
                //{
                //    throw;
                //}
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/appUsers
        [ResponseType(typeof(appUser))]
        public async Task<IHttpActionResult> PostappUser(appUser appuser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int id = db.appUsers.Where(u => u.userid == appuser.userid).Count();
            if (id == 0)
            {
                db.appUsers.Add(appuser);
                await db.SaveChangesAsync();
                appUser.usertoken.AddOrUpdate(appuser.token, appuser.Id, (key, oldvalue) => oldvalue = appuser.Id);
                return CreatedAtRoute("DefaultApi", new { id = appuser.Id }, appuser);
            }
            else {
                return BadRequest();
            }
        }

        // DELETE: api/appUsers/5
        [ResponseType(typeof(appUser))]
        public async Task<IHttpActionResult> DeleteappUser(int id)
        {
            appUser appUser = await db.appUsers.FindAsync(id);
            if (appUser == null)
            {
                return NotFound();
            }

            db.appUsers.Remove(appUser);
            await db.SaveChangesAsync();

            return Ok(appUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool appUserExists(int id)
        {
            return db.appUsers.Count(e => e.Id == id) > 0;
        }
    }
}