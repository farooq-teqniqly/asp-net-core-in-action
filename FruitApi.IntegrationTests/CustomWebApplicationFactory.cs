// <copyright file="CustomWebApplicationFactory.cs" company="Teqniqly">
// Copyright (c) Teqniqly. All rights reserved.
// </copyright>

namespace FruitApi.IntegrationTests
{
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.Mvc.Testing;

	public class CustomWebApplicationFactory : WebApplicationFactory<Program>
	{
		protected override void ConfigureWebHost(IWebHostBuilder builder) => base.ConfigureWebHost(builder);
	}
}