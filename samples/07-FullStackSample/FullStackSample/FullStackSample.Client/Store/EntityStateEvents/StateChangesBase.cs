namespace FullStackSample.Client.Store.EntityStateEvents
{
	public abstract class StateChangesBase<TKey>
	{
		public TKey Id { get; set; }
		public StateUpdateKind StateUpdateKind { get; set;  }

		protected StateChangesBase() { }

		protected StateChangesBase(StateUpdateKind stateUpdateKind, TKey id)
		{
			StateUpdateKind = stateUpdateKind;
			Id = id;
		}
	}
}
