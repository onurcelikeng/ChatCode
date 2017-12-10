using ChatCode.DAL.Entities.Tables;
using ChatCode.DAL.Entities.Tables.SocialMedia;
using ChatCode.DAL.Entities.Tables.WebsiteTypes;
using System.Collections.Generic;
using System.Data.Entity;

namespace ChatCode.DAL.Entities
{
    public class DataContext : DbContext
    {
        public virtual ISet<WebsiteType> WebsiteTypes { get; set; }

        public virtual DbSet<Website> Websites { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Tag> Tags { get; set; }

        public virtual DbSet<Developer> Developers { get; set; }

        public virtual DbSet<AboutMe> AboutMes { get; set; }

        public virtual DbSet<SocialLookUp> SocialLookUps { get; set; }

        public virtual DbSet<SocialLookupList> SocialLookupLists { get; set; } 

        public virtual DbSet<Project> Projects { get; set; }


        public DataContext()
            : base("name=DataContext")
        {
        }

    }
}