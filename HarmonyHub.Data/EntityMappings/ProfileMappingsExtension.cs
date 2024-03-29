﻿using HarmonyHub.Data.Entities;
using HarmonyHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarmonyHub.Data.EntityMappings
{
    public static class ProfileMappingsExtension
    {
        public static ProfileModel ToProfileModel(this User user)
        {
            var artistModels = new List<ArtistModel>();
            foreach (var artist in user.FollowingArtists)
            {
                if (artist.Artist != null)
                {
                    artistModels.Add(artist.Artist.ToArtistModel());
                }
            }
            return new ProfileModel
            {
                Name = user.FirstName + " " + user.LastName,
                Email = user?.Email,
                FollowingArtists = artistModels,
                PlayList = user?.PlayList?.ToPlayListModel()
            };
        }

        public static User UpdateFields(this User user, EditProfileModel model)
        {
            user.LastName = model.LastName;
            user.FirstName = model.FirstName;
            user.PhoneNumber = model.PhoneNumber;
            return user;
        }
    }
}
