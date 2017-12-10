using ChatCode.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChatCode.Portal.BL.DeveloperBL
{
    public class Statistic
    {
        /// <summary>
        /// Sistemde kayıtlı tüm eventleri sayısı
        /// </summary>
        /// <returns></returns>
        public int GetAllTemplateNumber()
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    return db.Websites.Count();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Bugün eklenen tüm templatelerin sayısıdır.
        /// </summary>
        /// <returns></returns>
        public int GetAddedTodayTemplate()
        {
            try
            {
                DateTime currenteDate = DateTime.UtcNow.Date.AddDays(-1);
                using (DataContext db = new DataContext())
                {
                    return db.Websites.Where(u => u.PublishedDate >= currenteDate).Count();
                }
            }
            catch (Exception)
            {
                return 0;
            }

        }

        /// <summary>
        /// Session açılan developerın tüm template sayılarıdır.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int getSessionTemplate(int id)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                   return db.Websites.Where(u => u.DeveloperId == id).Count();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Sesion olan developer in indirilen toplam downloade sayısı
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int getSessionDownloadedTemplate(int id)
        {
            using (DataContext db = new DataContext())
            {
               var result = db.Websites.Where(p => p.DeveloperId == id).Select(p => p.NumberOfDownload).Count();
                return result;
            }
        }
    }
}