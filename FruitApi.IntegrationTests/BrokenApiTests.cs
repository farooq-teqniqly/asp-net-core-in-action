// <copyright file="BrokenApiTests.cs" company="Teqniqly">
// Copyright (c) Teqniqly. All rights reserved.
// </copyright>

namespace FruitApi.IntegrationTests
{
	using FluentAssertions;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.TestHost;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.DependencyInjection.Extensions;
	using Services;

	public class BrokenApiTests : IClassFixture<CustomWebApplicationFactory>, IDisposable
	{
		private readonly ApiTestClient client;

		public BrokenApiTests(CustomWebApplicationFactory fixture)
		{
			var customFactory = fixture.WithWebHostBuilder(b =>
			{
				b.UseEnvironment("Production");
				
				b.ConfigureTestServices(s =>
				{
					s.RemoveAll<IIdFactory>();
					s.AddScoped<IIdFactory, BadIdFactory>();
				});
			});
			
			client = new ApiTestClient(customFactory.CreateClient());
		}

		[Fact]
		public async Task When_Exception_Thrown_In_Dependency_In_Production_Response_Has_No_Content()
		{
			var postRequestBody = new { name = "Banana", Stock = 10 };
			var response = await client.PostAsync(Program.BaseRoute, postRequestBody);

			var content = await response.Content.ReadAsStringAsync();
			
			content.Should().BeEmpty();
		}
		
		public void Dispose() => client.Dispose();
		
		private class BadIdFactory : IIdFactory
		{
			public string CreateId() => throw new NullReferenceException();
		}
	}
}