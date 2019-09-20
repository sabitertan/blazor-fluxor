﻿using FluentValidation;
using FluentValidation.Validators;
using FullStackSample.Api.Models;
using FullStackSample.Api.Requests;
using FullStackSample.Client.Services;
using System.Threading;
using System.Threading.Tasks;

namespace FullStackSample.Client.Validators
{
	public class ClientCreateDtoValidator : AbstractValidator<ClientCreateDto>
	{
		private readonly IApiService ApiService;

		public ClientCreateDtoValidator(IApiService apiService)
		{
			ApiService = apiService;

			When(x => !string.IsNullOrEmpty(x.Name), () =>
			{
				RuleFor(x => x.Name)
					.MustAsync(HaveUniqueName)
					.WithMessage("Name must be unique");
			});

			When(x => x.RegistrationNumber != 0, () =>
			{
				RuleFor(x => x.RegistrationNumber)
					.MustAsync(HaveUniqueRegistrationNumber)
					.WithMessage("Registration number must be unique");
			});
		}

		private async Task<bool> HaveUniqueName(
			ClientCreateDto client,
			string name,
			PropertyValidatorContext context,
			CancellationToken cancellationToken)
		{
			int? clientIdToIgnore = 
				client.Id == 0
				? (int?)null
				: client.Id;

			var apiQuery = new ClientIsNameAvailableQuery(
				clientIdToIgnore: clientIdToIgnore,
				name: name);
			var apiResponse = 
				await ApiService.Execute<ClientIsNameAvailableQuery, ClientIsNameAvailableResponse>(apiQuery);
			return apiResponse.Available;
		}

		private async Task<bool> HaveUniqueRegistrationNumber(
			ClientCreateDto client,
			int registrationNumber,
			PropertyValidatorContext context,
			CancellationToken cancellationToken)
		{
			int? clientIdToIgnore =
				client.Id == 0
				? (int?)null
				: client.Id;

			var apiQuery = new ClientIsRegistrationNumberAvailableQuery(
				clientIdToIgnore: clientIdToIgnore,
				registrationNumber: registrationNumber);
			var apiResponse =
				await ApiService.Execute<ClientIsRegistrationNumberAvailableQuery, ClientIsRegistrationNumberAvailableResponse>(apiQuery);
			return apiResponse.Available;
		}
	}
}