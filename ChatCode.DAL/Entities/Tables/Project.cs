using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatCode.DAL.Entities.Tables
{
    [Table("ProjectTable")]
    public class Project
    {
        [Key]
        public int Id { get; set; }

        public string WebSourceCode { get; set; }

        public string Url { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
