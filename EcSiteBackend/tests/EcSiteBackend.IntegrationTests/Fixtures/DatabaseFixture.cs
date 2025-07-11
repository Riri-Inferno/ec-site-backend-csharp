using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EcSiteBackend.IntegrationTests.Fixtures
{
    /// <summary>
    /// データベースフィクスチャ
    /// </summary>
    public class DatabaseFixture : IDisposable
    {

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DatabaseFixture()
        {
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}
