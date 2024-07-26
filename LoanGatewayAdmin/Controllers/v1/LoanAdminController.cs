using LoanGatewayAdmin.Models;
using LoanGatewayAdmin.Services;
using LoanGatewayShared.Models;
using Microsoft.AspNetCore.Mvc;

namespace LoanGatewayAdmin.Controllers.v1
{
    [Route("api/v1/[controller]")]
	[ApiController]
	public class LoanAdminController : ControllerBase
	{
		private readonly ILoanAdminService _loanAdminService;
		public LoanAdminController(ILoanAdminService loanAdminService)
		{
			_loanAdminService = loanAdminService;
		}

		/// <summary>
		/// Get all loan applications
		/// </summary>
		/// <returns></returns>
		[HttpGet("applications")]
		[ProducesResponseType(typeof(ApiResponse<List<LoanApplication>, string>), 200)]
		[ProducesResponseType(typeof(ApiResponse<string, List<ErrorDetail>>), 400)]
		[ProducesResponseType(typeof(ApiResponse<string, List<ErrorDetail>>), 500)]
		public async Task<IActionResult> GetApplications()
		{
			try
			{
				var applications = await _loanAdminService.GetLoanApplicationsAsync();

				return Ok(ApiResponse<List<LoanApplication>, string>.SuccessObject(applications, Message.Success));
			}
			catch (Exception ex)
			{
				var error = new List<ErrorDetail>
					{
						new ErrorDetail { ErrorCode = "500", Message = ex.Message }
					};
				return StatusCode(500, ApiResponse<string, List<ErrorDetail>>.ErrorObject(error));
			}
		}

		[HttpPut("applications/{arn}")]
		[ProducesResponseType(typeof(ApiResponse<LoanApplication, string>), 200)]
		[ProducesResponseType(typeof(ApiResponse<string, List<ErrorDetail>>), 400)]
		[ProducesResponseType(typeof(ApiResponse<string, List<ErrorDetail>>), 500)]
		public async Task<IActionResult> UpdateStatus(string arn, [FromBody] LoanApplicationUpdate loanApplicationUpdate)
		{
			try
			{
				if(string.IsNullOrWhiteSpace(arn))
				{
					var error = new List<ErrorDetail>
					{
						new ErrorDetail { ErrorCode = "400", Message = "Bad Request. ARN (Application Reference Number) is required" }
					};
					return StatusCode(400, ApiResponse<string, List<ErrorDetail>>.ErrorObject(error));
				}

				var application = await _loanAdminService.GetLoanApplicationAsync(arn);

				if (application == null)
				{
					var error = new List<ErrorDetail>
					{
						new ErrorDetail { ErrorCode = "400", Message = "Loan Application Reference not found" }
					};
					return StatusCode(400, ApiResponse<string, List<ErrorDetail>>.ErrorObject(error));
				}

				var updatedApplication = await _loanAdminService.UpdateStatus(application, loanApplicationUpdate);

				return Ok(ApiResponse<LoanApplication, string>.SuccessObject(updatedApplication, Message.Success));
			}
			catch (Exception ex)
			{
				var error = new List<ErrorDetail>
					{
						new ErrorDetail { ErrorCode = "500", Message = ex.Message }
					};
				return StatusCode(500, ApiResponse<string, List<ErrorDetail>>.ErrorObject(error));
			}
		}
	}
}
