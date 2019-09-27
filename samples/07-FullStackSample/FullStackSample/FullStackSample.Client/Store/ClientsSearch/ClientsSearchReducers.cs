using Blazor.Fluxor;
using Blazor.Fluxor.Routing;
using FullStackSample.Client.Store.EntityStateEvents;
using System;

namespace FullStackSample.Client.Store.ClientsSearch
{
	public class ClientsSearchReducers
	{
		[ReducerMethod]
		public static ClientsSearchState ReduceGo(ClientsSearchState state, Go action)
		{
				string uri = new Uri(action.NewUri ?? "").AbsolutePath.ToLowerInvariant();
				if (uri.StartsWith("/clients"))
					return state;
				return ClientsSearchState.Default;
		}

		[ReducerMethod]
		public static ClientsSearchState ReduceClientsSearchQuery(
			ClientsSearchState state,
			Api.Requests.ClientsSearchQuery action) =>
				new ClientsSearchState(
					isSearching: true,
					name: action.Name,
					errorMessage: null,
					clients: null);

		[ReducerMethod]
		public static ClientsSearchState ReduceClientsSearchResponse(
			ClientsSearchState state,
			Api.Requests.ClientsSearchResponse action) =>
				new ClientsSearchState(
						isSearching: false,
						name: state.Name,
						errorMessage: action.ErrorMessage,
						clients: action.Clients);

		[ReducerMethod]
		public static ClientsSearchState ReduceClientsStateNotificationsAction(
			ClientsSearchState state,
			ClientStatesChangedNotification action) =>
				new ClientsSearchState(
					isSearching: state.IsSearching,
					errorMessage: state.ErrorMessage,
					name: state.Name,
					clients: action.Update(state.Clients));
	}
}
