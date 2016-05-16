using GHCBWeb.Data.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace GHCBWeb.Infrastructure
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {           
            this.applicationUserBoards = new HashSet<Board>();
        }

       
        public string Text1 { get; set; }

        [Required]
        [MaxLength(100)]
        public string UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string UserToken { get; set; }

        [Required]
        public byte Level { get; set; }

        [Required]
        public DateTime JoinDate { get; set; }

        public ICollection<Board> applicationUserBoards { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }

    }
}