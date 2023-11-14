// <copyright file="BrokenApiTests.cs" company="Teqniqly">
// Copyright (c) Teqniqly. All rights reserved.
// </copyright>

namespace FruitApi.IntegrationTests
{
	using FluentAssertions;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.Mvc.Testing;
	using Microsoft.AspNetCore.TestHost;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.DependencyInjection.Extensions;
	using Services;

	public class BrokenApiTests : IClassFixture<CustomWebApplicationFactory>
	{
		private readonly CustomWebApplicationFactory fixture;

		public BrokenApiTests(CustomWebApplicationFactory fixture) => this.fixture = fixture;

		[Fact]
		public async Task When_Exception_Thrown_In_Dependency_In_Production_Response_Has_No_Content()
		{
			var customFactory = CreateCustomFactoryForEnvironment("Production");
			var client = new ApiTestClient(customFactory.CreateClient());

			var postRequestBody = new { name = "Banana", Stock = 10 };
			var response = await client.PostAsync(Program.BaseRoute, postRequestBody);

			var content = await response.Content.ReadAsStringAsync();

			content.Should().BeEmpty();
		}

		[Fact]
		public async Task When_Exception_Thrown_In_Dependency_In_Development_Response_Content_Has_Exception_Details()
		{
			var customFactory = CreateCustomFactoryForEnvironment("Development");
			var client = new ApiTestClient(customFactory.CreateClient());

			var postRequestBody = new { name = "Banana", Stock = 10 };
			var response = await client.PostAsync(Program.BaseRoute, postRequestBody);

			var content = await response.Content.ReadAsStringAsync();

			content.Should().Contain("System.NullReferenceException");
		}

		private WebApplicationFactory<Program> CreateCustomFactoryForEnvironment(string environment)
			=> fixture.WithWebHostBuilder(b =>
			{
				b.UseEnvironment(environment);

				b.ConfigureTestServices(s =>
				{
					s.RemoveAll<IIdFactory>();
					s.AddScoped<IIdFactory, BadIdFactory>();
				});
			});

		private class BadIdFactory : IIdFactory
		{
			public string CreateId() => throw new NullReferenceException();
		}
	}
}