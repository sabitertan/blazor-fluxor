using FullStackSample.Api.Requests;
using FullStackSample.Server.DomainLayer.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FullStackSample.Server.DomainLayer.RequestHandlers
{
	public class ClientIsRegistrationNumberAvailableQueryHandler : IRequestHandler<ClientIsRegistrationNumberAvailableQuery, ClientIsRegistrationNumberAvailableResponse>
	{
		private readonly FullStackDbContext DbContext;

		public ClientIsRegistrationNumberAvailableQueryHandler(FullStackDbContext dbContext)
		{
			DbContext = dbContext;
		}

		public async Task<ClientIsRegistrationNumberAvailableResponse> Handle(ClientIsRegistrationNumberAvailableQuery query, CancellationToken cancellationToken)
		{
			IQueryable<Entities.Client> dbQuery = DbContext.Clients
				.Where(x => x.RegistrationNumber == query.RegistrationNumber);
			if (query.ClientIdToIgnore.HasValue)
				dbQuery = dbQuery.Where(x => x.Id != query.ClientIdToIgnore.Value);

			bool found = await dbQuery.AnyAsync();
			return new ClientIsRegistrationNumberAvailableResponse(available: !found);
		}
	}
}
