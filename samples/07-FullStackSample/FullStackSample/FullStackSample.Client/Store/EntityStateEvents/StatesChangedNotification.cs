using System;
using System.Collections.Generic;

namespace FullStackSample.Client.Store.EntityStateEvents
{
	public sealed class StatesChangedNotification<TObjectStateChanges, TKey>
		where TObjectStateChanges : ObjectStateChangesBase<TKey>
	{
		public readonly IEnumerable<TObjectStateChanges> Notifications;

		public StatesChangedNotification(TObjectStateChanges notification)
			: this(new TObjectStateChanges[] { notification }) { }

		public StatesChangedNotification(IEnumerable<TObjectStateChanges> notifications)
		{
			Notifications = notifications ?? throw new ArgumentNullException(nameof(notifications));
		}
	}
}
