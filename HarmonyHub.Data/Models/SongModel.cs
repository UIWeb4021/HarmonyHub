﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HarmonyHub.Data.Models
{
    public class SongModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter name")]
        public string? Name { get; set; }
        public StorageFileModel? AudioStorageFile { get; set; }
        public StorageFileModel? CoverStorageFile { get; set; }
        public ICollection<ArtistModel> Artists { get; set; } = new List<ArtistModel>();
        public bool InPLayList { get; set; }
    }
}
