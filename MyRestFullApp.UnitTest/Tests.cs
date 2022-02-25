using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyRestFullApp.Core.Interfaces.Services;
using System.IO;
using System.Threading.Tasks;

namespace MyRestFullApp.UnitTest
{
    [TestClass]
    public class UnitTest
    {

        private static IConfiguration _configuration;
        private static IServiceScopeFactory _scopeFactory;
        [ClassInitialize]
        public static void ClassInitialize(TestContext _)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .AddEnvironmentVariables();
            _configuration = builder.Build();

            var startup = new Startup(_configuration);
            var services = new ServiceCollection();
            startup.ConfigureServices(services);
            _scopeFactory = services.AddLogging().BuildServiceProvider().GetService<IServiceScopeFactory>();
        }

        [TestMethod]
        public async Task GetPaises_CuandoSeEnvianParametrosCorrectos_RetonaOk()
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<IPaisesServices>();
            string pais = "AR";

            var result = await context.GetPaisML(pais);

            var expected = true;
            var actual = result.Result;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task GetPaises_CuandoSeEnvianParametrosInCorrectos_RetonaError()
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<IPaisesServices>();
            string pais = "";

            var result = await context.GetPaisML(pais);

            var expected = false;
            var actual = result.Result;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task GetArticulos_CuandoSeEnvianParametrosCorrectos_RetornaOk()
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<IBusquedaServices>();
            string articulo = "iphone";

            var result = await context.GetArticulos(articulo);

            var expected = true;
            var actual = result.Result;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task GetArticulos_CuandoSeEnvianParametrosInCorrectos_RetornaError()
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<IBusquedaServices>();
            string articulo = "";

            var result = await context.GetArticulos(articulo);

            var expected = false;
            var actual = result.Result;

            Assert.AreEqual(expected, actual);
        }
    }
}
