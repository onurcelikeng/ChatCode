using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatCode.DAL.Entities.Tables.WebsiteTypes
{
    [Table("AboutMeTable")]
    public class AboutMe
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        [Required]
        public string NameSurname { get; set; }

        public int Age { get; set; }

        public string Gender { get; set; }

        [Required, MaxLength(140)]
        public string Description { get; set; }

        [Required]
        public string Image { get; set; }

        public string Background { get; set; }

        public string Foreground { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
