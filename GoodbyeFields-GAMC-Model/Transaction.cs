using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodbyeFields_GAMC_Model
{
    [Table("Transaction")]
    public class Transaction
    {
        [Key]
        public long TransactionId { get; set; }
        public int TransactionTypeId { get; set; }
        [Range(1, 9999999, ErrorMessage = "Please enter amount bigger than 9999999")]
        [Required(ErrorMessage = "Enter Amount")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TransactionAmount { get; set; }
        public string PlayerId { get; set; }
        public string TransactionDescription { get; set; }
        public string TransactionCode { get; set; }
        public string TransactionStatus { get; set; }
        public DateTime TransactionTime { get; set; }
        public bool IsVoid { get; set; }
        public string VoidDescription { get; set; }
        public DateTime VoidDate { get; set; }
        public string Message { get; set; }
    }
}
