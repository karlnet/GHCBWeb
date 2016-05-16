using GHCBWeb.Data;
using GHCBWeb.Data.Entities;
using GHCBWeb.Filters;
using GHCBWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace GHCBWeb.Controllers
{
    [RoutePrefix("api/Ports")]
    [GHCBAuthorizeAttribute]
    public class BoardPortsController : BaseApiController
    {
        public BoardPortsController(IGHCBRepository repo)
            : base(repo)
        {
        }

        public class boardPortBindingModel
        {
            [Required]
            public string mac { set; get; }
            [Required]
            public int PortNo { get; set; }
            [Required]
            public string PortName { get; set; }
            [Required]
            public int PortTypeId { get; set; }
        }

        //GET: api/BoardPorts
        [Route("")]
       [HttpPost]
        public IEnumerable<BoardPortsModel> GetAllPortsByMac(boardPortBindingModel model)
        {

            Board board = TheRepository.GetBoardByMac(model.mac);
            if (board == null)
                return  new BoardPortsModel[] { };

            string userName = User.Identity.Name;
            if (!TheRepository.CheckBoardUser(board.Id, userName))
                return new BoardPortsModel[] { };

            var query = TheRepository.GetAllBoardPorts(board.Id).ToList().Select(s => TheModelFactory.Create(s));
            return query;
        }

        // GET: api/BoardPorts/5
        //public string Get(int entityId,int id)
        //{
        //    return "value";
        //}

        [Route("create")]
        [HttpPost]
        public IHttpActionResult Add(boardPortBindingModel boardPortsModels)
        {

            if (!ModelState.IsValid)           
                return BadRequest(ModelState);

            Board board = TheRepository.GetBoardByMac(boardPortsModels.mac);
            if (null== board)
                return BadRequest(ModelState);

            string userName = User.Identity.Name;
            if (!TheRepository.CheckBoardUser(board.Id, userName))
                return BadRequest();


            while (TheRepository.IsExistsBoardPort(board.Id, boardPortsModels.PortNo))
                TheRepository.DeleteBoardPort(board.Id, boardPortsModels.PortNo);

            BoardPort bp = new BoardPort()
            {
                BoardId = board.Id,
                Port = boardPortsModels.PortNo,
                PortDescriptionId = boardPortsModels.PortTypeId,
                Alias = boardPortsModels.PortName
            };

            if (!TheRepository.Insert(bp))
                return BadRequest(ModelState);

            return Ok();

        }

        [Route("remove")]
        [HttpPost]
        public IHttpActionResult Remove(boardPortBindingModel boardPortsModels)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            Board board = TheRepository.GetBoardByMac(boardPortsModels.mac);
            if (null == board)
                return BadRequest(ModelState);

            string userName = User.Identity.Name;
            if (!TheRepository.CheckBoardUser(board.Id, userName))
                return BadRequest();

            TheRepository.DeleteBoardPort(board.Id, boardPortsModels.PortNo);

            return Ok();

        }

        // PUT: api/BoardPorts/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE: api/BoardPorts/5
        //public void Delete(int id)
        //{
        //}
    }
}
