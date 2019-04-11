using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AnglerNet.Models
{
    public partial class AnglerNetContext : DbContext
    {
        public AnglerNetContext()
        {
        }

        public AnglerNetContext(DbContextOptions<AnglerNetContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<Conversation> Conversation { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<Photo> Photo { get; set; }
        public virtual DbSet<Profile> Profile { get; set; }
        public virtual DbSet<Relationship> Relationship { get; set; }
        public virtual DbSet<Wall> Wall { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-U5P13DK\\SQLEXPRESS;Database=AnglerNet;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreationDate).HasColumnName("creationDate");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Conversation>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateLatest).HasColumnType("date");

                entity.Property(e => e.DateStart).HasColumnType("date");

                entity.Property(e => e.UserIdOne)
                    .HasColumnName("UserID_one")
                    .HasMaxLength(450);

                entity.Property(e => e.UserIdTwo)
                    .HasColumnName("UserID_two")
                    .HasMaxLength(450);

                entity.HasOne(d => d.UserIdOneNavigation)
                    .WithMany(p => p.ConversationUserIdOneNavigation)
                    .HasForeignKey(d => d.UserIdOne)
                    .HasConstraintName("FK__Conversat__UserI__50FB042B");

                entity.HasOne(d => d.UserIdTwoNavigation)
                    .WithMany(p => p.ConversationUserIdTwoNavigation)
                    .HasForeignKey(d => d.UserIdTwo)
                    .HasConstraintName("FK__Conversat__UserI__51EF2864");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ConversationId).HasColumnName("ConversationID");

                entity.Property(e => e.DateStamp).HasColumnType("date");

                entity.Property(e => e.MessageId).HasColumnName("MessageID");

                entity.Property(e => e.MsgBody)
                    .IsRequired()
                    .HasColumnName("Msg_body")
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.UserIdRecieve)
                    .HasColumnName("UserID_recieve")
                    .HasMaxLength(450);

                entity.Property(e => e.UserIdSend)
                    .HasColumnName("UserID_send")
                    .HasMaxLength(450);

                entity.HasOne(d => d.Conversation)
                    .WithMany(p => p.Message)
                    .HasForeignKey(d => d.ConversationId)
                    .HasConstraintName("FK__Message__Convers__54CB950F");

                entity.HasOne(d => d.UserIdRecieveNavigation)
                    .WithMany(p => p.MessageUserIdRecieveNavigation)
                    .HasForeignKey(d => d.UserIdRecieve)
                    .HasConstraintName("FK__Message__UserID___56B3DD81");

                entity.HasOne(d => d.UserIdSendNavigation)
                    .WithMany(p => p.MessageUserIdSendNavigation)
                    .HasForeignKey(d => d.UserIdSend)
                    .HasConstraintName("FK__Message__UserID___55BFB948");
            });

            modelBuilder.Entity<Photo>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateAdded).HasColumnType("date");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.PictureUrl)
                    .IsRequired()
                    .HasColumnName("PictureURL")
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Photo)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Photo__UserID__4B422AD5");
            });

            modelBuilder.Entity<Profile>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Info)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.PictureUrl)
                    .HasColumnName("PictureURL")
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasMaxLength(450);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Profile)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Profile__UserID__4865BE2A");
            });

            modelBuilder.Entity<Relationship>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.FriendId)
                    .HasColumnName("FriendID")
                    .HasMaxLength(450);

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasMaxLength(450);

                entity.HasOne(d => d.Friend)
                    .WithMany(p => p.RelationshipFriend)
                    .HasForeignKey(d => d.FriendId)
                    .HasConstraintName("FK__Relations__Frien__4589517F");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RelationshipUser)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Relations__UserI__44952D46");
            });

            modelBuilder.Entity<Wall>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.DateAdded).HasColumnType("date");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Wall)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Wall__UserID__4E1E9780");
            });
        }
    }
}
