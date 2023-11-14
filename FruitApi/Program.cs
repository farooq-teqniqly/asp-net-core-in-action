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

		app.UseRouting();

		app.MapPost("/fruit", (NewFruitModel model) =>
		{
			var id = Guid.NewGuid().ToString("N");
			return TypedResults.Created($"/fruit/{id}", model);
		});

		app.Run();
	}
}