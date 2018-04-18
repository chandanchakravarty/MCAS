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
using Cms.CmsWeb;
using Cms.DataLayer;  

namespace Reports.Aspx
{
	/// <summary>
	/// Summary description for AgencyTransaction.
	/// </summary>
	public class AgencyTransaction : Cms.CmsWeb.cmsbase  //System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lblExpirationStartDate;
		protected System.Web.UI.WebControls.TextBox txtExpirationStartDate;
		protected System.Web.UI.WebControls.HyperLink hlkExpirationStartDate;
		protected System.Web.UI.WebControls.RegularExpressionValidator revExpirationStartDate;
		protected System.Web.UI.WebControls.CompareValidator cmpExpirationDate;
		protected System.Web.UI.WebControls.TextBox txtExpirationEndDate;
		protected System.Web.UI.WebControls.Label lblExpirationEndDate;
		protected System.Web.UI.WebControls.HyperLink hlkExpirationEndDate;
		protected System.Web.UI.WebControls.RegularExpressionValidator revExpirationEndDate;
		protected System.Web.UI.WebControls.Label lblTransactionStartDate;
		protected System.Web.UI.WebControls.TextBox txtTransactionStartDate;
		protected System.Web.UI.WebControls.HyperLink hlkTransactionStartDate;
		protected System.Web.UI.WebControls.RegularExpressionValidator revTransactionStartDate;
		protected System.Web.UI.WebControls.CompareValidator cmpTransactionDate;
		protected System.Web.UI.WebControls.TextBox txtTransactionEndDate;
		protected System.Web.UI.WebControls.Label lblTransactionEndDate;
		protected System.Web.UI.WebControls.HyperLink hlkTransactionEndDate;
		protected System.Web.UI.WebControls.RegularExpressionValidator revTransactionEndDate;
		protected System.Web.UI.WebControls.DropDownList cmbMonthEffectiveDate;
		protected System.Web.UI.WebControls.TextBox txtEffectiveyear;
        protected System.Web.UI.WebControls.TextBox txtTransactionyear; 
		protected System.Web.UI.WebControls.RegularExpressionValidator revEffectiveYear;
		protected System.Web.UI.WebControls.RegularExpressionValidator revTransactionYear;
		protected System.Web.UI.WebControls.RangeValidator rngEffectiveYEAR;
        protected System.Web.UI.WebControls.RangeValidator rngTransactionYEAR;
		protected System.Web.UI.WebControls.Label capPOLICY_ID;
		protected System.Web.UI.WebControls.Label capCUSTOMER_NAME;
		protected System.Web.UI.WebControls.TextBox txtPOLICY_ID;
        protected System.Web.UI.WebControls.TextBox txtCUSTOMER_NAME;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyID;
		protected System.Web.UI.WebControls.DropDownList cmbMonthTransactionDate;		
		protected System.Web.UI.WebControls.CheckBox chk_MonthYearEffectivedate;
		protected System.Web.UI.WebControls.CheckBox chk_MonthYearTransactiondate;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAGENCY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomer_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICYINFO;
		protected System.Web.UI.WebControls.Label lblOrderBy_2;
		protected System.Web.UI.WebControls.Label lblOrderBy_1;
		protected System.Web.UI.WebControls.ListBox lstOrderBy;
		protected System.Web.UI.WebControls.Button btnSel;
        protected System.Web.UI.WebControls.Button btnDeSel;
		protected Cms.CmsWeb.Controls.CmsButton btnAgencyTran;
		//protected Cms.CmsWeb.Controls.CmsButton btnAgencyTranComm;
		protected System.Web.UI.WebControls.ListBox txtAGENCY_NAME;

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEffectiveYear;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvTransactionYear;

		public string strAgencyID="";
		public string  strSystemID="";

		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId = "438";

			btnAgencyTran.CmsButtonClass                 =   Cms.CmsWeb.Controls.CmsButtonType.Execute; 
			btnAgencyTran.PermissionString				=	gstrSecurityXML;	

			/*btnAgencyTranComm.CmsButtonClass                 =   Cms.CmsWeb.Controls.CmsButtonType.Read; 
			btnAgencyTranComm.PermissionString				=	gstrSecurityXML;	*/


