﻿using LoanGatewayAdmin.Models;
using LoanGatewayShared.Models;

namespace LoanGatewayAdmin.Services
{
	public interface ILoanAdminService
	{

		Task<List<LoanApplication>> GetLoanApplicationsAsync();

		Task<LoanApplication> GetLoanApplicationAsync(string arn);

		Task<LoanApplication> UpdateStatus(LoanApplication application, LoanApplicationUpdate loanApplicationUpdate);

	}
}
