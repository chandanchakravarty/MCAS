/******************************************************************************************
	<Author					: -> Ravindra Kumar Gupta
	<Start Date				: -> 03-22-2006
	<End Date				: ->
	<Description			: -> Page to add Underlying Policies and Coverages
	<Review Date			: ->
	<Reviewed By			: ->
	
	Modification History
******************************************************************************************/

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using Cms.ExceptionPublisher.ExceptionManagement;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Cms.CmsWeb;
using Cms.CmsWeb.Controls;
using Cms.BusinessLayer.BlApplication;
using Cms.Model.Policy.Umbrella ;


namespace Cms.Policies.Aspx.Umbrella
{
	/// <summary>
	/// Summary description for PolicyAddScheduleOfUnderlying.
	/// </summary>
	public class PolicyAddScheduleOfUnderlying : Cms.Policies.policiesbase 
	{
		#region Page Controls Declaration
//		protected System.Web.UI.WebControls.CustomValidator csvEXPLAIN;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidHAS_MOTORIST_PROTECTION;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOWER_LIMITS;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidHAS_SIGNED_A9;
		protected System.Web.UI.WebControls.Label capPOLICY_LOB;
		protected System.Web.UI.WebControls.DropDownList cmbPOLICY_LOB;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOB;
		protected System.Web.UI.WebControls.Label capPOLICY_COMPANY;
		protected System.Web.UI.WebControls.DropDownList cmbPOLICY_COMPANY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOMPANY;
		protected System.Web.UI.WebControls.Label capCOMPANY_OTHER;
		protected System.Web.UI.WebControls.TextBox txtCOMPANY_OTHER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOMPANY_OTHER;
		protected System.Web.UI.WebControls.Label lbl_NA_COMPANY_OTHER;
		protected System.Web.UI.WebControls.Label capPOLICY_NUMBER;
		protected System.Web.UI.WebControls.DropDownList cmbPOLICY_NUMBER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPOLICY_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtPOLICY_NUMBER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvTXT_POLICY_NUMBER;
		//protected System.Web.UI.WebControls.Label capPOLICY_PREMIUM;
		//protected System.Web.UI.WebControls.TextBox txtPOLICY_PREMIUM;
		//protected System.Web.UI.WebControls.RegularExpressionValidator revPOLICY_PREMIUM;
		protected System.Web.UI.WebControls.Label capPOLICY_START_DATE;
		protected System.Web.UI.WebControls.TextBox txtPOLICY_START_DATE;
		protected System.Web.UI.WebControls.HyperLink hlkPOLICY_START_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revPOLICY_START_DATE;
		protected System.Web.UI.WebControls.Label capPOLICY_EXPIRATION_DATE;
		protected System.Web.UI.WebControls.TextBox txtPOLICY_EXPIRATION_DATE;
		protected System.Web.UI.WebControls.HyperLink hlkPOLICY_EXPIRATION_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revPOLICY_EXPIRATION_DATE;
		protected System.Web.UI.WebControls.Label capWAT_COV1;
		protected System.Web.UI.WebControls.TextBox txtWAT_COV1;
		protected System.Web.UI.WebControls.RegularExpressionValidator revWAT_COV1;
//		protected System.Web.UI.WebControls.Label capWAT_COV2;
//		protected System.Web.UI.WebControls.TextBox txtWAT_COV2;
//		protected System.Web.UI.WebControls.RegularExpressionValidator revWAT_COV2;
		protected System.Web.UI.WebControls.Label capWAT_COV3;
		protected System.Web.UI.WebControls.TextBox txtWAT_COV3;
		protected System.Web.UI.WebControls.RegularExpressionValidator revWAT_COV3;
//		protected System.Web.UI.WebControls.Label capWAT_COV4;
//		protected System.Web.UI.WebControls.TextBox txtWAT_COV4;
//		protected System.Web.UI.WebControls.RegularExpressionValidator revWAT_COV4;
//		protected System.Web.UI.WebControls.Label capAUTO_COV1;
//		protected System.Web.UI.WebControls.TextBox txtAUTO_COV1;
//		protected System.Web.UI.WebControls.RegularExpressionValidator revAUTO_COV1;
		protected System.Web.UI.WebControls.Label capAUTO_COV2;
		protected System.Web.UI.WebControls.TextBox txtAUTO_COV2;
		protected System.Web.UI.WebControls.RegularExpressionValidator revAUTO_COV2;
		protected System.Web.UI.WebControls.Label capAUTO_COV3;
		protected System.Web.UI.WebControls.TextBox txtAUTO_COV3;
		protected System.Web.UI.WebControls.RegularExpressionValidator revAUTO_COV3;
		protected System.Web.UI.WebControls.Label capAUTO_COV4;
		protected System.Web.UI.WebControls.TextBox txtAUTO_COV4;
		protected System.Web.UI.WebControls.RegularExpressionValidator revAUTO_COV4;
		protected System.Web.UI.WebControls.Label capAUTO_COV5;
		protected System.Web.UI.WebControls.TextBox txtAUTO_COV5;
		protected System.Web.UI.WebControls.RegularExpressionValidator revAUTO_COV5;
		protected System.Web.UI.WebControls.Label capAUTO_COV6;
		protected System.Web.UI.WebControls.TextBox txtAUTO_COV6;
		protected System.Web.UI.WebControls.RegularExpressionValidator revAUTO_COV6;
		protected System.Web.UI.WebControls.Label capAUTO_COV7;
		protected System.Web.UI.WebControls.TextBox txtAUTO_COV7;
		protected System.Web.UI.WebControls.RegularExpressionValidator revAUTO_COV7;
		
		protected System.Web.UI.WebControls.Label capAUTO_COV8;
		protected System.Web.UI.WebControls.TextBox txtAUTO_COV8;
		protected System.Web.UI.WebControls.RegularExpressionValidator revAUTO_COV8;
		protected System.Web.UI.WebControls.Label capAUTO_COV9;
		protected System.Web.UI.WebControls.TextBox txtAUTO_COV9;
		protected System.Web.UI.WebControls.Label capAUTO_COV10;
		protected System.Web.UI.WebControls.TextBox txtAUTO_COV10;
		protected System.Web.UI.WebControls.RegularExpressionValidator revAUTO_COV10;
		protected System.Web.UI.WebControls.Label capAUTO_COV11;
		protected System.Web.UI.WebControls.TextBox txtAUTO_COV11;
		protected System.Web.UI.WebControls.Label capAUTO_COV12;
		protected System.Web.UI.WebControls.TextBox txtAUTO_COV12;
		protected System.Web.UI.WebControls.RegularExpressionValidator revAUTO_COV12;
		protected System.Web.UI.WebControls.Label capAUTO_COV13;
		protected System.Web.UI.WebControls.TextBox txtAUTO_COV13;
		protected System.Web.UI.WebControls.Label capAUTO_COV14;
		protected System.Web.UI.WebControls.TextBox txtAUTO_COV14;
		protected System.Web.UI.WebControls.RegularExpressionValidator revAUTO_COV14;

//		protected System.Web.UI.WebControls.RegularExpressionValidator revAUTO_COV9;
//		protected System.Web.UI.WebControls.Label capHOME_COV1;
//		protected System.Web.UI.WebControls.TextBox txtHOME_COV1;
//		protected System.Web.UI.WebControls.RegularExpressionValidator revHOME_COV1;
//		protected System.Web.UI.WebControls.Label capHOME_COV2;
//		protected System.Web.UI.WebControls.TextBox txtHOME_COV2;
//		protected System.Web.UI.WebControls.RegularExpressionValidator revHOME_COV2;
//		protected System.Web.UI.WebControls.Label capHOME_COV3;
//		protected System.Web.UI.WebControls.TextBox txtHOME_COV3;
//		protected System.Web.UI.WebControls.RegularExpressionValidator revHOME_COV3;
//		protected System.Web.UI.WebControls.Label capHOME_COV4;
//		protected System.Web.UI.WebControls.TextBox txtHOME_COV4;
//		protected System.Web.UI.WebControls.RegularExpressionValidator revHOME_COV4;
		protected System.Web.UI.WebControls.Label capHOME_COV5;
		protected System.Web.UI.WebControls.TextBox txtHOME_COV5;
		protected System.Web.UI.WebControls.RegularExpressionValidator revHOME_COV5;
//		protected System.Web.UI.WebControls.Label capHOME_COV6;
//		protected System.Web.UI.WebControls.TextBox txtHOME_COV6;
//		protected System.Web.UI.WebControls.RegularExpressionValidator revHOME_COV6;
//		protected System.Web.UI.WebControls.Label capQUESTION;
//		protected System.Web.UI.WebControls.DropDownList cmbQUESTION;
//		protected System.Web.UI.WebControls.Label capEXPLAIN;
//		protected System.Web.UI.WebControls.TextBox txtEXPLAIN;
//		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEXPLAIN;
//		protected System.Web.UI.WebControls.Label lblNA_EXP;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlForm SCHEDULE_OF_UNDERLYING;
		protected System.Web.UI.HtmlControls.HtmlTableRow trPolicy;
		protected System.Web.UI.HtmlControls.HtmlTableRow trCOVERAGES;
		protected System.Web.UI.HtmlControls.HtmlTableRow trWATERCRAFT_COVERAGES1;
//		protected System.Web.UI.HtmlControls.HtmlTableRow trWATERCRAFT_COVERAGES2;
		protected System.Web.UI.HtmlControls.HtmlTableRow trAUTO_COVERAGES1;
		protected System.Web.UI.HtmlControls.HtmlTableRow trAUTO_COVERAGES2;
		protected System.Web.UI.HtmlControls.HtmlTableRow trAUTO_COVERAGES3;
		protected System.Web.UI.HtmlControls.HtmlTableRow trAUTO_COVERAGES4;
		protected System.Web.UI.HtmlControls.HtmlTableRow trAUTO_COVERAGES5;
		protected System.Web.UI.HtmlControls.HtmlTableRow trAUTO_COVERAGES6;
		protected System.Web.UI.HtmlControls.HtmlTableRow trAUTO_COVERAGES7;
		protected System.Web.UI.HtmlControls.HtmlTableRow trAUTO_COVERAGES8;
		protected System.Web.UI.HtmlControls.HtmlTableRow trAUTO_COVERAGES9;
		protected System.Web.UI.HtmlControls.HtmlTableRow trAUTO_COVERAGES10;
//		protected System.Web.UI.HtmlControls.HtmlTableRow trHOME_COVERAGES1;
//		protected System.Web.UI.HtmlControls.HtmlTableRow trHOME_COVERAGES2;
		protected System.Web.UI.HtmlControls.HtmlTableRow trHOME_COVERAGES3;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAppStateID;
		protected System.Web.UI.WebControls.Label lblMOT_COV4;
		protected System.Web.UI.WebControls.Label lblMOT_COV5;
		protected System.Web.UI.WebControls.Label lblMOT_COV6;
		protected System.Web.UI.WebControls.Label lblMOT_COV7;
		protected System.Web.UI.WebControls.Label capMOT_COV7;
		protected System.Web.UI.WebControls.TextBox txtMOT_COV7;
		protected System.Web.UI.WebControls.RegularExpressionValidator revMOT_COV7;
		protected System.Web.UI.HtmlControls.HtmlTableRow trMOT_COVERAGES4;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyNumber;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLobId;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPOLICY_START_DATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPOLICY_EXPIRATION_DATE;
		protected System.Web.UI.WebControls.Label capMOT_COV1;
		protected System.Web.UI.WebControls.TextBox txtMOT_COV1;
		protected System.Web.UI.WebControls.RegularExpressionValidator revMOT_COV1;
		protected System.Web.UI.WebControls.Label capMOT_COV2;
		protected System.Web.UI.WebControls.TextBox txtMOT_COV2;
		protected System.Web.UI.WebControls.RegularExpressionValidator revMOT_COV2;
		protected System.Web.UI.WebControls.Label capMOT_COV3;
		protected System.Web.UI.WebControls.TextBox txtMOT_COV3;
		protected System.Web.UI.WebControls.RegularExpressionValidator revMOT_COV3;
		protected System.Web.UI.WebControls.Label capMOT_COV4;
		protected System.Web.UI.WebControls.TextBox txtMOT_COV4;
		protected System.Web.UI.WebControls.RegularExpressionValidator revMOT_COV4;
		protected System.Web.UI.WebControls.Label capMOT_COV5;
		protected System.Web.UI.WebControls.TextBox txtMOT_COV5;
		protected System.Web.UI.WebControls.RegularExpressionValidator revMOT_COV5;
		protected System.Web.UI.WebControls.Label capMOT_COV6;
		protected System.Web.UI.WebControls.TextBox txtMOT_COV6;
		protected System.Web.UI.WebControls.RegularExpressionValidator revMOT_COV6;
		protected System.Web.UI.HtmlControls.HtmlTableRow trMOT_COVERAGES1;
		protected System.Web.UI.HtmlControls.HtmlTableRow trMOT_COVERAGES2;
		protected System.Web.UI.HtmlControls.HtmlTableRow trMOT_COVERAGES3;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
		//int intApp_ID;
		protected System.Web.UI.WebControls.Label lblAUTO_COV4;
		protected System.Web.UI.WebControls.Label lblAUTO_COV5;
		protected System.Web.UI.WebControls.Label lblAUTO_COV6;
		protected System.Web.UI.WebControls.Label lblAUTO_COV7;	
		/*** added by Manoj on 5th dec 2007***/
		protected System.Web.UI.WebControls.Label capEXCLUDE_UNINSURED_MOTORIST;
		protected System.Web.UI.WebControls.CheckBox chkEXCLUDE_UNINSURED_MOTORIST;
		
		/**************/
		protected System.Web.UI.WebControls.Label capHAS_MOTORIST_PROTECTION;
		protected System.Web.UI.WebControls.DropDownList cmbHAS_MOTORIST_PROTECTION;
		protected System.Web.UI.WebControls.Label capHAS_SIGNED_A9;
		protected System.Web.UI.WebControls.DropDownList cmbHAS_SIGNED_A9;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvHAS_MOTORIST_PROTECTION;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvHAS_SIGNED_A9;
		protected System.Web.UI.WebControls.Label lblDecPage;
		protected System.Web.UI.HtmlControls.HtmlImage imgOpenDecPage;

		protected System.Web.UI.HtmlControls.HtmlTableRow trCHECKBUTTONS;
		
		protected System.Web.UI.WebControls.RadioButton rdoCSL;
		protected System.Web.UI.WebControls.RadioButton rdoSPLIT;
		protected System.Web.UI.WebControls.Label capCHECK;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyCompany;
		protected System.Web.UI.WebControls.Label capLOWER_LIMITS;
		protected System.Web.UI.WebControls.DropDownList cmbLOWER_LIMITS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOWER_LIMITS;
		protected System.Web.UI.WebControls.CompareValidator cmpToDate;

		#endregion

		#region Private Variable Declaration

		protected string strPolicy_Number;
		protected string strPolicy_Company;

		int intPolicyID;
		//protected System.Web.UI.WebControls.Label capAUTO_COV8;
		//protected System.Web.UI.WebControls.TextBox txtAUTO_COV8;
		//protected System.Web.UI.WebControls.RegularExpressionValidator revAUTO_COV8;
		//protected System.Web.UI.HtmlControls.HtmlTableRow Tr1;
		int intPolicyVerID;
		
