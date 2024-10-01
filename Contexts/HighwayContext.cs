using System;
using Microsoft.EntityFrameworkCore;
using NSFreeway.Models;

namespace NSFreeway.Contexts;

public class HighwayContext : DbContext
{
    public DbSet<HighwayModel> Highways { get; set; }

    public HighwayContext(DbContextOptions options) : base(options)
    {
        
    }

}
