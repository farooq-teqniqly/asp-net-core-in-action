// <copyright file="Program.cs" company="Teqniqly">
// Copyright (c) Teqniqly. All rights reserved.
// </copyright>

using FruitApi.Models;

public class Program
{
	private static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		var app = builder.Build();
		const string baseRoute = "/fruit";

		app.UseRouting();

		app.MapPost(baseRoute, (NewFruitModel model) =>
		{
			var id = Guid.NewGuid().ToString("N");
			return TypedResults.Created($"{baseRoute}/{id}", model);
		});

		app.Run();
	}
}