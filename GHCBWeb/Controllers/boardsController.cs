using GHCBWeb.Data;
using GHCBWeb.Data.Entities;
using GHCBWeb.Filters;
using GHCBWeb.Infrastructure;
using GHCBWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace GHCBWeb.Controllers
{
    [RoutePrefix("api/Boards")]
    [GHCBAuthorizeAttribute]
    public class BoardsController : BaseApiController
    {
        public BoardsController(IGHCBRepository repo)
            : base(repo)
        {
        }

        public class ActiveTokenModel
        {
            [Required]
            public string mac { set; get; }
        }
        private async Task<bool> FogActive(string mac)
        {
            using (var client = new HttpClient())
            {
                var jsonObject = new
                {
                    product_id = MyAPPs.ProductID,
                    MAC = mac,
                    secret_key = MyUtils.getActiveToken(mac)
                };
                  
                HttpResponseMessage response = await client.PostAsJsonAsync(MyAPPs.FogActiveURL, jsonObject);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;
            }

        }
        private async Task<bool> FogBinding(string mac)
        {
            using (var client = new HttpClient())
            {
                string userName = User.Identity.Name;
                string userToken = "token " + MyAPPs.currentUsers[userName].UserToken;

                MyAPPs.setRequest(client);
                client.DefaultRequestHeaders.Add("Authorization", userToken);

                var jsonObject = new
                {
                    product_id = MyAPPs.ProductID,
                    user_id= MyAPPs.currentUsers[userName].Id,
                    MAC = mac,
                    secret_key = MyUtils.getActiveToken(mac)
                };

                HttpResponseMessage response = await client.PostAsJsonAsync(MyAPPs.FogAuthorizeURL, jsonObject);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;
            }

        }

        // GET: api/boards
        [Route("authorize")]
        [HttpPost]
        public async Task<IHttpActionResult> Authorize(ActiveTokenModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string mac = model.mac;         
            bool result;

            result = await FogActive(mac);

            if (!result)
                return BadRequest(ModelState);

            if (TheRepository.IsExistsBoard(mac))
                TheRepository.Delete(mac);

            Board board = new Board()
            {
                MAC = mac
            };

            if (!TheRepository.Insert(board))
                return BadRequest("Active failed.");


            result = await FogBinding(mac);

            if (!result)
                return BadRequest(ModelState);


            string userName = User.Identity.Name;
            string userId = MyAPPs.currentUsers[userName].Id;

            var applicationUserBoard = new ApplicationUserBoard()
            {
                BoardId = board.Id,
                UserType = "owner",
                ApplicationUserName = userName,
                ApplicationUserId = userId
            };

            if (!TheRepository.Insert(applicationUserBoard))
                return BadRequest("Bingding failed.");
            
            return Ok();
        }

        //// GET: api/boards
        //[Route("")]
        //public IEnumerable<BoardModel> Get()
        //{
        //    var v = Thread.CurrentPrincipal;
        //    string userName = User.Identity.Name;
        //    //string userName = ActionContext.Request.Properties["currentUserName"] as string;
        //    return TheRepository.GetAllBoards(userName).ToList().Select(s => TheModelFactory.Create(s));

        //}

        //[ResponseType(typeof(BoardModel)), Route("{id:int}", Name = "BoardRoute")]
        //public IHttpActionResult Get(int id)
        //{

        //    try
        //    {
        //        //string userName = ActionContext.Request.Properties["currentUserName"] as string;
        //        string userName = User.Identity.Name;
        //        if (!TheRepository.CheckBoardUser(id, userName))
        //            return NotFound();

        //        var board = TheRepository.GetBoardById(id);
        //        if (board != null)
        //            return Ok(TheModelFactory.Create(board));
        //        else
        //            return NotFound();
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }
        //}

        // POST: api/boards
        //[ResponseType(typeof(BoardModel))]
        //[Route("")]
        //[HttpPost]
        //public IHttpActionResult Postboard(Board board)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    if (TheRepository.IsExists(x => x.MAC.Equals(board.MAC)))
        //        return BadRequest("This board has been in the database.");

        //    if (!TheRepository.Insert(board))
        //        return BadRequest("Could not save board to the database.");

        //    //string userName = ActionContext.Request.Properties["currentUserName"] as string ;
        //    //string userId = ActionContext.Request.Properties["currentUserId"] as string;
        //    string userName = User.Identity.Name;
        //    string userId = App.currentUsers[userName].Id;

        //    var applicationUserBoard = new ApplicationUserBoard()
        //    {
        //        BoardId = board.Id,
        //        UserType = "owner",
        //        ApplicationUserName = userName ?? string.Empty,
        //        ApplicationUserId = userId ?? string.Empty
        //    };

        //    if (!TheRepository.Insert(applicationUserBoard))
        //        return BadRequest("Could not save applicationUserBoard to the database.");

        //    return Ok(TheModelFactory.Create(board));
        //}

        

    }
}
