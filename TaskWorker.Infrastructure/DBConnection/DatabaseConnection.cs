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
        public DbSet<AppGroupTeam> AppGroupTeam {  get; set; }
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //for entity databse
            modelBuilder.Entity<AppUser>().HasKey(x => x.UserId);
            modelBuilder.Entity<AppMetaData>().HasKey(x => x.Id);
            modelBuilder.Entity<AppMetaElement>().HasKey(x => x.ElementId);
            modelBuilder.Entity<AppGroupTeam>().HasKey(x => x.MemberId);
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


            // for Procedure

            modelBuilder.Entity<RoleWiseMenuDto>().HasNoKey();

            base.OnModelCreating(modelBuilder);
        }
    }
}
