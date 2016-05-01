using UniversityCourseResultManagementSystem.Models;

namespace UniversityCourseResultManagementSystem.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<UniversityCourseResultManagementSystem.Models.ProjectDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(UniversityCourseResultManagementSystem.Models.ProjectDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            //context.Days.AddOrUpdate(
            //    new Day { Name = "Saturday" },
            //    new Day { Name = "Sunday" },
            //    new Day { Name = "Monday" },
            //    new Day { Name = "Tuesday" },
            //    new Day { Name = "Wednesday" },
            //    new Day { Name = "Thursday" },
            //    new Day { Name = "Friday" }
            //    );
            //context.Semesters.AddOrUpdate(
            //    new Semester { SemesterName = "Spring-16" },
            //    new Semester { SemesterName = "Summer-16" },
            //    new Semester { SemesterName = "Fall-16" },
            //    new Semester { SemesterName = "Spring-17" },
            //    new Semester { SemesterName = "Summer-17" },
            //    new Semester { SemesterName = "Fall-17" },
            //    new Semester { SemesterName = "Spring-18" },
            //    new Semester { SemesterName = "Summer-18" },
            //    new Semester { SemesterName = "Fall-18" }
            //    );
            //context.Departments.AddOrUpdate(
            //    new Department { DeptCode = "CSE", DeptName = "Computer Science & Engineering" },
            //    new Department { DeptCode = "EEE", DeptName = "Electrical & Electronic Engineering" },
            //    new Department { DeptCode = "ETE", DeptName = "Electronic & Telecommunication Engineering" },
            //    new Department { DeptCode = "TE", DeptName = "Department Of Textile Engineering" },
            //    new Department { DeptCode = "BBA", DeptName = "Bachelor of Business Administration" }
            //    );
            //context.Designations.AddOrUpdate(
            //    new Designation { DesignationName = "Senior Lecturer" },
            //    new Designation { DesignationName = "Lecturer" },
            //    new Designation { DesignationName = "Professor" },
            //    new Designation { DesignationName = "Assiciate Professor" }

            //    );
            //context.Rooms.AddOrUpdate(
            //    new Room { Name = "A-202" },
            //    new Room { Name = "A-203" },
            //    new Room { Name = "A-204" },
            //    new Room { Name = "A-205" },
            //    new Room { Name = "AB-203" },
            //    new Room { Name = "AB-204" },
            //    new Room { Name = "AB-203-L" },
            //    new Room { Name = "AB-204-L" }

            //    );
            //context.Grades.AddOrUpdate(
            //    new Grade { GradeName = "A+" },
            //    new Grade { GradeName = "A" },
            //    new Grade { GradeName = "A-" },
            //    new Grade { GradeName = "B+" },
            //    new Grade { GradeName = "B" },
            //    new Grade { GradeName = "B-" },
            //    new Grade { GradeName = "C+" },
            //    new Grade { GradeName = "C" },
            //    new Grade { GradeName = "C-" },
            //    new Grade { GradeName = "D+" },
            //    new Grade { GradeName = "D" },
            //    new Grade { GradeName = "D" },
            //    new Grade { GradeName = "F" }
            //    );

        }
    }
}
