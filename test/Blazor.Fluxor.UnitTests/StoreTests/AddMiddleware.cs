using Blazor.Fluxor.UnitTests.SupportFiles;
using Moq;
using Xunit;

namespace Blazor.Fluxor.UnitTests.StoreTests
{
	public partial class StoreTests
	{
		public class AddMiddleware
		{
			TestStoreInitializer StoreInitializer;

			[Fact]
			public void ActivatesMiddleware_WhenPageHasAlreadyLoaded()
			{
				var subject = new Store(StoreInitializer);
				subject.Initialize();
				StoreInitializer.Complete();

				var mockMiddleware = new Mock<IMiddleware>();
				subject.AddMiddleware(mockMiddleware.Object);

				mockMiddleware
					.Verify(x => x.Initialize(subject));
			}

			[Fact]
			public void CallsAfterInitializeAllMiddlewares_WhenPageHasAlreadyLoaded()
			{
				var subject = new Store(StoreInitializer);
				subject.Initialize();
				StoreInitializer.Complete();

				var mockMiddleware = new Mock<IMiddleware>();
				subject.AddMiddleware(mockMiddleware.Object);

				mockMiddleware
					.Verify(x => x.AfterInitializeAllMiddlewares());
			}

			public AddMiddleware()
			{
				StoreInitializer = new TestStoreInitializer();
			}
		}
	}
}
