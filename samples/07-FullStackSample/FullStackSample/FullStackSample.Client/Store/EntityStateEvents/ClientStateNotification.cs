namespace FullStackSample.Client.Store.EntityStateEvents
{
	public class ClientStateNotification : StateNotficationBase<int>
	{
		public PropertyUpdate<string> Name { get; set; }
		public PropertyUpdate<int> RegistrationNumber { get; set; }

		public ClientStateNotification() : base() { }

		public ClientStateNotification(
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
