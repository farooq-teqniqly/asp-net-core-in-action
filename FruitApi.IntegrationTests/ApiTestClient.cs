// <copyright file="ApiTestClient.cs" company="Teqniqly">
// Copyright (c) Teqniqly. All rights reserved.
// </copyright>

namespace FruitApi.IntegrationTests
{
	using System.Text;
	using System.Text.Json;

	public class ApiTestClient
	{
		private readonly HttpClient client;

		public ApiTestClient(HttpClient httpClient) => client = httpClient;

		public async Task<HttpResponseMessage> PostAsync(string endpoint, object data)
		{
			var json = JsonSerializer.Serialize(data);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			return await client.PostAsync(endpoint, content);
		}

		public async Task<HttpResponseMessage> GetAsync(string endpoint) => await client.GetAsync(endpoint);

		public void Dispose() => client.Dispose();
	}
}