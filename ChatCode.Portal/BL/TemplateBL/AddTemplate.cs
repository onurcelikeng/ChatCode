using ChatCode.DAL.Entities;
using ChatCode.DAL.Entities.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatCode.Portal.BL.TemplateBL
{
    public class AddTemplate
    {
        
        public static bool Add(Website model, int id)
        {
            
            try
            {
                if (model != null)
                {
                    using (DataContext db = new DataContext())
                    {
                        model.PublishedDate = DateTime.Now;
                        model.DeveloperId = id;
                        db.Websites.Add(model);
                        db.SaveChanges();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}