using Microsoft.AspNetCore.Http;

namespace LoanGatewayShared.Models
{
	public class LoanApplication
    {
        public string? Arn { get; set; } // This will not be passed from frontend. This is for backend use
        public Guid? EligibilityRequestId { get; set; }
        public string FullName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Ssn { get; set; }
        public DateTime Dob { get; set; }
        public double Amount { get; set; } = 0.0;
        public int TenureMonths { get; set; } = 0;
        public List<IFormFile> Documents { get; set; }
        public string Occupation { get; set; } // Self imployed / Salaried
        public double AnnualIncome { get; set; } = 0;

        // This is for backend use start
        public string? Status { get; set; }  
        public string? Remarks { get; set; }
        public double? Emi { get; set; } =0;
        public double? InterestRate { get; set; } = 0;
        // This is for backend use end
    }
}
