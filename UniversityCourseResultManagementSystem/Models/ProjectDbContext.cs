using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace UniversityCourseResultManagementSystem.Models
{
    public class ProjectDbContext : DbContext
    {
     
        public DbSet<Department> Departments { set; get; }
        public DbSet<Course> Courses { set; get; }
        public DbSet<Semester> Semesters { set; get; }
        public DbSet<Teacher> Teachers { set; get; }
        public DbSet<Designation> Designations { set; get; }
        public DbSet<Student> Students { set; get; }
        public DbSet<AssignCourse> AssignCourses { get; set; }
        public DbSet<Grade> Grades { set; get; }
        public DbSet<Room> Rooms { set; get; }
        public DbSet<Day> Days { get; set; } 
        public DbSet<RoomAllocation> RoomAllocations { get; set; }
        public DbSet<EnrollCourse> EnrollCourses { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }
    }
}