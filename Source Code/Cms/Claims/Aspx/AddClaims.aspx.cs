/******************************************************************************************
<Author					: - Sumit Chhabra
<Start Date				: -	April 26,2006
<End Date				: -	
<Description			: - Claims details page
<Review Date			: - 
<Reviewed By			: - 


<Modified Date			: 
<Modified By			: 
<Purpose				: 




Modification History ****************************************************/

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
using Cms.BusinessLayer.BLClaims;
using Cms.Claims;
using Cms.Model.Claims;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.IO;
using Cms.CmsWeb.Controls; 
namespace  Cms.Claims.Aspx
{
	/// <summary>
	/// Summary description for AddClaims.
	/// </summary>
	public class AddClaims : Cms.Claims.ClaimBase
	{
		protected Cms.CmsWeb.Controls.CmsButton btnDummyPolicyPopUp;
		protected System.Web.UI.WebControls.Label lblMessage;
		System.Resources.ResourceManager objResourceMgr;
		
		
		private void Page_Load(object sender, System.EventArgs e)
		{

			try
			{
				base.ScreenId="300";

				btnDummyPolicyPopUp.CmsButtonClass	=	CmsButtonType.Write;
				btnDummyPolicyPopUp.PermissionString		=	gstrSecurityXML;

				objResourceMgr = new System.Resources.ResourceManager("Cms.Claims.Aspx.AddClaims" ,System.Reflection.Assembly.GetExecutingAssembly());
				if(!IsPostBack)
				{
					btnDummyPolicyPopUp.Attributes.Add("onClick","javascript:return OpenDummyPolicyWindow();");
				}
			
			}
			catch //(Exception exc)
			{}
			finally
			{}
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
