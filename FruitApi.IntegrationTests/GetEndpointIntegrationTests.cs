// <copyright file="GetEndpointIntegrationTests.cs" company="Teqniqly">
// Copyright (c) Teqniqly. All rights reserved.
// </copyright>

namespace FruitApi.IntegrationTests
{
	using FluentAssertions;
	using Models;
	using System.Net;
	using System.Net.Http.Json;

	public class GetEndpointIntegrationTests : IClassFixture<CustomWebApplicationFactory>, IDisposable
	{
		private readonly ApiTestClient client;

		public GetEndpointIntegrationTests(CustomWebApplicationFactory fixture)
			=> client = new ApiTestClient(fixture.CreateClient());

		[Fact]
		public async Task Get_When_Successful_Returns_Ok_Status_Code()
		{
			var response = await client.GetAsync(Program.BaseRoute);

			response.StatusCode.Should().Be(HttpStatusCode.OK);
		}

		[Fact]
		public async Task Get_When_Successful_Returns_All_Fruit()
		{
			var response = await client.GetAsync(Program.BaseRoute);
			var fruits = await client.ReadFromJsonAsync<FruitModel[]>(response);

			fruits.Should().NotBeNull();
			fruits.Length.Should().Be(2);

			foreach (var fruit in fruits)
			{
				fruit.Id.Should().NotBeNull();
				fruit.Name.Should().NotBeNull();
				fruit.Stock.Should().BeGreaterThan(0);
			}
		}

		public void Dispose() => client.Dispose();
	}
}