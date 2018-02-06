using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class AlbumContext : DbContext
    {
        public AlbumContext (DbContextOptions<AlbumContext> options)
            : base(options)
        {
        }

        public DbSet<WebApplication1.Models.Album> Album { get; set; }
    }
}
