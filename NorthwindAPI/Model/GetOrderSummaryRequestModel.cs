using System;

namespace NorthwindAPI.Model
{
    public class GetOrderSummaryRequestModel
    {
        public string CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