		protected int intCustomerID;
		protected int gintPolicy_ID=0;
		protected int gintPolicy_Ver_ID=0;
		protected System.Web.UI.WebControls.Label capMOT_COV8;
		protected System.Web.UI.WebControls.Label capMOT_COV9;
		protected System.Web.UI.WebControls.TextBox txtMOT_COV9;
		protected System.Web.UI.WebControls.RegularExpressionValidator revMOT_COV9;
		protected System.Web.UI.WebControls.Label capMOT_COV10;
		protected System.Web.UI.WebControls.Label capMOT_COV11;
		protected System.Web.UI.WebControls.TextBox txtMOT_COV11;
		protected System.Web.UI.WebControls.RegularExpressionValidator revMOT_COV11;
		protected System.Web.UI.WebControls.Label capMOT_COV12;
		protected System.Web.UI.WebControls.Label capMOT_COV13;
		protected System.Web.UI.WebControls.TextBox txtMOT_COV13;
		protected System.Web.UI.WebControls.RegularExpressionValidator revMOT_COV13;
		protected System.Web.UI.HtmlControls.HtmlTableRow trMOT_COVERAGES5;
		protected System.Web.UI.HtmlControls.HtmlTableRow trMOT_COVERAGES8;
		protected System.Web.UI.HtmlControls.HtmlTableRow trMOT_COVERAGES6;
		protected System.Web.UI.HtmlControls.HtmlTableRow trMOT_COVERAGES9;
		protected System.Web.UI.HtmlControls.HtmlTableRow trMOT_COVERAGES10;
		protected System.Web.UI.HtmlControls.HtmlTableRow trMOT_COVERAGES7;
		protected string strAPP="";
		public string strState ="";
		#endregion
	
		#region PageLoad Event
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId ="273_3_0";

			this.hlkPOLICY_START_DATE.Attributes.Add("OnClick","fPopCalendar(document.SCHEDULE_OF_UNDERLYING.txtPOLICY_START_DATE,document.SCHEDULE_OF_UNDERLYING.txtPOLICY_START_DATE)");
			this.hlkPOLICY_EXPIRATION_DATE.Attributes.Add("OnClick","fPopCalendar(document.SCHEDULE_OF_UNDERLYING.txtPOLICY_EXPIRATION_DATE,document.SCHEDULE_OF_UNDERLYING.txtPOLICY_EXPIRATION_DATE)");
			this.btnReset.Attributes.Add("onclick","javascript:return ResetForm();");
			this.imgOpenDecPage.Attributes.Add("onclick","javascript:OpenDecPage();");
			this.rdoCSL.Attributes.Add("onclick","javascript:EnableCoverages();");
			this.rdoSPLIT.Attributes.Add("onclick","javascript:EnableCoverages();");
			AddAttributes();
						
			btnReset.CmsButtonClass			=	CmsButtonType.Write ;
			btnReset.PermissionString		=	gstrSecurityXML;

			btnSave.CmsButtonClass	        =	CmsButtonType.Write ;
			btnSave.PermissionString		=	gstrSecurityXML;

			intPolicyID  =Convert.ToInt32(GetPolicyID()); 
			intPolicyVerID  =Convert.ToInt32 (GetPolicyVersionID());
			intCustomerID=Convert.ToInt32 (GetCustomerID());
			Cms.BusinessLayer.BlProcess.ClsCommonPdfXML objCommonPdfXML = new Cms.BusinessLayer.BlProcess.ClsCommonPdfXML();
			strState = objCommonPdfXML.SetStateCode("Policy",intPolicyID,intPolicyVerID,intCustomerID);
			if(!Page.IsPostBack)
			{
				SetCaptions();
				//DataTable dtLOBs = ClsScheduleOfUnderlying.GetUmbrellaLob(intCustomer_ID,intApp_ID,intApp_Version_ID );
				DataTable dtLOBs = Cms.CmsWeb.ClsFetcher.LOBs;;
				cmbPOLICY_LOB.DataSource			= dtLOBs;
				cmbPOLICY_LOB.DataTextField		= "LOB_DESC";
				cmbPOLICY_LOB.DataValueField		= "LOB_ID";
				cmbPOLICY_LOB.DataBind();
				cmbPOLICY_LOB.Items.Insert(0,new ListItem("",""));

				//Remove General Liability and Umbrella from the list
				ListItem iListItem = cmbPOLICY_LOB.Items.FindByValue(((int)enumLOB.UMB).ToString());
				if(iListItem!=null)
					cmbPOLICY_LOB.Items.Remove(iListItem);

				iListItem = cmbPOLICY_LOB.Items.FindByValue(((int)enumLOB.GENL).ToString());
				if(iListItem!=null)
					cmbPOLICY_LOB.Items.Remove(iListItem);

				cmbPOLICY_LOB.SelectedIndex=0;

				cmbPOLICY_NUMBER.Items.Insert(0,new ListItem("",""));

				DisableAllCoverages();
				SetDropdownList();
				SetValidators();
				strPolicy_Number =Request.QueryString["POLICY_NUMBER"];
				if(strPolicy_Number != null)
				{
					hidPolicyNumber.Value =strPolicy_Number;
					hidPolicyCompany.Value = strPolicy_Company;
					LoadData();
				}

				if(cmbPOLICY_NUMBER.SelectedValue != null && cmbPOLICY_NUMBER.SelectedValue != "")
				{
					string strCombined =cmbPOLICY_NUMBER.SelectedValue.ToString();
					string [] strArray = strCombined.Split('-');
			
					if(strArray.Length>0)
					{
						if(strArray[0]!=null && strArray[0].ToString().Trim()!="")
							strAPP = strArray[0].ToString().Trim();
						if(strArray[1]!=null && strArray[1].ToString()!="")
							gintPolicy_ID = int.Parse(strArray[1].ToString().Trim());
						if(strArray[2]!=null && strArray[2].ToString()!="")
							gintPolicy_Ver_ID = int.Parse(strArray[2].ToString().Trim());
					}
				}
			
			}
			
			SetWorkFlow();
		}
		#endregion

		#region DisableAllCoverages Function

		private void DisableAllCoverages()
		{
			this.trCHECKBUTTONS.Attributes.Add("style","display:none");
			this.trCOVERAGES.Attributes.Add("style","display:none");
			this.trCOVERAGES.Attributes.Add("style","display:none");		
			this.trAUTO_COVERAGES1.Attributes.Add("style","display:none");					
			this.trAUTO_COVERAGES2.Attributes.Add("style","display:none");		
			this.trAUTO_COVERAGES3.Attributes.Add("style","display:none");		
			this.trAUTO_COVERAGES4.Attributes.Add("style","display:none");
			this.trAUTO_COVERAGES5.Attributes.Add("style","display:none");					
			this.trAUTO_COVERAGES6.Attributes.Add("style","display:none");		
			this.trAUTO_COVERAGES7.Attributes.Add("style","display:none");		
			this.trAUTO_COVERAGES8.Attributes.Add("style","display:none");
			this.trAUTO_COVERAGES9.Attributes.Add("style","display:none");		
			this.trAUTO_COVERAGES10.Attributes.Add("style","display:none");		
			
		
		
			this.trMOT_COVERAGES1.Attributes.Add("style","display:none");					
			this.trMOT_COVERAGES2.Attributes.Add("style","display:none");		
			this.trMOT_COVERAGES3.Attributes.Add("style","display:none");		
			this.trMOT_COVERAGES4.Attributes.Add("style","display:none");
			
			this.trMOT_COVERAGES5.Attributes.Add("style","display:none");					
			this.trMOT_COVERAGES6.Attributes.Add("style","display:none");		
			this.trMOT_COVERAGES7.Attributes.Add("style","display:none");		
			this.trMOT_COVERAGES8.Attributes.Add("style","display:none");
			this.trMOT_COVERAGES9.Attributes.Add("style","display:none");		
			this.trMOT_COVERAGES10.Attributes.Add("style","display:none");
					
			this.trWATERCRAFT_COVERAGES1.Attributes.Add("style","display:none");						
			this.trHOME_COVERAGES3.Attributes.Add("style","display:none");	
			
			this.imgOpenDecPage.Attributes.Add("style","display:none");
			this.lblDecPage.Attributes.Add("style","display:none");
		}
							

		#endregion

		#region SetValidators Function

		private void SetValidators()
		{
			this.rfvLOB.ErrorMessage =Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			this.rfvCOMPANY.ErrorMessage =Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
			this.rfvCOMPANY_OTHER.ErrorMessage  =Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");
			this.rfvPOLICY_NUMBER.ErrorMessage  =Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"4");
			this.rfvTXT_POLICY_NUMBER.ErrorMessage  =Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"8");

			//this.revPOLICY_PREMIUM.ValidationExpression =aRegExpDoublePositiveZero;
			//this.revPOLICY_PREMIUM.ErrorMessage =Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");

			this.rfvPOLICY_START_DATE.ErrorMessage =Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"9");
			this.rfvPOLICY_EXPIRATION_DATE.ErrorMessage=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"10");
			this.revPOLICY_START_DATE.ValidationExpression  =aRegExpDate ;
			this.revPOLICY_START_DATE.ErrorMessage =Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"6");

			this.revPOLICY_EXPIRATION_DATE.ValidationExpression  =aRegExpDate ;
			this.revPOLICY_EXPIRATION_DATE .ErrorMessage =Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"6");
			
//			this.revAUTO_COV1.ValidationExpression =aRegExpDoublePositiveZero;
//			this.revAUTO_COV1.ErrorMessage =Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");
			
			this.revAUTO_COV2.ValidationExpression =aRegExpDoublePositiveZero;
			this.revAUTO_COV2.ErrorMessage =Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");

			this.revAUTO_COV3.ValidationExpression =aRegExpDoublePositiveZero;
			this.revAUTO_COV3.ErrorMessage =Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");
			
			this.revAUTO_COV4.ValidationExpression =aRegExpDoublePositiveZero;
			this.revAUTO_COV4.ErrorMessage =Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");
			
			this.revAUTO_COV5.ValidationExpression =aRegExpDoublePositiveZero;
			this.revAUTO_COV5.ErrorMessage =Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");
			
			this.revAUTO_COV6.ValidationExpression =aRegExpDoublePositiveZero;
			this.revAUTO_COV6.ErrorMessage =Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");
			
			this.revAUTO_COV7.ValidationExpression =aRegExpDoublePositiveZero;
			this.revAUTO_COV7.ErrorMessage =Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");
			
			this.revAUTO_COV8.ValidationExpression =aRegExpDoublePositiveZero;
			this.revAUTO_COV8.ErrorMessage =Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");
			
			this.revAUTO_COV10 .ValidationExpression =aRegExpDoublePositiveZero;
			this.revAUTO_COV10.ErrorMessage =Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");

			this.revAUTO_COV12 .ValidationExpression =aRegExpDoublePositiveZero;
			this.revAUTO_COV12.ErrorMessage =Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");

			this.revAUTO_COV14 .ValidationExpression =aRegExpDoublePositiveZero;
			this.revAUTO_COV14.ErrorMessage =Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");

			this.revMOT_COV1.ValidationExpression =aRegExpDoublePositiveZero;
			this.revMOT_COV1.ErrorMessage =Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");
			
			this.revMOT_COV2.ValidationExpression =aRegExpDoublePositiveZero;
			this.revMOT_COV2.ErrorMessage =Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");

			this.revMOT_COV3.ValidationExpression =aRegExpDoublePositiveZero;
			this.revMOT_COV3.ErrorMessage =Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");
			
			this.revMOT_COV4.ValidationExpression =aRegExpDoublePositiveZero;
			this.revMOT_COV4.ErrorMessage =Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");
			
			this.revMOT_COV5.ValidationExpression =aRegExpDoublePositiveZero;
			this.revMOT_COV5.ErrorMessage =Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");
			
			this.revMOT_COV6.ValidationExpression =aRegExpDoublePositiveZero;
			this.revMOT_COV6.ErrorMessage =Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");

			this.revMOT_COV7.ValidationExpression =aRegExpDoublePositiveZero;
			this.revMOT_COV7.ErrorMessage =Cms.CmsWeb.ClsMessages.GetMessage (base.ScreenId,"5");
/////
			this.revMOT_COV9.ValidationExpression =aRegExpDoublePositiveZero;
			this.revMOT_COV9.ErrorMessage =Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");

			this.revMOT_COV11.ValidationExpression =aRegExpDoublePositiveZero;
			this.revMOT_COV11.ErrorMessage =Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");
			
			this.revMOT_COV13.ValidationExpression =aRegExpDoublePositiveZero;
			this.revMOT_COV13.ErrorMessage =Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");
			
			
////
			this.revWAT_COV1.ValidationExpression =aRegExpDoublePositiveZero;
			this.revWAT_COV1 .ErrorMessage =Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");

//			this.revWAT_COV2.ValidationExpression =aRegExpDoublePositiveZero;
//			this.revWAT_COV2 .ErrorMessage =Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");

			this.revWAT_COV3.ValidationExpression =aRegExpDoublePositiveZero;
			this.revWAT_COV3 .ErrorMessage =Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");

//			this.revWAT_COV4 .ValidationExpression =aRegExpDoublePositiveZero;
//			this.revWAT_COV4 .ErrorMessage =Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");


//			this.revHOME_COV1.ValidationExpression =aRegExpDoublePositiveZero;
//			this.revHOME_COV1 .ErrorMessage =Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");

//			this.revHOME_COV2.ValidationExpression =aRegExpDoublePositiveZero;
//			this.revHOME_COV2 .ErrorMessage =Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");

//			this.revHOME_COV3.ValidationExpression =aRegExpDoublePositiveZero;
//			this.revHOME_COV3 .ErrorMessage =Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");

//			this.revHOME_COV4.ValidationExpression =aRegExpDoublePositiveZero;
//			this.revHOME_COV4 .ErrorMessage =Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");

			this.revHOME_COV5.ValidationExpression =aRegExpDoublePositiveZero;
			this.revHOME_COV5 .ErrorMessage =Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");

//			this.revHOME_COV6.ValidationExpression =aRegExpDoublePositiveZero;
//			this.revHOME_COV6 .ErrorMessage =Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");

