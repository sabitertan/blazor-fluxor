namespace FullStackSample.Client.Store.EntityStateEvents
{
	public abstract class StateChangesBase<TKey>
	{
		public TKey Id { get; }
		public StateUpdateKind StateUpdateKind { get; }

		public StateChangesBase(TKey id, StateUpdateKind stateUpdateKind)
		{
			Id = id;
			StateUpdateKind = stateUpdateKind;
		}
	}
}
