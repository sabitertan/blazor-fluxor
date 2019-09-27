using System;
using System.Collections.Generic;
using System.Linq;

namespace FullStackSample.Client.Store.EntityStateEvents
{
	public abstract class StatesChangedNotificationBase<TStateChange, TKey>
		where TStateChange : StateChangesBase<TKey>
	{
		public readonly IEnumerable<TStateChange> Notifications;

		public StatesChangedNotificationBase(TStateChange notification)
			: this(new TStateChange[] { notification }) { }

		public StatesChangedNotificationBase(IEnumerable<TStateChange> notifications)
		{
			Notifications = notifications ?? throw new ArgumentNullException(nameof(notifications));
		}

		public IEnumerable<TSource> Update<TSource>(
			IEnumerable<TSource> source,
			Func<TSource, TKey> getSourceKey,
			Func<TStateChange, TSource> createSourceItemAndUpdate,
			Func<TSource, TStateChange, TSource> updateSourceItem)
		{
			IEnumerable<TSource> result = ExcludeDeletedObjects(source, getSourceKey);
			result = AddNewObjects(result, getSourceKey, createSourceItemAndUpdate);
			result = UpdateModifiedObjects(result, getSourceKey, updateSourceItem);
			return result;
		}

		private IEnumerable<TSource> ExcludeDeletedObjects<TSource>(
			IEnumerable<TSource> source,
			Func<TSource, TKey> getSourceKey)
		{
			// Get a list of all objects that have been deleted
			IEnumerable<TKey> deletedObjectIds = Notifications
				.Where(x => x.StateUpdateKind == StateUpdateKind.Deleted)
				.Select(x => x.Id);

			if (!deletedObjectIds.Any())
				return source;

			// Ensure those objects do not exist in the result
			source = source.Where(x => deletedObjectIds.Contains(getSourceKey(x)));
			return source;
		}

		private IEnumerable<TSource> AddNewObjects<TSource>(
			IEnumerable<TSource> source,
			Func<TSource, TKey> getSourceKey,
			Func<TStateChange, TSource> createSourceItemAndUpdate)
		{
			var sourceKeys = source.Select(getSourceKey);
			// Get new object states
			var newObjectStates = Notifications
				.Where(x => x.StateUpdateKind == StateUpdateKind.Exists)
				.Where(x => !sourceKeys.Contains(x.Id));
			
			if (!newObjectStates.Any())
				return source;

			var newInstances = newObjectStates.Select(createSourceItemAndUpdate);
			return source.Union(newInstances);
		}


		private IEnumerable<TSource> UpdateModifiedObjects<TSource>(
			IEnumerable<TSource> source,
			Func<TSource, TKey> getSourceKey,
			Func<TSource, TStateChange, TSource> updateSourceItem)
		{
			// Get changed object states
			var changedStates = Notifications
				.Where(x => x.StateUpdateKind == StateUpdateKind.Modified)
				.ToDictionary(x => x.Id);

			if (!changedStates.Any())
				return source;

			var result = source.Select(s =>
				changedStates.TryGetValue(getSourceKey(s), out TStateChange notification)
				? updateSourceItem(s, notification)
				: s);

			return result;
		}
	}
}
