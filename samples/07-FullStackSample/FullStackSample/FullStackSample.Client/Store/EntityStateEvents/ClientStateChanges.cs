namespace FullStackSample.Client.Store.EntityStateEvents
{
	public class ClientStateChanges : StateChangesBase<int>
	{
		public PropertyUpdate<string> Name { get; set; }
		public PropertyUpdate<int> RegistrationNumber { get; set; }

		public ClientStateChanges(int clientId, StateUpdateKind stateUpdateKind)
			: base(clientId, stateUpdateKind) { }
	}
}
