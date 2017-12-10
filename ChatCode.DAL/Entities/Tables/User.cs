using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatCode.DAL.Entities.Tables
{
    [Table("UserTable")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
