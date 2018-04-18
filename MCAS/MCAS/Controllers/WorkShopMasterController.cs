using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MCAS.Web.Objects.MastersHelper;
using MCAS.Web.Objects.CommonHelper;
using MCAS.Entity;
namespace MCAS.Controllers
{
    public class WorkShopMasterController : Controller
    {
        //
        // GET: /WorkShopMaster/
        MCASEntities _db = new MCASEntities();

        public ActionResult Index()
        {
            return View();
        }

        #region Work Shop master
        public ActionResult WorkShopMasterIndex() {
            return View();
        }

        #endregion

    }
}
