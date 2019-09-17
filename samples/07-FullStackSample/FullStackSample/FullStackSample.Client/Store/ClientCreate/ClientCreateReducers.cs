using Blazor.Fluxor;
using Blazor.Fluxor.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullStackSample.Client.Store.ClientCreate
{
	public static class ClientCreateReducers
	{
		public static ClientCreateState GoReducer(ClientCreateState state, Go action)
			{
				string uri = new Uri(action.NewUri ?? "").AbsolutePath.ToLowerInvariant();
				if (uri.StartsWith("/clients/create"))
					return state;
				return new ClientCreateState(
					client: null,
					isExecutingApi: false,
					errorMessage: null,
					validationErrors: null);
			}

		public static ClientCreateState ClientCreateCommandReducer(
			ClientCreateState state,
			Api.Requests.ClientCreateCommand action) =>
				new ClientCreateState(
								client: state.Client,
								isExecutingApi: true,
								errorMessage: null,
								validationErrors: null);

		public static ClientCreateState ClientCreateResponseReducer(
			ClientCreateState state,
			Api.Requests.ClientCreateResponse action) =>
				new ClientCreateState(
					client: state.Client,
					isExecutingApi: false,
					errorMessage: action.ErrorMessage,
					validationErrors: action.ValidationErrors);
	}
}
