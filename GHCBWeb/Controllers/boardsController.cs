using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using GHCBWeb.Models;
using GHCBWeb.DTO;
using System.Linq.Expressions;
using System.Net.Http.Headers;

namespace GHCBWeb.Controllers
{
    [RoutePrefix("api/boards")]
    public class boardsController : ApiController
    {
        private ghcbDBEntities db = new ghcbDBEntities();

        // GET: api/boards
        public IQueryable<board> Getboards()
        {
            return db.boards;
        }

        // GET: api/boards/5
        [ResponseType(typeof(board)), Route("{id:int}")]
      
        public IHttpActionResult Getboard(int id)
        {
            board board = db.boards.Find(id);
            if (board == null)
            {
                return NotFound();
            }

            return Ok(board);
        }

        [Route("{id:int}/users")]
        public IQueryable<userDTO> GetboardUsers(int id)
        {
            var users = from b in db.userboards.Include(u => u.appUser)
                        where b.boardId == id
                        select new userDTO
                        {
                            name = b.appUser.name,
                            mobile = b.appUser.mobile,
                            email = b.appUser.email,
                            role = b.role,
                        };

            return users;
        }
   
        [Route("{id:int}/ports")]
        public IQueryable<portDTO> GetboardPorts(int id)
        {
            return db.boardinterfaces.Include(b => b.interfaceDescription)
                .Where(b => b.boardId == id)
                .Select(o => new portDTO
                {
                    alias=o.alias,
                    hide=o.hide,
                    name=o.interfaceDescription.name,
                    type=o.interfaceDescription.type,
                    description=o.interfaceDescription.description

                });
        }


        // PUT: api/boards/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putboard(int id, board board)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            if (id != board.Id)
            {
                return BadRequest();
            }

            db.Entry(board).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!boardExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/boards
        [ResponseType(typeof(board))]
        public IHttpActionResult Postboard(board board)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //var authorization = Request.Headers["Authorization"];
            if ( 0!=db.boards.Where(b => b.deviceid == board.deviceid).Count())
                return BadRequest();

            db.boards.Add(board);
            db.SaveChanges();

            var userBoard = new userboard() {
                userId = 2,
                boardId =board.Id,
                role = "owner"
            };

            db.userboards.Add(userBoard);         
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = board.Id }, board);
        }

        // DELETE: api/boards/5
        [ResponseType(typeof(board))]
        [NonAction]
        public IHttpActionResult Deleteboard(int id)
        {
            board board = db.boards.Find(id);
            if (board == null)
            {
                return NotFound();
            }

            db.boards.Remove(board);
            db.SaveChanges();

            return Ok(board);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool boardExists(int id)
        {
            return db.boards.Count(e => e.Id == id) > 0;
        }
    }
}