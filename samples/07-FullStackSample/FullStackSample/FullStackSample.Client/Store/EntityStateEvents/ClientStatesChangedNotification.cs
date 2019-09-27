using FullStackSample.Api.Models;
using System.Collections.Generic;

namespace FullStackSample.Client.Store.EntityStateEvents
{
	public class ClientStatesChangedNotification : StatesChangedNotificationBase<ClientStateChanges, int>
	{
		public ClientStatesChangedNotification(ClientStateChanges notification)
			: base(notification) { }

		public ClientStatesChangedNotification(IEnumerable<ClientStateChanges> notifications)
			: base(notifications) { }

		public IEnumerable<ClientSummaryDto> Update(IEnumerable<ClientSummaryDto> source) =>
			Update(
				source: source,
				getSourceKey: x => x.Id,
				createSourceItemAndUpdate: CreateAndRemapClientSummaryDto,
				updateSourceItem: UpdateSourceItem);

		private ClientSummaryDto CreateAndRemapClientSummaryDto(ClientStateChanges notification) =>
			UpdateSourceItem(null, notification);

		private ClientSummaryDto UpdateSourceItem(ClientSummaryDto state, ClientStateChanges notification) =>
			new ClientSummaryDto(
				id: notification.Id,
				name: notification.Name.GetUpdatedValue(state?.Name));


	}
}
