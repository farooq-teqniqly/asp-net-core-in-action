// <copyright file="CustomWebApplicationFactory.cs" company="Teqniqly">
// Copyright (c) Teqniqly. All rights reserved.
// </copyright>

namespace FruitApi.IntegrationTests
{
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.Mvc.Testing;
	using Microsoft.AspNetCore.TestHost;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.DependencyInjection.Extensions;
	using Services;

	public class CustomWebApplicationFactory : WebApplicationFactory<Program>
	{
		protected override void ConfigureWebHost(IWebHostBuilder builder)
			=> builder.ConfigureTestServices(s =>
			{
				s.RemoveAll<IIdFactory>();
				s.AddScoped<IIdFactory, Fakes.FakeIdFactory>();
			});
	}
}