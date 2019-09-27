namespace FullStackSample.Client.Store.EntityStateEvents
{
	public abstract class StateNotficationBase<TKey>
	{
		public TKey Id { get; set; }
		public StateUpdateKind StateUpdateKind { get; set;  }

		public StateNotficationBase() { }

		public StateNotficationBase(StateUpdateKind stateUpdateKind, TKey id)
		{
			StateUpdateKind = stateUpdateKind;
			Id = id;
		}
	}
}
