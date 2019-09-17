using Blazor.Fluxor;
using Blazor.Fluxor.Routing;
using System;

namespace FullStackSample.Client.Store.ClientsSearch
{
	public class ClientsSearchReducers
	{
		public static ClientsSearchState GoReducer(ClientsSearchState state, Go action)
		{
				string uri = new Uri(action.NewUri ?? "").AbsolutePath.ToLowerInvariant();
				if (uri.StartsWith("/clients"))
					return state;
				return ClientsSearchState.Default;
		}

		public static ClientsSearchState ClientsSearchQueryReducer(
			ClientsSearchState state,
			Api.Requests.ClientsSearchQuery action) =>
				new ClientsSearchState(
					isSearching: true,
					name: action.Name,
					errorMessage: null,
					clients: null);

		public static ClientsSearchState ClientsSearchResponseReducer(
			ClientsSearchState state,
			Api.Requests.ClientsSearchResponse action) =>
				new ClientsSearchState(
						isSearching: false,
						name: state.Name,
						errorMessage: action.ErrorMessage,
						clients: action.Clients);
	}
}
