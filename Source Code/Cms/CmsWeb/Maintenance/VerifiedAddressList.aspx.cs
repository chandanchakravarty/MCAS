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
using System.Resources;
using System.Reflection;

namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// Summary description for VerifiedAddressList.
	/// </summary>
	public class VerifiedAddressList : Cms.CmsWeb.cmsbase
    {
      
      public String lblHeaderEffectCenter;
      public String lblAddress;
      public String lblCompliment;
      public String lblDistrict;
      public String lblCity;
      public String lblState;
      public String lblZip;
      protected System.Web.UI.HtmlControls.HtmlInputButton btnClose;
      public String header;
        
        ResourceManager objResourceMgr;
		private void Page_Load(object sender, System.EventArgs e)
		{
            base.ScreenId = "";
            // Put user code to initialize the page here
            objResourceMgr = new ResourceManager("Cms.CmsWeb.Maintenance.VerifiedAddressList", Assembly.GetExecutingAssembly());
            this.SetCaptions();
		}
        private void SetCaptions()
        {
            lblHeaderEffectCenter = objResourceMgr.GetString("lblHeaderEffectCenter");
            lblAddress = objResourceMgr.GetString("lblAddress");
            lblCompliment = objResourceMgr.GetString("lblCompliment");
            lblDistrict = objResourceMgr.GetString("lblDistrict");
            lblCity = objResourceMgr.GetString("lblCity");
            lblState = objResourceMgr.GetString("lblState");
            lblZip = objResourceMgr.GetString("lblZip");
            btnClose.Value = objResourceMgr.GetString("btnClose");
            header = objResourceMgr.GetString("header");
        }
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	}
}
