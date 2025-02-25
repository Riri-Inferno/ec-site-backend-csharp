namespace EcSiteBackend.Presentation.EcSiteBackend.WebAPI
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddGraphQLServer()
                .AddQueryType<Query>();
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
                endpoints.MapGraphQL(); // GraphQLエンドポイントをマッピング
            });
        }
    }
}