			strSystemID = GetSystemId();
			string  strCarrierSystemID = System.Configuration.ConfigurationManager.AppSettings.Get("CarrierSystemID").ToString();
           Cms.DataLayer.DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.Text , DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);

			if ( strSystemID.Trim().ToUpper() != strCarrierSystemID.Trim().ToUpper())		
			{
				//Sorted on the basis if agency_name.
				string selStmt = "select (isnull(AGENCY_DISPLAY_NAME,'')) + '-'+RTRIM(ISNULL (AGENCY_CODE,'')) + '-' + cast(isnull(NUM_AGENCY_CODE,'') as varchar) as Name ,AGENCY_ID as ID from MNT_AGENCY_LIST where AGENCY_CODE = '" + strSystemID + "' order by AGENCY_DISPLAY_NAME,AGENCY_CODE";
				DataSet ds2 = objDataWrapper.ExecuteDataSet(selStmt);
				txtAGENCY_NAME.DataSource = ds2.Tables[0];					
				txtAGENCY_NAME.DataTextField = "Name";
				txtAGENCY_NAME.DataValueField = "ID";
				txtAGENCY_NAME.DataBind();
				this.txtAGENCY_NAME.SelectedIndex =0;
			}
			else
			{				
				//Sorted On the basis of agency_name 
				DataSet ds2  = objDataWrapper.ExecuteDataSet("select (isnull(AGENCY_DISPLAY_NAME,'')) + '-' + RTRIM(ISNULL (AGENCY_CODE,'')) + '-' + cast(isnull(NUM_AGENCY_CODE,'') as varchar) as Name ,AGENCY_ID as ID from MNT_AGENCY_LIST order by AGENCY_DISPLAY_NAME,AGENCY_CODE"); 
				txtAGENCY_NAME.DataSource = ds2.Tables[0];					
				txtAGENCY_NAME.DataTextField = "Name";
				txtAGENCY_NAME.DataValueField = "ID";
				txtAGENCY_NAME.DataBind();
				this.txtAGENCY_NAME.Items.Insert(0,"All");
				this.txtAGENCY_NAME.SelectedIndex =0;
			}
			
			btnAgencyTran.Attributes.Add("onClick","ShowEffectiveDate();ShowTransactionDate();Page_ClientValidate();ShowReport();return false;");
			//btnAgencyTran.Attributes.Add("onClick","ShowEffectiveDate();ShowTransactionDate();Page_ClientValidate();ShowReport('AGENCYTRANSACTION');return false;");
			//btnAgencyTranComm.Attributes.Add("onClick","ShowEffectiveDate();ShowTransactionDate();Page_ClientValidate();ShowReport('AGENCYTRANWITHCOMM');return false;");		

			
			btnSel.Attributes.Add("onClick","funcSetOrderBy();return false;");
			btnDeSel.Attributes.Add("onClick","funcRemoveOrderBy();return false;");
			
			hlkExpirationStartDate.Attributes.Add("OnClick","fPopCalendar(document.forms[0].txtExpirationStartDate,document.forms[0].txtExpirationStartDate)");			
			hlkExpirationEndDate.Attributes.Add("OnClick","fPopCalendar(document.forms[0].txtExpirationEndDate,document.forms[0].txtExpirationEndDate)");			
			
			revExpirationStartDate.ValidationExpression = aRegExpDate;
			revExpirationEndDate.ValidationExpression = aRegExpDate;	

			rfvEffectiveYear.ErrorMessage  = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId ,"3"); 
			rfvTransactionYear.ErrorMessage  = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId ,"3"); 

			hlkTransactionStartDate.Attributes.Add("OnClick","fPopCalendar(document.forms[0].txtTransactionStartDate,document.forms[0].txtTransactionStartDate)");			
			hlkTransactionEndDate.Attributes.Add("OnClick","fPopCalendar(document.forms[0].txtTransactionEndDate,document.forms[0].txtTransactionEndDate)");			
			
			revTransactionStartDate.ValidationExpression = aRegExpDate;
			revTransactionEndDate.ValidationExpression = aRegExpDate;
	
			revEffectiveYear.ValidationExpression = aRegExpInteger;
			revEffectiveYear.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");

			revTransactionYear.ValidationExpression = aRegExpInteger;
			revTransactionYear.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");


			rngEffectiveYEAR.MaximumValue = (DateTime.Now.Year+1).ToString();
			rngEffectiveYEAR.MinimumValue = aAppMinYear  ;
			rngEffectiveYEAR.ErrorMessage =  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");

			rngTransactionYEAR.MaximumValue = (DateTime.Now.Year+1).ToString();
			rngTransactionYEAR.MinimumValue = aAppMinYear  ;
			rngTransactionYEAR.ErrorMessage =  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");

		}

		#region Web Form Designer generated code
		 protected override void OnInit(EventArgs e)
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
