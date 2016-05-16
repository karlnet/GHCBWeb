using GHCBWeb.Data.Entities;
using GHCBWeb.Infrastructure;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;

namespace GHCBWeb.Models
{
    public class ModelFactory
    {
        private UrlHelper _UrlHelper;
        private ApplicationUserManager _AppUserManager;

        public ModelFactory(HttpRequestMessage request, ApplicationUserManager appUserManager)
        {
            _UrlHelper = new UrlHelper(request);
            _AppUserManager = appUserManager;
        }
        

        public RoleReturnModel Create(IdentityRole appRole)
        {

            return new RoleReturnModel
            {
                Url = _UrlHelper.Link("GetRoleById", new { id = appRole.Id }),
                Id = appRole.Id,
                Name = appRole.Name
            };
        }


        public BoardModel Create(Board board)
        {

            return new BoardModel
            {
                Url = _UrlHelper.Link("BoardRoute", new { id = board.Id }),
                Id = board.Id,
                SerialNo = board.SerialNo,
                Alias = board.Name,
                MAC = board.MAC,
                ROMVersion = board.ROMVersion,
                Description = board.Description,
                Privateip = board.Privateip,
                Publicip = board.Publicip,
                SSID = board.SSID,
                BSSID = board.BSSID,
                Deviceid = board.Deviceid,
                Status = board.Status,
                Offtime = board.Offtime,
                Onlinetime = board.Onlinetime,
                Createtime = board.Createtime
            };
        }

        public BoardPortsModel Create(BoardPort boardPort)
        {

            return new BoardPortsModel
            {
                //Url = _UrlHelper.Link("BoardPortRoute", new { portId = boardPort.Id }),
                BoardId = boardPort.BoardId,
                PortId = boardPort.Id,
                PortNo = boardPort.Port,
                PortName = boardPort.Alias,
                PortTypeId = boardPort.PortDescriptionId,
                PortTypeName = boardPort.portDescription.DeviceModel,
                PortType = boardPort.portDescription.DeviceType,
                PortTypeDescription = boardPort.portDescription.Description,
                ClassType = boardPort.portDescription.ClassType
            };
        }
        public UserReturnModel Create(ApplicationUser appUser)
        {
            return new UserReturnModel
            {
                Url = _UrlHelper.Link("GetUserById", new { id = appUser.Id }),
                Id = appUser.Id,
                UserName = appUser.UserName,
                //FullName = string.Format("{0} {1}", appUser.FirstName, appUser.LastName),
                //Email = appUser.Email,
                //EmailConfirmed = appUser.EmailConfirmed,
                //Level = appUser.Level,
                //JoinDate = appUser.JoinDate,
                //Roles = _AppUserManager.GetRolesAsync(appUser.Id).Result,
                //Claims = _AppUserManager.GetClaimsAsync(appUser.Id).Result
            };
        }
    }
    public class RoleReturnModel
    {
        public string Url { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
    }
    public class UserReturnModel
    {
        public string Url { get; set; }
        public string Id { get; set; }
        public string UserName { get; set; }
     
        //public string FullName { get; set; }
        //public string Email { get; set; }
        //public bool EmailConfirmed { get; set; }
        //public int Level { get; set; }
        //public DateTime JoinDate { get; set; }
        //public IList<string> Roles { get; set; }
        //public IList<System.Security.Claims.Claim> Claims { get; set; }
    }
}