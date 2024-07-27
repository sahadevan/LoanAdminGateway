namespace LoanGatewayShared.Models
{
    public class StatusUpdateRequest
	{
		public string Arn { get; set; }

        public string Status { get; set; }

		public string? Remarks { get; set; }

		public override string ToString()
		{
			return string.Join(':', Arn, Status, Remarks);
		}
	}
}