//			this.rfvEXPLAIN.ErrorMessage=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"11");
//			this.csvEXPLAIN.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"12");
		}
		#endregion 

		# region SET DROP DOWN LIST
		private void SetDropdownList()
		{
			cmbHAS_MOTORIST_PROTECTION.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbHAS_MOTORIST_PROTECTION.DataTextField="LookupDesc"; 
			cmbHAS_MOTORIST_PROTECTION.DataValueField="LookupCode";
			cmbHAS_MOTORIST_PROTECTION.DataBind();
			//cmbHAS_MOTORIST_PROTECTION.Items.Insert(0,"");

			cmbHAS_SIGNED_A9.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbHAS_SIGNED_A9.DataTextField="LookupDesc"; 
			cmbHAS_SIGNED_A9.DataValueField="LookupCode";
			cmbHAS_SIGNED_A9.DataBind();
			//cmbHAS_SIGNED_A9.Items.Insert(0,"");

			cmbLOWER_LIMITS.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbLOWER_LIMITS.DataTextField="LookupDesc"; 
			cmbLOWER_LIMITS.DataValueField="LookupCode";
			cmbLOWER_LIMITS.DataBind();

		}
		# endregion 

		#region SetCaptions Function 
		private void SetCaptions()
		{
			System.Resources.ResourceManager objResourceMgr  = new System.Resources.ResourceManager("Cms.Policies.Aspx.Umbrella.PolicyAddScheduleOfUnderlying" ,System.Reflection.Assembly.GetExecutingAssembly());
			this.capPOLICY_LOB.Text =objResourceMgr.GetString("cmbPOLICY_LOB");
			this.capPOLICY_COMPANY.Text =objResourceMgr.GetString("cmbPOLICY_COMPANY");
			this.capCOMPANY_OTHER.Text=objResourceMgr.GetString("txtCOMPANY_OTHER");
			this.capPOLICY_NUMBER.Text =objResourceMgr.GetString("cmbPOLICY_NUMBER");
			this.capPOLICY_START_DATE .Text =objResourceMgr.GetString("txtPOLICY_START_DATE");
			this.capPOLICY_EXPIRATION_DATE.Text =objResourceMgr.GetString("txtPOLICY_EXPIRATION_DATE");
			//this.capPOLICY_PREMIUM .Text =objResourceMgr.GetString("txtPOLICY_PREMIUM");
			
//			this.capAUTO_COV1.Text =objResourceMgr.GetString("txtAUTO_COV1");
			this.capAUTO_COV2.Text =objResourceMgr.GetString("txtAUTO_COV2");
			this.capAUTO_COV3.Text =objResourceMgr.GetString("txtAUTO_COV3");
			this.capAUTO_COV4.Text =objResourceMgr.GetString("txtAUTO_COV4");
			this.capAUTO_COV5.Text =objResourceMgr.GetString("txtAUTO_COV5");
			this.capAUTO_COV6.Text =objResourceMgr.GetString("txtAUTO_COV6");
			this.capAUTO_COV7.Text =objResourceMgr.GetString("txtAUTO_COV7");
			this.capAUTO_COV8.Text =objResourceMgr.GetString("txtAUTO_COV8");
			this.capAUTO_COV9.Text =objResourceMgr.GetString("capAUTO_COV9");
			this.capAUTO_COV10.Text =objResourceMgr.GetString("txtAUTO_COV10");
			this.capAUTO_COV11.Text =objResourceMgr.GetString("capAUTO_COV11");
			this.capAUTO_COV12.Text =objResourceMgr.GetString("txtAUTO_COV12");
			this.capAUTO_COV13.Text =objResourceMgr.GetString("capAUTO_COV13");
			this.capAUTO_COV14.Text =objResourceMgr.GetString("txtAUTO_COV14");


			this.capMOT_COV1.Text =objResourceMgr.GetString("txtMOT_COV1");
			this.capMOT_COV2.Text =objResourceMgr.GetString("txtMOT_COV2");
			this.capMOT_COV3.Text =objResourceMgr.GetString("txtMOT_COV3");
			this.capMOT_COV4.Text =objResourceMgr.GetString("txtMOT_COV4");
			this.capMOT_COV5.Text =objResourceMgr.GetString("txtMOT_COV5");
			this.capMOT_COV6.Text =objResourceMgr.GetString("txtMOT_COV6");
			this.capMOT_COV7.Text =objResourceMgr.GetString("txtMOT_COV7");

			this.capMOT_COV8.Text =objResourceMgr.GetString("capMOT_COV8");
			this.capMOT_COV9.Text =objResourceMgr.GetString("txtMOT_COV9");
			this.capMOT_COV10.Text =objResourceMgr.GetString("capMOT_COV10");
			this.capMOT_COV11.Text =objResourceMgr.GetString("txtMOT_COV11");
			this.capMOT_COV12.Text =objResourceMgr.GetString("capMOT_COV12");
			this.capMOT_COV13.Text =objResourceMgr.GetString("txtMOT_COV13");

			this.capWAT_COV1.Text =objResourceMgr.GetString("txtWAT_COV1");
//			this.capWAT_COV2.Text =objResourceMgr.GetString("txtWAT_COV2");
			this.capWAT_COV3.Text =objResourceMgr.GetString("txtWAT_COV3");
//			this.capWAT_COV4.Text =objResourceMgr.GetString("txtWAT_COV4");

//			this.capHOME_COV1.Text =objResourceMgr.GetString("txtHOME_COV1");
//			this.capHOME_COV2.Text =objResourceMgr.GetString("txtHOME_COV2");
//			this.capHOME_COV3.Text =objResourceMgr.GetString("txtHOME_COV3");
//			this.capHOME_COV4.Text =objResourceMgr.GetString("txtHOME_COV4");
			this.capHOME_COV5.Text =objResourceMgr.GetString("txtHOME_COV5");
//			this.capHOME_COV6.Text =objResourceMgr.GetString("txtHOME_COV6");

//			this.capQUESTION.Text  =objResourceMgr.GetString("cmbQUESTION");
//			this.capEXPLAIN.Text   =objResourceMgr.GetString("txtEXPLAIN");

			this.capHAS_MOTORIST_PROTECTION.Text = objResourceMgr.GetString("cmbHAS_MOTORIST_PROTECTION");
			this.capHAS_SIGNED_A9.Text = objResourceMgr.GetString("cmbHAS_SIGNED_A9");
			this.capCHECK.Text = objResourceMgr.GetString("capCHECK");
			this.capLOWER_LIMITS.Text = objResourceMgr.GetString("cmbLOWER_LIMITS");

			
		}
		#endregion 

		#region EnableDisableCoverages Function

		void EnableDisableCoverages(int intLob_ID)
		{
			
			if(cmbPOLICY_COMPANY.SelectedValue.ToString() == "1" && cmbPOLICY_LOB.SelectedIndex > 0)				
				this.trCOVERAGES.Attributes.Add("style","display:inline");		
			else
				this.trCOVERAGES.Attributes.Add("style","display:none");	
			//
			if(cmbPOLICY_COMPANY.SelectedValue.ToString() == "0" && cmbPOLICY_LOB.SelectedIndex > 0 && cmbPOLICY_NUMBER.SelectedIndex > 0)				
			{
				this.imgOpenDecPage.Attributes.Add("style","display:inline;CURSOR:hand");	
				this.lblDecPage.Attributes.Add("style","display:inline");	
				
			}
			else
			{
				this.imgOpenDecPage.Attributes.Add("style","display:none");	
				this.lblDecPage.Attributes.Add("style","display:none");	
			}
	
			if(cmbPOLICY_COMPANY.SelectedValue.ToString() == "1") // if other then 
			{
				if(intLob_ID==2) //Auto or Cycle
				{
					this.trAUTO_COVERAGES1.Attributes.Add("style","display:inline");					
					this.trAUTO_COVERAGES2.Attributes.Add("style","display:inline");		
					this.trAUTO_COVERAGES3.Attributes.Add("style","display:inline");		
					this.trAUTO_COVERAGES4.Attributes.Add("style","display:inline");
		
					this.trMOT_COVERAGES1.Attributes.Add("style","display:none");					
					this.trMOT_COVERAGES2.Attributes.Add("style","display:none");		
					this.trMOT_COVERAGES3.Attributes.Add("style","display:none");		
					
					
					this.trWATERCRAFT_COVERAGES1.Attributes.Add("style","display:none");						
					this.trHOME_COVERAGES3.Attributes.Add("style","display:none");
				}
				else if(intLob_ID==3) //Cycle
				{
					this.trAUTO_COVERAGES1.Attributes.Add("style","display:none");					
					this.trAUTO_COVERAGES2.Attributes.Add("style","display:none");		
					this.trAUTO_COVERAGES3.Attributes.Add("style","display:none");		
					this.trAUTO_COVERAGES4.Attributes.Add("style","display:none");
		
					this.trMOT_COVERAGES1.Attributes.Add("style","display:inline");					
					this.trMOT_COVERAGES2.Attributes.Add("style","display:inline");		
					this.trMOT_COVERAGES3.Attributes.Add("style","display:inline");		
					this.trMOT_COVERAGES4.Attributes.Add("style","display:inline");
					this.trMOT_COVERAGES5.Attributes.Add("style","display:inline");					
					this.trMOT_COVERAGES6.Attributes.Add("style","display:inline");		
					this.trMOT_COVERAGES7.Attributes.Add("style","display:inline");		
					this.trMOT_COVERAGES8.Attributes.Add("style","display:inline");		
					this.trMOT_COVERAGES9.Attributes.Add("style","display:inline");		
					this.trMOT_COVERAGES10.Attributes.Add("style","display:inline");	
					
					this.trWATERCRAFT_COVERAGES1.Attributes.Add("style","display:none");						
					this.trHOME_COVERAGES3.Attributes.Add("style","display:none");
				}
				else if(intLob_ID==4) //Watercraft
				{
					this.trAUTO_COVERAGES1.Attributes.Add("style","display:none");					
					this.trAUTO_COVERAGES2.Attributes.Add("style","display:none");		
					this.trAUTO_COVERAGES3.Attributes.Add("style","display:none");		
					this.trAUTO_COVERAGES4.Attributes.Add("style","display:none");
		
					this.trMOT_COVERAGES1.Attributes.Add("style","display:none");					
					this.trMOT_COVERAGES2.Attributes.Add("style","display:none");		
					this.trMOT_COVERAGES3.Attributes.Add("style","display:none");		
					this.trMOT_COVERAGES4.Attributes.Add("style","display:none");						
					this.trMOT_COVERAGES5.Attributes.Add("style","display:none");					
					this.trMOT_COVERAGES6.Attributes.Add("style","display:none");		
					this.trMOT_COVERAGES7.Attributes.Add("style","display:none");		
					this.trMOT_COVERAGES8.Attributes.Add("style","display:none");		
					this.trMOT_COVERAGES9.Attributes.Add("style","display:none");		
					this.trMOT_COVERAGES10.Attributes.Add("style","display:none");	

					this.trWATERCRAFT_COVERAGES1.Attributes.Add("style","display:inline");						
					this.trHOME_COVERAGES3.Attributes.Add("style","display:none");						
				}
				else if(intLob_ID==1 ||intLob_ID==6 ) //Home & rental
				{
					this.trAUTO_COVERAGES1.Attributes.Add("style","display:none");					
					this.trAUTO_COVERAGES2.Attributes.Add("style","display:none");		
					this.trAUTO_COVERAGES3.Attributes.Add("style","display:none");		
					this.trAUTO_COVERAGES4.Attributes.Add("style","display:none");
		
					this.trMOT_COVERAGES1.Attributes.Add("style","display:none");					
					this.trMOT_COVERAGES2.Attributes.Add("style","display:none");		
					this.trMOT_COVERAGES3.Attributes.Add("style","display:none");		
					this.trMOT_COVERAGES4.Attributes.Add("style","display:none");
					this.trMOT_COVERAGES5.Attributes.Add("style","display:none");					
					this.trMOT_COVERAGES6.Attributes.Add("style","display:none");		
					this.trMOT_COVERAGES7.Attributes.Add("style","display:none");		
					this.trMOT_COVERAGES8.Attributes.Add("style","display:none");		
					this.trMOT_COVERAGES9.Attributes.Add("style","display:none");		
					this.trMOT_COVERAGES10.Attributes.Add("style","display:none");		
					
					this.trWATERCRAFT_COVERAGES1.Attributes.Add("style","display:none");						
					this.trHOME_COVERAGES3.Attributes.Add("style","display:inline");						
				}
			}
			else
				DisableAllCoverages();
		}
		#endregion 

		#region ResetCoverages Function
		void ResetCoverages()
		{
			//ResetAutoCoverages TextBox
//			this.txtAUTO_COV1.Text ="";
			this.txtAUTO_COV2.Text ="";
			this.txtAUTO_COV3.Text ="";
			this.txtAUTO_COV4.Text ="";
			this.txtAUTO_COV5.Text ="";
			this.txtAUTO_COV6.Text ="";
			this.txtAUTO_COV7.Text ="";
//			this.txtAUTO_COV8.Text ="";
//			this.txtAUTO_COV9.Text ="";

			//Reset Cycle Coverages TextBox
			this.txtMOT_COV1.Text ="";
			this.txtMOT_COV2.Text ="";
			this.txtMOT_COV3.Text ="";
			this.txtMOT_COV4.Text ="";
			this.txtMOT_COV5.Text ="";
			this.txtMOT_COV6.Text ="";
			this.txtMOT_COV7.Text ="";
			this.txtMOT_COV9.Text ="";
			this.txtMOT_COV11.Text ="";
			this.txtMOT_COV13.Text ="";
			//Reset Home Coverages TextBox

//			this.txtHOME_COV1.Text ="";
//			this.txtHOME_COV2.Text ="";
//			this.txtHOME_COV3.Text ="";
//			this.txtHOME_COV4.Text ="";
			this.txtHOME_COV5.Text ="";
//			this.txtHOME_COV6.Text ="";

			//Reset WaterCraft Coverages

			this.txtWAT_COV1.Text ="";
//			this.txtWAT_COV2.Text ="";
			this.txtWAT_COV3.Text ="";
//			this.txtWAT_COV4.Text ="";
			
//			this.txtEXPLAIN.Text="";
//			this.cmbQUESTION.SelectedIndex=0; 

		}

		#endregion

		#region SetWorkFlow
		private void SetWorkFlow()
		{
			if(base.ScreenId == "273_3_0")
			{
				myWorkFlow.IsTop	=	false;
				myWorkFlow.ScreenID	=	base.ScreenId;
				myWorkFlow.ScreenID	=	base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID",GetCustomerID());
				myWorkFlow.AddKeyValue("POLICY_ID",GetPolicyID());
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",GetPolicyVersionID());
				myWorkFlow.WorkflowModule="POL";
				myWorkFlow.GetScreenStatus();
				myWorkFlow.SetScreenStatus();
			}
			else
			{
				myWorkFlow.Display	=	false;
			}
		}
		#endregion

		#region AddAttributes Function
		private void AddAttributes()
		{

			//this.txtPOLICY_PREMIUM.Attributes .Add("onBlur","this.value=formatCurrency(this.value);");
			this.txtWAT_COV1.Attributes .Add("onBlur","this.value=formatCurrency(this.value);");
//			this.txtWAT_COV2.Attributes .Add("onBlur","this.value=formatCurrency(this.value);");
			this.txtWAT_COV3.Attributes .Add("onBlur","this.value=formatCurrency(this.value);");
//			this.txtWAT_COV4.Attributes .Add("onBlur","this.value=formatCurrency(this.value);");

//			this.txtHOME_COV1.Attributes .Add("onBlur","this.value=formatCurrency(this.value);");
//			this.txtHOME_COV2.Attributes .Add("onBlur","this.value=formatCurrency(this.value);");
//			this.txtHOME_COV3.Attributes .Add("onBlur","this.value=formatCurrency(this.value);");
//			this.txtHOME_COV4.Attributes .Add("onBlur","this.value=formatCurrency(this.value);");
			this.txtHOME_COV5.Attributes .Add("onBlur","this.value=formatCurrency(this.value);");
//			this.txtHOME_COV6.Attributes .Add("onBlur","this.value=formatCurrency(this.value);");

//			this.txtAUTO_COV1.Attributes .Add("onBlur","this.value=formatCurrency(this.value);");
			this.txtAUTO_COV2.Attributes .Add("onBlur","this.value=formatCurrency(this.value);");
			this.txtAUTO_COV3.Attributes .Add("onBlur","this.value=formatCurrency(this.value);");
			this.txtAUTO_COV4.Attributes .Add("onBlur","this.value=formatCurrency(this.value);");
			this.txtAUTO_COV5.Attributes .Add("onBlur","this.value=formatCurrency(this.value);");
			this.txtAUTO_COV6.Attributes .Add("onBlur","this.value=formatCurrency(this.value);");
			this.txtAUTO_COV7.Attributes .Add("onBlur","this.value=formatCurrency(this.value);");
//			this.txtAUTO_COV8.Attributes .Add("onBlur","this.value=formatCurrency(this.value);");
//			this.txtAUTO_COV9.Attributes .Add("onBlur","this.value=formatCurrency(this.value);");

			this.txtMOT_COV1.Attributes .Add("onBlur","this.value=formatCurrency(this.value);");
			this.txtMOT_COV2.Attributes .Add("onBlur","this.value=formatCurrency(this.value);");
			this.txtMOT_COV3.Attributes .Add("onBlur","this.value=formatCurrency(this.value);");
			this.txtMOT_COV4.Attributes .Add("onBlur","this.value=formatCurrency(this.value);");
			this.txtMOT_COV5.Attributes .Add("onBlur","this.value=formatCurrency(this.value);");
			this.txtMOT_COV6.Attributes .Add("onBlur","this.value=formatCurrency(this.value);");
			this.txtMOT_COV7.Attributes .Add("onBlur","this.value=formatCurrency(this.value);");
			this.txtMOT_COV9.Attributes .Add("onBlur","this.value=formatCurrency(this.value);");
			this.txtMOT_COV11.Attributes .Add("onBlur","this.value=formatCurrency(this.value);");
			this.txtMOT_COV13.Attributes .Add("onBlur","this.value=formatCurrency(this.value);");

		}
		#endregion

		#region LoadData Function

		private void LoadData()
		{
			DataTable dtOldData = ClsScheduleOfUnderlying.GetPolicyScheduleOfUnderlying(intCustomerID ,intPolicyID,intPolicyVerID ,strPolicy_Number );
			hidOldData.Value = Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(dtOldData);
			this.txtPOLICY_START_DATE.Text  =dtOldData.Rows[0]["POLICY_START_DATE"].ToString ();
			this.txtPOLICY_EXPIRATION_DATE.Text =dtOldData.Rows[0]["POLICY_EXPIRATION_DATE"].ToString();
			if (dtOldData.Rows[0]["LOWER_LIMITS"].ToString()=="1")
				this.cmbLOWER_LIMITS.SelectedIndex = 1;
			else
				this.cmbLOWER_LIMITS.SelectedIndex =0;
			
			//if(dtOldData.Rows[0]["POLICY_PREMIUM"].ToString () != null || dtOldData.Rows[0]["POLICY_PREMIUM"].ToString () != "")
			//	this.txtPOLICY_PREMIUM.Text  =dtOldData.Rows[0]["POLICY_PREMIUM"].ToString ();
				
			
			//if(this.txtPOLICY_PREMIUM.Text == "0")
			//	this.txtPOLICY_PREMIUM.Text = "";
			
			if(dtOldData.Rows[0]["POLICY_COMPANY"].ToString() == "Wolverine")
			{
				this.cmbPOLICY_COMPANY .SelectedIndex =0;
				//	this.cmbPOLICY_NUMBER.SelectedItem.Text =dtOldData.Rows[0]["POLICY_NUMBER"].ToString ();
			}
			else
			{
				this.cmbPOLICY_COMPANY .SelectedIndex =1;
				this.txtCOMPANY_OTHER.Text =dtOldData.Rows[0]["POLICY_COMPANY"].ToString();
				this.txtPOLICY_NUMBER.Text =dtOldData.Rows[0]["POLICY_NUMBER"].ToString ();
				
			}

			if(dtOldData.Rows[0]["HAS_MOTORIST_PROTECTION"].ToString() == "1")
			{
				//this.cmbHAS_MOTORIST_PROTECTION.SelectedIndex =1;
				hidHAS_MOTORIST_PROTECTION.Value = "1";
			}
			else
			{
				//this.cmbHAS_MOTORIST_PROTECTION.SelectedIndex =0;
				hidHAS_MOTORIST_PROTECTION.Value = "0";
			}
			if(dtOldData.Rows[0]["LOWER_LIMITS"].ToString() == "1")
			{
				hidLOWER_LIMITS.Value = "1";
			}
			else
			{
				hidLOWER_LIMITS.Value = "0";
			}


			if(dtOldData.Rows[0]["HAS_SIGNED_A9"].ToString() == "1")
			{
				//this.cmbHAS_SIGNED_A9.SelectedIndex =1;
				hidHAS_SIGNED_A9.Value = "1";
			}
			else
			{
				//this.cmbHAS_SIGNED_A9.SelectedIndex =0;
				hidHAS_SIGNED_A9.Value = "0";
			}
						
			int i=0;
//			string strLOB= dtOldData.Rows[0]["POLICY_LOB"].ToString();
//			foreach(ListItem l in this.cmbPOLICY_LOB.Items)
//			{
//				if(l.Text.ToString ()== strLOB)
//				{
//					this.cmbPOLICY_LOB.SelectedIndex =i;
//					this.hidLobId.Value =l.Value.ToString ();
//					break;
//				}
//				i++;
//			}
			//added by Manoj Rathore on 8 th jan 2007
//			if(chkEXCLUDE_UNINSURED_MOTORIST.Checked ==true)
//				objInfo.EXCLUDE_UNINSURED_MOTORIST = 1;
//			else
//				objInfo.EXCLUDE_UNINSURED_MOTORIST = 0;
			//added by Manoj Rathore on 8 th jan 2007
			//if(dtOldData.Rows[0]["OFFICE_PREMISES"]!=null && dtOldData.Rows[0]["OFFICE_PREMISES"].ToString()!="")			
			//	cmbOFFICE_PREMISES.SelectedValue = dtOldData.Rows[0]["OFFICE_PREMISES"].ToString();
	
			//if(dtOldData.Rows[0]["RENTAL_DWELLINGS_UNIT"]!=null && dtOldData.Rows[0]["RENTAL_DWELLINGS_UNIT"].ToString()!="")			
			//	cmbRENTAL_DWELLINGS_UNIT.SelectedValue = dtOldData.Rows[0]["RENTAL_DWELLINGS_UNIT"].ToString();
 
			//
			if(dtOldData.Rows[0]["POLICY_LOB"]!=null && dtOldData.Rows[0]["POLICY_LOB"].ToString()!="")
				hidLobId.Value = cmbPOLICY_LOB.SelectedValue = dtOldData.Rows[0]["POLICY_LOB"].ToString();				 
			 
//			int intLobID;
							
			this.trCOVERAGES.Visible =true;
//			intLobID=Convert.ToInt32(cmbPOLICY_LOB.SelectedValue.ToString()) ;
			DataTable dtPolicies =ClsScheduleOfUnderlying.GetPolicyForCustomer(intCustomerID,int.Parse(hidLobId.Value));
			cmbPOLICY_NUMBER.DataSource =dtPolicies;
			cmbPOLICY_NUMBER.DataTextField ="POLICY_NUMBER";
			cmbPOLICY_NUMBER.DataValueField ="POLICY_ID";
			cmbPOLICY_NUMBER.DataBind ();
			cmbPOLICY_NUMBER.Items.Insert(0,new ListItem ("",""));
			cmbPOLICY_NUMBER.SelectedIndex =0;
			EnableDisableCoverages(int.Parse(hidLobId.Value));
			
			i=0;

			foreach(ListItem l in this.cmbPOLICY_NUMBER.Items )
			{
				if(l.Text.ToString ()== dtOldData.Rows[0]["POLICY_NUMBER"].ToString ())
				{
					this.cmbPOLICY_NUMBER.SelectedIndex =i;
					break;
				}
				i++;
			}

			if(cmbPOLICY_COMPANY.SelectedItem!=null && cmbPOLICY_COMPANY.SelectedItem.Value=="0")
			{
				string strCombined = "";
				//Added by Swarup:13-june-2007 for application converted to policy  :start
				if (cmbPOLICY_NUMBER.SelectedValue.ToString()=="")
				{
					string strAppCombined = dtOldData.Rows[0]["POLICY_NUMBER"].ToString ();
					string [] strPolicyArray = strAppCombined.Split('-');
					strAPP="";
					if(strPolicyArray.Length>0)
					{
						if(strPolicyArray[0]!=null && strPolicyArray[0].ToString().Trim()!="")
							strAPP = strPolicyArray[0].ToString().Trim();
					}
					strCombined = ClsScheduleOfUnderlying.GetPolicyInformationForUmbrella(strAPP.Replace("APP"," "));
					int indexdash = strCombined.IndexOf("-");
					string listpol = strCombined.Substring(indexdash, strCombined.Length-indexdash);
					listpol = " " + listpol;
					i=0;
					foreach(ListItem l in this.cmbPOLICY_NUMBER.Items )
					{
						if(l.Value == listpol)
						{
							this.cmbPOLICY_NUMBER.SelectedIndex =i;
							break;
						}
						i++;
					}
					lblMessage.Text = "This Application has been converted to policy, so policy data is displayed";
					lblMessage.Visible = true;
				} //:end 
				else
				{
					strCombined =cmbPOLICY_NUMBER.SelectedValue.ToString();
				}
				string [] strArray = strCombined.Split('-');
				//string strAPP="";
				int intPolicy_ID=0,intPolicy_Ver_ID=0;
				if(strArray.Length>0)
				{
					if(strArray[0]!=null && strArray[0].ToString().Trim()!="")
						strAPP = strArray[0].ToString().Trim();
				}
				if(strArray.Length>1)
				{
					if(strArray[1]!=null && strArray[1].ToString()!="")
						intPolicy_ID = int.Parse(strArray[1].ToString().Trim());
				}
				if(strArray.Length>2)
				{
					if(strArray[2]!=null && strArray[2].ToString()!="")
						intPolicy_Ver_ID = int.Parse(strArray[2].ToString().Trim());
				}
				if(intPolicy_ID!=0 && intPolicy_Ver_ID!=0 && intCustomerID!=0)
				{
					if(strAPP!="")
						hidAppStateID.Value = ClsVehicleInformation.GetStateIdForApplication(intCustomerID,intPolicy_ID,intPolicy_Ver_ID).ToString();
					else
						hidAppStateID.Value = ClsVehicleInformation.GetStateIdForpolicy(intCustomerID,intPolicy_ID.ToString(),intPolicy_Ver_ID.ToString()).ToString();
				}
				else
					hidAppStateID.Value = "";
			}
			//Populate Coverages

			DataTable dtCoverages=ClsScheduleOfUnderlying.GetPolicyScheduleOfUnderlyingCoverages(
					intCustomerID ,intPolicyID,intPolicyVerID,strPolicy_Number );

			if (hidLobId.Value==((int)enumLOB.AUTOP).ToString()) //Auto
			{
				//HideAutoFieldsForMichigan();
				foreach( DataRow dtRow in dtCoverages.Rows )
				{
//					if(dtRow["COV_DES"].ToString () == this.capAUTO_COV1.Text )
//						this.txtAUTO_COV1.Text =  dtRow["COV_AMOUNT"].ToString();
			
					if(dtRow["COV_DES"].ToString () == this.capAUTO_COV9.Text  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")
					{
						string [] chrArray = dtRow["COV_AMOUNT"].ToString().Split('/');
						if(chrArray!=null && chrArray.Length>0)
						{
							if(chrArray[0]!=null)
								this.txtAUTO_COV2.Text=chrArray[0].ToString();
							if(chrArray[0]!=null)
								this.txtAUTO_COV10.Text=chrArray[1].ToString();
						}
					}
						//this.txtAUTO_COV2.Text = String.Format("{0:,#,###}",Convert.ToString(dtRow["COV_AMOUNT"]));
						//this.txtAUTO_COV2.Text =  dtRow["COV_AMOUNT"].ToString();
									
					if(dtRow["COV_DES"].ToString () == this.capAUTO_COV3.Text  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")
						this.txtAUTO_COV3.Text = String.Format("{0:,#,###}",Convert.ToString(dtRow["COV_AMOUNT"]));
						//this.txtAUTO_COV3.Text =  dtRow["COV_AMOUNT"].ToString();
					
					//if (dtRow["COV_DES"].ToString () == this.capAUTO_COV4.Text  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")
					if (dtRow["COV_DES"].ToString () == this.capAUTO_COV4.Text  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")
						this.txtAUTO_COV4.Text = String.Format("{0:,#,###}",Convert.ToString(dtRow["COV_AMOUNT"]));
						//this.txtAUTO_COV4.Text =  dtRow["COV_AMOUNT"].ToString();
													
					//if (dtRow["COV_DES"].ToString () == this.capAUTO_COV5.Text  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")
					if (dtRow["COV_DES"].ToString () == this.capAUTO_COV5.Text  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")
						this.txtAUTO_COV5.Text = String.Format("{0:,#,###}",Convert.ToString(dtRow["COV_AMOUNT"]));
						//this.txtAUTO_COV5.Text = dtRow["COV_AMOUNT"].ToString ();
					
					//if (dtRow["COV_DES"].ToString () == this.capAUTO_COV6.Text  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")
					if (dtRow["COV_DES"].ToString () == this.capAUTO_COV11.Text  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")
					{
						string [] chrArray = dtRow["COV_AMOUNT"].ToString().Split('/');
						if(chrArray!=null && chrArray.Length>0)
						{
							if(chrArray[0]!=null)
								this.txtAUTO_COV6.Text=chrArray[0].ToString();
							if(chrArray[0]!=null)
								this.txtAUTO_COV12.Text=chrArray[1].ToString();
						}
					}
						//this.txtAUTO_COV6.Text = String.Format("{0:,#,###}",Convert.ToString(dtRow["COV_AMOUNT"]));
						//this.txtAUTO_COV6.Text = dtRow["COV_AMOUNT"].ToString ();
					
					//if (dtRow["COV_DES"].ToString () == this.capAUTO_COV7.Text  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")
					if (dtRow["COV_DES"].ToString () == this.capAUTO_COV13.Text  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")
					{
						string [] chrArray = dtRow["COV_AMOUNT"].ToString().Split('/');
						if(chrArray!=null && chrArray.Length>0)
						{
							if(chrArray[0]!=null)
								this.txtAUTO_COV7.Text=chrArray[0].ToString();
							if(chrArray[0]!=null)
								this.txtAUTO_COV14.Text=chrArray[1].ToString();
						}
					}
						//this.txtAUTO_COV7.Text = String.Format("{0:,#,###}",Convert.ToString(dtRow["COV_AMOUNT"]));
						//this.txtAUTO_COV7.Text = dtRow["COV_AMOUNT"].ToString ();
					
					if(dtRow["COV_DES"].ToString () == this.capAUTO_COV8.Text )
						this.txtAUTO_COV8.Text = dtRow["COV_AMOUNT"].ToString ();
					
					//if(dtRow["COV_DES"].ToString () == this.capCHECK.Text)
					//{
						if(dtRow["COVERAGE_TYPE"].ToString()== "C")
							this.rdoCSL.Checked = true;
						else if(dtRow["COVERAGE_TYPE"].ToString()== "S")
							this.rdoSPLIT.Checked =true;
					//}
//					if(dtRow["COV_DES"].ToString () == this.capAUTO_COV9.Text )
//						this.txtAUTO_COV9.Text = dtRow["COV_AMOUNT"].ToString ();
					
				}
			}
			else if (hidLobId.Value==((int)enumLOB.CYCL).ToString())//Cycle
			{
				//HideCycleFieldsForMichigan();
				foreach( DataRow dtRow in dtCoverages.Rows )
				{
					if(dtRow["COV_DES"].ToString () == this.capMOT_COV1.Text  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")
						this.txtMOT_COV1.Text = String.Format("{0:,#,###}",Convert.ToString(dtRow["COV_AMOUNT"]));
						//this.txtMOT_COV1.Text =  dtRow["COV_AMOUNT"].ToString();
			
					if(dtRow["COV_DES"].ToString () == this.capMOT_COV8.Text  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")
					{
						string [] chrArray = dtRow["COV_AMOUNT"].ToString().Split('/');
						if(chrArray!=null && chrArray.Length>0)
						{
							if(chrArray[0]!=null)
								this.txtMOT_COV2.Text=chrArray[0].ToString();
							if(chrArray[0]!=null)
								this.txtMOT_COV9.Text=chrArray[1].ToString();
						}
					}
						//this.txtMOT_COV2.Text = String.Format("{0:,#,###}",Convert.ToString(dtRow["COV_AMOUNT"]));
						//this.txtMOT_COV2.Text =  dtRow["COV_AMOUNT"].ToString();
									
					if(dtRow["COV_DES"].ToString () == this.capMOT_COV3.Text  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")
						this.txtMOT_COV3.Text = String.Format("{0:,#,###}",Convert.ToString(dtRow["COV_AMOUNT"]));
						//this.txtMOT_COV3.Text =  dtRow["COV_AMOUNT"].ToString();
					
					if (dtRow["COV_DES"].ToString () == this.capMOT_COV4.Text  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")
						this.txtMOT_COV4.Text = String.Format("{0:,#,###}",Convert.ToString(dtRow["COV_AMOUNT"]));
						//this.txtMOT_COV4.Text =  dtRow["COV_AMOUNT"].ToString();
													
					//if (dtRow["COV_DES"].ToString () == this.capMOT_COV5.Text  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")
					if (dtRow["COV_DES"].ToString () == this.capMOT_COV10.Text  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")
					{
						string [] chrArray = dtRow["COV_AMOUNT"].ToString().Split('/');
						if(chrArray!=null && chrArray.Length>0)
						{
							if(chrArray[0]!=null)
								this.txtMOT_COV5.Text=chrArray[0].ToString();
							if(chrArray[0]!=null)
								this.txtMOT_COV11.Text=chrArray[1].ToString();
						}
					}
						//this.txtMOT_COV5.Text = String.Format("{0:,#,###}",Convert.ToString(dtRow["COV_AMOUNT"]));
						//this.txtMOT_COV5.Text = dtRow["COV_AMOUNT"].ToString ();
					
					if (dtRow["COV_DES"].ToString () == this.capMOT_COV6.Text  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")
						this.txtMOT_COV6.Text = String.Format("{0:,#,###}",Convert.ToString(dtRow["COV_AMOUNT"]));
						//this.txtMOT_COV6.Text = dtRow["COV_AMOUNT"].ToString ();

					//if (dtRow["COV_DES"].ToString () == this.capMOT_COV7.Text  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")
					if (dtRow["COV_DES"].ToString () == this.capMOT_COV12.Text  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")
					{
						string [] chrArray = dtRow["COV_AMOUNT"].ToString().Split('/');
						if(chrArray!=null && chrArray.Length>0)
						{
							if(chrArray[0]!=null)
								this.txtMOT_COV7.Text=chrArray[0].ToString();
							if(chrArray[0]!=null)
								this.txtMOT_COV13.Text=chrArray[1].ToString();
						}
					}
						
						//this.txtMOT_COV7.Text = String.Format("{0:,#,###}",Convert.ToString(dtRow["COV_AMOUNT"]));
					//if(dtRow["COV_DES"].ToString () == this.capCHECK.Text)
					//{
						if(dtRow["COVERAGE_TYPE"].ToString()== "C")
							this.rdoCSL.Checked = true;
						else if(dtRow["COVERAGE_TYPE"].ToString()== "S")
							this.rdoSPLIT.Checked =true;
					//}

				}

			}
			else if (hidLobId.Value==((int)enumLOB.HOME).ToString() || hidLobId.Value==((int)enumLOB.REDW).ToString()) //Home & Rental
			{
				foreach( DataRow dtRow in dtCoverages.Rows )
				{
//					if(dtRow["COV_DES"].ToString () == this.capHOME_COV1 .Text  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")
//						this.txtHOME_COV1.Text = String.Format("{0:,#,###}",Convert.ToString(dtRow["COV_AMOUNT"]));
						//this.txtHOME_COV1.Text = dtRow["COV_AMOUNT"].ToString ();
					
//					if(dtRow["COV_DES"].ToString () == this.capHOME_COV2 .Text  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")
//						this.txtHOME_COV2.Text = String.Format("{0:,#,###}",Convert.ToString(dtRow["COV_AMOUNT"]));
//						//this.txtHOME_COV2.Text = dtRow["COV_AMOUNT"].ToString ();
//					
//					if(dtRow["COV_DES"].ToString () == this.capHOME_COV3 .Text  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")
//						this.txtHOME_COV3.Text = String.Format("{0:,#,###}",Convert.ToString(dtRow["COV_AMOUNT"]));
//						//this.txtHOME_COV3.Text = dtRow["COV_AMOUNT"].ToString ();
//					
//					if(dtRow["COV_DES"].ToString () == this.capHOME_COV4.Text  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")
//						this.txtHOME_COV4.Text = String.Format("{0:,#,###}",Convert.ToString(dtRow["COV_AMOUNT"]));
//						//this.txtHOME_COV4.Text = dtRow["COV_AMOUNT"].ToString ();
					
					if(dtRow["COV_DES"].ToString () == this.capHOME_COV5 .Text  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")
						this.txtHOME_COV5.Text = String.Format("{0:,#,###}",Convert.ToString(dtRow["COV_AMOUNT"]));
						//this.txtHOME_COV5.Text = dtRow["COV_AMOUNT"].ToString ();
					
//					if(dtRow["COV_DES"].ToString () == this.capHOME_COV6 .Text  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")
//						this.txtHOME_COV6.Text = String.Format("{0:,#,###}",Convert.ToString(dtRow["COV_AMOUNT"]));
						//this.txtHOME_COV6.Text = dtRow["COV_AMOUNT"].ToString ();
					

				}

			}
			else if (hidLobId.Value==((int)enumLOB.BOAT).ToString())
			{
				foreach( DataRow dtRow in dtCoverages.Rows )
				{
					if(dtRow["COV_DES"].ToString () == this.capWAT_COV1.Text  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")
						this.txtWAT_COV1.Text = String.Format("{0:,#,###}",Convert.ToString(dtRow["COV_AMOUNT"]));
						//this.txtWAT_COV1.Text = dtRow["COV_AMOUNT"].ToString ();
					
//					if(dtRow["COV_DES"].ToString () == this.capWAT_COV2.Text )
//						this.txtWAT_COV2 .Text = dtRow["COV_AMOUNT"].ToString ();
					
					if(dtRow["COV_DES"].ToString () == this.capWAT_COV3.Text  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")
						this.txtWAT_COV3.Text = String.Format("{0:,#,###}",Convert.ToString(dtRow["COV_AMOUNT"]));
						//this.txtWAT_COV3.Text = dtRow["COV_AMOUNT"].ToString ();
					
//					if(dtRow["COV_DES"].ToString () == this.capWAT_COV4.Text )
//						this.txtWAT_COV4 .Text = dtRow["COV_AMOUNT"].ToString ();
					
				}


			}
		
//			if(dtOldData.Rows[0]["QUESTION"].ToString()  == "Y")
//			{
//				this.cmbQUESTION .SelectedIndex =2;
//			}
//			else if(dtOldData.Rows[0]["QUESTION"].ToString()  == "N")
//			{
//				this.cmbQUESTION .SelectedIndex =1;
//			}
//			else
//			{
//				this.cmbQUESTION .SelectedIndex =0;
//			}
//
//			if(dtOldData.Rows[0]["QUES_DESC"].ToString ().Trim () !="")
//				this.txtEXPLAIN.Text = dtOldData.Rows[0]["QUES_DESC"].ToString ();
			LoadProtectionControls();
		}
		#endregion

		#region Event Handler cmbLOB_SelectedIndexChanged
		private void cmbPOLICY_LOB_SelectedIndexChanged(object sender, System.EventArgs e)
		{

			int intLob_ID;

			if(cmbPOLICY_LOB.SelectedIndex >0)
			{
				intLob_ID=Convert.ToInt32(cmbPOLICY_LOB.SelectedValue.ToString()) ;
				DataTable dtPolicies =ClsScheduleOfUnderlying.GetPolicyForCustomer(intCustomerID,intLob_ID);
				cmbPOLICY_NUMBER.DataSource =dtPolicies;
				cmbPOLICY_NUMBER.DataTextField ="POLICY_NUMBER";
				cmbPOLICY_NUMBER.DataValueField ="POLICY_ID";
				cmbPOLICY_NUMBER.DataBind ();
				cmbPOLICY_NUMBER.Items.Insert(0,new ListItem ("",""));
				cmbPOLICY_NUMBER.SelectedIndex =0;
				this.hidLobId.Value =cmbPOLICY_LOB.SelectedValue.ToString();
				lblMessage.Visible=false;
			}
			else
			{
				intLob_ID =0;
				this.hidLobId.Value="0";
				DisableAllCoverages();
			}
			EnableDisableCoverages(intLob_ID);
		
		}
		#endregion 

		#region Event Handler  cmbPOLICY_NUMBER_SelectedIndexChanged

		private void cmbPOLICY_NUMBER_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
			if(cmbPOLICY_COMPANY.SelectedValue.ToString() == "0" && cmbPOLICY_LOB.SelectedIndex > 0 && cmbPOLICY_NUMBER.SelectedIndex > 0)				
			{				
				this.imgOpenDecPage.Attributes.Add("style","display:inline;CURSOR:hand");	
				this.lblDecPage.Attributes.Add("style","display:inline");	
				
			}
			else
			{
				this.imgOpenDecPage.Attributes.Add("style","display:none");	
				this.lblDecPage.Attributes.Add("style","display:none");	
			}
			if(this.cmbPOLICY_NUMBER.SelectedIndex <=0 )
				return;
			
			int intLob_ID=0;			
			string strCombined;			
			//string strAPP="";
			strAPP="";
			
			lblMessage.Visible=false;

			strCombined =cmbPOLICY_NUMBER.SelectedValue.ToString();
			//strPolicyID =strCombined.Substring(0,strCombined.IndexOf("-"));
			//strPolicyVerID =strCombined.Substring(strCombined.IndexOf("-") +1 );

			strCombined =cmbPOLICY_NUMBER.SelectedValue.ToString();
			string [] strArray = strCombined.Split('-');
			
			if(strArray.Length>0)
			{
				if(strArray[0]!=null && strArray[0].ToString().Trim()!="")
					strAPP = strArray[0].ToString().Trim();
				if(strArray[1]!=null && strArray[1].ToString()!="")
					gintPolicy_ID = int.Parse(strArray[1].ToString().Trim());
				if(strArray[2]!=null && strArray[2].ToString()!="")
					gintPolicy_Ver_ID = int.Parse(strArray[2].ToString().Trim());
			}

			intLob_ID=Convert.ToInt32(cmbPOLICY_LOB.SelectedValue.ToString()) ;
			
			//gintPolicy_ID=Convert.ToInt32(strPolicyID.Trim());
			//gintPolicy_Ver_ID =Convert.ToInt32(strPolicyVerID.Trim());
			DataTable dtDates=ClsScheduleOfUnderlying.GetPolicyTerms(intCustomerID,gintPolicy_ID,gintPolicy_Ver_ID,strAPP);
			if(dtDates.Rows.Count >0)
			{
				this.txtPOLICY_START_DATE.Text =dtDates.Rows[0][0].ToString ();
				this.txtPOLICY_EXPIRATION_DATE.Text =dtDates.Rows[0][1].ToString ();
				//this.txtPOLICY_PREMIUM .Text =dtDates.Rows[0][2].ToString ();
				hidAppStateID.Value = dtDates.Rows[0]["STATE_ID"].ToString();
			}
			else
				hidAppStateID.Value = "";
			
			ClsScheduleOfUnderlying objSchedule=new ClsScheduleOfUnderlying();
            int intRetVal1;//intRetVal,
			//intRetVal = objSchedule.CheckMotorist(this.intCustomer_ID,gintPolicy_ID,gintPolicy_Ver_ID,strAPP);
			int iReject,iLower;
			objSchedule.CheckMotorist(intCustomerID,gintPolicy_ID,gintPolicy_Ver_ID,strAPP, out iReject, out iLower);
			if (iReject==1)
			{
				cmbHAS_MOTORIST_PROTECTION.SelectedIndex = 1;
				hidHAS_MOTORIST_PROTECTION.Value = "1";
			}
			else if(iReject==2)
			{
				cmbHAS_MOTORIST_PROTECTION.SelectedIndex = 0;
				hidHAS_MOTORIST_PROTECTION.Value = "0";
			}

			if (iLower==1)
			{
				cmbLOWER_LIMITS.SelectedIndex = 1;
				hidLOWER_LIMITS.Value = "1";
			}
			else if(iLower==2)
			{
				cmbLOWER_LIMITS.SelectedIndex = 0;
				hidLOWER_LIMITS.Value = "0";
			}
			intRetVal1 = objSchedule.CheckSignedForm(this.intCustomerID,gintPolicy_ID,gintPolicy_Ver_ID,strAPP);
			if (intRetVal1==1)
			{
				cmbHAS_SIGNED_A9.SelectedIndex = 1;
				cmbHAS_SIGNED_A9.SelectedValue = "1";
				hidHAS_SIGNED_A9.Value="1";
			}
			else if(intRetVal1==2)
			{
				cmbHAS_SIGNED_A9.SelectedIndex = 0;
				cmbHAS_SIGNED_A9.SelectedValue = "0";
				hidHAS_SIGNED_A9.Value="0";
			}


			ResetCoverages ();
			if (intLob_ID==2 ) //Auto
			{
							
				DataTable dtCoverages=ClsScheduleOfUnderlying.GetPolicyCoveragesAuto(this.intCustomerID ,gintPolicy_ID,gintPolicy_Ver_ID,strAPP);
				int covID = 0;
				//HideAutoFieldsForMichigan();
				foreach( DataRow dtRow in dtCoverages.Rows )
				{
					
					covID = 0;
					if ( dtRow["COV_ID"] != DBNull.Value )
					{
						covID = Convert.ToInt32(dtRow["COV_ID"]);
					}
					// Residual Liability CSL (BI and PD)
//					if ( covID ==113)
//					{
//						this.txtAUTO_COV1.Text = dtRow["COV_AMOUNT"].ToString ();
//					}

					//Bodily Injury Liability (Split Limit)
//					if ((covID ==114 || covID ==2)  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")										
//						this.txtAUTO_COV2.Text = String.Format("{0:,#,###}",Convert.ToString(dtRow["COV_AMOUNT"]));
						//this.txtAUTO_COV2.Text = dtRow["COV_AMOUNT"].ToString ();
					if(dtRow["COV_CODE"].ToString () == "BISPL" && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")
					{
						string [] chrArray = dtRow["COV_AMOUNT"].ToString().Split('/');
						if(chrArray!=null && chrArray.Length>0)
						{
							if(chrArray[0]!=null)
								this.txtAUTO_COV2.Text=chrArray[0].ToString();
							if(chrArray[0]!=null)
								this.txtAUTO_COV10.Text=chrArray[1].ToString();
						}
					}

					//Property Damage Liability
					if (dtRow["COV_CODE"].ToString () == "PD" && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")										
						this.txtAUTO_COV3.Text = String.Format("{0:,#,###}",Convert.ToString(dtRow["COV_AMOUNT"]));					
						//this.txtAUTO_COV3.Text = dtRow["COV_AMOUNT"].ToString ();
					

					//Uninsured Motorists (CSL)
					if(dtRow["COV_CODE"].ToString () == "PUNCS" && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")															
						this.txtAUTO_COV4.Text = String.Format("{0:,#,###}",Convert.ToString(dtRow["COV_AMOUNT"]));					
						//this.txtAUTO_COV4.Text = dtRow["COV_AMOUNT"].ToString ();
					

					//Underinsured Motorists (CSL)
					if(dtRow["COV_CODE"].ToString () == "UNCSL"  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")																				 					
						this.txtAUTO_COV5.Text = String.Format("{0:,#,###}",Convert.ToString(dtRow["COV_AMOUNT"]));					
						//this.txtAUTO_COV5.Text = dtRow["COV_AMOUNT"].ToString ();					

					//Uninsured Motorists (BI Split Limits)
//					if((covID ==120 || covID ==12)  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")					 
//						this.txtAUTO_COV6.Text = String.Format("{0:,#,###}",Convert.ToString(dtRow["COV_AMOUNT"]));					
						//this.txtAUTO_COV6.Text = dtRow["COV_AMOUNT"].ToString ();		
					if (dtRow["COV_CODE"].ToString () == "PUMSP"  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")
					{
						string [] chrArray = dtRow["COV_AMOUNT"].ToString().Split('/');
						if(chrArray!=null && chrArray.Length>0)
						{
							if(chrArray[0]!=null)
								this.txtAUTO_COV6.Text=chrArray[0].ToString();
							if(chrArray[0]!=null)
								this.txtAUTO_COV12.Text=chrArray[1].ToString();
						}
					}

					//Underinsured Motorists (BI Split Limits)
//					if((covID ==121 || covID ==34)  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")					 					 
//						this.txtAUTO_COV7.Text = String.Format("{0:,#,###}",Convert.ToString(dtRow["COV_AMOUNT"]));					
						//this.txtAUTO_COV7.Text = dtRow["COV_AMOUNT"].ToString ();

					if (dtRow["COV_CODE"].ToString () == "UNDSP"  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")
					{
						string [] chrArray = dtRow["COV_AMOUNT"].ToString().Split('/');
						if(chrArray!=null && chrArray.Length>0)
						{
							if(chrArray[0]!=null)
								this.txtAUTO_COV7.Text=chrArray[0].ToString();
							if(chrArray[0]!=null)
								this.txtAUTO_COV14.Text=chrArray[1].ToString();
						}
					}

					// Single Limits Liability (CSL) 
					if (dtRow["COV_CODE"].ToString () == "SLL" && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")
					{
						this.txtAUTO_COV8.Text = String.Format("{0:,#,###}",Convert.ToString(dtRow["COV_AMOUNT"]));
						//this.txtMOT_COV1.Text  = dtRow["COV_AMOUNT"].ToString ();
					}
					

//					//Property Protection Insurance
//					if ( covID ==117)
//					{
//						this.txtAUTO_COV8.Text = dtRow["COV_AMOUNT"].ToString ();
//					}

//					//Personal Injury Protection
//					if ( covID ==116)
//					{
////						this.txtAUTO_COV9.Text = dtRow["COV_AMOUNT"].ToString ();
//					}
					if(dtRow["COV_CODE"].ToString () == "BISPL" || dtRow["COV_CODE"].ToString () == "PUMSP" || dtRow["COV_CODE"].ToString () == "UNDSP"  )
					{
						this.rdoSPLIT.Checked =true;
					}
					else
						this.rdoCSL.Checked = true;
					
				}
			}
			if (intLob_ID==3 ) //Cycle
			{
							
				DataTable dtCoverages=ClsScheduleOfUnderlying.GetPolicyCoveragesAuto(this.intCustomerID ,gintPolicy_ID,gintPolicy_Ver_ID,strAPP);
				int covID = 0;
				//HideCycleFieldsForMichigan();
				foreach( DataRow dtRow in dtCoverages.Rows )
				{
					
					covID = 0;
					if ( dtRow["COV_ID"] != DBNull.Value )
					{
						covID = Convert.ToInt32(dtRow["COV_ID"]);
					}
					// Single Limits Liability (CSL) 
					if (dtRow["COV_CODE"].ToString () == "RLCSL"&& dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")
						this.txtMOT_COV1.Text = String.Format("{0:,#,###}",Convert.ToString(dtRow["COV_AMOUNT"]));					
						//this.txtMOT_COV1.Text  = dtRow["COV_AMOUNT"].ToString ();

					//Bodily Injury Liability (Split Limit) 
//					if ((covID ==127 || covID ==207)  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")					 
//						this.txtMOT_COV2.Text = String.Format("{0:,#,###}",Convert.ToString(dtRow["COV_AMOUNT"]));					
						//this.txtMOT_COV2.Text = dtRow["COV_AMOUNT"].ToString ();
					if(dtRow["COV_CODE"].ToString () == "BISPL"  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")
					{
						string [] chrArray = dtRow["COV_AMOUNT"].ToString().Split('/');
						if(chrArray!=null && chrArray.Length>0)
						{
							if(chrArray[0]!=null)
								this.txtMOT_COV2.Text=chrArray[0].ToString();
							if(chrArray[0]!=null)
								this.txtMOT_COV9.Text=chrArray[1].ToString();
						}
					}
					

					//Property Damage Liability 
					if (dtRow["COV_CODE"].ToString () == "PD" && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")					 
						this.txtMOT_COV3.Text = String.Format("{0:,#,###}",Convert.ToString(dtRow["COV_AMOUNT"]));					
						//this.txtMOT_COV3.Text = dtRow["COV_AMOUNT"].ToString ();

					//Uninsured Motorists (CSL) 
					//if((covID ==211 || covID ==131)  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")					 
					if(dtRow["COV_CODE"].ToString () == "PUNCS" && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")					 
						this.txtMOT_COV4.Text = String.Format("{0:,#,###}",Convert.ToString(dtRow["COV_AMOUNT"]));					
						//this.txtMOT_COV4.Text = dtRow["COV_AMOUNT"].ToString ();

					//Uninsured Motorists (BI Split Limit) 
					//if((covID ==131 || covID ==212)  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")					
//					if((covID ==131 || covID ==212)  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")					
//						this.txtMOT_COV5.Text = String.Format("{0:,#,###}",Convert.ToString(dtRow["COV_AMOUNT"]));					
						//this.txtMOT_COV5.Text = dtRow["COV_AMOUNT"].ToString ();
					if (dtRow["COV_CODE"].ToString () == "PUMSP" && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")
					{
						string [] chrArray = dtRow["COV_AMOUNT"].ToString().Split('/');
						if(chrArray!=null && chrArray.Length>0)
						{
							if(chrArray[0]!=null)
								this.txtMOT_COV5.Text=chrArray[0].ToString();
							if(chrArray[0]!=null)
								this.txtMOT_COV11.Text=chrArray[1].ToString();
						}
					}

					//Medical Payments 
					//if((covID ==25 || covID ==29)  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")										 
					if(dtRow["COV_CODE"].ToString () == "UNCSL"  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")										 
						this.txtMOT_COV6.Text = String.Format("{0:,#,###}",Convert.ToString(dtRow["COV_AMOUNT"]));					
						//this.txtMOT_COV6.Text = dtRow["COV_AMOUNT"].ToString ();

					//if ((covID ==214) && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")																				
//					if ((covID ==214) && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")																				
//					{
//						this.txtMOT_COV7.Text = String.Format("{0:,#,###}",Convert.ToString(dtRow["COV_AMOUNT"]));						
//					}
					//Underinsured Motorists (BI Split Limit)
					if (dtRow["COV_CODE"].ToString () == "UNDSP" && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")
					{
						string [] chrArray = dtRow["COV_AMOUNT"].ToString().Split('/');
						if(chrArray!=null && chrArray.Length>0)
						{
							if(chrArray[0]!=null)
								this.txtMOT_COV7.Text=chrArray[0].ToString();
							if(chrArray[0]!=null)
								this.txtMOT_COV13.Text=chrArray[1].ToString();
						}
					}
					if(dtRow["COV_CODE"].ToString () == "BISPL" || dtRow["COV_CODE"].ToString () == "PUMSP" || dtRow["COV_CODE"].ToString () == "UNDSP"  )
					{
						this.rdoSPLIT.Checked =true;
					}
					else
						this.rdoCSL.Checked = true;

				}
			}
			else if (intLob_ID==1 ) //HomeOwner
			{
				
				int covID = 0;

				DataTable dtCoverages=ClsScheduleOfUnderlying.GetPolicyCoveragesHome(this.intCustomerID ,gintPolicy_ID,gintPolicy_Ver_ID,strAPP);
				
				foreach( DataRow dtRow in dtCoverages.Rows )
				{
					covID = 0;
					if ( dtRow["COV_ID"] != DBNull.Value )
					{
						covID = Convert.ToInt32(dtRow["COV_ID"]);
					}
					
					//Coverage A
//					if ((covID ==3 || covID ==134)  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")					
//						this.txtHOME_COV1.Text = String.Format("{0:,#,###}",Convert.ToString(dtRow["COV_AMOUNT"]));					
//						//this.txtHOME_COV1.Text = dtRow["COV_AMOUNT"].ToString ();
//					
//					//Coverage B
//					if ((covID ==5 || covID ==135)  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")					
//						this.txtHOME_COV2.Text = String.Format("{0:,#,###}",Convert.ToString(dtRow["COV_AMOUNT"]));					
//						//this.txtHOME_COV2.Text = dtRow["COV_AMOUNT"].ToString ();
//
//					//Coverage C
//					if ((covID ==7 || covID ==136)  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")					
//						this.txtHOME_COV3.Text = String.Format("{0:,#,###}",Convert.ToString(dtRow["COV_AMOUNT"]));					
//						//this.txtHOME_COV3.Text = dtRow["COV_AMOUNT"].ToString ();
//
//					//Coverage D
//					if ((covID == 8 || covID ==137)  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")					
//						this.txtHOME_COV4.Text = String.Format("{0:,#,###}",Convert.ToString(dtRow["COV_AMOUNT"]));					
						//this.txtHOME_COV4.Text = dtRow["COV_AMOUNT"].ToString ();
					

					//Coverage E
					if ((covID == 10 || covID ==170)  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")					
						this.txtHOME_COV5.Text = String.Format("{0:,#,###}",Convert.ToString(dtRow["COV_AMOUNT"]));					
						//this.txtHOME_COV5.Text = dtRow["COV_AMOUNT"].ToString ();
					

//					//Coverage F
//					if ((covID == 13 || covID ==171)  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")					
//						this.txtHOME_COV6.Text = String.Format("{0:,#,###}",Convert.ToString(dtRow["COV_AMOUNT"]));					
						//this.txtHOME_COV6.Text = dtRow["COV_AMOUNT"].ToString ();
					

				}

			}
			else if (intLob_ID==6) //Rental
			{
				
				int covID = 0;

				DataTable dtCoverages=ClsScheduleOfUnderlying.GetPolicyCoveragesHome(this.intCustomerID ,gintPolicy_ID,gintPolicy_Ver_ID,strAPP);
				
				foreach( DataRow dtRow in dtCoverages.Rows )
				{
					covID = 0;
					if ( dtRow["COV_ID"] != DBNull.Value )
					{
						covID = Convert.ToInt32(dtRow["COV_ID"]);
					}
					
					//Coverage A
//					if ((covID == 773 || covID ==793)  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")					
//						this.txtHOME_COV1.Text = String.Format("{0:,#,###}",Convert.ToString(dtRow["COV_AMOUNT"]));					
//						//this.txtHOME_COV1.Text = dtRow["COV_AMOUNT"].ToString ();
//					
//					//Coverage B
//					if (( covID == 774 || covID == 779 || covID ==794 || covID==799)  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")					
//						this.txtHOME_COV2.Text = String.Format("{0:,#,###}",Convert.ToString(dtRow["COV_AMOUNT"]));					
//						//this.txtHOME_COV2.Text = dtRow["COV_AMOUNT"].ToString ();					
//
//					//Coverage C
//					if (( covID == 795 || covID == 775 )  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")					
//						this.txtHOME_COV3.Text = String.Format("{0:,#,###}",Convert.ToString(dtRow["COV_AMOUNT"]));
//						//this.txtHOME_COV3.Text = dtRow["COV_AMOUNT"].ToString ();
//
//					//Coverage D
//					if (( covID == 776 || covID == 796 )  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")					
//						this.txtHOME_COV4.Text = String.Format("{0:,#,###}",Convert.ToString(dtRow["COV_AMOUNT"]));
//						//this.txtHOME_COV4.Text = dtRow["COV_AMOUNT"].ToString ();
//
//					//Coverage E
					if (( covID == 292 || covID == 293 || covID == 777 || covID == 797)  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")
						this.txtHOME_COV5.Text = String.Format("{0:,#,###}",Convert.ToString(dtRow["COV_AMOUNT"]));
						//this.txtHOME_COV5.Text = dtRow["COV_AMOUNT"].ToString ();

					//Coverage F
//					if (( covID == 778 || covID == 798 )  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")					
//						this.txtHOME_COV6.Text = String.Format("{0:,#,###}",Convert.ToString(dtRow["COV_AMOUNT"]));
						//this.txtHOME_COV6.Text = dtRow["COV_AMOUNT"].ToString ();

				}

			}
			else if (intLob_ID==4)
			{
				int covID = 0;
				DataTable dtCoverages=ClsScheduleOfUnderlying.GetPolicyCoveragesWatercraft(this.intCustomerID ,gintPolicy_ID,gintPolicy_Ver_ID,strAPP);
				

				foreach( DataRow dtRow in dtCoverages.Rows )
				{

					covID = 0;
					if ( dtRow["COV_ID"] != DBNull.Value )
					{
						covID = Convert.ToInt32(dtRow["COV_ID"]);
					}
					//Watercraft Liability
					if (( covID == 820 || covID == 65 || covID == 19 )  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")					
						this.txtWAT_COV1.Text = String.Format("{0:,#,###}",Convert.ToString(dtRow["COV_AMOUNT"]));
						//this.txtWAT_COV1 .Text = dtRow["COV_AMOUNT"].ToString ();

					//Section II - Medical
//					if ( covID == 21 || covID == 68 || covID == 821 )
//					{
//						this.txtWAT_COV2 .Text = dtRow["COV_AMOUNT"].ToString ();
//					}

					//Uninsured Boaters
					if (( covID == 822 || covID == 70 || covID == 24 )  && dtRow["COV_AMOUNT"]!=System.DBNull.Value && dtRow["COV_AMOUNT"].ToString()!="")
						this.txtWAT_COV3.Text = String.Format("{0:,#,###}",Convert.ToString(dtRow["COV_AMOUNT"]));
						//this.txtWAT_COV3 .Text = dtRow["COV_AMOUNT"].ToString ();

					//Increase in "Unattached Equipment" And Personal Effects Coverage
//					if ( covID == 823 || covID == 71)
//					{
//						this.txtWAT_COV4 .Text = dtRow["COV_AMOUNT"].ToString ();
//					}
				
				}


			}

		}
		private void LoadProtectionControls()
		{
			if(hidHAS_MOTORIST_PROTECTION.Value != "" && hidHAS_MOTORIST_PROTECTION.Value!="0")			
				cmbHAS_MOTORIST_PROTECTION.SelectedIndex =int.Parse(hidHAS_MOTORIST_PROTECTION.Value);							

			if(hidHAS_SIGNED_A9.Value != "" && hidHAS_SIGNED_A9.Value!="0")									
				cmbHAS_SIGNED_A9.SelectedIndex =int.Parse(hidHAS_SIGNED_A9.Value);
			if(hidLOWER_LIMITS.Value != "" && hidLOWER_LIMITS.Value!="0")									
				cmbLOWER_LIMITS.SelectedIndex =int.Parse(hidLOWER_LIMITS.Value);

		}
		#endregion

		#region Event Handler btnSave_Click

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			ClsScheduleOfUnderlying objSchedule=new ClsScheduleOfUnderlying();
			ClsUnderlyingPoliciesInfo  objInfo=new ClsUnderlyingPoliciesInfo();
			int intRetVal;
			string strPolicyNumber=this.hidPolicyNumber.Value.ToString () ;
			int flag=0;
			if(strPolicyNumber != "0")
			{
				objSchedule.DeletePolicy(intCustomerID,intPolicyID,intPolicyVerID,strPolicyNumber);
				this.hidPolicyNumber.Value="0";
				flag=1;
			}


			objInfo.CUSTOMER_ID =intCustomerID;
			objInfo.POLICY_ID =intPolicyID ;
			objInfo.POLICY_VERSION_ID=intPolicyVerID;
			
			objInfo.POLICY_LOB = this.cmbPOLICY_LOB.SelectedItem.Value;

			//if(txtPOLICY_PREMIUM.Text != "")
			//	objInfo.POLICY_PREMIUM =Convert.ToDecimal(txtPOLICY_PREMIUM.Text.ToString ());

			if(txtPOLICY_START_DATE.Text  != "")
				objInfo.POLICY_START_DATE =ConvertToDate(txtPOLICY_START_DATE.Text .ToString()) ;
			
			if(txtPOLICY_EXPIRATION_DATE.Text  != "")
				objInfo.POLICY_EXPIRATION_DATE =ConvertToDate(txtPOLICY_EXPIRATION_DATE.Text.ToString());

			if(hidAppStateID.Value!="" && int.Parse(hidAppStateID.Value)>0)
				objInfo.STATE_ID = int.Parse(hidAppStateID.Value);			

			if(this.cmbPOLICY_COMPANY.SelectedValue=="0")
			{
				string [] strArray = cmbPOLICY_NUMBER.SelectedItem.Value.Split('-');
				if(strArray.Length>0 && strArray[0]!=null && strArray[0].ToString().Trim()=="APP")				
					objInfo.IS_POLICY = false;
				else				
					objInfo.IS_POLICY = true;
				objInfo.POLICY_NUMBER =this.cmbPOLICY_NUMBER.SelectedItem.Text.ToString();
				objInfo.POLICY_COMPANY =this.cmbPOLICY_COMPANY.SelectedItem.Text ;
			}
			else if(this.cmbPOLICY_COMPANY.SelectedValue=="1")
			{
				objInfo.POLICY_NUMBER =this.txtPOLICY_NUMBER.Text.Trim().ToString();
				objInfo.POLICY_COMPANY =this.txtCOMPANY_OTHER.Text;
			}
			//added by Manoj Rathore on 8 th jan 2007
			if(chkEXCLUDE_UNINSURED_MOTORIST.Checked ==true)
				objInfo.EXCLUDE_UNINSURED_MOTORIST = 1;
			else
				objInfo.EXCLUDE_UNINSURED_MOTORIST = 0;
			//added by Manoj Rathore 0n 8 th jan 2007
			// Office Premises , Rental Dwellings
			//if(cmbOFFICE_PREMISES.SelectedItem!=null && cmbOFFICE_PREMISES.SelectedItem.Value!="")
			//	objInfo.OFFICE_PREMISES = int.Parse(cmbOFFICE_PREMISES.SelectedItem.Value);

			//if(cmbRENTAL_DWELLINGS_UNIT.SelectedItem!=null && cmbRENTAL_DWELLINGS_UNIT.SelectedItem.Value!="")
			//	objInfo.RENTAL_DWELLINGS_UNIT = int.Parse(cmbRENTAL_DWELLINGS_UNIT.SelectedItem.Value);
	
//			if(this.cmbQUESTION.SelectedIndex   ==2)
//			{
//				objInfo.QUESTION ="Y";
//			}
//			else if(this.cmbQUESTION.SelectedIndex  ==1)
//			{
//				objInfo.QUESTION ="N";
//				objInfo.QUES_DESC =this.txtEXPLAIN.Text ;
//			}
			if(this.cmbPOLICY_COMPANY.SelectedValue.ToString() == "0")	
			{
				if(hidHAS_MOTORIST_PROTECTION.Value!="")
					objInfo.HAS_MOTORIST_PROTECTION = int.Parse(hidHAS_MOTORIST_PROTECTION.Value);			

				if(hidHAS_SIGNED_A9.Value!="")
					objInfo.HAS_SIGNED_A9 = int.Parse(hidHAS_SIGNED_A9.Value);

				if(hidLOWER_LIMITS.Value!="")
					objInfo.LOWER_LIMITS = int.Parse(hidLOWER_LIMITS.Value);
			}
			else if(this.cmbPOLICY_COMPANY.SelectedValue.ToString() == "1")	
			{
				if(this.cmbLOWER_LIMITS.SelectedValue!=null)
					objInfo.LOWER_LIMITS = int.Parse(this.cmbLOWER_LIMITS.SelectedValue);

				if(this.cmbHAS_MOTORIST_PROTECTION.SelectedValue!=null)
					objInfo.HAS_MOTORIST_PROTECTION = int.Parse(this.cmbHAS_MOTORIST_PROTECTION.SelectedValue);			

				if(this.cmbHAS_SIGNED_A9.SelectedValue!=null)
					objInfo.HAS_SIGNED_A9 = int.Parse(this.cmbHAS_SIGNED_A9.SelectedValue);
			}
			try
			{
				objInfo.CREATED_BY = int.Parse(GetUserId());
				intRetVal = objSchedule.AddPolicy(objInfo);
				if(intRetVal == 1)
				{
					SetWorkFlow();
					
					// Coverages to be saved only in case of 'Other'
					//if(cmbPOLICY_COMPANY.SelectedValue.ToString() == "1")
						InsertCoverages (objSchedule);

					if(flag==0)
					{
						lblMessage.Text	=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("29");
					}
					else 
					{
						lblMessage.Text= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("31");
					}
					hidFormSaved.Value		=	"1";
					hidPolicyNumber.Value =objInfo.POLICY_NUMBER.ToString (); 
					hidPolicyCompany.Value = objInfo.POLICY_COMPANY.ToString();
					LoadProtectionControls();
					base.OpenEndorsementDetails();
										
				}
				else  //Record Is Already There
				{
					lblMessage.Text=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"7");
					
				}

				lblMessage.Visible	=	true;
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("20");
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}

		}
		
		#endregion

		private ClsUnderlyingPolicyCoverages PrepareCoverageObject()
		{
			int intLob_ID;
			intLob_ID=Convert.ToInt32(cmbPOLICY_LOB.SelectedValue.ToString()) ;

			ClsUnderlyingPolicyCoverages objCovInfo = new ClsUnderlyingPolicyCoverages();
			objCovInfo = new ClsUnderlyingPolicyCoverages();
			objCovInfo.CUSTOMER_ID =intCustomerID;
			objCovInfo.POLICY_ID=intPolicyID ;
			objCovInfo.POLICY_VERSION_ID=intPolicyVerID;
			objCovInfo.CREATED_BY = objCovInfo.MODIFIED_BY = int.Parse(GetUserId());
			objCovInfo.CREATED_DATETIME = objCovInfo.LAST_UPDATED_DATETIME = System.DateTime.Now;
			if(this.cmbPOLICY_COMPANY.SelectedValue=="0")
			{
				objCovInfo.POLICY_NUMBER =this.cmbPOLICY_NUMBER.SelectedItem.Text.ToString();
				string [] strArray = cmbPOLICY_NUMBER.SelectedItem.Value.Split('-');
				if(strArray.Length>0 && strArray[0]!=null && strArray[0].ToString().Trim()=="APP")				
					objCovInfo.IS_POLICY = false;				
				else
					objCovInfo.IS_POLICY = true;
			}
			else if(this.cmbPOLICY_COMPANY.SelectedValue=="1")
			{
				objCovInfo.POLICY_NUMBER =this.txtPOLICY_NUMBER.Text.Trim().ToString();
			}
			if(this.txtCOMPANY_OTHER.Text.Trim () != "")
				objCovInfo.POLICY_COMPANY = this.txtCOMPANY_OTHER.Text.Trim().ToString();
			else if(this.cmbPOLICY_COMPANY.SelectedItem.Text.Trim() != "")
				objCovInfo.POLICY_COMPANY = this.cmbPOLICY_COMPANY.SelectedItem.Text.Trim().ToString();
			if (intLob_ID == 2 || intLob_ID == 3)
			{
				if(this.rdoCSL.Checked == true )
				{
					objCovInfo.COVERAGE_TYPE = "C";
					//objSchedule.AddCoverage (objCovInfo);
				}
				else if(this.rdoSPLIT.Checked ==true)
				{
					objCovInfo.COVERAGE_TYPE ="S";
					//objSchedule.AddCoverage (objCovInfo);
				}
			}
			else
			{
				objCovInfo.COVERAGE_TYPE ="";
			}
			return objCovInfo;
		}

		#region InsertCoverages Function
		private void InsertCoverages(ClsScheduleOfUnderlying objSchedule)
		{
			//Coverages  
			int intLob_ID;
			ArrayList alCov = new ArrayList();

			intLob_ID=Convert.ToInt32(cmbPOLICY_LOB.SelectedValue.ToString()) ;
			System.Resources.ResourceManager objResourceMgr  = new System.Resources.ResourceManager("Cms.Policies.Aspx.Umbrella.PolicyAddScheduleOfUnderlying" ,System.Reflection.Assembly.GetExecutingAssembly());

			ClsUnderlyingPolicyCoverages  objCovInfo;
//			objCovInfo = new ClsUnderlyingPolicyCoverages();
//			objCovInfo.CUSTOMER_ID =intCustomerID;
//			objCovInfo.POLICY_ID=intPolicyID ;
//			objCovInfo.POLICY_VERSION_ID=intPolicyVerID;
//			objCovInfo.CREATED_BY = objCovInfo.MODIFIED_BY = int.Parse(GetUserId());
//			objCovInfo.CREATED_DATETIME = objCovInfo.LAST_UPDATED_DATETIME = System.DateTime.Now;
//			if(this.cmbPOLICY_COMPANY.SelectedValue=="0")
//			{
//				objCovInfo.POLICY_NUMBER =this.cmbPOLICY_NUMBER.SelectedItem.Text.ToString();
//				string [] strArray = cmbPOLICY_NUMBER.SelectedItem.Value.Split('-');
//				if(strArray.Length>0 && strArray[0]!=null && strArray[0].ToString().Trim()=="APP")				
//					objCovInfo.IS_POLICY = false;				
//				else
//					objCovInfo.IS_POLICY = true;
//			}
//			else if(this.cmbPOLICY_COMPANY.SelectedValue=="1")
//			{
//				objCovInfo.POLICY_NUMBER =this.txtPOLICY_NUMBER.Text.Trim().ToString();
//			}
//			if(this.rdoCSL.Checked == true )
//			{
//				objCovInfo.COVERAGE_TYPE = "C";
//				//objSchedule.AddCoverage (objCovInfo);
//			}
//			else if(this.rdoSPLIT.Checked ==true)
//			{
//				objCovInfo.COVERAGE_TYPE ="S";
//				//objSchedule.AddCoverage (objCovInfo);
//			}
			
			//Insert Coverages For Aoto 
			if(intLob_ID==2 )
			{
//				if(this.txtAUTO_COV1.Text.Trim () != "")
//				{
//					objCovInfo.COVERAGE_AMOUNT =Convert.ToString(this.txtAUTO_COV1.Text.Trim ());
//					objCovInfo.COVERAGE_DESC =this.capAUTO_COV1.Text ;
//					objSchedule.AddPolicyCoverage (objCovInfo);
//				}
				if(this.rdoSPLIT.Checked == true)
				{
					this.txtAUTO_COV8.Text = "";
					this.txtAUTO_COV4.Text = "";
					this.txtAUTO_COV5.Text = "";

				}
				else if(this.rdoCSL.Checked == true)
				{
					this.txtAUTO_COV3.Text = "";
					this.txtAUTO_COV12.Text = "";
					this.txtAUTO_COV14.Text = "";
					this.txtAUTO_COV10.Text = "";
					this.txtAUTO_COV2.Text = "";
					this.txtAUTO_COV6.Text = "";
					this.txtAUTO_COV7.Text = "";
				}
				if(this.txtAUTO_COV4.Text.Trim () != "")
				{
					objCovInfo=PrepareCoverageObject();
					objCovInfo.COVERAGE_AMOUNT =Convert.ToString(this.txtAUTO_COV4.Text.Trim ());
					objCovInfo.COVERAGE_DESC =this.capAUTO_COV4.Text ;
					objCovInfo.COV_CODE		= objResourceMgr.GetString("txtAUTO_COV4_CODE");
					//objSchedule.AddPolicyCoverage (objCovInfo);
					alCov.Add(objCovInfo);
				}
				if(this.txtAUTO_COV5.Text.Trim () != "")
				{
					objCovInfo=PrepareCoverageObject();
					objCovInfo.COVERAGE_AMOUNT =Convert.ToString(this.txtAUTO_COV5.Text.Trim ());
					objCovInfo.COVERAGE_DESC =this.capAUTO_COV5.Text ;
					objCovInfo.COV_CODE		= objResourceMgr.GetString("txtAUTO_COV5_CODE");
					//objSchedule.AddPolicyCoverage (objCovInfo);
					alCov.Add(objCovInfo);
				}
				if(this.txtAUTO_COV8.Text.Trim () != "")
				{
					objCovInfo=PrepareCoverageObject();
					objCovInfo.COVERAGE_AMOUNT =Convert.ToString(this.txtAUTO_COV8.Text.Trim ());
					objCovInfo.COVERAGE_DESC =this.capAUTO_COV8.Text ;
					objCovInfo.COV_CODE		= objResourceMgr.GetString("txtAUTO_COV8_CODE");
					//objSchedule.AddPolicyCoverage (objCovInfo);
					alCov.Add(objCovInfo);
				}

				if(this.txtAUTO_COV2.Text.Trim () != "" || this.txtAUTO_COV10.Text.Trim () != "")
				{
					objCovInfo=PrepareCoverageObject();
					objCovInfo.COVERAGE_AMOUNT=Convert.ToString(this.txtAUTO_COV2.Text+ "/" +this.txtAUTO_COV10.Text);
					objCovInfo.COVERAGE_DESC =this.capAUTO_COV9.Text ;
					objCovInfo.COV_CODE		= objResourceMgr.GetString("txtAUTO_COV2_CODE");
					//objSchedule.AddPolicyCoverage (objCovInfo);
					alCov.Add(objCovInfo);
				}
				if(this.txtAUTO_COV3.Text.Trim () != "")
				{
					objCovInfo=PrepareCoverageObject();
					objCovInfo.COVERAGE_AMOUNT =Convert.ToString(this.txtAUTO_COV3.Text.Trim ());
					objCovInfo.COVERAGE_DESC =this.capAUTO_COV3.Text ;
					objCovInfo.COV_CODE		= objResourceMgr.GetString("txtAUTO_COV3_CODE");
					//objSchedule.AddPolicyCoverage (objCovInfo);
					alCov.Add(objCovInfo);
				}
				
				if(this.txtAUTO_COV6.Text.Trim () != "" || this.txtAUTO_COV12.Text.Trim () != "")
				{
					objCovInfo=PrepareCoverageObject();
					objCovInfo.COVERAGE_AMOUNT=Convert.ToString(this.txtAUTO_COV6.Text+ "/" +this.txtAUTO_COV12.Text);
					objCovInfo.COVERAGE_DESC =this.capAUTO_COV11.Text ;
					objCovInfo.COV_CODE		= objResourceMgr.GetString("txtAUTO_COV6_CODE");
					//objSchedule.AddPolicyCoverage (objCovInfo);
					alCov.Add(objCovInfo);
				}
				if(this.txtAUTO_COV7.Text.Trim () != "" || this.txtAUTO_COV14.Text.Trim () != "")
				{
					objCovInfo=PrepareCoverageObject();
					objCovInfo.COVERAGE_AMOUNT=Convert.ToString(this.txtAUTO_COV7.Text+ "/" +this.txtAUTO_COV14.Text);
					objCovInfo.COVERAGE_DESC =this.capAUTO_COV13.Text ;
					objCovInfo.COV_CODE		= objResourceMgr.GetString("txtAUTO_COV7_CODE");
					//objSchedule.AddPolicyCoverage (objCovInfo);
					alCov.Add(objCovInfo);
				}
//				if(this.txtAUTO_COV9.Text.Trim () != "")
//				{
//					objCovInfo.COVERAGE_AMOUNT =Convert.ToString(this.txtAUTO_COV9.Text.Trim ());
//					objCovInfo.COVERAGE_DESC =this.capAUTO_COV9.Text ;
//					objSchedule.AddPolicyCoverage (objCovInfo);
//				}
				

			}
				//Insert Coverages For Cycle
			else if(intLob_ID==3)
			{
				if(this.rdoSPLIT.Checked == true)
				{
					this.txtMOT_COV1.Text = "";
					this.txtMOT_COV6.Text = "";
					this.txtMOT_COV4.Text = "";

				}
				else if(this.rdoCSL.Checked == true)
				{
					this.txtMOT_COV2.Text = "";
					this.txtMOT_COV3.Text = "";
					this.txtMOT_COV5.Text = "";
					this.txtMOT_COV7.Text = "";
					this.txtMOT_COV11.Text = "";
					this.txtMOT_COV9.Text = "";
					this.txtMOT_COV13.Text = "";
				}
				if(this.txtMOT_COV1.Text.Trim () != "")
				{
					objCovInfo=PrepareCoverageObject();
					objCovInfo.COVERAGE_AMOUNT =Convert.ToString(this.txtMOT_COV1.Text.Trim ());
					objCovInfo.COVERAGE_DESC =this.capMOT_COV1.Text ;
					objCovInfo.COV_CODE		= objResourceMgr.GetString("txtMOT_COV1_CODE");
					//objSchedule.AddPolicyCoverage (objCovInfo);
					alCov.Add(objCovInfo);
				}
				if(this.txtMOT_COV2.Text.Trim () != "" ||this.txtMOT_COV9.Text.Trim () != "")
				{
					objCovInfo=PrepareCoverageObject();
					objCovInfo.COVERAGE_AMOUNT=Convert.ToString(this.txtMOT_COV2.Text+ "/" +this.txtMOT_COV9.Text);
					objCovInfo.COVERAGE_DESC =this.capMOT_COV8.Text ;
					objCovInfo.COV_CODE		= objResourceMgr.GetString("txtMOT_COV2_CODE");
					//objSchedule.AddPolicyCoverage (objCovInfo);
					alCov.Add(objCovInfo);
				}
				if(this.txtMOT_COV3.Text.Trim () != "")
				{
					objCovInfo=PrepareCoverageObject();
					objCovInfo.COVERAGE_AMOUNT =Convert.ToString(this.txtMOT_COV3.Text.Trim ());
					objCovInfo.COVERAGE_DESC =this.capMOT_COV3.Text ;
					objCovInfo.COV_CODE		= objResourceMgr.GetString("txtMOT_COV3_CODE");
					//objSchedule.AddPolicyCoverage (objCovInfo);
					alCov.Add(objCovInfo);
				}
				if(this.txtMOT_COV4.Text.Trim () != "")
				{
					objCovInfo=PrepareCoverageObject();
					objCovInfo.COVERAGE_AMOUNT =Convert.ToString(this.txtMOT_COV4.Text.Trim ());
					objCovInfo.COVERAGE_DESC =this.capMOT_COV4.Text ;
					objCovInfo.COV_CODE		= objResourceMgr.GetString("txtMOT_COV4_CODE");
					//objSchedule.AddPolicyCoverage (objCovInfo);
					alCov.Add(objCovInfo);
				}
				if(this.txtMOT_COV5.Text.Trim () != "" || this.txtMOT_COV11.Text.Trim () != "")
				{
					objCovInfo=PrepareCoverageObject();
					objCovInfo.COVERAGE_AMOUNT=Convert.ToString(this.txtMOT_COV5.Text+ "/" +this.txtMOT_COV11.Text);
					objCovInfo.COVERAGE_DESC =this.capMOT_COV10.Text ;
					objCovInfo.COV_CODE		= objResourceMgr.GetString("txtMOT_COV5_CODE");
					//objSchedule.AddPolicyCoverage (objCovInfo);
					alCov.Add(objCovInfo);
				}
				if(this.txtMOT_COV6.Text.Trim () != "")
				{
					objCovInfo=PrepareCoverageObject();
					objCovInfo.COVERAGE_AMOUNT =Convert.ToString(this.txtMOT_COV6.Text.Trim ());
					objCovInfo.COVERAGE_DESC =this.capMOT_COV6.Text ;
					objCovInfo.COV_CODE		= objResourceMgr.GetString("txtMOT_COV6_CODE");
					//objSchedule.AddPolicyCoverage (objCovInfo);
					alCov.Add(objCovInfo);
				}
				if(this.txtMOT_COV7.Text.Trim () != "" || this.txtMOT_COV13.Text.Trim () != "")
				{
					objCovInfo=PrepareCoverageObject();
					objCovInfo.COVERAGE_AMOUNT=Convert.ToString(this.txtMOT_COV7.Text+ "/" +this.txtMOT_COV13.Text);
					objCovInfo.COVERAGE_DESC =this.capMOT_COV12.Text ;
					objCovInfo.COV_CODE		= objResourceMgr.GetString("txtMOT_COV7_CODE");
					//objSchedule.AddPolicyCoverage (objCovInfo);
					alCov.Add(objCovInfo);
				}

				
			}
				//Insert COverages for Watercraft
			else if(intLob_ID==4)
			{
				if(this.txtWAT_COV1.Text.Trim () != "")
				{
					objCovInfo=PrepareCoverageObject();
					objCovInfo.COVERAGE_DESC =this.capWAT_COV1.Text ;
					objCovInfo.COVERAGE_AMOUNT =Convert.ToString (this.txtWAT_COV1.Text.Trim());
					objCovInfo.COV_CODE		= objResourceMgr.GetString("txtWAT_COV1_CODE");
					//objSchedule.AddPolicyCoverage (objCovInfo);
					alCov.Add(objCovInfo);
				}
//				if(this.txtWAT_COV2.Text.Trim () != "")
//				{
//					objCovInfo.COVERAGE_DESC =this.capWAT_COV2.Text ;
//					objCovInfo.COVERAGE_AMOUNT =Convert.ToString (this.txtWAT_COV2.Text.Trim());
//					objSchedule.AddPolicyCoverage (objCovInfo);
//				}
				if(this.txtWAT_COV3.Text.Trim () != "")
				{
					objCovInfo=PrepareCoverageObject();
					objCovInfo.COVERAGE_DESC =this.capWAT_COV3.Text ;
					objCovInfo.COVERAGE_AMOUNT =Convert.ToString (this.txtWAT_COV3.Text.Trim());
					objCovInfo.COV_CODE		= objResourceMgr.GetString("txtWAT_COV3_CODE");
					//objSchedule.AddPolicyCoverage (objCovInfo);
					alCov.Add(objCovInfo);
				}
//				if(this.txtWAT_COV4.Text.Trim () != "")
//				{
//					objCovInfo.COVERAGE_DESC =this.capWAT_COV4.Text ;
//					objCovInfo.COVERAGE_AMOUNT =Convert.ToString (this.txtWAT_COV4.Text.Trim());
//					objSchedule.AddPolicyCoverage (objCovInfo);
//				}

			}
				//For Rental & Home
			else if(intLob_ID==1 ||intLob_ID==6 )
			{
//				if(this.txtHOME_COV1 .Text.Trim () != "")
//				{
//					objCovInfo.COVERAGE_DESC =this.capHOME_COV1 .Text ;
//					objCovInfo.COVERAGE_AMOUNT =Convert.ToString (this.txtHOME_COV1.Text.Trim());
//					objSchedule.AddPolicyCoverage (objCovInfo);
//				}
//				if(this.txtHOME_COV2 .Text.Trim () != "")
//				{
//					objCovInfo.COVERAGE_DESC =this.capHOME_COV2 .Text ;
//					objCovInfo.COVERAGE_AMOUNT =Convert.ToString (this.txtHOME_COV2.Text.Trim());
//					objSchedule.AddPolicyCoverage (objCovInfo);
//				}
//				if(this.txtHOME_COV3 .Text.Trim () != "")
//				{
//					objCovInfo.COVERAGE_DESC =this.capHOME_COV3 .Text ;
//					objCovInfo.COVERAGE_AMOUNT =Convert.ToDecimal (this.txtHOME_COV3.Text.Trim());
//					objSchedule.AddPolicyCoverage (objCovInfo);
//				}
//				if(this.txtHOME_COV4 .Text.Trim () != "")
//				{
//					objCovInfo.COVERAGE_DESC =this.capHOME_COV4 .Text ;
//					objCovInfo.COVERAGE_AMOUNT =Convert.ToDecimal (this.txtHOME_COV4.Text.Trim());
//					objSchedule.AddPolicyCoverage (objCovInfo);
//				}
				if(this.txtHOME_COV5 .Text.Trim () != "")
				{
					objCovInfo=PrepareCoverageObject();
					objCovInfo.COVERAGE_DESC =this.capHOME_COV5.Text ;
					objCovInfo.COVERAGE_AMOUNT =Convert.ToString (this.txtHOME_COV5.Text.Trim());
					objCovInfo.COV_CODE		= objResourceMgr.GetString("txtHOME_COV5_CODE");
					//objSchedule.AddPolicyCoverage (objCovInfo);
					alCov.Add(objCovInfo);
				}
//				if(this.txtHOME_COV6 .Text.Trim () != "")
//				{
//					objCovInfo.COVERAGE_DESC =this.capHOME_COV6.Text ;
//					objCovInfo.COVERAGE_AMOUNT =Convert.ToString (this.txtHOME_COV6.Text.Trim());
//					objSchedule.AddPolicyCoverage (objCovInfo);
//				}

			}
			objSchedule.AddPolicyCoverage(alCov);
		}
		#endregion

		private void HideAutoFieldsForMichigan()
		{
			if(hidAppStateID.Value == ((int)enumState.Michigan).ToString())
			{
				lblAUTO_COV4.Text = NOT_COVERED;
				lblAUTO_COV5.Text = NOT_COVERED;
				lblAUTO_COV6.Text = NOT_COVERED;
				lblAUTO_COV7.Text = NOT_COVERED;
				this.txtAUTO_COV4.Attributes.Add("style","display:none");
				this.txtAUTO_COV5.Attributes.Add("style","display:none");
				this.txtAUTO_COV6.Attributes.Add("style","display:none");
				this.txtAUTO_COV7.Attributes.Add("style","display:none");
			}
			else
			{
				lblAUTO_COV4.Text = "";
				lblAUTO_COV5.Text = "";
				lblAUTO_COV6.Text = "";
				lblAUTO_COV7.Text = "";
				this.txtAUTO_COV4.Attributes.Clear();
				this.txtAUTO_COV5.Attributes.Clear();
				this.txtAUTO_COV6.Attributes.Clear();
				this.txtAUTO_COV7.Attributes.Clear();
			}
		}
		private void HideCycleFieldsForMichigan()
		{
			if(hidAppStateID.Value  == ((int)enumState.Michigan).ToString())
			{
				lblMOT_COV4.Text = NOT_COVERED;
				lblMOT_COV5.Text = NOT_COVERED;					
				lblMOT_COV6.Text = NOT_COVERED;					
				lblMOT_COV7.Text = NOT_COVERED;					
				this.txtMOT_COV4.Attributes.Add("style","display:none");
				this.txtMOT_COV5.Attributes.Add("style","display:none");					
				this.txtMOT_COV6.Attributes.Add("style","display:none");					
				this.txtMOT_COV7.Attributes.Add("style","display:none");					
			}
			else
			{
				lblMOT_COV4.Text = "";
				lblMOT_COV5.Text = "";					
				lblMOT_COV6.Text = "";					
				lblMOT_COV7.Text = "";					
				this.txtMOT_COV4.Attributes.Clear();
				this.txtMOT_COV5.Attributes.Clear();
				this.txtMOT_COV6.Attributes.Clear();
				this.txtMOT_COV7.Attributes.Clear();
			}
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
			this.cmbPOLICY_LOB.SelectedIndexChanged += new System.EventHandler(this.cmbPOLICY_LOB_SelectedIndexChanged);
			this.cmbPOLICY_NUMBER.SelectedIndexChanged += new System.EventHandler(this.cmbPOLICY_NUMBER_SelectedIndexChanged);
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnReset_Click(object sender, System.EventArgs e)
		{
			if(hidPolicyNumber.Value !="0" || hidPolicyNumber.Value != "")
			{
				strPolicy_Number =hidPolicyNumber.Value;
				if(hidPolicyCompany.Value !="0" || hidPolicyCompany.Value != "")
					strPolicy_Company = hidPolicyCompany.Value;
				LoadData();
			}
			
		
		}
	}
}
