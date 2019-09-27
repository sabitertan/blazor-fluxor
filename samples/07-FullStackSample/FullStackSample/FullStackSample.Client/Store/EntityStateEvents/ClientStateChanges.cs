namespace FullStackSample.Client.Store.EntityStateEvents
{
	public class ClientStateChanges : StateChangesBase<int>
	{
		public PropertyUpdate<string> Name { get; set; }
		public PropertyUpdate<int> RegistrationNumber { get; set; }

		public ClientStateChanges() : base() { }

		public ClientStateChanges(
			StateUpdateKind stateUpdateKind,
			int id,
			PropertyUpdate<string> name,
			PropertyUpdate<int> registrationNumber)
			: base(stateUpdateKind, id)
		{
			Name = name;
			RegistrationNumber = registrationNumber;
		}
	}
}
