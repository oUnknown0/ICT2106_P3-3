﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using YouthActionDotNet.Data;

#nullable disable

namespace YouthActionDotNet.Migrations
{
    [DbContext(typeof(DBContext))]
    [Migration("20230117184938_restructure_v1")]
    partial class restructurev1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.1");

            modelBuilder.Entity("YouthActionDotNet.Models.ServiceCenter", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("RegionalDirectorId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ServiceCenterAddress")
                        .HasColumnType("TEXT");

                    b.Property<string>("ServiceCenterName")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.HasIndex("RegionalDirectorId");

                    b.ToTable("ServiceCenter", (string)null);
                });

            modelBuilder.Entity("YouthActionDotNet.Models.User", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<string>("Role")
                        .HasColumnType("TEXT");

                    b.Property<string>("address")
                        .HasColumnType("TEXT");

                    b.Property<string>("phoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<string>("username")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("YouthActionDotNet.Models.Employee", b =>
                {
                    b.HasBaseType("YouthActionDotNet.Models.User");

                    b.Property<string>("BankAccountNumber")
                        .HasColumnType("TEXT");

                    b.Property<string>("BankName")
                        .HasColumnType("TEXT");

                    b.Property<string>("DateJoined")
                        .HasColumnType("TEXT");

                    b.Property<string>("EmployeeNationalId")
                        .HasColumnType("TEXT");

                    b.Property<string>("EmployeeRole")
                        .HasColumnType("TEXT");

                    b.Property<string>("EmployeeType")
                        .HasColumnType("TEXT");

                    b.Property<string>("PAYE")
                        .HasColumnType("TEXT");

                    b.ToTable("Employee", (string)null);
                });

            modelBuilder.Entity("YouthActionDotNet.Models.Volunteer", b =>
                {
                    b.HasBaseType("YouthActionDotNet.Models.User");

                    b.Property<string>("ApprovalStatus")
                        .HasColumnType("TEXT");

                    b.Property<string>("ApprovedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("CriminalHistory")
                        .HasColumnType("TEXT");

                    b.Property<string>("CriminalHistoryDesc")
                        .HasColumnType("TEXT");

                    b.Property<string>("Qualifications")
                        .HasColumnType("TEXT");

                    b.Property<string>("VolunteerDateBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("VolunteerDateJoined")
                        .HasColumnType("TEXT");

                    b.Property<string>("VolunteerNationalId")
                        .HasColumnType("TEXT");

                    b.HasIndex("ApprovedBy");

                    b.ToTable("Volunteer", (string)null);
                });

            modelBuilder.Entity("YouthActionDotNet.Models.ServiceCenter", b =>
                {
                    b.HasOne("YouthActionDotNet.Models.Employee", "RegionalDirector")
                        .WithMany()
                        .HasForeignKey("RegionalDirectorId");

                    b.Navigation("RegionalDirector");
                });

            modelBuilder.Entity("YouthActionDotNet.Models.Employee", b =>
                {
                    b.HasOne("YouthActionDotNet.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("YouthActionDotNet.Models.Volunteer", b =>
                {
                    b.HasOne("YouthActionDotNet.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("ApprovedBy");

                    b.HasOne("YouthActionDotNet.Models.User", null)
                        .WithOne()
                        .HasForeignKey("YouthActionDotNet.Models.Volunteer", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}