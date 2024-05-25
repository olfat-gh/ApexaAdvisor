using Apexa.Lib.Cache.Services;
using Apexa.Lib.Cache.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;
using FluentAssertions;

namespace Apexa.Lib.Cache.UnitTests
{
    public class CacheServiceTests
    {
        private IServiceProvider _serviceProvider;
        private record Advisor(string Name, string SIN, string Address);
        public CacheServiceTests()
        {
            var services = new ServiceCollection();
            var m = Directory.GetCurrentDirectory();
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.test.json", optional: false);


            var config = configBuilder.Build();
            services.AddCachingSystem(config);
            _serviceProvider = services.BuildServiceProvider().CreateScope().ServiceProvider;

        }

        [Fact]
        public void CheckCache_Get_Put_IsOk()
        {
            var cacheService = _serviceProvider.GetService<ICacheService<Advisor>>();
            cacheService.Should().NotBeNull();

            if (!cacheService.TryGetValue("user1", out Advisor value))
                cacheService.PutValue("user1", new Advisor(Name: "test1", SIN: "1234567", Address: "Canada"));
            else
            {
                value.Name.Should().Be("user1");
            }

            var ret = cacheService.TryGetValue("user1", out Advisor value1);
            ret.Should().Be(true);
            value1.SIN.Should().Be("1234567");
            

            cacheService.PutValue("user1", new Advisor(Name: "updatedName", SIN: "1234567", Address: "Canada"));
            ret = cacheService.TryGetValue("user1", out Advisor value2);
            ret.Should().Be(true);
            value2.SIN.Should().Be("1234567");
           


            cacheService.PutValue("user2", new Advisor(Name: "user2", SIN: "1234567", Address: "Canada"));
            cacheService.PutValue("user3", new Advisor(Name: "user3", SIN: "1234567", Address: "Canada"));
            cacheService.PutValue("user4", new Advisor(Name: "user4", SIN: "1234567", Address: "Canada"));

            ret = cacheService.TryGetValue("user1", out Advisor value3);
            ret.Should().Be(false);
        

            ret = cacheService.TryGetValue("user4", out Advisor value4);
            ret.Should().Be(true);
            value4.Name.Should().Be("user4");
            

            cacheService.PutValue("user5", new Advisor(Name: "user5", SIN: "1234567", Address: "Canada"));

            ret = cacheService.TryGetValue("user4", out Advisor value5);
            ret.Should().Be(false);


        }

    }
}