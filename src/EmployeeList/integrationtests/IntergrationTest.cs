using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using server;
using server.Data;
using System.Net.Http.Headers;
using System;

namespace integrationtests
{
    public class IntegrationTest
    {
        protected readonly HttpClient TestClient;
        protected IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder => 
                {
                    builder.ConfigureServices(services => 
                    {
                        services.RemoveAll(typeof(EmployeeDbContext));
                        services.AddDbContext<EmployeeDbContext>(opt => 
                        {
                            opt.UseInMemoryDatabase("TestDatabase");
                        });
                    });
                });

            TestClient = appFactory.CreateClient();
        }
    }
}