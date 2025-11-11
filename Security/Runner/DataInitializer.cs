using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using PlataformaEducativa.Models;
using PlataformaEducativa.Data;
using BCrypt.Net;

namespace PlataformaEducativa.Runner
{
    public class DataInitializer
    {
        public static async Task SeedAsync(IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<AppDbContext>();
            var configuration = services.GetRequiredService<IConfiguration>();

            // 🔹 Aplica migraciones pendientes
            await context.Database.MigrateAsync();

            // Leer credenciales del appsettings.json
            var adminUsername = configuration["Security:User"];
            var adminPassword = configuration["Security:Password"];

            if (!context.Users.Any())
            {
                Console.WriteLine("🌱 Creando datos iniciales...");

                // 🔹 Crear permisos
                var permissions = new List<Permission>
                {
                    new Permission { PermissionName = "READ" },
                    new Permission { PermissionName = "WRITE" },
                    new Permission { PermissionName = "UPDATE" },
                    new Permission { PermissionName = "CREATE" }
                };

                context.Permissions.AddRange(permissions);
                await context.SaveChangesAsync();

                // 🔹 Crear rol ADMIN
                var adminRole = new Role
                {
                    RoleName = "ADMIN",
                    Permissions = permissions
                };

                context.Roles.Add(adminRole);
                await context.SaveChangesAsync();

                // 🔹 Crear usuario admin con BCrypt
                var adminUser = new UserSec
                {
                    Username = adminUsername,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(adminPassword), // 🔒 BCrypt
                    Enabled = true,
                    AccountNotExpired = true,
                    AccountNotLocked = true,
                    CredentialNotExpired = true,
                    Roles = new List<Role> { adminRole }
                };

                context.Users.Add(adminUser);
                await context.SaveChangesAsync();

                Console.WriteLine("✅ Usuario ADMIN creado con éxito.");
            }
            else
            {
                Console.WriteLine("✔️ Datos iniciales ya existen. No se realizó ningún cambio.");
            }
        }
    }
}
