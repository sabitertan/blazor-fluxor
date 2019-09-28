using FullStackSample.Api.Models;
using FullStackSample.Client.Store.EntityStateEvents;
using System.Collections.Generic;

namespace FullStackSample.Client.Store.StateNotificationReducers
{
	public static class ClientSummaryDtoStateChangesReducer
	{
		public static IEnumerable<ClientSummaryDto> Update(
			IEnumerable<ClientSummaryDto> source,
			IEnumerable<ClientStateChanges> stateChanges) =>
			CollectionReducer.Update(
				stateChanges: stateChanges,
				source: source,
				getSourceKey: x => x.Id,
				createSourceItemAndUpdate: CreateAndRemapClientSummaryDto,
				updateSourceItem: UpdateSourceItem);

		private static ClientSummaryDto CreateAndRemapClientSummaryDto(ClientStateChanges notification) =>
			UpdateSourceItem(null, notification);

		private static ClientSummaryDto UpdateSourceItem(ClientSummaryDto state, ClientStateChanges notification) =>
			new ClientSummaryDto(
				id: notification.Id,
				name: notification.Name.GetUpdatedValue(state?.Name));
	}
}
