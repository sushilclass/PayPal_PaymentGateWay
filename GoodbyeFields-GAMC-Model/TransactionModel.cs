using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodbyeFields_GAMC_Model
{
    public class TransactionModel
    {
        public decimal TransactionAmount { get; set; }
        public string PlayerId { get; set; }
        public string TransactionDescription { get; set; }
        public string TransactionCode { get; set; }

        //for transaciton history
        public DateTime TransactionFromDate { get; set; }
        public DateTime TransactionToDate { get; set; }

        //for void transaction
        public long TransactionId { get; set; }
        public string VoidDescription { get; set; }
    }
}
