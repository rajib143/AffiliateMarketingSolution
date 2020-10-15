using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AffiliateMarketingAutomation.CoreDataLayer.Models
{
    [Table("Category", Schema = "Admin")]
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Nullable<int> ParentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SiteName { get; set; }
        public int ProductCount { get; set; }
    }
}
