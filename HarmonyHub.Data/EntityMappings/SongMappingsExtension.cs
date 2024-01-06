﻿using HarmonyHub.Data.Entities;
using HarmonyHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarmonyHub.Data.EntityMappings
{
    public static class SongMappingsExtension
    {
        public static SongModel ToSongModel(this Song song)
        {
            if (song == null)
            {
                throw new ArgumentNullException(nameof(song));
            }

            return new SongModel
            {
                Id = song.Id,
                Artists = song.Artists.ToList().ToArtistModels(),
                Name = song.Name,
                AudioStorageFile = song.AudioStorageFile?.ToStorageFileModel(),
                CoverStorageFile = song.CoverStorageFile?.ToStorageFileModel(),
            };
        }

        public static List<SongModel> ToSongModels(this List<Song> songs)
        {
            var songModels = new List<SongModel>();

            if (songs == null)
            {
                throw new ArgumentNullException(nameof(songs));
            }

            foreach (var song in songs)
            {
                songModels.Add(song.ToSongModel());
            }
            return songModels;
        }
    }
}
