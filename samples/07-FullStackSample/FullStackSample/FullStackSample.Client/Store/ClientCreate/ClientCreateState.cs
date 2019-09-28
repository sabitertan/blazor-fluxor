using FullStackSample.Api.Models;
using System;
using System.Collections.Generic;

namespace FullStackSample.Client.Store.ClientCreate
{
	public class ClientCreateState
	{
		public bool IsExecutingApi { get; }
		public string ErrorMessage { get; }
		public ClientCreateDto Client { get; }
		public IEnumerable<KeyValuePair<string, string>> ValidationErrors { get; }

		public ClientCreateState(
			ClientCreateDto client,
			bool isExecutingApi,
			string errorMessage,
			IEnumerable<KeyValuePair<string, string>> validationErrors)
		{
			Client = client ?? new ClientCreateDto();
			IsExecutingApi = isExecutingApi;
			ErrorMessage = errorMessage;
			ValidationErrors = validationErrors ?? Array.Empty<KeyValuePair<string, string>>();
		}
	}
}
