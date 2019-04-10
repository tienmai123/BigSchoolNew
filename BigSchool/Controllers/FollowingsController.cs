using BigSchool.DTOs;
using BigSchool.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Bigschool.Controllers
{
    public class FollowingsController : ApiController
    {
        private readonly ApplicationDbContext _dbContext;
        public FollowingsController()
        {
            _dbContext = new ApplicationDbContext();

        }

        [HttpPost]
        public IHttpActionResult Follow(FollowingDto folowingDto)
        {

            var userId = User.Identity.GetUserId();
            if (_dbContext.Followings.Any(f => f.FollowerId == userId && f.FolloweeId == folowingDto.FolloweeId))
                return BadRequest("Followings already exits! ");
            var Following = new Following
            {
                FollowerId = userId,
                FolloweeId = folowingDto.FolloweeId
            };
            _dbContext.Followings.Add(Following);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
