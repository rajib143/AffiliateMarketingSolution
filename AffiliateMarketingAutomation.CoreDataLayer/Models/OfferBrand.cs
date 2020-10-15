
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AffiliateMarketingAutomation.CoreDataLayer.Models
{
    [Table("OfferBrands", Schema = "Admin")]
    public class OfferBrand
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string BrandName { get; set; }
        public string Description { get; set; }
    }
}
