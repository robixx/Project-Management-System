using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskWorker.Application.ModelViews;
using TaskWorker.Domain.Entity;

namespace TaskWorker.Infrastructure.DBConnection
{
    public class DatabaseConnection: DbContext
    {
        public  DatabaseConnection(DbContextOptions<DatabaseConnection> options) : base(options)
        {

        }


        // for data table
        public DbSet<AppUser> AppUser {  get; set; }
        public DbSet<AppGroupMember> AppGroupTeam {  get; set; }
        public DbSet<AppIssue> AppIssue {  get; set; }
        public DbSet<AppMenu> AppMenu {  get; set; }
        public DbSet<AppProject> AppProject {  get; set; }
        public DbSet<AppRole> AppRole {  get; set; }
        public DbSet<AppRoleWiseMenu> AppRoleWiseMenu {  get; set; }
        public DbSet<AppTask> AppTask {  get; set; }
        public DbSet<AppTaskAssign> AppTaskAssign {  get; set; }
        public DbSet<AppTeam> AppTeam {  get; set; }
        public DbSet<AppWorkDocument> AppWorkDocument {  get; set; }
        public DbSet<AppMetaData> AppMetaData {  get; set; }
        public DbSet<AppMetaElement> AppMetaElement {  get; set; }
        public DbSet<AppEncryptedData> AppEncryptedData {  get; set; }
        public DbSet<AppSecUser> AppSecUser {  get; set; }
        public DbSet<RoleWiseMenuDto> RoleWiseMenuDto {  get; set; }
        public DbSet<AppUserRole> AppUserRole {  get; set; }
        public DbSet<UserRoleDto> UserRoleDto {  get; set; }
        public DbSet<AppDepartmentApproved> AppDepartmentApproved {  get; set; }
        public DbSet<GetUnitDto> GetUnitDto {  get; set; }
        public DbSet<AppAssignType> AppAssignType {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //for entity databse
            modelBuilder.Entity<AppUser>().HasKey(x => x.UserId);
            modelBuilder.Entity<AppMetaData>().HasKey(x => x.Id);
            modelBuilder.Entity<AppMetaElement>().HasKey(x => x.ElementId);
            modelBuilder.Entity<AppGroupMember>().HasKey(x => x.MemberId);
            modelBuilder.Entity<AppIssue>().HasKey(x => x.IssueId);
            modelBuilder.Entity<AppMenu>().HasKey(x => x.Id);
            modelBuilder.Entity<AppProject>().HasKey(x => x.ProjectId);
            modelBuilder.Entity<AppRole>().HasKey(x => x.RoleId);
            modelBuilder.Entity<AppRoleWiseMenu>().HasKey(x => x.Id);
            modelBuilder.Entity<AppTask>().HasKey(x => x.TasksId);
            modelBuilder.Entity<AppTaskAssign>().HasKey(x => x.AssignId);
            modelBuilder.Entity<AppTeam>().HasKey(x => x.TeamId);
            modelBuilder.Entity<AppWorkDocument>().HasKey(x => x.Id);
            modelBuilder.Entity<AppEncryptedData>().HasKey(x => x.Id);
            modelBuilder.Entity<AppSecUser>().HasKey(x => x.Id);
            modelBuilder.Entity<AppAssignType>().HasKey(x => x.Id);
            modelBuilder.Entity<AppUserRole>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("app_UserRole");

                entity.HasIndex(e => new { e.UserId, e.RoleId })
                      .IsUnique();

            });

            modelBuilder.Entity<AppDepartmentApproved>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("app_Department_Approved");

                entity.HasIndex(e => new { e.UserId, e.DepartmentId })
                      .IsUnique();
            });


            // for Procedure

            modelBuilder.Entity<RoleWiseMenuDto>().HasNoKey();
           
            modelBuilder.Entity<UserRoleDto>().HasNoKey();

            modelBuilder.Entity<GetUnitDto>(entity =>
            {
                entity.HasNoKey();
                entity.ToView(null);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
