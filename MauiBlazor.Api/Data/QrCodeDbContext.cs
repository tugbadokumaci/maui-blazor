using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MauiBlazor.Shared.Models;

namespace MauiBlazor.Api.Data;


public class MyDbContext : IdentityUserContext<IdentityUser>
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
    }


    public override DbSet<IdentityUser> Users => base.Users;
    public DbSet<CardModel> Cards => Set<CardModel>();
    public DbSet<SavedModel> SavedCards { get; set; }

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<CardModel>()
    //        .HasOne(c => c.CardOwner)
    //        .WithMany(u => u.Cards)
    //        .HasForeignKey(c => c.CardOwnerId)
    //        .OnDelete(DeleteBehavior.Cascade);
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // ...
    }
}

