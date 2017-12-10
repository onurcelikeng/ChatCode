using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatCode.DAL.Entities.Tables.SocialMedia
{
    [Table("SocialLookUpTable")]
    public class SocialLookUp
    {
        [Key]
        public int Id { get; set; }

        [Required,MaxLength(15)]
        public string SocialMedia { get; set; }
    }
}
