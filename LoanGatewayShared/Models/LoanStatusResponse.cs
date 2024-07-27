namespace LoanGatewayShared.Models
{
	public class LoanStatusResponse
	{
		public string Arn { get; set; }
		public string UserId { get; set; }
		public string Status { get; set; }
		public string Remarks { get; set; }
		public List<string> CompletedSteps { get; set; }
		public List<string> PendingSteps { get; set; }
	}
}
