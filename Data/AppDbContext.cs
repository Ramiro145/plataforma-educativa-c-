using Microsoft.EntityFrameworkCore;
using PlataformaEducativa.Models;

namespace PlataformaEducativa.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // 👇 Aquí defines tus tablas (entidades)

        // DbSets
        public DbSet<Course> Courses { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Student> Students { get; set; }
           
        // apartado de seguridad
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserSec> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relación One-to-Many: Professor -> Courses
            modelBuilder.Entity<Course>()
                        .HasOne(c => c.Professor)
                        .WithMany(p => p.Courses)
                        .HasForeignKey(c => c.ProfessorId);

            // Relación Many-to-Many: Course <-> Student
            modelBuilder.Entity<Course>()
                        .HasMany(c => c.Students)
                        .WithMany(s => s.Courses)
                        .UsingEntity<Dictionary<string, object>>(
                            "CourseStudent",  // nombre de la tabla intermedia
                            j => j.HasOne<Student>().WithMany().HasForeignKey("StudentId"),
                            j => j.HasOne<Course>().WithMany().HasForeignKey("CourseId")
                        );

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.PermissionName)
                      .IsRequired();
                entity.HasIndex(p => p.PermissionName)
                      .IsUnique();
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.Property(r => r.RoleName)
                      .IsRequired();
            });

            // Relación many-to-many Role ↔ Permission
            modelBuilder.Entity<Role>()
                .HasMany(r => r.Permissions)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "RolesPermissions", // nombre de la tabla de unión
                    r => r.HasOne<Permission>().WithMany().HasForeignKey("PermissionId"),
                    p => p.HasOne<Role>().WithMany().HasForeignKey("RoleId"),
                    je =>
                    {
                        je.HasKey("RoleId", "PermissionId");
                    }
                );


            modelBuilder.Entity<UserSec>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.HasIndex(u => u.Username).IsUnique();

                // Relación many-to-many con Role
                entity.HasMany(u => u.Roles)
                      .WithMany()
                      .UsingEntity<Dictionary<string, object>>(
                          "UserRoles",
                          r => r.HasOne<Role>().WithMany().HasForeignKey("RoleId"),
                          u => u.HasOne<UserSec>().WithMany().HasForeignKey("UserId"),
                          je => je.HasKey("UserId", "RoleId")
                      );
            });


            modelBuilder.Entity<Professor>()
                        .HasOne(p => p.User)
                        .WithOne()
                        .HasForeignKey<Professor>(p => p.UserId);

            modelBuilder.Entity<Student>()
                        .HasOne(s => s.User)
                        .WithOne()
                        .HasForeignKey<Student>(s => s.UserId);


        }



    }
}
