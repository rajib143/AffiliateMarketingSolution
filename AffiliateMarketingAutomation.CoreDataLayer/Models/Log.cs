using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace AffiliateMarketingAutomation.CoreDataLayer.Models
{
    [Table("Logs", Schema = "dbo")]
    public class Log
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Thread { get; set; }
        public string Level { get; set; }
        public string Logger { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
    }
}
