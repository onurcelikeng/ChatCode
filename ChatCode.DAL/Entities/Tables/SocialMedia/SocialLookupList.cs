using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatCode.DAL.Entities.Tables.SocialMedia
{
    [Table("SocialLookupListTable")]
    public class SocialLookupList
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int SocialLookupId { get; set; }

        [Required]
        public string Url { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        
        [ForeignKey("SocialLookupId")]
        public virtual SocialLookUp SocialLookUp { get; set; }
    }
}
