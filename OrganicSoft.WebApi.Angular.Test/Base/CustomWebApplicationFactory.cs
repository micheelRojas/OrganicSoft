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
        private readonly string ConnectionString = @"Server=tcp:sqlservermicheel.database.windows.net,1433;Initial Catalog=organicsoft;Persist Security Info=False;User ID=micheel;Password=ismael2021.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=1000;";

        public OrganicSoftContext _context { get; private set; }
        /* Cadena conexión prueba local: Data Source=DESKTOP-FFMH835\SQLEXPRESS; Initial Catalog=OrganisoftProduction; Integrated Security=True; MultipleActiveResultSets=True
         * Cadena de conexión correcta: Server=tcp:sqlservermicheel.database.windows.net,1433;Initial Catalog=organicsoft;Persist Security Info=False;User ID=micheel;Password=ismael2021.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=1000;
         * @"Server=sqlservermicheel.database.windows.net;Database=organicsoft;User Id = micheel; Password=ismael2021.;";
         * Server=tcp:sqlservermicheel.database.windows.net,1433;Initial Catalog=organicsoft;Persist Security Info=False;User ID=micheel;Password=ismael2021.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;
         */

        public CustomWebApplicationFactory()
        {
            _context = CreateContext();
            _context.Database.SetCommandTimeout(new TimeSpan(0, 2, 0));
            var time = _context.Database.GetCommandTimeout();
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        private void Reset()
        {
            var time = _context.Database.GetCommandTimeout();
        }

        //teardown
        public void Dispose()
        {
            // Dispose here
            //_context.Database.EnsureCreated();
            _context.Database.EnsureDeleted();
        }

        //private readonly string _connectionString = @"Data Source=C:\sqlite\bancoDataBaseEndToEnd.db";
        protected OrganicSoftContext CreateContext()
        {
            var builder = new DbContextOptionsBuilder<OrganicSoftContext>().UseSqlServer(ConnectionString);
            var db = new OrganicSoftContext(builder.Options);
            //db.Database.EnsureDeleted();
            //db.Database.EnsureCreated();
            return db;
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
                    options.UseSqlServer(@"Server=tcp:sqlservermicheel.database.windows.net,1433;Initial Catalog=organicsoft;Persist Security Info=False;User ID=micheel;Password=ismael2021.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=1000;");
                });
                #endregion

                #region Eliminar y Crear nueva base de datos. 
                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<OrganicSoftContext>();
                   
                    //db.Database.EnsureDeleted();
                    //db.Database.EnsureCreated();
                    //invocar clase que inicilice los datos semillas. 
                }
                #endregion 
            });
        }
    }
}
