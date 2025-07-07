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
using EcSiteBackend.Presentation.EcSiteBackend.WebAPI.GraphQL.Filters;
using EcSiteBackend.Application.Common.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

            // JWT設定を読み込み
            var jwtSettings = new JwtSettings();
            _configuration.GetSection("JwtSettings").Bind(jwtSettings);
            services.Configure<JwtSettings>(_configuration.GetSection("JwtSettings"));

            // JWT認証の設定
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtSettings.Secret))
                    };
                });

            // 1. DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString));

            // 2. Repositories
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IHistoryRepository<>), typeof(HistoryRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();

            // 3. Services
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddSingleton<IUserAgentParser, UserAgentParser>();
            services.AddHttpContextAccessor();

            // 4. UseCases
            services.AddScoped<ISignUpUseCase, SignUpInteractor>();
            services.AddScoped<ISignInUseCase, SignInInteractor>();

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
                .AddAuthorization()
                .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true); // エラー詳細を表示

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            // 認証ミドルウェア
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });
        }
    }
}
