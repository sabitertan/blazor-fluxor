using FullStackSample.Api.Models;
using FullStackSample.Client.Store.EntityStateEvents;
using System.Collections.Generic;

namespace FullStackSample.Client.Store.StateNotificationReducers
{
	public static class ClientSummaryDtoStateChangesReducer
	{
		public static IEnumerable<ClientSummaryDto> ReduceCollection(
			IEnumerable<ClientSummaryDto> source,
			IEnumerable<ClientStateChanges> stateChanges) =>
			StateCollectionReducer.ReduceCollection(
				stateChanges: stateChanges,
				source: source,
				getSourceKey: x => x.Id,
				reduce: ReduceObject);

		private static ClientSummaryDto ReduceObject(ClientSummaryDto state, ClientStateChanges notification) =>
			new ClientSummaryDto(
				id: notification.Id,
				name: notification.Name.GetUpdatedValue(state?.Name));
	}
}
