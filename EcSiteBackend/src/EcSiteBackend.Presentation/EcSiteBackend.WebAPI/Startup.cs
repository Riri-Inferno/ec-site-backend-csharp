using Microsoft.EntityFrameworkCore;
using EcSiteBackend.Application.Common.Interfaces;
using EcSiteBackend.Application.Common.Interfaces.Repositories;
using EcSiteBackend.Infrastructure.DbContext;
using EcSiteBackend.Infrastructure.Persistence.Repositories;
using EcSiteBackend.Infrastructure.Services;
using EcSiteBackend.Application.Common.Mappings;
using EcSiteBackend.Presentation.EcSiteBackend.WebAPI.GraphQL.Queries;
using EcSiteBackend.Presentation.EcSiteBackend.WebAPI.GraphQL.Mutations;
using EcSiteBackend.Application.UseCases.Interfaces;
using EcSiteBackend.Application.UseCases.Interactors;
using HotChocolate.Data;
using EcSiteBackend.Presentation.EcSiteBackend.WebAPI.GraphQL.Filters;

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
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            // 4. UseCases
            services.AddScoped<ICreateUserUseCase, CreateUserInteractor>();

            // 5. AutoMapper
            services.AddAutoMapper(typeof(UserMappingProfile));

            // 6. MediatR（TODO）
            // services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(
            //     typeof(CreateUserCommand).Assembly));

            // 7. FluentValidation（TODO）
            // services.AddValidatorsFromAssembly(typeof(CreateUserCommand).Assembly);

            // 8. GraphQL
            services
                .AddGraphQLServer()
                .AddQueryType(d => d.Name("Query"))
                .AddType<UserQueries>()
                .AddMutationType(d => d.Name("Mutation"))
                .AddType<UserMutations>()
                .AddProjections()
                .AddFiltering()
                .AddErrorFilter<ErrorFilter>()
                .AddSorting()
                .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true); // エラー詳細を表示

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
