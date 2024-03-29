﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HarmonyHub.Data.Entities
{
    public class User : IdentityUser
    {
        [Required, StringLength(100)]
        public string? FirstName { get; set; }
        [Required, StringLength(100)]
        public string? LastName { get; set; }
        public int PlayListId { get; set; }
        public PlayList? PlayList { get; set; }
        public ICollection<UserFollowing> FollowingArtists { get; set; } = new List<UserFollowing>();
    }
}
