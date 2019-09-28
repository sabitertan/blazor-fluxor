namespace FullStackSample.Client.Store.EntityStateEvents
{
	public abstract class ObjectStateChangesBase<TKey>
	{
		public TKey Id { get; }
		public StateUpdateKind StateUpdateKind { get; }

		public ObjectStateChangesBase(TKey id, StateUpdateKind stateUpdateKind)
		{
			Id = id;
			StateUpdateKind = stateUpdateKind;
		}
	}
}
