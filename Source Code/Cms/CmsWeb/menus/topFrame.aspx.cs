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

/******************************************************************************************
	<Author					: - >
	<Start Date				: -	>
	<End Date				: - >
	<Description			: - >
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: - >
	<Modified By			: - >
	<Purpose				: - >
*******************************************************************************************/

namespace Cms.CmsWeb.menus
{
	/// <summary>
	/// Summary description for topFrame.
	/// </summary>
	public class topFrame : Cms.CmsWeb.cmsbase  
	{
    
		protected string lStrImageFolder="";
		new protected string CarrierSystemID = "";
        protected String strHome;
        protected String strPassword;
        protected String strSearch;
        protected String strInquiry;
        protected String strHelp;
        protected String strLogOut;
        private string XmlSchemaFileName = "";

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page *here
			lStrImageFolder=GetImageFolder();
            CarrierSystemID = cmsbase.CarrierSystemID;// System.Configuration.ConfigurationManager.AppSettings.Get("CarrierSystemID");

            string strSysID = GetSystemId();
            if (strSysID == "ALBAUAT")
                strSysID = "ALBA";

            XmlSchemaFileName = "topFrame.xml";
            //XmlFullFilePath = Request.PhysicalApplicationPath + "cmsweb/support/PageXml/" + strSysID + "/" + XmlSchemaFileName;

            if (ClsCommon.IsXMLResourceExists(Request.PhysicalApplicationPath + "cmsweb/support/PageXML/" + strSysID, XmlSchemaFileName))
                setPageControls(Page, Request.PhysicalApplicationPath + "cmsweb/support/PageXml/" + strSysID + "/" + XmlSchemaFileName);
            
            ClsMessages.SetCustomizedXml(GetLanguageCode());
            strHome =ClsMessages.GetMessage("G","1771");
            strPassword =ClsMessages.GetMessage("G","1772");
            strSearch =ClsMessages.GetMessage("G","1773");
            strInquiry =ClsMessages.GetMessage("G","1774");
            strHelp =ClsMessages.GetMessage("G","1775");
            strLogOut = ClsMessages.GetMessage("G","2004");
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
