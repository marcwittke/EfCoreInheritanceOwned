using System;
using Microsoft.EntityFrameworkCore;
using EfCoreInheritanceOwned.Orders;
using Xunit;
using Xunit.Abstractions;

namespace EfCoreInheritanceOwned.Tests
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private static int _nextId = 123;

        public UnitTest1(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;

            using (var dbc = new OrderDbContext())
            {
                dbc.Database.EnsureDeleted();
                dbc.Database.Migrate();
            }
        }

        [Fact]
        public void Test1()
        {
            try
            {
                int externalOrderId;
                using (var dbc = new OrderDbContext())
                {
                    var externalOrder = new ExternalOrder(_nextId++) { Recipient = "me", Supplier = null};
                    dbc.Add(externalOrder);
                    dbc.SaveChanges();

                    Assert.Null(externalOrder.Supplier);
                    externalOrderId = externalOrder.Id;
                }

                using (var dbc = new OrderDbContext())
                {
                    var externalOrder = dbc.Find<ExternalOrder>(externalOrderId);
                    Assert.Null(externalOrder.Supplier);
                }
            }
            catch (Exception ex)
            {
                _testOutputHelper.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}