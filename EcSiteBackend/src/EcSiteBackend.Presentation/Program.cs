using System;

namespace EcSiteBackend.Presentation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // アプリケーションの起動コード
            var builder = WebApplication.CreateBuilder(args);

            // サービスの追加
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // ミドルウェアの設定
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // エンドポイントのマッピング
            // （必要に応じて追加）

            app.Run();
        }
    }
}
