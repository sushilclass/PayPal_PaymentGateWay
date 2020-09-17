using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodbyeFields_GAMC_Model
{
    [Table("TransactionType")]
    public class TransactionType
    {
        [Key]
        public int Id { get; set; }
        public string TransactionTypeName { get; set; }
        public string TransactionTypeDescription { get; set; }
    }
}
