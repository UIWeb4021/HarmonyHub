﻿using HarmonyHub.Data;
using HarmonyHub.Data.Entities;
using HarmonyHub.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarmonyHub.Services
{
    public class ArtistService : IArtistService
    {
        private readonly HarmonyHubDbContext dbContext;

        public ArtistService(HarmonyHubDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Artist>> GetAllArtistsAsync()
        {
            return await dbContext.Artists
                .Include(x => x.Songs)
                .ToListAsync();
        }

        public async Task<Artist> GetArtistByIdAsync(int id)
        {
			return await dbContext.Artists
				.Include(x => x.Songs)
                    .ThenInclude(s => s.CoverStorageFile)
                .Include(x => x.Songs)
                    .ThenInclude(s => s.AudioStorageFile)
                .FirstAsync(x => x.Id == id);
		}

        public async Task<List<Artist>> SearchArtistByString(string query)
        {
            var list = await dbContext.Artists
                .Include(a => a.Songs)
                .Where(a => a.FirstName.Contains(query)
                         || a.LastName.Contains(query)
                )
                .ToListAsync();

            return list;
        }
    }
}
