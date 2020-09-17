using System;
using System.Collections.Generic;
using System.Text;

namespace GoodbyeFields_GAMC_Model
{
    public class PayPalResponseModel
    {
        public string Id { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public string Intent { get; set; }
        public string PaymentMethod { get; set; }
        public string PayerId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string InvoiceNumber { get; set; }
        public string FullResponse { get; set; }
        public string Failure_reason { get; set; }
    }
}
