using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Demo_SQL.DTO;

namespace Demo_SQL.Entities;

public partial class TestsqlContext : DbContext
{
    public TestsqlContext()
    {
    }

    public TestsqlContext(DbContextOptions<TestsqlContext> options)
        : base(options)
    {
    }

    public virtual DbSet<UserTable> UserTables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseMySQL();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserTable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user_table");

            entity.Property(e => e.CreateTime)
                .HasColumnType("timestamp")
                .HasColumnName("create_time");
            entity.Property(e => e.Password)
                .HasMaxLength(32)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(16)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
