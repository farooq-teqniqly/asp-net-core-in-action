// <copyright file="Fakes.cs" company="Teqniqly">
// Copyright (c) Teqniqly. All rights reserved.
// </copyright>

namespace FruitApi.IntegrationTests
{
	using Services;

	public class Fakes
	{
		public class FakeIdFactory : IIdFactory
		{
			public string CreateId() => Guid.NewGuid().ToString("N");
		}
	}
}