using System;

namespace NorthwindAPI.Model
{
    public class OrderModel
    {
        public string FullEmployeeName { get; set; }
        public string Shipper { get; set; }
        public string Customer { get; set; }
        public DateTime OrderDate { get; set; }
        public int NoOfOrders { get; set; }
        public float TotalFreighCosts { get; set; }
        public float TotalOrderValue { get; set; }

    }
}
