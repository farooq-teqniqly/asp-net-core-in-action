// <copyright file="EndpointIntegrationTests.cs" company="Teqniqly">
// Copyright (c) Teqniqly. All rights reserved.
// </copyright>

namespace FruitApi.IntegrationTests
{
	using FluentAssertions;
	using System.Net;
	using System.Text;
	using System.Text.Json;

	public class EndpointIntegrationTests : IClassFixture<CustomWebApplicationFactory>, IDisposable
	{
		private readonly HttpClient client;

		public EndpointIntegrationTests(CustomWebApplicationFactory fixture) => client = fixture.CreateClient();

		[Fact]
		public async Task Add_New_Fruit()
		{
			var newFruitModel = new { name = "Banana", Stock = 10 };

			var response = await client.PostAsync("/fruit",
				new StringContent(
					JsonSerializer.Serialize(newFruitModel),
					Encoding.UTF8,
					"application/json"));

			response.EnsureSuccessStatusCode();

			response.StatusCode.Should().Be(HttpStatusCode.Created);
		}

		[Fact]
		public async Task Can_Call_Hello_World_Endpoint()
		{
			var response = await client.GetAsync("/hello");
			response.EnsureSuccessStatusCode();

			var content = await response.Content.ReadAsStringAsync();
			content.Should().Be("Hello World!");
		}

		public void Dispose() => client.Dispose();
	}
}