using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using Cms.CmsWeb.WebControls;
using System.Resources;
using System.Reflection;
using Cms.CmsWeb.Controls;
using Cms.CmsWeb;
using Cms.CmsWeb.Maintenance;
using Cms.Model.Maintenance.Accounting;
using Cms.BusinessLayer.BlCommon.Accounting;
using Cms.BusinessLayer.BlCommon;
using Cms.ExceptionPublisher.ExceptionManagement; 

namespace Cms.CmsWeb.Accounting
{
    public  partial class RIInterface : Cms.CmsWeb.cmsbase
    {
        ResourceManager objResourceMgr = null;
        private void Page_Load(object sender, EventArgs e)
        {
            try
            {
                base.ScreenId = "566";
                objResourceMgr = new ResourceManager("Cms.CmsWeb.Accounting.RIInterface", Assembly.GetExecutingAssembly());

                btnSave.CmsButtonClass = CmsButtonType.Write;
                btnSave.PermissionString = gstrSecurityXML;
                hlkDATE_EFFETIVE_FROM.Attributes.Add("OnClick", "fPopCalendar(document.getElementById('txtDATE_EFFETIVE_FROM'), document.getElementById('txtDATE_EFFETIVE_FROM'))");
                hlkDATE_EFFETIVE_TO.Attributes.Add("OnClick", "fPopCalendar(document.getElementById('txtDATE_EFFETIVE_TO'), document.getElementById('txtDATE_EFFETIVE_TO'))");
                txtDATE_EFFETIVE_FROM.Attributes.Add("onBlur", "FormatDate()");
                txtDATE_EFFETIVE_TO.Attributes.Add("onBlur", "FormatDate()");

                if (!IsPostBack)
                {
                    LoadDropDown();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.FetchGeneralMessage("21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }

        }


        private void LoadDropDown()
        { 
            try
            {

                cmbFILE_NAME.DataSource = ClsCommon.GetLookup("FLNME");
                cmbFILE_NAME.DataTextField = "LookupDesc";
                cmbFILE_NAME.DataValueField = "LookupID";
                cmbFILE_NAME.DataBind();
                cmbFILE_NAME.Items.Insert(0, "");



                cmbFILE_TYPE.DataSource = ClsCommon.GetLookup("FLTYP");
                cmbFILE_TYPE.DataTextField = "LookupDesc";
                cmbFILE_TYPE.DataValueField = "LookupID";
                cmbFILE_TYPE.DataBind();
                cmbFILE_TYPE.Items.Insert(0, "");



            }



            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.FetchGeneralMessage("21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
        
        }
    }
}