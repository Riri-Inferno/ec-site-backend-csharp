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
using EcSiteBackend.Presentation.EcSiteBackend.WebAPI.GraphQL.Mappings;
using EcSiteBackend.Application.Common.Settings;
using EcSiteBackend.Presentation.EcSiteBackend.WebAPI.GraphQL.Interactors;
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
            var jwtSecret = _configuration["JwtSettings:Secret"];
            var jwtIssuer = _configuration["JwtSettings:Issuer"];
            var jwtAudience = _configuration["JwtSettings:Audience"];
            var jwtExpiration = _configuration["JwtSettings:ExpirationInMinutes"];

            // JWT設定をDIコンテナに登録
            services.Configure<JwtSettings>(options =>
            {
                options.Secret = jwtSecret ?? throw new ArgumentNullException("JWT Secret is not configured.");
                options.Issuer = jwtIssuer ?? throw new ArgumentNullException("JWT Issuer is not configured.");
                options.Audience = jwtAudience ?? throw new ArgumentNullException("JWT Audience is not configured.");
                options.ExpirationInMinutes = int.Parse(jwtExpiration ?? "60");
            });

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
                        ValidIssuer = jwtIssuer,
                        ValidAudience = jwtAudience,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtSecret!))
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
            services.AddScoped<IReadCurrentUserUseCase, ReadCurrentUserInteractor>();

            // 5. AutoMapper
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<UserMappingProfile>();
                cfg.AddProfile<GraphQLMappingProfile>();
                cfg.AddProfile<BaseEntityMappingProfile>();
                cfg.AddProfile<LoginHistoryMappingProfile>();
                cfg.AddProfile<CartMappingProfile>();
            });

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
                .AddErrorFilter(sp => 
                {
                    var logger = sp.GetRequiredService<ILogger<ErrorFilter>>();
                    return new ErrorFilter(logger);
                })
                .AddSorting()
                .AddAuthorization()
                .AddHttpRequestInterceptor<HttpRequestInterceptor>()
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
