// <copyright file="IIdFactory.cs" company="Teqniqly">
// Copyright (c) Teqniqly. All rights reserved.
// </copyright>

namespace FruitApi.Services
{
	public interface IIdFactory
	{
		public string CreateId();
	}

	public class IdFactory : IIdFactory
	{
		public string CreateId() => Guid.NewGuid().ToString("N");
	}
}