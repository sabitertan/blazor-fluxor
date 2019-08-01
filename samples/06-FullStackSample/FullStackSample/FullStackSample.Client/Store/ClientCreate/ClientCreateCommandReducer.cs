﻿using Blazor.Fluxor;

namespace FullStackSample.Client.Store.ClientCreate
{
	public class ClientCreateCommandReducer : Reducer<ClientCreateState, Api.Requests.ClientCreateCommand>
	{
		public override ClientCreateState Reduce(ClientCreateState state, Api.Requests.ClientCreateCommand action)
			=> new ClientCreateState(
				client: state.Client,
				isExecutingApi: true,
				errorMessage: null,
				validationErrors: null);
	}
}