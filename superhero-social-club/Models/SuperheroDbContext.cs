using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace superhero_social_club.Models;

public partial class SuperheroDbContext : DbContext
{
    public SuperheroDbContext()
    {
    }

    public SuperheroDbContext(DbContextOptions<SuperheroDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Superhero> Superheroes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SuperheroDb;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Superhero>(entity =>
        {
            entity.HasKey(e => e.SuperheroId).HasName("PK__Superher__05B0D017637B9561");

            entity.Property(e => e.SecretIdenty).HasMaxLength(50);
            entity.Property(e => e.SuperheroName).HasMaxLength(50);
            entity.Property(e => e.Superpower).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
