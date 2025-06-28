using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Reflection;
using EcSiteBackend.Infrastructure.DbContext;

namespace EcSiteBackend.WebAPI
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // 実行アセンブリの場所からプロジェクトルートを逆算
            var executableLocation = Assembly.GetExecutingAssembly().Location;
            var startupContentRoot = System.IO.Path.GetDirectoryName(executableLocation);
            var projectRoot = System.IO.Path.GetFullPath(System.IO.Path.Combine(startupContentRoot!, "..", "..", ".."));

            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            // 設定ファイルとUserSecretsから接続文字列を取得
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(projectRoot)
                .AddUserSecrets<EcSiteBackend.Presentation.EcSiteBackend.WebAPI.Program>() // UserSecretsを優先
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException(
                    $"Connection string 'DefaultConnection' not found. " +
                    $"Ensure it's in appsettings.json or user secrets. " +
                    $"Current Directory: {Directory.GetCurrentDirectory()}, " +
                    $"Startup Project Root: {projectRoot}, " +
                    $"Environment: {environmentName}"
                );
            }

            // DbContextのオプションを構成しインスタンスを返す
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
