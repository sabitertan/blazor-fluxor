using Blazor.Fluxor.UnitTests.SupportFiles;
using Moq;
using Xunit;

namespace Blazor.Fluxor.UnitTests.StoreTests
{
	public partial class StoreTests
	{
		public class Initialize
		{
			TestStoreInitializer StoreInitializer;

			[Fact]
			public void ActivatesMiddleware_WhenStoreInitializerCompletes()
			{
				var subject = new Store(StoreInitializer);
				subject.Initialize();
				var mockMiddleware = new Mock<IMiddleware>();
				subject.AddMiddleware(mockMiddleware.Object);

				StoreInitializer.Complete();

				mockMiddleware
					.Verify(x => x.Initialize(subject));
			}

			[Fact]
			public void CallsAfterInitializeAllMiddlewares_WhenStoreInitializerCompletes()
			{
				var subject = new Store(StoreInitializer);
				subject.Initialize();
				var mockMiddleware = new Mock<IMiddleware>();
				subject.AddMiddleware(mockMiddleware.Object);

				StoreInitializer.Complete();

				mockMiddleware
					.Verify(x => x.AfterInitializeAllMiddlewares());
			}

			public Initialize()
			{
				StoreInitializer = new TestStoreInitializer();
			}
		}
	}
}