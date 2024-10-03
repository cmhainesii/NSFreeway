using System;
using Microsoft.EntityFrameworkCore;
using NSFreeway.Models;

namespace NSFreeway.Contexts;

public class HighwayDataContext : DbContext
{
    public DbSet<HighwayModel> Highways { get; set; }
    public DbSet<RoadConstruction> ConstructionProjects {get; set;}

    public HighwayDataContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RoadConstruction>()
            .HasOne(rc => rc.Highway)
            .WithMany()
            .HasForeignKey(rc => rc.RoadId);

        base.OnModelCreating(modelBuilder);
    }

}
