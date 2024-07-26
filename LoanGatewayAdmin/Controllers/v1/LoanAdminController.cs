using LoanGatewayAdmin.Services;
using LoanGatewayShared.Models;
using Microsoft.AspNetCore.Mvc;

namespace LoanGatewayAdmin.Controllers.v1
{
	[Route("api/v1/[controller]")]
	[ApiController]
	public class LoanAdminController : ControllerBase
	{
		private readonly ILogger<LoanAdminController> _logger;
		private readonly ILoanAdminService _loanAdminService;
		public LoanAdminController(ILoanAdminService loanAdminService, ILogger<LoanAdminController> logger)
		{
			_loanAdminService = loanAdminService;
			_logger = logger;
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
			_logger.LogInformation("Received Get All Loan Applications request");
			try
			{
				var applications = await _loanAdminService.GetLoanApplicationsAsync();
				_logger.LogInformation($"Total Applications: {applications.Count}");

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

		/// <summary>
		/// Get all loan application by Application Reference Number(Arn)
		/// </summary>
		/// <returns></returns>
		[HttpGet("applications/{arn}")]
		[ProducesResponseType(typeof(ApiResponse<LoanApplication, string>), 200)]
		[ProducesResponseType(typeof(ApiResponse<string, List<ErrorDetail>>), 400)]
		[ProducesResponseType(typeof(ApiResponse<string, List<ErrorDetail>>), 500)]
		public async Task<IActionResult> GetApplication(string arn)
		{
			_logger.LogInformation($"Received Get Loan Application request for ARN : {arn}");
			try
			{
				if (string.IsNullOrWhiteSpace(arn))
				{
					var error = new List<ErrorDetail>
					{
						new ErrorDetail { ErrorCode = "400", Message = "Bad Request. ARN (Application Reference Number) is required" }
					};
					return StatusCode(400, ApiResponse<string, List<ErrorDetail>>.ErrorObject(error));
				}

				var application = await _loanAdminService.GetLoanApplicationAsync(arn);

				if (application == null || string.IsNullOrWhiteSpace(application.Arn))
				{
					var error = new List<ErrorDetail>
					{
						new ErrorDetail { ErrorCode = "404", Message = Message.NotFound }
					};
					return StatusCode(404, ApiResponse<string, List<ErrorDetail>>.ErrorObject(error));
				}

				return Ok(ApiResponse<LoanApplication, string>.SuccessObject(application, Message.Success));
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

		//[HttpPut("applications/{arn}")]
		//[ProducesResponseType(typeof(ApiResponse<LoanApplication, string>), 200)]
		//[ProducesResponseType(typeof(ApiResponse<string, List<ErrorDetail>>), 400)]
		//[ProducesResponseType(typeof(ApiResponse<string, List<ErrorDetail>>), 500)]
		//public async Task<IActionResult> UpdateStatus(string arn, [FromBody] LoanApplicationUpdate loanApplicationUpdate)
		//{
		//	try
		//	{
		//		if(string.IsNullOrWhiteSpace(arn))
		//		{
		//			var error = new List<ErrorDetail>
		//			{
		//				new ErrorDetail { ErrorCode = "400", Message = "Bad Request. ARN (Application Reference Number) is required" }
		//			};
		//			return StatusCode(400, ApiResponse<string, List<ErrorDetail>>.ErrorObject(error));
		//		}

		//		var application = await _loanAdminService.GetLoanApplicationAsync(arn);

		//		if (application == null)
		//		{
		//			var error = new List<ErrorDetail>
		//			{
		//				new ErrorDetail { ErrorCode = "400", Message = "Loan Application Reference not found" }
		//			};
		//			return StatusCode(400, ApiResponse<string, List<ErrorDetail>>.ErrorObject(error));
		//		}

		//		var updatedApplication = await _loanAdminService.UpdateStatus(application, loanApplicationUpdate);

		//		return Ok(ApiResponse<LoanApplication, string>.SuccessObject(updatedApplication, Message.Success));
		//	}
		//	catch (Exception ex)
		//	{
		//		var error = new List<ErrorDetail>
		//			{
		//				new ErrorDetail { ErrorCode = "500", Message = ex.Message }
		//			};
		//		return StatusCode(500, ApiResponse<string, List<ErrorDetail>>.ErrorObject(error));
		//	}
		//}
	}
}
