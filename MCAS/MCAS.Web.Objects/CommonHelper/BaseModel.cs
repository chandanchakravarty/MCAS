using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Security;

namespace MCAS.Web.Objects.CommonHelper
{
   public class BaseModel
   {
       #region constructor

       #endregion

       #region Properties
       /// <summary>
        /// Created By user Id
        /// </summary>
       private string _createdby;
       private DateTime _createdOn = DateTime.Now;
       private string _modifiedby;
       private string culture;
       public string CreatedBy
       {
           get
           {
               if (HttpContext.Current.Session["LoggedInUserId"] == null)
               {
                   LoggedOut();
                   return null;
               }
               else
               {
                   if (_createdby!=String.Empty)
                       return _createdby;
                   else
                       return HttpContext.Current.Session["LoggedInUserId"].ToString();
               }
           }
           set { _createdby = value; }
       }

        /// <summary>
        /// DateTime on which Record created
        /// </summary>
        public DateTime CreatedOn {
            get { return _createdOn; }
            set { _createdOn = value; } 
        }

        public string ModifiedBy 
        {
            get
            {
                if (HttpContext.Current.Session["LoggedInUserId"] == null)
                {
                    LoggedOut();
                    return null;
                }
                else
                {
                    if (_modifiedby != String.Empty)
                        return _modifiedby;
                   else
                        return HttpContext.Current.Session["LoggedInUserId"].ToString();
                }
            }
            set { _modifiedby = value; }
        }
        public string GetCulture()
        {

            if (HttpContext.Current.Session["CurrentUICulture"] != null)
                {
                    culture = HttpContext.Current.Session["CurrentUICulture"].ToString();
                }
                return culture;
        }


        /// <summary>
        /// DateTime when record modified
        /// </summary>
        public DateTime? ModifiedOn {get; set; }

        public int LangId { get; set; }
        public string Resource { get; set; }
        public string CultureCode { get; set; }
        private string _viewMode = "Read"; // default view
        private SecurityPermissions _securityPermission = new SecurityPermissions(); // default view
        private bool _readOnly = false; // default view
        public string ViewMode { get { return _viewMode; } set { _viewMode = value; } }
        public bool ReadOnly { get { return _readOnly; } set { _readOnly = value; } }
        public virtual string screenId {get;set;}
        public virtual string listscreenId { get; set; }
        public SecurityPermissions UserPermissions { get { return _securityPermission; } set { _securityPermission = value; } }
       #endregion

        protected void LoggedOut()
        {
            HttpContext.Current.Session.Clear();
            //RedirectToAction("Login", "Home");
        }

   }

   public class SecurityPermissions
   {
       #region Properties
       private bool mRead = false;
       private bool mWrite = false;
       private bool mDelete = false;
       private bool mSplPermission = false;

       public bool Read { get { return mRead; } set { mRead=value; } }
       public bool Write { get { return mWrite; } set { mWrite = value; } }
       public bool Delete { get { return mDelete; } set { mDelete = value; } }
       public bool SplPermission { get { return mSplPermission; } set { mSplPermission = value; } }
       #endregion

   }
}
