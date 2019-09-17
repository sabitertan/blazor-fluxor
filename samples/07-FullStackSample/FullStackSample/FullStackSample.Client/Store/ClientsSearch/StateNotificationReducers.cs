using FullStackSample.Client.Store.EntityStateEvents;
using System.Linq;
using FullStackSample.Client.Extensions;
using Blazor.Fluxor;

namespace FullStackSample.Client.Store.ClientsSearch
{
	public static class StateNotificationReducers
	{
		[ReducerMethod]
		public static ClientsSearchState ClientStateNotificationReducer(
			ClientsSearchState state,
			ClientStateNotification action)
		{
			var clients = state.Clients.UpdateState(action);
			if (state.Name != null)
			{
				string searchName = state.Name.ToLowerInvariant();
				clients = clients
					.Where(x => (x.Name ?? "").ToLowerInvariant().Contains(searchName));
			}
			return new ClientsSearchState(
				isSearching: state.IsSearching,
				name: state.Name,
				errorMessage: state.ErrorMessage,
				clients: clients);
		}
	}
}
