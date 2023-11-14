// <copyright file="Program.cs" company="Teqniqly">
// Copyright (c) Teqniqly. All rights reserved.
// </copyright>

namespace FruitApi
{
	using FruitApi.Models;
	using Services;

	public class Program
	{
		public const string BaseRoute = "/fruit";

		private static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddProblemDetails();

			var app = builder.Build();

			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler();
			}

			app.UseStatusCodePages();
			app.UseRouting();

			app.MapGet(BaseRoute, () =>
			{
				var fruits = new FruitModel[]
				{
				new("ban", "Banana", 10),
				new("app", "Apple", 20)
				};

				return TypedResults.Ok(fruits);
			});

			app.MapPost(BaseRoute, (NewFruitModel model, IIdFactory idFactory) =>
			{
				var id = idFactory.CreateId();
				return TypedResults.Created($"{BaseRoute}/{id}", new FruitModel(id, "Banana", 10));
			});

			app.Run();
		}
	}
}