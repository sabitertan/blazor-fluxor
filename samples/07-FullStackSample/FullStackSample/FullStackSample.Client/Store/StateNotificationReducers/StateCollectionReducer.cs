using FullStackSample.Client.Store.EntityStateEvents;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FullStackSample.Client.Store.StateNotificationReducers
{
	internal static class StateCollectionReducer
	{
		internal static IEnumerable<TSource> ReduceCollection<TSource, TKey, TObjectStateChanges>(
			IEnumerable<TObjectStateChanges> stateChanges,
			IEnumerable<TSource> source,
			Func<TSource, TKey> getSourceKey,
			Func<TSource, TObjectStateChanges, TSource> reduce)
			where TObjectStateChanges : ObjectStateChangesBase<TKey>
		{
			IEnumerable<TSource> result = ExcludeDeletedObjects(stateChanges, source, getSourceKey);
			result = AddNewObjects(stateChanges, result, getSourceKey, reduce);
			result = UpdateModifiedObjects(stateChanges, result, getSourceKey, reduce);
			return result;
		}

		private static IEnumerable<TSource> ExcludeDeletedObjects<TSource, TKey, TObjectStateChanges>(
			IEnumerable<TObjectStateChanges> stateChanges,
			IEnumerable<TSource> source,
			Func<TSource, TKey> getSourceKey)
			where TObjectStateChanges : ObjectStateChangesBase<TKey>
		{
			// Get a list of all objects that have been deleted
			IEnumerable<TKey> deletedObjectIds = stateChanges
				.Where(x => x.StateUpdateKind == StateUpdateKind.Deleted)
				.Select(x => x.Id);

			if (!deletedObjectIds.Any())
				return source;

			// Ensure those objects do not exist in the result
			source = source.Where(x => deletedObjectIds.Contains(getSourceKey(x)));
			return source;
		}

		private static IEnumerable<TSource> AddNewObjects<TSource, TKey, TObjectStateChanges>(
			IEnumerable<TObjectStateChanges> stateChanges,
			IEnumerable<TSource> source,
			Func<TSource, TKey> getSourceKey,
			Func<TSource, TObjectStateChanges, TSource> reduce)
			where TObjectStateChanges : ObjectStateChangesBase<TKey>
		{
			var sourceKeys = source.Select(getSourceKey);
			// Get new object states
			var newObjectStates = stateChanges
				.Where(x => x.StateUpdateKind == StateUpdateKind.Exists)
				.Where(x => !sourceKeys.Contains(x.Id));

			if (!newObjectStates.Any())
				return source;

			TSource defaultSourceState = default(TSource);
			var newInstances = newObjectStates.Select(x => reduce(defaultSourceState, x));
			return source.Union(newInstances);
		}


		private static IEnumerable<TSource> UpdateModifiedObjects<TSource, TKey, TObjectStateChanges>(
			IEnumerable<TObjectStateChanges> stateChanges,
			IEnumerable<TSource> source,
			Func<TSource, TKey> getSourceKey,
			Func<TSource, TObjectStateChanges, TSource> updateSourceItem)
			where TObjectStateChanges : ObjectStateChangesBase<TKey>
		{
			// Get changed object states
			var changedStates = stateChanges
				.Where(x => x.StateUpdateKind == StateUpdateKind.Modified)
				.ToDictionary(x => x.Id);

			if (!changedStates.Any())
				return source;

			var result = source.Select(s =>
				changedStates.TryGetValue(getSourceKey(s), out TObjectStateChanges notification)
				? updateSourceItem(s, notification)
				: s);

			return result;
		}
	}
}
