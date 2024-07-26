using LoanGatewayAdmin.Models;
using LoanGatewayShared.Models;

namespace LoanGatewayAdmin.Services
{
	public class LoanAdminService : ILoanAdminService
	{
		public Task<LoanApplication> GetLoanApplicationAsync(string arn)
		{
			if (string.IsNullOrEmpty(arn))
			{
				throw new ArgumentNullException(nameof(arn));
			}

			return Task.FromResult(new LoanApplication());
		}

		public Task<List<LoanApplication>> GetLoanApplicationsAsync()
		{
			var applications = new List<LoanApplication>();

			// TBD get from DB
			

			return Task.FromResult(applications);
		}

		public Task<LoanApplication> UpdateStatus(LoanApplication application, LoanApplicationUpdate loanApplicationUpdate)
		{	


			throw new NotImplementedException();
		}
	}
}
