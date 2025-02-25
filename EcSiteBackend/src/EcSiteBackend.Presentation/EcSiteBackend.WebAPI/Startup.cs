using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EcSiteBackend.Presentation.EcSiteBackend.WebAPI
{
    public class Startup
    {
        // このメソッドでサービスを登録します。
        public void ConfigureServices(IServiceCollection services)
        {
            // GraphQL サービスの登録
            services
                .AddGraphQLServer()
                .AddQueryType<Query>();
            
            

            // サービスの登録（MVC、認証など）
            // services.AddControllers();
        }
        
        // 仮作成
        private class Query
        {
            public string Hello() => "Hello World!";
        }

        // このメソッドでHTTPリクエストパイプラインを設定します。
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            // GraphQL エンドポイントのマッピング
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });
        }
    }
}
