using Microsoft.JSInterop;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Blazor.Fluxor
{
	public class JavaScriptStoreInitializer : IStoreInitializer
	{
		public readonly IJSRuntime JSRuntime;

		public JavaScriptStoreInitializer(IJSRuntime jSRuntime)
		{
			JSRuntime = jSRuntime;
		}

		public void Initialize(Action completed)
		{
			new Timer(async state =>
			{
				bool success = false;
				while (!success)
				{
					try
					{
						success = await JSRuntime.InvokeAsync<bool>("TryInitializeFluxor");
					}
					catch (JSException err)
					{
#if DEBUG
						System.Diagnostics.Debug.WriteLine("JSRuntime not ready, trying again: " + err.Message);
#endif
					}
					if (!success)
						await Task.Yield();
				}
				completed();
			}, null, 0, 0);
		}
	}
}
