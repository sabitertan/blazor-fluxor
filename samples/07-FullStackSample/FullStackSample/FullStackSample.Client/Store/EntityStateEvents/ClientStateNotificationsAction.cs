using FullStackSample.Api.Models;
using System.Collections.Generic;

namespace FullStackSample.Client.Store.EntityStateEvents
{
	public class ClientStateNotificationsAction : StateNotificationsActionBase<ClientStateNotification, int>
	{
		public ClientStateNotificationsAction(ClientStateNotification notification)
			: base(notification) { }

		public ClientStateNotificationsAction(IEnumerable<ClientStateNotification> notifications)
			: base(notifications) { }

		public IEnumerable<ClientSummaryDto> Update(IEnumerable<ClientSummaryDto> source) =>
			Update(
				source: source,
				getSourceKey: x => x.Id,
				createSourceItemAndUpdate: CreateAndRemapClientSummaryDto,
				updateSourceItem: UpdateSourceItem);

		private ClientSummaryDto CreateAndRemapClientSummaryDto(ClientStateNotification notification) =>
			UpdateSourceItem(null, notification);

		private ClientSummaryDto UpdateSourceItem(ClientSummaryDto state, ClientStateNotification notification) =>
			new ClientSummaryDto(
				id: notification.Id,
				name: notification.Name.GetUpdatedValue(state?.Name));


	}
}
