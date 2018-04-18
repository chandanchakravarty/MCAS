using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;

namespace Cms.CmsWeb.webservices
{
	/// <summary>
	/// Summary description for CmsWebService.
	/// </summary>
	public class CmsWebService : System.Web.Services.WebService
	{
		public CmsWebService()
		{
			//CODEGEN: This call is required by the ASP.NET Web Services Designer
			InitializeComponent();
		}

		#region Component Designer generated code
		
		//Required by the Web Services Designer 
		private IContainer components = null;
				
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr= System.Configuration.ConfigurationManager.AppSettings.Get("DB_CON_STRING");
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if(disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);		
		}
		
		#endregion

		[WebMethod]
		public DataSet GetLOBBYSTATEID(string Stateid)
		{
			Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGenInfo = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
			return objGenInfo.GetLOBBYSTATEID(int.Parse(Stateid));
		}
		[WebMethod]
		public DataSet GetActiveSTATEList()
		{
			DataTable dtState= Cms.CmsWeb.ClsFetcher.ActiveState;
			DataSet ds=new DataSet();
			ds.Merge(dtState);
			return ds;
		}
		
	}
}
