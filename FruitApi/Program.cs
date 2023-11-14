// <copyright file="Program.cs" company="Teqniqly">
// Copyright (c) Teqniqly. All rights reserved.
// </copyright>

internal class Program
{
	private static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		var app = builder.Build();

		app.MapGet("/", () => "Hello World!");

		app.Run();
	}
}