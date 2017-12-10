using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatCode.DAL.Entities.Tables
{
    [Table("WebsiteTable")]
    public class Website
    {
       

        [Key]
        public int Id { get; set; }
        public string TemplateName { get; set; }

        public string SourceCode { get; set; }

        public float Price { get; set; }

        public string Types { get; set; }

        public string Style1 { get; set; }

        public string Style2 { get; set; }

        public string Decsription { get; set; }

        public string Sex { get; set; }
        public int NumberOfDownload { get; set; } 
        public int Rating { get; set; } 

        public string ScreenShotUrl { get; set; }
        public DateTime PublishedDate{ get; set; }
        public int DeveloperId { get; set; }       



        [ForeignKey("DeveloperId")]
        public virtual Developer Developer { get; set; }


        
    }
}
