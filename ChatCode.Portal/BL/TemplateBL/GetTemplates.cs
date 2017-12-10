using ChatCode.DAL.Entities;
using ChatCode.DAL.Entities.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatCode.Portal.BL.TemplateBL
{
    public class GetTemplates
    {
        public static List<Website> getTemplates(int id)
        {
            try
            {
                if (id != 0)
                {
                    using (DataContext db = new DataContext())
                    {
                        var result = db.Websites.Where(u => u.DeveloperId == id).ToList();
                        return result;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static Website getOneTemplate(int templateId, int SessionId)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                   var result =  db.Websites.SingleOrDefault(p => p.Id == templateId && p.DeveloperId == SessionId);
                    return result;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static void UpdateWesite(Website model, int SessionId)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    
                    var result = db.Websites.SingleOrDefault(p => p.Id == model.Id && p.DeveloperId == SessionId);
                    result.TemplateName = model.TemplateName;
                    result.SourceCode = model.SourceCode;
                    result.Price = model.Price;
                    result.Types = model.Types;
                    result.Style1 = model.Style1;
                    result.Style2 = model.Style2;
                    result.Decsription = model.Decsription;
                    result.Sex = model.Sex;
                    result.PublishedDate = DateTime.Now;
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                
            }
        }
    }
}