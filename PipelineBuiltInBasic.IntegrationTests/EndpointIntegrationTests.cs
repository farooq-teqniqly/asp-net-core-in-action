// <copyright file="EndpointIntegrationTests.cs" company="Teqniqly">
// Copyright (c) Teqniqly. All rights reserved.
// </copyright>

namespace PipelineBuiltInBasic.IntegrationTests
{
	using FluentAssertions;

	public class EndpointIntegrationTests : IClassFixture<CustomWebApplicationFactory>, IDisposable
	{
		private readonly HttpClient client;

		public EndpointIntegrationTests(CustomWebApplicationFactory fixture) => client = fixture.CreateClient();

		[Fact]
		public async Task Can_Call_Hello_World_Endpoint()
		{
			var response = await client.GetAsync("/hello");
			response.EnsureSuccessStatusCode();

			var content = await response.Content.ReadAsStringAsync();
			content.Should().Be("Hello World!");
		}

		[Fact]
		public async Task Can_Browse_The_Welcome_Page()
		{
			var response = await client.GetAsync("/");
			response.EnsureSuccessStatusCode();

			var content = await response.Content.ReadAsStringAsync();
			content.Should().Contain("Your ASP.NET Core application has been successfully started");
		}

		public void Dispose() => client.Dispose();
	}
}