using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MusicApplication.Model;

public partial class NeondbContext : DbContext
{
    public NeondbContext()
    {
    }

    public NeondbContext(DbContextOptions<NeondbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbAlbum> TbAlbums { get; set; }

    public virtual DbSet<TbAuthor> TbAuthors { get; set; }

    public virtual DbSet<TbMusic> TbMusics { get; set; }

    public virtual DbSet<TbPlaylist> TbPlaylists { get; set; }

    public virtual DbSet<TbUser> TbUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=ep-polished-glade-22606167.eu-central-1.aws.neon.tech;Port=5432;Database=neondb;Username=tverdohlebovartem;Password=1aYkNAxZhLO8");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbAlbum>(entity =>
        {
            entity.HasKey(e => e.AlbumId).HasName("tb_albums_pkey");

            entity.ToTable("tb_albums");

            entity.Property(e => e.AlbumId).HasColumnName("album_id");
            entity.Property(e => e.AlbAuthorId).HasColumnName("alb_author_id");
            entity.Property(e => e.AlbumName)
                .HasMaxLength(50)
                .HasColumnName("album_name");

            entity.HasOne(d => d.AlbAuthor).WithMany(p => p.TbAlbums)
                .HasForeignKey(d => d.AlbAuthorId)
                .HasConstraintName("tb_albums_alb_author_id_fkey");
        });

        modelBuilder.Entity<TbAuthor>(entity =>
        {
            entity.HasKey(e => e.AuthId).HasName("tb_author_pkey");

            entity.ToTable("tb_author");

            entity.Property(e => e.AuthId)
                .ValueGeneratedNever()
                .HasColumnName("auth_id");
            entity.Property(e => e.AuthName)
                .HasMaxLength(50)
                .HasColumnName("auth_name");
        });

        modelBuilder.Entity<TbMusic>(entity =>
        {
            entity.HasKey(e => e.MusicId).HasName("tb_music_pkey");

            entity.ToTable("tb_music");

            entity.Property(e => e.MusicId)
                .ValueGeneratedNever()
                .HasColumnName("music_id");
            entity.Property(e => e.MusicAuthorId).HasColumnName("music_author_id");
            entity.Property(e => e.MusicName)
                .HasMaxLength(100)
                .HasColumnName("music_name");
            entity.Property(e => e.MusicPath)
                .HasMaxLength(150)
                .HasColumnName("music_path");

            entity.HasOne(d => d.MusicAuthor).WithMany(p => p.TbMusics)
                .HasForeignKey(d => d.MusicAuthorId)
                .HasConstraintName("tb_music_music_author_id_fkey");
        });

        modelBuilder.Entity<TbPlaylist>(entity =>
        {
            entity.HasKey(e => e.PlstId).HasName("tb_playlist_pkey");

            entity.ToTable("tb_playlist");

            entity.Property(e => e.PlstId)
                .ValueGeneratedNever()
                .HasColumnName("plst_id");
            entity.Property(e => e.PlstMusicsId).HasColumnName("plst_musics_id");
            entity.Property(e => e.PlstName)
                .HasMaxLength(50)
                .HasColumnName("plst_name");
            entity.Property(e => e.PlstUserId).HasColumnName("plst_user_id");

            entity.HasOne(d => d.PlstUser).WithMany(p => p.TbPlaylists)
                .HasForeignKey(d => d.PlstUserId)
                .HasConstraintName("tb_playlist_plst_user_id_fkey");
        });

        modelBuilder.Entity<TbUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("tb_user_pkey");

            entity.ToTable("tb_user");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("user_id");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .HasColumnName("user_name");
            entity.Property(e => e.UserPassword)
                .HasMaxLength(20)
                .HasColumnName("user_password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
