namespace LoanGatewayShared.Models
{
	public class LoanStatus
	{
		public static string Submitted { get; set; } = "SUBMITTED";
		public static string InReview { get; set; } = "IN-REVIEW";
		public static string Approved { get; set; } = "APPROVED";
		public static string PartialApproved { get; set; } = "PARTIALAPPROVED";
		public static string Rejected { get; set; } = "REJECTED";
	}
}
