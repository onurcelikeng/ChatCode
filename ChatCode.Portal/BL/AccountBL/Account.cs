using ChatCode.DAL.Entities;
using ChatCode.DAL.Entities.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatCode.Portal.BL.AccountBL
{
    public class Account
    {
        /// <summary>
        /// Developer added to database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool RegisterDeveloper(Developer model)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    db.Developers.Add(model);
                    db.SaveChanges();
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public static Developer getDeveloperCredential(Developer model)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    var result = db.Developers.Where(u=>u.Email == model.Email).SingleOrDefault();
                    return result;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static bool isNotExist(Developer model)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    var developer = db.Developers.Where(u => u.Email == model.Email).SingleOrDefault();
                    if (developer == null)
                        return true;
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// Coming model is null or not checking
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool isValid(Developer model)
        {
            if (model != null)
                return true;
            return false;
        }
    }
}