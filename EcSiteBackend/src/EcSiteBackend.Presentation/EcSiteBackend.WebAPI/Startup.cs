// Presentation.EcSiteBackend.WebAPI/Startup.cs
using Microsoft.EntityFrameworkCore;
using EcSiteBackend.Application.Common.Interfaces;
using EcSiteBackend.Application.Common.Interfaces.Repositories;
using EcSiteBackend.Infrastructure.DbContext;
using EcSiteBackend.Infrastructure.Persistence.Repositories;
using EcSiteBackend.Infrastructure.Services;

namespace EcSiteBackend.Presentation.EcSiteBackend.WebAPI
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // secrets.jsonから接続文字列を取得
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            // 1. DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString));

            // 2. Repositories
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();

            // 3. Services
            services.AddScoped<ITransactionService, TransactionService>();
            // services.AddScoped<IPasswordHasher, PasswordHasher>(); // 後で作成

            // 4. AutoMapper（TODO）
            // services.AddAutoMapper(typeof(MappingProfile));

            // 5. MediatR（TODO）
            // services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(
            //     typeof(CreateUserCommand).Assembly));

            // 6. FluentValidation（TODO）
            // services.AddValidatorsFromAssembly(typeof(CreateUserCommand).Assembly);

            // 7. GraphQL
            services
                .AddGraphQLServer()
                .AddQueryType<Query>();
            // .AddMutationType<TODO>() // 後で作成
        }

        // 仮置き
        public class Query
        {
            public string Hello() => "Hello, GraphQL!";
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });
        }
    }
}
