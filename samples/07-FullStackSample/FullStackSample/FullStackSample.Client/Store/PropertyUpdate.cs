namespace FullStackSample.Client.Store
{
	public sealed class PropertyUpdate<T>
	{
		public readonly bool Updated;
		internal readonly T Value;

		public static implicit operator PropertyUpdate<T>(T value) => new PropertyUpdate<T>(value);

		private PropertyUpdate(T value)
		{
			Updated = true;
			Value = value;
		}

		public override string ToString() =>
			Updated ? null : Value?.ToString();
	}

	public static class PropertyUpdateExtensions
	{
		public static T GetValueOrDefault<T>(this PropertyUpdate<T> update) =>
			update.GetUpdatedValue(default(T));

		public static T GetUpdatedValue<T>(this PropertyUpdate<T> update, T originalValue) =>
			(update != null && update.Updated) ? update.Value : originalValue;
	}
}
