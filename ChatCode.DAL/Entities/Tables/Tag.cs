using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatCode.DAL.Entities.Tables
{
    [Table("TagTable")]
    public class Tag
    {
        public Tag()
        {
            Websites = new List<Website>();
        }


        [Key]
        public int Id { get; set; }

        [Required]
        public string TagName { get; set; }

        public virtual ICollection<Website> Websites { get; set; }
    }
}
