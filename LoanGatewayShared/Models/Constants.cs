namespace LoanGatewayShared.Models
{
	public class Message
    {
        public static string UnknownError { get; set; } = "Unable to process the request at the moment.";
        public static string Success { get; set; } = "Request processed successfully";
    }
}
