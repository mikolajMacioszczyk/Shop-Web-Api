using System;
using Microsoft.EntityFrameworkCore;
using ShopApi.DAL;

namespace ShopApi.Tests
{
    public class ShopApiTestBase : IDisposable
    {
        protected readonly ShopDbContext _context;
        public ShopApiTestBase()
        {
            var options = new DbContextOptionsBuilder<ShopDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            
            _context = new ShopDbContext(options);

            _context.Database.EnsureCreated();
        }
        
        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            
            _context.Dispose();
        }
    }
}