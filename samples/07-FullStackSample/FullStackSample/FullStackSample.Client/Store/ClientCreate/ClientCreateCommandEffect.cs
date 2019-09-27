using System.Threading.Tasks;
using Blazor.Fluxor;
using Blazor.Fluxor.Routing;
using FullStackSample.Api.Models;
using FullStackSample.Client.Services;
using FullStackSample.Client.Store.EntityStateEvents;
using FullStackSample.Client.Store.Main;

namespace FullStackSample.Client.Store.ClientCreate
{
	public class ClientCreateCommandEffect : Effect<Api.Requests.ClientCreateCommand>
	{
		private readonly IApiService ApiService;

		public ClientCreateCommandEffect(IApiService apiService)
		{
			ApiService = apiService;
		}

		protected async override Task HandleAsync(Api.Requests.ClientCreateCommand action, IDispatcher dispatcher)
		{
			try
			{
				var response = 
					await ApiService.Execute<Api.Requests.ClientCreateCommand, Api.Requests.ClientCreateResponse>(action);

				dispatcher.Dispatch(response);
				if (response.Successful)
				{
					var notification = new ClientStateNotification(
						stateUpdateKind: StateUpdateKind.Exists,
						id: response.ClientId,
						name: response.Client.Name,
						registrationNumber: response.Client.RegistrationNumber);
					dispatcher.Dispatch(new ClientStateNotificationsAction(notification));
					dispatcher.Dispatch(new Go("/clients/search/"));
				}
			}
			catch
			{
				dispatcher.Dispatch(new Api.Requests.ClientCreateResponse());
				dispatcher.Dispatch(new NotifyUnexpectedServerErrorStatusChanged(true));
			}
		}
	}
}
