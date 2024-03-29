﻿using HarmonyHub.Data.EntityMappings;
using HarmonyHub.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HarmonyHub.Data.Utilities;

namespace HarmonyHub.Controllers
{
	public class ArtistController : Controller
	{
		private readonly IArtistService artistService;
		private readonly IUserService userService;
		private readonly IUserFollowingService userFollowingService;

		public ArtistController(
			IArtistService artistService,
			IUserService userService,
			IUserFollowingService userFollowingService
			)
        {
			this.artistService = artistService;
			this.userService = userService;
			this.userFollowingService = userFollowingService;
		}

		public ActionResult Index()
		{
			//redirect to home page
			return RedirectToAction("Index", "Home");
		}

        // GET: Artist/Details/1
        public async Task<ActionResult> Details(int id)
		{
			var artist = await artistService.GetArtistByIdAsync(id);
			var artistModel = artist.ToArtistModel();
			var userName = User?.Identity?.Name;
			if (userName != null)
			{
                var user = await userService.GetUserByEmailAsync(userName);
                if (user != null)
				{
					// mark the artist if followed
					artistModel = artistModel.MarkFollowedArtist(user);

					// mark the songs in the artist's playlist
					artistModel.Songs = artistModel.Songs.MarkPlayListSongs(user);
                }
            }
			return View(artistModel);
		}
		[Authorize]
		// Get: Artist/Follow/1
		public async Task<ActionResult> Follow(int id)
		{
            var artist = await artistService.GetArtistByIdAsync(id);
			var userName = User?.Identity?.Name;
			if (userName != null)
			{
                var user = await userService.GetUserByEmailAsync(userName);
                if (user != null)
				{
					// check if the artist is already followed
					var isFollowed = await userFollowingService.Exists(id, user);
					if (!isFollowed)
					{
                        // follow the artist
                        await userFollowingService.FollowArtistAsync(id, user);
                    } else
					{
                        // unfollow the artist
                        await userFollowingService.UnfollowArtistAsync(id, user);
                    }
				}
			}
            // redirect to the artist details page
            return Redirect(Request.Headers["Referer"].ToString());
        }
	}
}
