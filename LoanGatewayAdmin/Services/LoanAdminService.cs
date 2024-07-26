using LoanGateway.Models;
using LoanGatewayAdmin.Models;
using LoanGatewayShared.Models;
using Newtonsoft.Json;
using RestSharp;

namespace LoanGatewayAdmin.Services
{
	public class LoanAdminService : ILoanAdminService
	{
		private readonly LoanGatewayServiceOptions _loanGatewayServiceOptions;

		private readonly RestClient _loanGatewayClient;	

		public LoanAdminService(LoanGatewayServiceOptions loanGatewayServiceOptions) 
		{
			_loanGatewayServiceOptions = loanGatewayServiceOptions;

			var options = new RestClientOptions(_loanGatewayServiceOptions.Url);

			_loanGatewayClient = new RestClient(options);
		}

		public async Task<LoanApplication> GetLoanApplicationAsync(string arn)
		{
			if (string.IsNullOrEmpty(arn))
			{
				throw new ArgumentNullException(nameof(arn));
			}

			var applications = await GetLoanApplicationsAsync();

			if(applications.Any())
			{
				return applications.Find(app => string.Equals(arn, app.Arn, StringComparison.InvariantCultureIgnoreCase));
			}

			return null;
		}

		public async Task<List<LoanApplication>> GetLoanApplicationsAsync()
		{
			var applications = new List<LoanApplication>();

			var request = new RestRequest(_loanGatewayServiceOptions.LoanApplicationsEndPoint);

			var response = await _loanGatewayClient.GetAsync(request);

			var apiResponse = JsonConvert.DeserializeObject<ApiResponse<UserRequestHistory, string>>(response.Content);

			applications = apiResponse?.Data?.LoanApplications;

			return applications;
		}

		public Task<LoanApplication> UpdateStatus(LoanApplication application, LoanApplicationUpdate loanApplicationUpdate)
		{	


			throw new NotImplementedException();
		}
	}
}
