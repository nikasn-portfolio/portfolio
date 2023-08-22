﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.App.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CategoryImageUrl")
                        .HasColumnType("text");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Domain.App.Client", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Domain.App.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("IdentificationCode")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("VatNumber")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("Domain.App.Identity.AppRefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AppUserId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ExpirationDt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("PreviousExpirationDt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("PreviousRefreshToken")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.ToTable("AppRefreshTokens");
                });

            modelBuilder.Entity("Domain.App.Identity.AppRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Domain.App.Identity.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Domain.App.Invoice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AppUserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uuid");

                    b.Property<string>("Comment")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<Guid?>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("InvoiceDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("InvoiceFooterId")
                        .HasColumnType("uuid");

                    b.Property<string>("InvoiceNumber")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<bool>("IsCompany")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("PaymentDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("PaymentMethodId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RecordId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.HasIndex("ClientId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("InvoiceFooterId");

                    b.HasIndex("PaymentMethodId");

                    b.HasIndex("RecordId")
                        .IsUnique();

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("Domain.App.InvoiceFooter", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Email")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Iban")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Phone")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("InvoiceFooters");
                });

            modelBuilder.Entity("Domain.App.InvoiceRow", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("InvoiceId")
                        .HasColumnType("uuid");

                    b.Property<decimal?>("PriceOverride")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Tax")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("ServiceId");

                    b.ToTable("InvoiceRows");
                });

            modelBuilder.Entity("Domain.App.Language", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("LanImageUrl")
                        .HasColumnType("text");

                    b.Property<string>("LanguageName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.HasKey("Id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("Domain.App.PaymentMethod", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("MethodImageUrl")
                        .HasColumnType("text");

                    b.Property<string>("PaymentMethodName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.HasKey("Id");

                    b.ToTable("PaymentMethods");
                });

            modelBuilder.Entity("Domain.App.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AppUserId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("LanguageId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("PersonBirthDate")
                        .HasColumnType("Date");

                    b.Property<string>("PersonName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("PersonPhoneNumber")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("PersonSurname")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId")
                        .IsUnique();

                    b.HasIndex("LanguageId");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("Domain.App.Record", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AppUserId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ClientId")
                        .HasColumnType("uuid");

                    b.Property<string>("Comment")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsHidden")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.HasIndex("ClientId");

                    b.ToTable("Records");
                });

            modelBuilder.Entity("Domain.App.RecordService", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("RecordId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RecordId");

                    b.HasIndex("ServiceId");

                    b.ToTable("RecordServices");
                });

            modelBuilder.Entity("Domain.App.Service", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<TimeSpan?>("ServiceDuration")
                        .HasColumnType("interval");

                    b.Property<string>("ServiceName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<double>("ServicePrice")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Domain.App.Identity.AppRefreshToken", b =>
                {
                    b.HasOne("Domain.App.Identity.AppUser", "AppUser")
                        .WithMany("AppRefreshTokens")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("Domain.App.Invoice", b =>
                {
                    b.HasOne("Domain.App.Identity.AppUser", "AppUser")
                        .WithMany("Invoices")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.App.Client", "Client")
                        .WithMany("Invoices")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.App.Company", "Company")
                        .WithMany("Invoices")
                        .HasForeignKey("CompanyId");

                    b.HasOne("Domain.App.InvoiceFooter", "InvoiceFooter")
                        .WithMany("Invoices")
                        .HasForeignKey("InvoiceFooterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.App.PaymentMethod", "PaymentMethod")
                        .WithMany("Invoices")
                        .HasForeignKey("PaymentMethodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.App.Record", "Record")
                        .WithOne("Invoice")
                        .HasForeignKey("Domain.App.Invoice", "RecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");

                    b.Navigation("Client");

                    b.Navigation("Company");

                    b.Navigation("InvoiceFooter");

                    b.Navigation("PaymentMethod");

                    b.Navigation("Record");
                });

            modelBuilder.Entity("Domain.App.InvoiceRow", b =>
                {
                    b.HasOne("Domain.App.Invoice", "Invoice")
                        .WithMany("InvoiceRows")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.App.Service", "Service")
                        .WithMany("InvoiceRows")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Invoice");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("Domain.App.Person", b =>
                {
                    b.HasOne("Domain.App.Identity.AppUser", "AppUser")
                        .WithOne("Person")
                        .HasForeignKey("Domain.App.Person", "AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.App.Language", "Language")
                        .WithMany("Persons")
                        .HasForeignKey("LanguageId");

                    b.Navigation("AppUser");

                    b.Navigation("Language");
                });

            modelBuilder.Entity("Domain.App.Record", b =>
                {
                    b.HasOne("Domain.App.Identity.AppUser", "AppUser")
                        .WithMany("Records")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.App.Client", "Client")
                        .WithMany("Records")
                        .HasForeignKey("ClientId");

                    b.Navigation("AppUser");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("Domain.App.RecordService", b =>
                {
                    b.HasOne("Domain.App.Record", "Record")
                        .WithMany("RecordServices")
                        .HasForeignKey("RecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.App.Service", "Service")
                        .WithMany("RecordServices")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Record");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("Domain.App.Service", b =>
                {
                    b.HasOne("Domain.App.Category", "Category")
                        .WithMany("Services")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Domain.App.Identity.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("Domain.App.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("Domain.App.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Domain.App.Identity.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.App.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("Domain.App.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.App.Category", b =>
                {
                    b.Navigation("Services");
                });

            modelBuilder.Entity("Domain.App.Client", b =>
                {
                    b.Navigation("Invoices");

                    b.Navigation("Records");
                });

            modelBuilder.Entity("Domain.App.Company", b =>
                {
                    b.Navigation("Invoices");
                });

            modelBuilder.Entity("Domain.App.Identity.AppUser", b =>
                {
                    b.Navigation("AppRefreshTokens");

                    b.Navigation("Invoices");

                    b.Navigation("Person");

                    b.Navigation("Records");
                });

            modelBuilder.Entity("Domain.App.Invoice", b =>
                {
                    b.Navigation("InvoiceRows");
                });

            modelBuilder.Entity("Domain.App.InvoiceFooter", b =>
                {
                    b.Navigation("Invoices");
                });

            modelBuilder.Entity("Domain.App.Language", b =>
                {
                    b.Navigation("Persons");
                });

            modelBuilder.Entity("Domain.App.PaymentMethod", b =>
                {
                    b.Navigation("Invoices");
                });

            modelBuilder.Entity("Domain.App.Record", b =>
                {
                    b.Navigation("Invoice");

                    b.Navigation("RecordServices");
                });

            modelBuilder.Entity("Domain.App.Service", b =>
                {
                    b.Navigation("InvoiceRows");

                    b.Navigation("RecordServices");
                });
#pragma warning restore 612, 618
        }
    }
}
