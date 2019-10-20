using System;

namespace Blazor.Fluxor.UnitTests.SupportFiles
{
	public class TestStoreInitializer : IStoreInitializationStrategy
	{
		Action Completed;

		public void Initialize(Action completed)
		{
			Completed = completed;
		}

		public void Complete() => Completed();
	}
}
