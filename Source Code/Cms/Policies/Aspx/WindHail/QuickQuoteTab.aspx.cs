/******************************************************************************************
<Author				: - Pradeep Kushwaha
<Start Date			: -	15-Sep-2010
<End Date			: -	 
<Description		: - Wind hail quick quote 
<Review Date		: - 
<Reviewed By		: - 	

Modification History
<Modified Date		: - 
<Modified By		: -   
<Purpose			: -  
*******************************************************************************************/
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Cms.BusinessLayer.BlCommon;
using Cms.CmsWeb;
using Cms.CmsWeb.WebControls;
using Cms.CmsWeb.Controls;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.Model.Maintenance;
using System.Resources;
using System.Reflection;
using Cms.BusinessLayer.BlApplication;
using System.Collections.Generic;
using Coolite.Ext.Web;


namespace Cms.Policies.Aspx.WindHail
{
    public partial class QuickQuoteTab : Cms.CmsWeb.cmsbase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.ScreenId = "";
            btnLogoutExit.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnLogoutExit.PermissionString = gstrSecurityXML;

            btnSubmit.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnSubmit.PermissionString = gstrSecurityXML;

            btnConfirm.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnConfirm.PermissionString = gstrSecurityXML;


            btnExit.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnExit.PermissionString = gstrSecurityXML;

            btnDiary.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnDiary.PermissionString = gstrSecurityXML;

            btnNotepad.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnNotepad.PermissionString = gstrSecurityXML;

            btnLetter.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnLetter.PermissionString = gstrSecurityXML;


            btnExit1.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnExit1.PermissionString = gstrSecurityXML;

            btnPolicy.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnPolicy.PermissionString = gstrSecurityXML;

            
            if (!IsPostBack)
            {
                SetCaptions();
            }

            //Window win = new Window();
        }
        private void SetCaptions()
        {
            lblManHeader.Text = ClsMessages.FetchGeneralMessage("1168");
            lblManHeader1.Text = lblManHeader.Text;
            lblpageHeader2.Text = lblManHeader.Text; 
        }
    }
}
