using LoanGateway.Models;
using LoanGatewayAdmin.Models;
using LoanGatewayShared.Models;
using Newtonsoft.Json;
using RestSharp;

namespace LoanGatewayAdmin.Services
{
	public class LoanAdminService : ILoanAdminService
	{
		private readonly ILogger<LoanAdminService> _logger;

		private readonly LoanGatewayServiceOptions _loanGatewayServiceOptions;

		private readonly RestClient _loanGatewayClient;	

		public LoanAdminService(LoanGatewayServiceOptions loanGatewayServiceOptions, ILogger<LoanAdminService> logger) 
		{
			_loanGatewayServiceOptions = loanGatewayServiceOptions;
			_logger = logger;

			var options = new RestClientOptions(_loanGatewayServiceOptions.Url);

			_loanGatewayClient = new RestClient(options);
		}

		public async Task<LoanApplication> GetLoanApplicationAsync(string arn)
		{
			var application = new LoanApplication();

			if (string.IsNullOrEmpty(arn))
			{
				throw new ArgumentNullException(nameof(arn));
			}

			try
			{
				_logger.LogInformation("Getting loan application from loan gateway");
				var applications = await GetLoanApplicationsAsync();

				if (applications.Any())
				{
					return applications.Find(app => string.Equals(arn, app.Arn, StringComparison.InvariantCultureIgnoreCase));
				}

				return application;
			}
			catch(Exception ex)
			{
				_logger.LogError(ex, $"Unable to get Loan Application details");
				return new LoanApplication() { Arn = string.Empty };
			}
		}

		public async Task<List<LoanApplication>> GetLoanApplicationsAsync()
		{
			var applications = new List<LoanApplication>();
			try
			{
				var request = new RestRequest(_loanGatewayServiceOptions.LoanApplicationsEndPoint);

				var response = await _loanGatewayClient.GetAsync(request);

				var apiResponse = JsonConvert.DeserializeObject<ApiResponse<UserRequestHistory, string>>(response.Content);

				applications = apiResponse?.Data?.LoanApplications;

				return applications;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Unable to get all Loan Application details");
				return Enumerable.Empty<LoanApplication>().ToList();
			}
		}

		public async Task<bool> UpdateStatus(StatusUpdateRequest statusUpdateRequest)
		{
			var statusResponse = false;
			try
			{
				var request = new RestRequest(_loanGatewayServiceOptions.UpdateStatusEndPoint);
				request.AddBody(statusUpdateRequest);

				var response = await _loanGatewayClient.PutAsync(request);

				var apiResponse = JsonConvert.DeserializeObject<ApiResponse<bool, string>>(response.Content);

				statusResponse = apiResponse != null && apiResponse.Data;

				return statusResponse;

			}
			catch(Exception ex)
			{
				_logger.LogError(ex, $"Unable to update Status");
				return statusResponse;
			}
		}
	}
}
