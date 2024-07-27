namespace LoanGatewayAdmin.Models
{
	public class LoanGatewayServiceOptions
	{
	   public const string LoanGatewayService = "LoanGatewayService";

	   public string Url { get; set; }

	   public string LoanApplicationsEndPoint { get; set; }

	   public string UpdateStatusEndPoint { get; set; }
	}
}
