using System;

namespace Blazor.Fluxor
{
	public interface IStoreInitializer
	{
		void Initialize(Action completed);
	}
}
