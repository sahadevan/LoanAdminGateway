using LoanGatewayShared.Models;

namespace LoanGatewayAdmin.Services
{
	public interface ILoanAdminService
	{

		Task<List<LoanApplication>> GetLoanApplicationsAsync();

		Task<LoanApplication> GetLoanApplicationAsync(string arn);

		Task<bool> UpdateStatus(StatusUpdateRequest statusUpdateRequest);

	}
}
