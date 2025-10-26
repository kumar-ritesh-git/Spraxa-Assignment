using Microsoft.EntityFrameworkCore;
using MedicinesApi.Entities;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Medicine> Medicines => Set<Medicine>();
    public DbSet<User> Users => Set<User>();
}
