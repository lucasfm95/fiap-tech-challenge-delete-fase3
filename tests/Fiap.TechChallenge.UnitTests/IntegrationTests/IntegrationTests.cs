namespace Fiap.TechChallenge.UnitTests.IntegrationTests;

using System;
using System.Data.Common;
using System.Threading.Tasks;
using Npgsql;
using Xunit;
using System.Data;
using Testcontainers.PostgreSql;
using Testcontainers.RabbitMq;

public class IntegrationTests
{
    private readonly RabbitMqContainer _rabbitMqContainer;
    private readonly PostgreSqlContainer _postgreSqlContainer;

    public IntegrationTests()
    {
        _rabbitMqContainer = new RabbitMqBuilder().Build();

        _postgreSqlContainer = new PostgreSqlBuilder()
            .WithDatabase("testdb")
            .WithUsername("user")
            .WithPassword("password")
            .Build();
    }

    // [Fact]
    // public async Task TestIntegration()
    // {
    //     await _postgreSqlContainer.StartAsync();
    //     await _rabbitMqContainer.StartAsync();
    //
    //     var postgresConnectionString = _postgreSqlContainer.GetConnectionString();
    //
    //     var rabbitMqHostName = _rabbitMqContainer.Hostname;
    //
    //     await _postgreSqlContainer.StopAsync();
    //     await _rabbitMqContainer.StopAsync();
    // }
}