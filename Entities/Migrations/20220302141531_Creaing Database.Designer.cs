// <auto-generated />
using System;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Entities.Migrations
{
    [DbContext(typeof(ContactListContext))]
    [Migration("20220302141531_Creaing Database")]
    partial class CreaingDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("public")
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "6.0.0-preview.5.21301.9")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Entities.DataModel.Contact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<string>("CompanyName")
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)")
                        .HasColumnName("CompanyName");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("DateCreated");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("LastName");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.HasIndex("Name", "LastName");

                    b.ToTable("Contact", "public");
                });

            modelBuilder.Entity("Entities.DataModel.ContactDetail", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("Id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)")
                        .HasColumnName("Address");

                    b.Property<Guid>("ContactId")
                        .HasColumnType("uuid")
                        .HasColumnName("ContactId");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("DateCreated");

                    b.Property<string>("EMailAddress")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("EMailAddress");

                    b.Property<string>("TelephoneNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)")
                        .HasColumnName("TelephoneNumber");

                    b.HasKey("Id");

                    b.HasIndex("ContactId");

                    b.ToTable("ContactDetail", "public");
                });

            modelBuilder.Entity("Entities.DataModel.Log", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("Id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("CallSite")
                        .HasColumnType("text")
                        .HasColumnName("CallSite");

                    b.Property<string>("CorrelationId")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("CorrelationId");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("DateCreated");

                    b.Property<string>("ErrorMessage")
                        .HasColumnType("text")
                        .HasColumnName("ErrorMessage");

                    b.Property<string>("Exception")
                        .HasColumnType("text")
                        .HasColumnName("Exception");

                    b.Property<string>("Level")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("Level");

                    b.Property<string>("Logger")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("Logger");

                    b.Property<string>("MachineName")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("MachineName");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Message");

                    b.Property<string>("RemoteAddress")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("RemoteAddress");

                    b.Property<string>("RequestMethod")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("RequestMethod");

                    b.Property<string>("RequestUserAgent")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)")
                        .HasColumnName("RequestUserAgent");

                    b.Property<string>("ServerAddress")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("ServerAddress");

                    b.Property<string>("SiteName")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("SiteName");

                    b.Property<string>("StackTrace")
                        .HasColumnType("text")
                        .HasColumnName("StackTrace");

                    b.Property<string>("TraceIdentifier")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("TraceIdentifier");

                    b.Property<string>("URL")
                        .HasColumnType("text")
                        .HasColumnName("URL");

                    b.HasKey("Id");

                    b.ToTable("Log", "public");
                });

            modelBuilder.Entity("Entities.DataModel.Report", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("Id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)")
                        .HasColumnName("Address");

                    b.Property<int>("ContactCount")
                        .HasColumnType("integer")
                        .HasColumnName("ContactCount");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("DateCreated");

                    b.Property<DateTime>("DateRequested")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("DateRequested");

                    b.Property<int>("PhoneRecordCount")
                        .HasColumnType("integer")
                        .HasColumnName("PhoneRecordCount");

                    b.Property<string>("ReportName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("ReportName");

                    b.Property<byte>("ReportStatusId")
                        .HasColumnType("smallint")
                        .HasColumnName("ReportStatusId");

                    b.HasKey("Id");

                    b.HasIndex("ReportStatusId");

                    b.ToTable("Report", "public");
                });

            modelBuilder.Entity("Entities.DataModel.ReportStatus", b =>
                {
                    b.Property<byte>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasColumnName("Id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValue(new DateTime(2022, 3, 2, 14, 15, 31, 558, DateTimeKind.Utc).AddTicks(2459))
                        .HasColumnName("DateCreated");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("ReportStatus", "public");

                    b.HasData(
                        new
                        {
                            Id = (byte)1,
                            DateCreated = new DateTime(2022, 3, 2, 14, 15, 31, 558, DateTimeKind.Utc).AddTicks(3301),
                            Name = "Hazırlanıyor"
                        },
                        new
                        {
                            Id = (byte)2,
                            DateCreated = new DateTime(2022, 3, 2, 14, 15, 31, 558, DateTimeKind.Utc).AddTicks(3308),
                            Name = "Tamamlandı"
                        });
                });

            modelBuilder.Entity("Entities.DataModel.ContactDetail", b =>
                {
                    b.HasOne("Entities.DataModel.Contact", "Contact")
                        .WithMany("ContactDetails")
                        .HasForeignKey("ContactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contact");
                });

            modelBuilder.Entity("Entities.DataModel.Contact", b =>
                {
                    b.Navigation("ContactDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
