// <copyright file="PostEndpointIntegrationTests.cs" company="Teqniqly">
// Copyright (c) Teqniqly. All rights reserved.
// </copyright>

namespace FruitApi.IntegrationTests
{
	using FluentAssertions;
	using System;
	using System.Net;
	using System.Threading.Tasks;

	public class PostEndpointIntegrationTests : IClassFixture<CustomWebApplicationFactory>, IDisposable
	{
		private readonly ApiTestClient client;
		private readonly object postRequestBody = new { name = "Banana", Stock = 10 };

		public PostEndpointIntegrationTests(CustomWebApplicationFactory fixture)
			=> client = new ApiTestClient(fixture.CreateClient());

		[Fact]
		public async Task Post_When_Successful_Returns_Created_Status_Code()
		{
			var response = await client.PostAsync("/fruit", postRequestBody);

			response.StatusCode.Should().Be(HttpStatusCode.Created);
		}

		[Fact]
		public async Task Post_When_Successful_Returns_Id_In_Location_Header()
		{
			var response = await client.PostAsync("/fruit", postRequestBody);
			var location = response.Headers.Location;

			location.Should().NotBeNull();
			location.ToString().Split("/")[1].Length.Should().BeGreaterThan(1);
		}

		public void Dispose() => client.Dispose();
	}
}