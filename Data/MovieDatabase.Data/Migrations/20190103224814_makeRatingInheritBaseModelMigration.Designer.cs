﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MovieDatabase.Data;

namespace MovieDatabase.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190103224814_makeRatingInheritBaseModelMigration")]
    partial class makeRatingInheritBaseModelMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("MovieDatabase.Data.Models.Actor", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.ToTable("Actors");
                });

            modelBuilder.Entity("MovieDatabase.Data.Models.ApplicationRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("MovieDatabase.Data.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<DateTimeOffset>("EmailConfirmationTokenResentOn");

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("Firstname");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Lastname");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("MovieDatabase.Data.Models.Award", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("MovieId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("MovieId");

                    b.ToTable("Award");
                });

            modelBuilder.Entity("MovieDatabase.Data.Models.Category", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name");

                    b.Property<string>("Slug");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("Slug")
                        .IsUnique()
                        .HasFilter("[Slug] IS NOT NULL");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("MovieDatabase.Data.Models.Comment", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("MovieId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("MovieId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("MovieDatabase.Data.Models.Composer", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.ToTable("Composers");
                });

            modelBuilder.Entity("MovieDatabase.Data.Models.Director", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.ToTable("Directors");
                });

            modelBuilder.Entity("MovieDatabase.Data.Models.Event", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime>("Date");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Location");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("MovieId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("MovieId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("MovieDatabase.Data.Models.EventParticipant", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("EventId");

                    b.HasKey("UserId", "EventId");

                    b.HasIndex("EventId");

                    b.ToTable("EventParticipant");
                });

            modelBuilder.Entity("MovieDatabase.Data.Models.Festival", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime>("Date");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Info");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Location");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.ToTable("Festivals");
                });

            modelBuilder.Entity("MovieDatabase.Data.Models.Keyword", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("MovieId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("MovieId");

                    b.ToTable("Keyword");
                });

            modelBuilder.Entity("MovieDatabase.Data.Models.Movie", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ComposerId");

                    b.Property<string>("Country");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Description");

                    b.Property<string>("DirectorId");

                    b.Property<string>("Duration");

                    b.Property<double>("ImdbRating");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Language");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name");

                    b.Property<string>("PosterImageLink");

                    b.Property<DateTime>("ReleaseDate");

                    b.Property<string>("ScreenwriterId");

                    b.Property<string>("Slug");

                    b.Property<string>("Studio");

                    b.Property<string>("TrailerLink");

                    b.HasKey("Id");

                    b.HasIndex("ComposerId");

                    b.HasIndex("DirectorId");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("ScreenwriterId");

                    b.HasIndex("Slug")
                        .IsUnique()
                        .HasFilter("[Slug] IS NOT NULL");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("MovieDatabase.Data.Models.MovieActor", b =>
                {
                    b.Property<string>("MovieId");

                    b.Property<string>("ActorId");

                    b.Property<string>("CharacterName");

                    b.HasKey("MovieId", "ActorId");

                    b.HasIndex("ActorId");

                    b.ToTable("MovieActor");
                });

            modelBuilder.Entity("MovieDatabase.Data.Models.MovieCategory", b =>
                {
                    b.Property<string>("MovieId");

                    b.Property<string>("CategoryId");

                    b.HasKey("MovieId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("MovieCategory");
                });

            modelBuilder.Entity("MovieDatabase.Data.Models.MovieRating", b =>
                {
                    b.Property<string>("RatingId");

                    b.Property<string>("MovieId");

                    b.HasKey("RatingId", "MovieId");

                    b.HasIndex("MovieId");

                    b.ToTable("MovieRating");
                });

            modelBuilder.Entity("MovieDatabase.Data.Models.Post", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("MovieId");

                    b.Property<string>("Name");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("MovieId");

                    b.HasIndex("UserId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("MovieDatabase.Data.Models.Rating", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name");

                    b.Property<string>("RatedById");

                    b.Property<DateTime>("RatedOn");

                    b.Property<double>("Score");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("RatedById");

                    b.ToTable("Rating");
                });

            modelBuilder.Entity("MovieDatabase.Data.Models.Screenwriter", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.ToTable("Screenwriters");
                });

            modelBuilder.Entity("MovieDatabase.Data.Models.Setting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("MovieDatabase.Data.Models.UserOwnedMovie", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("MovieId");

                    b.HasKey("UserId", "MovieId");

                    b.HasIndex("MovieId");

                    b.ToTable("UserOwnedMovie");
                });

            modelBuilder.Entity("MovieDatabase.Data.Models.UserRating", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RatingId");

                    b.HasKey("UserId", "RatingId");

                    b.HasIndex("RatingId");

                    b.ToTable("UserRating");
                });

            modelBuilder.Entity("MovieDatabase.Data.Models.UserWatchedMovie", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("MovieId");

                    b.HasKey("UserId", "MovieId");

                    b.HasIndex("MovieId");

                    b.ToTable("UserWatchedMovie");
                });

            modelBuilder.Entity("MovieDatabase.Data.Models.UserWishlist", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("MovieId");

                    b.HasKey("UserId", "MovieId");

                    b.HasIndex("MovieId");

                    b.ToTable("UserWishlist");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("MovieDatabase.Data.Models.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("MovieDatabase.Data.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("MovieDatabase.Data.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("MovieDatabase.Data.Models.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MovieDatabase.Data.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("MovieDatabase.Data.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("MovieDatabase.Data.Models.Award", b =>
                {
                    b.HasOne("MovieDatabase.Data.Models.Movie", "Movie")
                        .WithMany("Awards")
                        .HasForeignKey("MovieId");
                });

            modelBuilder.Entity("MovieDatabase.Data.Models.Comment", b =>
                {
                    b.HasOne("MovieDatabase.Data.Models.Movie", "Movie")
                        .WithMany("Comments")
                        .HasForeignKey("MovieId");
                });

            modelBuilder.Entity("MovieDatabase.Data.Models.Event", b =>
                {
                    b.HasOne("MovieDatabase.Data.Models.Movie", "Movie")
                        .WithMany()
                        .HasForeignKey("MovieId");
                });

            modelBuilder.Entity("MovieDatabase.Data.Models.EventParticipant", b =>
                {
                    b.HasOne("MovieDatabase.Data.Models.ApplicationUser", "Participant")
                        .WithMany("Events")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MovieDatabase.Data.Models.Event", "Event")
                        .WithMany("Participants")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("MovieDatabase.Data.Models.Keyword", b =>
                {
                    b.HasOne("MovieDatabase.Data.Models.Movie", "Movie")
                        .WithMany("Keywords")
                        .HasForeignKey("MovieId");
                });

            modelBuilder.Entity("MovieDatabase.Data.Models.Movie", b =>
                {
                    b.HasOne("MovieDatabase.Data.Models.Composer", "Composer")
                        .WithMany("Composed")
                        .HasForeignKey("ComposerId");

                    b.HasOne("MovieDatabase.Data.Models.Director", "Director")
                        .WithMany("Directed")
                        .HasForeignKey("DirectorId");

                    b.HasOne("MovieDatabase.Data.Models.Screenwriter", "Screenwriter")
                        .WithMany("Written")
                        .HasForeignKey("ScreenwriterId");
                });

            modelBuilder.Entity("MovieDatabase.Data.Models.MovieActor", b =>
                {
                    b.HasOne("MovieDatabase.Data.Models.Actor", "Actor")
                        .WithMany("StaredIn")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MovieDatabase.Data.Models.Movie", "Movie")
                        .WithMany("Actors")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("MovieDatabase.Data.Models.MovieCategory", b =>
                {
                    b.HasOne("MovieDatabase.Data.Models.Category", "Category")
                        .WithMany("MoviesList")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MovieDatabase.Data.Models.Movie", "Movie")
                        .WithMany("Categories")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("MovieDatabase.Data.Models.MovieRating", b =>
                {
                    b.HasOne("MovieDatabase.Data.Models.Movie", "Movie")
                        .WithMany("Ratings")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MovieDatabase.Data.Models.Rating", "Rating")
                        .WithMany("MovieRatings")
                        .HasForeignKey("RatingId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("MovieDatabase.Data.Models.Post", b =>
                {
                    b.HasOne("MovieDatabase.Data.Models.Movie", "Movie")
                        .WithMany()
                        .HasForeignKey("MovieId");

                    b.HasOne("MovieDatabase.Data.Models.ApplicationUser", "User")
                        .WithMany("Posts")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("MovieDatabase.Data.Models.Rating", b =>
                {
                    b.HasOne("MovieDatabase.Data.Models.ApplicationUser", "RatedBy")
                        .WithMany()
                        .HasForeignKey("RatedById");
                });

            modelBuilder.Entity("MovieDatabase.Data.Models.UserOwnedMovie", b =>
                {
                    b.HasOne("MovieDatabase.Data.Models.ApplicationUser", "User")
                        .WithMany("UserOwnedMovies")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MovieDatabase.Data.Models.Movie", "Movie")
                        .WithMany("UserOwnedMovies")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("MovieDatabase.Data.Models.UserRating", b =>
                {
                    b.HasOne("MovieDatabase.Data.Models.Rating", "Rating")
                        .WithMany("UserRatings")
                        .HasForeignKey("RatingId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MovieDatabase.Data.Models.ApplicationUser", "User")
                        .WithMany("UserRatings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("MovieDatabase.Data.Models.UserWatchedMovie", b =>
                {
                    b.HasOne("MovieDatabase.Data.Models.ApplicationUser", "User")
                        .WithMany("UserWatchedMovies")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MovieDatabase.Data.Models.Movie", "Movie")
                        .WithMany("UserWatchedMovies")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("MovieDatabase.Data.Models.UserWishlist", b =>
                {
                    b.HasOne("MovieDatabase.Data.Models.ApplicationUser", "User")
                        .WithMany("UserWishlists")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MovieDatabase.Data.Models.Movie", "Movie")
                        .WithMany("UserWishlists")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
