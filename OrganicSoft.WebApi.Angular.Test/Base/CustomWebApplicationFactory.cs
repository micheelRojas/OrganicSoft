using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrganicSoft.Infraestructura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicSoft.WebApi.Angular.Test.Base
{
    public class CustomWebApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup> where TStartup : class
    {
        private readonly string ConnectionString = @"Data Source=DESKTOP-FFMH835\SQLEXPRESS;Initial Catalog=OrganisoftTest;Integrated Security=True;";

        //private readonly string _connectionString = @"Data Source=C:\sqlite\bancoDataBaseEndToEnd.db";
        public OrganicSoftContext CreateContext()
        {
            var builder = new DbContextOptionsBuilder<OrganicSoftContext>().UseSqlServer(ConnectionString);
            return new OrganicSoftContext(builder.Options);
        }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                #region Reemplazar la inyección del Contexto de Datos de EF Core
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<OrganicSoftContext>));

                services.Remove(descriptor);

                services.AddDbContext<OrganicSoftContext>(options =>
                {
                    options.UseSqlServer(@"Data Source=DESKTOP-FFMH835\SQLEXPRESS;Initial Catalog=OrganisoftTest;Integrated Security=True;");
                });
                #endregion

                #region Eliminar y Crear nueva base de datos. 
                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<OrganicSoftContext>();
                    db.Database.EnsureDeleted();
                    db.Database.EnsureCreated();
                    //invocar clase que inicilice los datos semillas. 
                }
                #endregion 
            });
        }
    }
}
