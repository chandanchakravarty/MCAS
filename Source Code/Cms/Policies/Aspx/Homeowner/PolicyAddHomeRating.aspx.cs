/******************************************************************************************
<Author					: - shafi
<Start Date				: -	17 feb 2006
<End Date				: -	
<Description			: -  Combinatio of Home rating, Prot devices. Square footage and 
							Construction tabs
<Review Date			: - 
<Reviewed By			: - 	
Modification History


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
using Cms.CmsWeb;
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BlApplication;
using Cms.Model.Policy.Homeowners;
using System.Reflection;
using System.Resources;  
using Cms.CmsWeb.Controls;  
using Cms.ExceptionPublisher;  

namespace Cms.Policies.aspx.HomeOwners
{
	/// <summary>
	/// Summary description for AddHomeRating_New.
	/// </summary>
	public class PolicyAddHomeRating : Cms.Policies.policiesbase
	{
		
		protected System.Web.UI.WebControls.Label capSPRINKER;
		protected System.Web.UI.WebControls.DropDownList cmbSPRINKER;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capHYDRANT_DIST;
		//protected System.Web.UI.WebControls.TextBox txtHYDRANT_DIST;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvHYDRANT_DIST;		
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvROOF_TYPE;
		protected System.Web.UI.WebControls.Label capFIRE_STATION_DIST;
		protected System.Web.UI.WebControls.TextBox txtFIRE_STATION_DIST;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvFIRE_STATION_DIST;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNO_OF_FAMILIES;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDWELLING_CONST_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revFIRE_STATION_DIST;
		protected System.Web.UI.WebControls.RegularExpressionValidator revNUM_LOC_ALARMS_APPLIES;
		protected System.Web.UI.WebControls.RangeValidator rngFIRE_STATION_DIST;
		
		protected System.Web.UI.WebControls.Label capIS_UNDER_CONSTRUCTION;
		protected System.Web.UI.WebControls.DropDownList cmbIS_UNDER_CONSTRUCTION;
		protected System.Web.UI.WebControls.DropDownList cmbHYDRANT_DIST;
		//protected System.Web.UI.WebControls.Label capIS_AUTO_POL_WITH_CARRIER;  //Commented by Charles on 6-Nov-09 for Itrack 6722
		//protected System.Web.UI.WebControls.DropDownList cmbIS_AUTO_POL_WITH_CARRIER; //Commented by Charles on 6-Nov-09 for Itrack 6722
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPROT_CLASS;
		//Nov 10,2005:Sumit Chhabra:Commented as it will not be used now
		//protected System.Web.UI.WebControls.Label capPERSONAL_LIAB_TER_CODE;
		//protected System.Web.UI.WebControls.TextBox txtPERSONAL_LIAB_TER_CODE;
		protected System.Web.UI.WebControls.Label capPROT_CLASS;
		protected System.Web.UI.WebControls.DropDownList cmbPROT_CLASS;
		protected System.Web.UI.WebControls.Label capRATING_METHOD;
		//protected System.Web.UI.WebControls.Label capTOT_SQR_FOOTAGE;
		//protected System.Web.UI.WebControls.TextBox txtTOT_SQR_FOOTAGE;
		//protected System.Web.UI.WebControls.RegularExpressionValidator revTOT_SQR_FOOTAGE;
		//protected System.Web.UI.WebControls.Label capGARAGE_SQR_FOOTAGE;
		//protected System.Web.UI.WebControls.TextBox txtGARAGE_SQR_FOOTAGE;
		//protected System.Web.UI.WebControls.RegularExpressionValidator revGARAGE_SQR_FOOTAGE;
		//protected System.Web.UI.WebControls.Label capBREEZE_SQR_FOOTAGE;
		//protected System.Web.UI.WebControls.TextBox txtBREEZE_SQR_FOOTAGE;
		//protected System.Web.UI.WebControls.RegularExpressionValidator revBREEZE_SQR_FOOTAGE;
		//protected System.Web.UI.WebControls.Label capBASMT_SQR_FOOTAGE;
		//protected System.Web.UI.WebControls.TextBox txtBASMT_SQR_FOOTAGE;
		//protected System.Web.UI.WebControls.RegularExpressionValidator revBASMT_SQR_FOOTAGE;
		protected System.Web.UI.WebControls.Label capWIRING_RENOVATION;
		protected System.Web.UI.WebControls.DropDownList cmbWIRING_RENOVATION;
		protected System.Web.UI.WebControls.Label capWIRING_UPDATE_YEAR;
		protected System.Web.UI.WebControls.TextBox txtWIRING_UPDATE_YEAR;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvWIRING_UPDATE_YEAR;
		protected System.Web.UI.WebControls.Label capPLUMBING_RENOVATION;
		protected System.Web.UI.WebControls.DropDownList cmbPLUMBING_RENOVATION;
		protected System.Web.UI.WebControls.Label capPLUMBING_UPDATE_YEAR;
		protected System.Web.UI.WebControls.TextBox txtPLUMBING_UPDATE_YEAR;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPLUMBING_UPDATE_YEAR;
		protected System.Web.UI.WebControls.Label capHEATING_RENOVATION;
		protected System.Web.UI.WebControls.DropDownList cmbHEATING_RENOVATION;
		protected System.Web.UI.WebControls.Label capHEATING_UPDATE_YEAR;
		protected System.Web.UI.WebControls.TextBox txtHEATING_UPDATE_YEAR;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvHEATING_UPDATE_YEAR;
		protected System.Web.UI.WebControls.Label capROOFING_RENOVATION;
		protected System.Web.UI.WebControls.DropDownList cmbROOFING_RENOVATION;
		protected System.Web.UI.WebControls.Label capROOFING_UPDATE_YEAR;
		protected System.Web.UI.WebControls.TextBox txtROOFING_UPDATE_YEAR;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvROOFING_UPDATE_YEAR;
		protected System.Web.UI.WebControls.Label capNO_OF_AMPS;
//		protected System.Web.UI.WebControls.TextBox txtNO_OF_AMPS;
		protected System.Web.UI.WebControls.DropDownList cmbNO_OF_AMPS;

//		protected System.Web.UI.WebControls.RangeValidator rngNO_OF_AMPS;
		protected System.Web.UI.WebControls.Label capCIRCUIT_BREAKERS;
		protected System.Web.UI.WebControls.DropDownList cmbCIRCUIT_BREAKERS;
		protected Cms.CmsWeb.Controls.CmsButton Cmsbutton1;
		protected Cms.CmsWeb.Controls.CmsButton Cmsbutton2;
		protected System.Web.UI.WebControls.Label Label2;
		//protected System.Web.UI.WebControls.Label capNO_OF_APARTMENTS;
		//protected System.Web.UI.WebControls.TextBox txtNO_OF_APARTMENTS;
		//protected System.Web.UI.WebControls.RegularExpressionValidator revNO_OF_APARTMENTS;
		protected System.Web.UI.WebControls.Label capNO_OF_FAMILIES;
		protected System.Web.UI.WebControls.TextBox txtNO_OF_FAMILIES;
		protected System.Web.UI.WebControls.RegularExpressionValidator revNO_OF_FAMILIES;
		protected System.Web.UI.WebControls.Label capEXTERIOR_CONSTRUCTION;
		protected System.Web.UI.WebControls.DropDownList cmbEXTERIOR_CONSTRUCTION;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEXTERIOR_CONSTRUCTION;
		protected System.Web.UI.WebControls.Label lblDESC;
		protected System.Web.UI.WebControls.Label capFOUNDATION;
		protected System.Web.UI.WebControls.DropDownList cmbFOUNDATION;
		protected System.Web.UI.WebControls.Label capROOF_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbROOF_TYPE;
		protected System.Web.UI.WebControls.TextBox txtROOF_OTHER_DESC;
		//protected System.Web.UI.WebControls.Label capWIRING;
		//protected System.Web.UI.WebControls.DropDownList cmbWIRING;
		//protected System.Web.UI.WebControls.Label capWIRING_LAST_INSPECTED;
		//protected System.Web.UI.WebControls.TextBox txtWIRING_LAST_INSPECTED;
		//protected System.Web.UI.WebControls.HyperLink hlkWIRING_LAST_INSPECTED;
		//protected System.Web.UI.WebControls.RegularExpressionValidator revWIRING_LAST_INSPECTED;
		//protected System.Web.UI.WebControls.CustomValidator csvWIRING_LAST_INSPECTED;
		protected System.Web.UI.WebControls.Label capPRIMARY_HEAT_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbPRIMARY_HEAT_TYPE;
		protected System.Web.UI.WebControls.TextBox txtPRIMARY_HEAT_OTHER_DESC;
		protected System.Web.UI.WebControls.Label capSECONDARY_HEAT_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbSECONDARY_HEAT_TYPE;
		//protected System.Web.UI.WebControls.Label capADD_COVERAGE_INFO;
		//protected System.Web.UI.WebControls.TextBox txtADD_COVERAGE_INFO;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvADD_COVERAGE_INFO;
		//protected System.Web.UI.WebControls.Label capIS_OUTSIDE_STAIR;
		//protected System.Web.UI.WebControls.DropDownList cmbIS_OUTSIDE_STAIR;
		protected Cms.CmsWeb.Controls.CmsButton Cmsbutton3;
		protected Cms.CmsWeb.Controls.CmsButton Cmsbutton4;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidStateID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOL_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOL_VERSION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDWELLING_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hiddyear;
		 protected System.Web.UI.HtmlControls.HtmlInputHidden hidPPC;
		protected System.Web.UI.WebControls.Label Label3;
		//protected System.Web.UI.WebControls.Label capSWIMMING_POOL;
		//protected System.Web.UI.WebControls.DropDownList cmbSWIMMING_POOL;
		//protected System.Web.UI.WebControls.Label capSWIMMING_POOL_TYPE;
		//protected System.Web.UI.WebControls.Label lblSWIMMING_POOL_TYPE;
		protected Cms.CmsWeb.Controls.CmsButton Cmsbutton5;
		protected Cms.CmsWeb.Controls.CmsButton Cmsbutton6;
		//protected System.Web.UI.WebControls.DropDownList cmbSWIMMING_POOL_TYPE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidRowId;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;

		public string strCalledFrom="";
		private string strRowId, strFormSaved;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.WebControls.CheckBoxList cblBurgFire;
		protected System.Web.UI.WebControls.CheckBoxList cblDIRECT;
		protected System.Web.UI.WebControls.CheckBoxList cblLOCAL;
		protected System.Web.UI.WebControls.Label lblSC_HEAT_OTHER_DESC;
		protected System.Web.UI.WebControls.Label lblPR_HEAT_OTHER_DESC;
		protected System.Web.UI.WebControls.TextBox txtSECONDARY_HEAT_OTHER_DESC;
		protected System.Web.UI.WebControls.Label capROOF_TYPE_OTHER_DESC;		
		protected System.Web.UI.WebControls.Label capDWELLING_CONST_DATE;
		protected System.Web.UI.WebControls.TextBox txtDWELLING_CONST_DATE;
		protected System.Web.UI.WebControls.HyperLink hlkDWELLING_CONST_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDWELLING_CONST_DATE;
		protected System.Web.UI.WebControls.CustomValidator csvDWELLING_CONST_DATE;
		protected System.Web.UI.WebControls.Label lblDWELLING_CONST_DATE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDefaultTerr;
		protected System.Web.UI.WebControls.CustomValidator csvpWIRING_UPDATE_YEAR;
		protected System.Web.UI.WebControls.CustomValidator csvPLUMBING_UPDATE_YEAR;
		protected System.Web.UI.WebControls.CustomValidator csvROOFING_UPDATE_YEAR;
		protected System.Web.UI.WebControls.CustomValidator csvHEATING_UPDATE_YEAR;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_UNDER_CONSTRUCTION;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_SUPERVISED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNO_OF_AMPS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCIRCUIT_BREAKERS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPRIMARY_HEAT_OTHER_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSECONDARY_HEAT_OTHER_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvROOF_OTHER_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator	rfvWIRING_RENOVATION;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPLUMBING_RENOVATION;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvROOFING_RENOVATION;
		protected System.Web.UI.WebControls.Label capSECONDARY_HEAT_OTHER_DESC;
		protected System.Web.UI.WebControls.Label lblROOF_OTHER_DESC;
		protected System.Web.UI.WebControls.Label capCONSTRUCTION_CODE;
		protected System.Web.UI.WebControls.DropDownList cmbCONSTRUCTION_CODE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvHEATING_RENOVATION;
		protected System.Web.UI.WebControls.RangeValidator rngNO_OF_FAMILIES;
		protected System.Web.UI.HtmlControls.HtmlTableRow trProtectiveDevices;
		protected System.Web.UI.HtmlControls.HtmlTableRow trArmsSupplies;
		protected System.Web.UI.WebControls.Label lblNUM_LOC_ALARMS_APPLIES;
		protected System.Web.UI.WebControls.TextBox txtNUM_LOC_ALARMS_APPLIES;
		protected System.Web.UI.WebControls.RangeValidator rngNUM_LOC_ALARMS_APPLIES;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_VERSION_ID;
		protected System.Web.UI.WebControls.Label capPRIMARY_HEAT_OTHER_DESC;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyType;
		protected System.Web.UI.WebControls.Label capALARM_CERT_ATTACHED;
		protected System.Web.UI.WebControls.DropDownList cmbALARM_CERT_ATTACHED;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spnIS_UNDER_CONSTRUCTION;
		protected System.Web.UI.WebControls.Label capIS_SUPERVISED;
		protected System.Web.UI.WebControls.DropDownList cmbIS_SUPERVISED;
		
		protected System.Web.UI.WebControls.Label capNEED_OF_UNITS;
		protected System.Web.UI.WebControls.RangeValidator rngNEED_OF_UNITS;
		protected System.Web.UI.WebControls.TextBox txtNEED_OF_UNITS;
		protected System.Web.UI.WebControls.TextBox txtRATED_CLASS;
		protected System.Web.UI.WebControls.Label lblRATED_CLASS;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFRAME;
		protected System.Web.UI.WebControls.Label CapLOCATED_IN_SUBDIVISION;
		protected System.Web.UI.WebControls.DropDownList cmbLOCATED_IN_SUBDIVISION;		
		protected System.Web.UI.WebControls.Label CapSUBURBAN_CLASS;
		protected System.Web.UI.WebControls.CheckBox cbSUBURBAN_CLASS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOCATED_IN_SUBDIVISION;
		protected string curDate="";
		public string App_Effective_Date="";
		//ADDED BY PRAVEEN KUMAR(09-02-09)
		public string WebServiceURL;
		//END PRAVEEN

		private void Page_Load(object sender, System.EventArgs e)
		{
			Ajax.Utility.RegisterTypeForAjax(typeof(PolicyAddHomeRating));

			// Put user code to initialize the page here
			//ADDED BY PRAVEEN KUMAR(09-02-09)
			WebServiceURL = ClsCommon.WebServiceURL;
			//END PRAVEEN
			hlkDWELLING_CONST_DATE.Attributes.Add("OnClick","fPopCalendar(document.POL_HOME_RATING_INFO.txtDWELLING_CONST_DATE,document.POL_HOME_RATING_INFO.txtDWELLING_CONST_DATE)");
			cmbIS_UNDER_CONSTRUCTION.Attributes.Add("onChange","javascript:DisplayDate();");
			
			#region Setting ScreenId
			// Check from where the screen is called.
			if (Request.QueryString["CalledFrom"] != null || Request.QueryString["CalledFrom"] !="")
			{
				strCalledFrom=Request.QueryString["CalledFrom"].ToString();
			}
			// Suburban Discount Field
			cbSUBURBAN_CLASS.Attributes.Add("onclick","javascript:DisplaySuburbanHomeLocation(false);");
		
		
			//Setting screen Id.	
			switch (strCalledFrom) 
			{
				case "Home":
				case "HOME":
					base.ScreenId="239_1";
					capIS_UNDER_CONSTRUCTION.Text="Is the dwelling under construction?";
					//capIS_AUTO_POL_WITH_CARRIER.Text="Does Wolverine Mutual write your Auto policy?"; //Commented by Charles on 6-Nov-09 for Itrack 6722
					trArmsSupplies.Visible=false;
					trProtectiveDevices.Visible=true;
					capNEED_OF_UNITS.Text="If HO-4/HO-6, # of units/apts";//Done for Itrack Issue 6492 on 5 Oct 09
					//rngNEED_OF_UNITS.ErrorMessage = "#units/apts should be numeric and must lie between 1 and 100";                  
					rngNEED_OF_UNITS.ErrorMessage = ClsMessages.FetchGeneralMessage("216");
					break;
				case "Rental":
				case "RENTAL":
					base.ScreenId="259_1";
					capIS_UNDER_CONSTRUCTION.Text="Building Under Construction - Builders Risk";
					//capIS_AUTO_POL_WITH_CARRIER.Text="Does Wolverine Mutual write any policy for you?"; //Commented by Charles on 6-Nov-09 for Itrack 6722
					trArmsSupplies.Visible=true;
					trProtectiveDevices.Visible=false;
					capNEED_OF_UNITS.Text="#Units/Apts"  ;
					//rngNEED_OF_UNITS.ErrorMessage = "#Units/Apts should be numeric and must lie between 1 and 100";				
					rngNEED_OF_UNITS.ErrorMessage = ClsMessages.FetchGeneralMessage("216");
					disableValidators();
					break;
				default:
					base.ScreenId="5";
					disableValidators();
					break;
			}
			#endregion
			// Put user code to initialize the page here
			btnReset.Attributes.Add("onclick","javascript:return ResetForm();");
			btnSave.Attributes.Add("onclick","javascript:chkNo_FAMILIES_FOR_HO_4_HO_6();DisplaySuburbanHomeLocation(true);");//Done for Itrack Issue 6492 on 5 Oct 09
			//hlkWIRING_LAST_INSPECTED.Attributes.Add("OnClick","OpenCalendar(document.POL_HOME_RATING_INFO.txtWIRING_LAST_INSPECTED,document.POL_HOME_RATING_INFO.txtWIRING_LAST_INSPECTED)");

			lblMessage.Visible = false;
			SetErrorMessages();
			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass	=	CmsButtonType.Write ;
			btnReset.PermissionString		=	gstrSecurityXML;

			btnSave.CmsButtonClass	        =	CmsButtonType.Write ;
			btnSave.PermissionString		=	gstrSecurityXML;

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			
			curDate=DateTime.Now.ToString() ;  

			if(!Page.IsPostBack)
			{   
				PopulateHiddenFields();
				LoadCombos();

				if(Request.QueryString["DWELLINGID"]!=null && Request.QueryString["DWELLINGID"]!="" )
				{
					LoadData();
				}
					
				//Disable Builder's risk according to product
				if ( this.hidPolicyType.Value == "11458" || this.hidPolicyType.Value == "11480" || this.hidPolicyType.Value == "11482" || this.hidPolicyType.Value == "11290" || this.hidPolicyType.Value == "11292")
				{
					this.cmbIS_UNDER_CONSTRUCTION.Enabled = false;
					cmbIS_UNDER_CONSTRUCTION.SelectedIndex = 1;
				}

				SetCaptions();
				SetWorkFlow();
				ShowHideMultiPolicyDiscount();
			}
		}//end pageload
		
		
		/// <summary>
		/// populating hidden fields
		/// </summary>
		private void PopulateHiddenFields()
		{
			//policies/aspx/Homeowner/PolicyAddHomeRating.aspx?CustomerID=770&POLICY_ID=18&POLICY_VERSION_ID=1&DWELLINGID=2&CalledFrom=Rental&DWELLING_ID=2&transferdata=
			hidCUSTOMER_ID.Value    = Request.QueryString["CustomerID"].ToString()  ;
			hidPOL_ID.Value      = Request.QueryString["POLICY_ID"].ToString();
			hidPOL_VERSION_ID.Value = Request.QueryString["POLICY_VERSION_ID"].ToString();
			hidDWELLING_ID.Value    = Request.QueryString["DWELLINGID"].ToString();   
            
			clsapplication objApp = new clsapplication();
			
			DataSet dsProduct = objApp.GetPolicyTypeAndStateForPolicy(Convert.ToInt32(hidCUSTOMER_ID.Value),Convert.ToInt32(hidPOL_ID.Value),Convert.ToInt32(hidPOL_VERSION_ID.Value));
			
			if ( dsProduct != null && dsProduct.Tables[0].Rows.Count > 0 )
			{
				if ( dsProduct.Tables[0].Rows[0]["POLICY_TYPE"] != DBNull.Value )
				{
					this.hidPolicyType.Value = dsProduct.Tables[0].Rows[0]["POLICY_TYPE"].ToString();
				} 
			}

		}

		private void ShowHideMultiPolicyDiscount()
		{
			int stateID;
			stateID=ClsVehicleInformation.GetStateIdForpolicy(int.Parse(hidCUSTOMER_ID.Value),hidPOL_ID.Value ,hidPOL_VERSION_ID.Value);
			hidStateID.Value =stateID.ToString();
			//If the state is Indiana (14) and the page is called for Rental, then hide the multi policy discount 
			if((strCalledFrom.ToUpper()=="REN" || strCalledFrom.ToUpper()=="RENTAL") && (hidStateID.Value=="14"))
			{
				//Commented by Charles on 6-Nov-09 for Itrack 6722
				//capIS_AUTO_POL_WITH_CARRIER.Visible=false;
				//cmbIS_AUTO_POL_WITH_CARRIER.Visible=false;
			}
		}
			

		private void enablevalidators()
		{
			//		rfvWIRING_RENOVATION.Visible =true;
		}
		private void disableValidators()
		{
			//		rfvWIRING_RENOVATION.Visible=false;

		}
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetErrorMessages()
		{
         
			//<Gaurav Tyagi> 30 May 2005 ,START Added for new requried fields BUG No:588

			rfvFIRE_STATION_DIST.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("676");
			rfvNO_OF_FAMILIES.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1011");
			rfvDWELLING_CONST_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1006");
			rngNO_OF_FAMILIES.ErrorMessage	=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"19");
			rfvROOF_TYPE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"22");
			rfvHYDRANT_DIST.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			revNUM_LOC_ALARMS_APPLIES.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"20");
			rngNUM_LOC_ALARMS_APPLIES.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage ("216");
			rngFIRE_STATION_DIST.ErrorMessage		= Cms.CmsWeb.ClsMessages.FetchGeneralMessage ("216");
			//<Gaurav Tyagi> 30 May 2005 ,START Added for new requried fields BUG No:588
			
			//revHYDRANT_DIST.ValidationExpression        = aRegExpDoublePositiveNonZero; 
			revFIRE_STATION_DIST.ValidationExpression   = aRegExpDoublePositiveNonZero;
			revNUM_LOC_ALARMS_APPLIES.ValidationExpression=	aRegExpInteger;
			//revHYDRANT_DIST.ErrorMessage                = Cms.CmsWeb.ClsMessages.FetchGeneralMessage ("216"); 
			revFIRE_STATION_DIST.ErrorMessage           = Cms.CmsWeb.ClsMessages.FetchGeneralMessage ("216");
			rfvPROT_CLASS.ErrorMessage =			Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");
			
			//Construction
			//revNO_OF_APARTMENTS.ValidationExpression	=	aRegExpInteger;
			revNO_OF_FAMILIES.ValidationExpression		=	aRegExpInteger;
			//revMONTH_OCC_EACH_YEAR.ValidationExpression  =   aRegExpInteger;
			//revWIRING_LAST_INSPECTED.ValidationExpression	  =	aRegExpDate;
			//revNO_OF_APARTMENTS.ErrorMessage			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"11");
			revNO_OF_FAMILIES.ErrorMessage				=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"12");
			//revMONTH_OCC_EACH_YEAR.ErrorMessage         =   Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			//revWIRING_LAST_INSPECTED.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"22");
			//csvWIRING_LAST_INSPECTED.ErrorMessage		=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"13");
			rfvEXTERIOR_CONSTRUCTION.ErrorMessage       =	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"14");
			//rfvADD_COVERAGE_INFO.ErrorMessage			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"15");
			
			//Sqaure ffotage
			rfvHEATING_UPDATE_YEAR.ErrorMessage			=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage ("155");
			//rfvEXTERIOR_PAINT_YEAR.ErrorMessage			=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage ("161"); 
			rfvROOFING_UPDATE_YEAR.ErrorMessage			=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage ("157");
			rfvPLUMBING_UPDATE_YEAR.ErrorMessage		=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage ("153");
			rfvWIRING_UPDATE_YEAR.ErrorMessage			=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage ("151");
			rfvLOCATED_IN_SUBDIVISION.ErrorMessage		=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1056");
			//rngPLUMBING_UPDATE_YEAR.ErrorMessage          =   Cms.CmsWeb.ClsMessages.FetchGeneralMessage ("164");
			//rngHEATING_UPDATE_YEAR.ErrorMessage        =   Cms.CmsWeb.ClsMessages.FetchGeneralMessage ("164");
			//rngROOFING_UPDATE_YEAR.ErrorMessage         =   Cms.CmsWeb.ClsMessages.FetchGeneralMessage ("164");
			//rngEXTERIOR_PAINT_YEAR.ErrorMessage         =   Cms.CmsWeb.ClsMessages.FetchGeneralMessage ("164");
			//rngWIRING_UPDATE_YEAR.ErrorMessage         =   Cms.CmsWeb.ClsMessages.FetchGeneralMessage ("164");


			//revTOT_SQR_FOOTAGE.ValidationExpression=aRegExpCurrencyformat ;
			//revGARAGE_SQR_FOOTAGE.ValidationExpression=aRegExpCurrencyformat ;
			//revBREEZE_SQR_FOOTAGE.ValidationExpression=aRegExpCurrencyformat ;
			//revBASMT_SQR_FOOTAGE.ValidationExpression=aRegExpCurrencyformat ;
			//revTOT_SQR_FOOTAGE.ValidationExpression=aRegExpDoublePositiveNonZero;
			//revGARAGE_SQR_FOOTAGE.ValidationExpression=aRegExpDoublePositiveNonZero;
			//revBREEZE_SQR_FOOTAGE.ValidationExpression =aRegExpDoublePositiveNonZero;
			//revBASMT_SQR_FOOTAGE.ValidationExpression =aRegExpDoublePositiveNonZero;

			//revTOT_SQR_FOOTAGE.ErrorMessage =Cms.CmsWeb.ClsMessages.FetchGeneralMessage ("216"); 
			//revGARAGE_SQR_FOOTAGE.ErrorMessage =Cms.CmsWeb.ClsMessages.FetchGeneralMessage ("216");
			//revBREEZE_SQR_FOOTAGE.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage ("216");
			//revBASMT_SQR_FOOTAGE.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage ("216");
//			rngNO_OF_AMPS.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage ("216");

			//-------Added by mohit.
			revDWELLING_CONST_DATE.ValidationExpression	  =	aRegExpDate;
			revDWELLING_CONST_DATE.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"22");
			csvDWELLING_CONST_DATE.ErrorMessage		= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("487");

			if(base.ScreenId	==	"239_1")
			{
				rfvIS_UNDER_CONSTRUCTION.ErrorMessage=Cms.CmsWeb.ClsMessages.GetMessage (base.ScreenId, "16");
				rfvIS_SUPERVISED.ErrorMessage=Cms.CmsWeb.ClsMessages.GetMessage (base.ScreenId, "23");
			}
			else
				rfvIS_UNDER_CONSTRUCTION.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("564");
				rfvIS_SUPERVISED.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1005");

			rfvCIRCUIT_BREAKERS.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("566");
			rfvNO_OF_AMPS.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("565");

			rfvROOF_OTHER_DESC.ErrorMessage=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"457");
			rfvPRIMARY_HEAT_OTHER_DESC.ErrorMessage	 =Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"457");
			rfvSECONDARY_HEAT_OTHER_DESC.ErrorMessage=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"457");

		}


		private void SetWorkFlow()
		{
			if(base.ScreenId	==	"239_1" || base.ScreenId == "259_1")
			{
				myWorkFlow.IsTop	=	false;
				myWorkFlow.ScreenID	=	base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID",hidCUSTOMER_ID.Value);
				myWorkFlow.AddKeyValue("POLICY_ID",hidPOL_ID.Value);
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",hidPOL_VERSION_ID.Value);
				myWorkFlow.AddKeyValue("DWELLING_ID",hidDWELLING_ID.Value);
				myWorkFlow.GetScreenStatus();
				myWorkFlow.SetScreenStatus();
			}
			else
			{
				myWorkFlow.Display	=	false;
			}
		}

		/// <summary>
		/// populating combo fields
		/// </summary>
		private void LoadCombos()
		{
			
			DataSet dsLookup = ClsHomeRating.GetHomeRatingLookup();

			//Rating method
			//Nov11,2005:Sumit Chhabra:Commented as the control has been removed from here
			//			cmbRATING_METHOD.DataSource = dsLookup.Tables[0];
			//			cmbRATING_METHOD.DataTextField = "LOOKUP_VALUE_DESC";
			//			cmbRATING_METHOD.DataValueField = "LOOKUP_UNIQUE_ID";
			//			cmbRATING_METHOD.DataBind(); 
			
			//Renovations//////////////////////////// 
			//Oct7:2005:Sumit:Double binding of data has been commented
			//			cmbWIRING_RENOVATION.DataSource = dsLookup.Tables[1];
			//			cmbWIRING_RENOVATION.DataTextField = "LOOKUP_VALUE_DESC";
			//			cmbWIRING_RENOVATION.DataValueField = "LOOKUP_UNIQUE_ID";
			//			cmbWIRING_RENOVATION.DataBind();
			
			cmbWIRING_RENOVATION.DataSource = dsLookup.Tables[1];
			cmbWIRING_RENOVATION.DataTextField = "LOOKUP_VALUE_DESC";
			cmbWIRING_RENOVATION.DataValueField = "LOOKUP_UNIQUE_ID";
			cmbWIRING_RENOVATION.DataBind();
			cmbWIRING_RENOVATION.Items.Insert(0,"");
			cmbWIRING_RENOVATION.SelectedIndex=2;


			cmbPLUMBING_RENOVATION.DataSource = dsLookup.Tables[1];
			cmbPLUMBING_RENOVATION.DataTextField = "LOOKUP_VALUE_DESC";
			cmbPLUMBING_RENOVATION.DataValueField = "LOOKUP_UNIQUE_ID";
			cmbPLUMBING_RENOVATION.DataBind();
			cmbPLUMBING_RENOVATION.Items.Insert(0,"");
			cmbPLUMBING_RENOVATION.SelectedIndex=2;

			cmbHEATING_RENOVATION.DataSource = dsLookup.Tables[1];;
			cmbHEATING_RENOVATION.DataTextField = "LOOKUP_VALUE_DESC";
			cmbHEATING_RENOVATION.DataValueField = "LOOKUP_UNIQUE_ID";
			cmbHEATING_RENOVATION.DataBind();
			cmbHEATING_RENOVATION.Items.Insert(0,"");
			cmbHEATING_RENOVATION.SelectedIndex=2;
			
			cmbROOFING_RENOVATION.DataSource = dsLookup.Tables[1];
			cmbROOFING_RENOVATION.DataTextField = "LOOKUP_VALUE_DESC";
			cmbROOFING_RENOVATION.DataValueField = "LOOKUP_UNIQUE_ID";
			cmbROOFING_RENOVATION.DataBind();
			cmbROOFING_RENOVATION.Items.Insert(0,"");
			cmbROOFING_RENOVATION.SelectedIndex=2;
			/////////////////////////////////////
			

			//Yes no for circuit breakers
			cmbCIRCUIT_BREAKERS.DataSource = dsLookup.Tables[2];
			cmbCIRCUIT_BREAKERS.DataTextField = "LOOKUP_VALUE_DESC";
			cmbCIRCUIT_BREAKERS.DataValueField = "LOOKUP_UNIQUE_ID";
			cmbCIRCUIT_BREAKERS.DataBind();
			// Added by Mohit on 2/11/2005
			cmbCIRCUIT_BREAKERS.Items.Insert(0,"");
				
			//Foundation code
			cmbFOUNDATION.DataSource = dsLookup.Tables[3];
			cmbFOUNDATION.DataTextField = "LOOKUP_VALUE_DESC";
			cmbFOUNDATION.DataValueField = "LOOKUP_UNIQUE_ID";
			cmbFOUNDATION.DataBind();
			cmbFOUNDATION.Items.Insert(0,"");

			//Exterior construction
			cmbEXTERIOR_CONSTRUCTION.DataSource = dsLookup.Tables[4];
			cmbEXTERIOR_CONSTRUCTION.DataTextField = "LOOKUP_VALUE_DESC";
			cmbEXTERIOR_CONSTRUCTION.DataValueField = "LOOKUP_UNIQUE_ID";
			cmbEXTERIOR_CONSTRUCTION.DataBind();

			hidFRAME.Value = ClsHomeRating.GetFRAME_OR_MASONRY();
			
			//Added By Shafi 16-01-2006
			cmbEXTERIOR_CONSTRUCTION.Items.Insert(0,"");
			
			//Wiring
			//			cmbWIRING.DataSource = dsLookup.Tables[5];
			//			cmbWIRING.DataTextField = "LOOKUP_VALUE_DESC";
			//			cmbWIRING.DataValueField = "LOOKUP_UNIQUE_ID";
			//			cmbWIRING.DataBind();
			
			//Primary heat and secondary heat
			cmbPRIMARY_HEAT_TYPE.DataSource = dsLookup.Tables[6];
			cmbPRIMARY_HEAT_TYPE.DataTextField = "LOOKUP_VALUE_DESC";
			cmbPRIMARY_HEAT_TYPE.DataValueField = "LOOKUP_UNIQUE_ID";
			cmbPRIMARY_HEAT_TYPE.DataBind();

			cmbSECONDARY_HEAT_TYPE.DataSource = dsLookup.Tables[6];
			cmbSECONDARY_HEAT_TYPE.DataTextField = "LOOKUP_VALUE_DESC";
			cmbSECONDARY_HEAT_TYPE.DataValueField = "LOOKUP_UNIQUE_ID";
			cmbSECONDARY_HEAT_TYPE.DataBind();

			//Roofing type
			cmbROOF_TYPE.DataSource = dsLookup.Tables[7];
			cmbROOF_TYPE.DataTextField = "LOOKUP_VALUE_DESC";
			cmbROOF_TYPE.DataValueField = "LOOKUP_UNIQUE_ID";
			cmbROOF_TYPE.DataBind();
			//Added By Shafi
			cmbROOF_TYPE.Items.Insert(0,"");

			//Construction Code
			if(Request.QueryString["CALLEDFROM"].ToUpper()=="HOME" )
			{
				cmbCONSTRUCTION_CODE.DataSource = dsLookup.Tables[12];
				//cmbCONSTRUCTION_CODE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("HOCC");
				cmbCONSTRUCTION_CODE.DataTextField = "LOOKUP_VALUE_DESC";
				cmbCONSTRUCTION_CODE.DataValueField = "LOOKUP_UNIQUE_ID";
				cmbCONSTRUCTION_CODE.DataBind();
				cmbCONSTRUCTION_CODE.Items.Insert(0,"");
			}
			else 
				if(Request.QueryString["CALLEDFROM"].ToUpper()=="RENTAL" || Request.QueryString["CALLEDFROM"]=="Rental")
			{
				cmbCONSTRUCTION_CODE.DataSource = dsLookup.Tables[13];
				//cmbCONSTRUCTION_CODE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("RDCC");
				cmbCONSTRUCTION_CODE.DataTextField = "LOOKUP_VALUE_DESC";
				cmbCONSTRUCTION_CODE.DataValueField = "LOOKUP_UNIQUE_ID";
				cmbCONSTRUCTION_CODE.DataBind();
				cmbCONSTRUCTION_CODE.Items.Insert(0,"");
			}

			cmbHYDRANT_DIST.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("RIFS");
			cmbHYDRANT_DIST.DataTextField = "LookupDesc";
			cmbHYDRANT_DIST.DataValueField = "LookupID";
			cmbHYDRANT_DIST.DataBind();

			cmbPROT_CLASS.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("PRCLS","-1","Y");
			cmbPROT_CLASS.DataTextField = "LookupDesc";
			cmbPROT_CLASS.DataValueField = "LookupID";
			cmbPROT_CLASS.DataBind();

			// Added by Mohit Agarwal to get PPC from APP_PPC_STATE_ADD
			// Date: 18-Jan-2007
			string ppc = ClsHomeRating.GetHomeRatingPPCForPolicy(int.Parse(hidPOL_ID.Value),
				int.Parse(hidCUSTOMER_ID.Value),
				int.Parse(hidPOL_VERSION_ID.Value),
				int.Parse(hidDWELLING_ID.Value));
			if(ppc.StartsWith("1"))
				cmbPROT_CLASS.SelectedIndex = 0;
			else if(ppc.StartsWith("2"))
				cmbPROT_CLASS.SelectedIndex = 1;
			else if(ppc.StartsWith("3"))
				cmbPROT_CLASS.SelectedIndex = 2;
			else if(ppc.StartsWith("4"))
				cmbPROT_CLASS.SelectedIndex = 3;
			else if(ppc.StartsWith("5"))
				cmbPROT_CLASS.SelectedIndex = 4;
			else if(ppc.StartsWith("6"))
				cmbPROT_CLASS.SelectedIndex = 5;
			else if(ppc.StartsWith("7"))
				cmbPROT_CLASS.SelectedIndex = 6;
			else if(ppc.StartsWith("8B"))
				cmbPROT_CLASS.SelectedIndex = 8;
			else if(ppc.StartsWith("8"))
				cmbPROT_CLASS.SelectedIndex = 7;
			else if(ppc.StartsWith("9"))
				cmbPROT_CLASS.SelectedIndex = 9;
			else if(ppc.StartsWith("10"))
				cmbPROT_CLASS.SelectedIndex = 10;
			else
				cmbPROT_CLASS.SelectedIndex = 0;
			hidPPC.Value = cmbPROT_CLASS.SelectedIndex.ToString();

			if(Request.QueryString["CALLEDFROM"].ToUpper()=="RENTAL")
			{
				ListItem Li = cmbPROT_CLASS.Items.FindByValue("11862"); // Value : '8B'
				cmbPROT_CLASS.Items.Remove(Li);
			}

			/*
			//Burglary
			DataTable dtBurglary = dsLookup.Tables[10];
			dtBurglary.Rows.RemoveAt(0);
			cblBURGLAR.DataSource = dtBurglary;
			cblBURGLAR.DataTextField = "LOOKUP_VALUE_DESC";
			cblBURGLAR.DataValueField = "LOOKUP_UNIQUE_ID";
			cblBURGLAR.DataBind();
			
			
			//Smoke
			DataTable dtSmoke = dsLookup.Tables[9];
			dtSmoke.Rows.RemoveAt(0);
			cblSMOKE.DataSource = dtSmoke;
			cblSMOKE.DataTextField = "LOOKUP_VALUE_DESC";
			cblSMOKE.DataValueField = "LOOKUP_UNIQUE_ID";
			cblSMOKE.DataBind();

			//Temperature
			DataTable dtTemp = dsLookup.Tables[8];
			dtTemp.Rows.RemoveAt(0);
			cblTEMPERATURE.DataSource = dtTemp;
			cblTEMPERATURE.DataTextField = "LOOKUP_VALUE_DESC";
			cblTEMPERATURE.DataValueField = "LOOKUP_UNIQUE_ID";
			cblTEMPERATURE.DataBind();
			*/

			//Swiming pool
			//			this.cmbSWIMMING_POOL_TYPE.DataSource = dsLookup.Tables[11];
			//			cmbSWIMMING_POOL_TYPE.DataTextField = "LOOKUP_VALUE_DESC";
			//			cmbSWIMMING_POOL_TYPE.DataValueField = "LOOKUP_UNIQUE_ID";
			//			cmbSWIMMING_POOL_TYPE.DataBind();
			
			ListItem liBoth = new ListItem();
			
			liBoth.Text = "Central Stations Burglary and Fire Alarm System";
			//liBoth.Attributes.Add("onClick","Check(1,this.checked)"); //Commented by Charles on 20-Oct-09 for Itrack 6586

			this.cblBurgFire.Items.Add(liBoth);
			this.cblBurgFire.Items.Add(new ListItem("Central Station Fire",""));
			this.cblBurgFire.Items.Add(new ListItem("Central Station Burglary",""));
			
			ListItem liDirect = new ListItem();
			liDirect.Text = "Direct to Fire and Police";

			//liDirect.Attributes.Add("onClick","Check(2,this.checked)"); //Commented by Charles on 20-Oct-09 for Itrack 6586

			this.cblDIRECT.Items.Add(liDirect);
			this.cblDIRECT.Items.Add(new ListItem("Direct to Fire",""));
			this.cblDIRECT.Items.Add(new ListItem("Direct to Police",""));

			this.cblLOCAL.Items.Add(new ListItem("Local Fire or local Gas Alarm",""));
			this.cblLOCAL.Items.Add(new ListItem("Two or more Local Fire Alarms",""));
			//Added funtion MAND_ALARM_CERT_ATTACHED to onclick events, Itrack 6586, Charles,20-Oct-09 
			this.cblLOCAL.Attributes.Add("onclick",
				"disableChkLocal('cblLOCAL');MAND_ALARM_CERT_ATTACHED()");

			this.cblBurgFire.Attributes.Add("onclick", 
				"disableListItems('cblBurgFire', '0', '3');MAND_ALARM_CERT_ATTACHED()");
			
			this.cblDIRECT.Attributes.Add("onclick", 
				"disableListItems('cblDIRECT', '0', '3');MAND_ALARM_CERT_ATTACHED()");
			//Loading sprinker drop-down
			cmbSPRINKER.DataSource = dsLookup.Tables[14];			
			cmbSPRINKER.DataTextField = "LOOKUP_VALUE_DESC";
			cmbSPRINKER.DataValueField = "LOOKUP_UNIQUE_ID";
			cmbSPRINKER.DataBind();
			cmbSPRINKER.Items.Insert(0,"");
			// ALARM_CERT_ATTACHED
			cmbALARM_CERT_ATTACHED.DataSource = dsLookup.Tables[2];
			cmbALARM_CERT_ATTACHED.DataTextField = "LOOKUP_VALUE_DESC";
			cmbALARM_CERT_ATTACHED.DataValueField = "LOOKUP_UNIQUE_ID";
			cmbALARM_CERT_ATTACHED.DataBind();
			cmbALARM_CERT_ATTACHED.Items.Insert(0,"");

			cmbNO_OF_AMPS.DataSource = dsLookup.Tables[15];
			cmbNO_OF_AMPS.DataTextField = "LOOKUP_VALUE_DESC";
			cmbNO_OF_AMPS.DataValueField = "LOOKUP_UNIQUE_ID";
			cmbNO_OF_AMPS.DataBind();
			cmbNO_OF_AMPS.Items.Insert(0,"");
			
			cmbLOCATED_IN_SUBDIVISION.DataSource = dsLookup.Tables[2];
			cmbLOCATED_IN_SUBDIVISION.DataTextField = "LOOKUP_VALUE_DESC";
			cmbLOCATED_IN_SUBDIVISION.DataValueField = "LOOKUP_UNIQUE_ID";
			cmbLOCATED_IN_SUBDIVISION.DataBind();
			cmbLOCATED_IN_SUBDIVISION.Items.Insert(0,"");

		}


		/// <summary>
		/// Setting captions for the labels
		/// </summary>
		private void SetCaptions()
		{
			 
			System.Resources.ResourceManager objResourceMgr  = new System.Resources.ResourceManager("Cms.Policies.aspx.HomeOwners.PolicyAddHomeRating"  ,System.Reflection.Assembly.GetExecutingAssembly());

			//Home rating
			//capHYDRANT_DIST.Text						    =		objResourceMgr.GetString("txtHYDRANT_DIST");
			capHYDRANT_DIST.Text						    =		objResourceMgr.GetString("cmbHYDRANT_DIST");
			capFIRE_STATION_DIST.Text						=		objResourceMgr.GetString("txtFIRE_STATION_DIST");
			//	capIS_UNDER_CONSTRUCTION.Text					=		objResourceMgr.GetString("cmbIS_UNDER_CONSTRUCTION");
            
			//capIS_AUTO_POL_WITH_CARRIER.Text				=		objResourceMgr.GetString("cmbIS_AUTO_POL_WITH_CARRIER");
			//capPERSONAL_LIAB_TER_CODE.Text					=		objResourceMgr.GetString("txtPERSONAL_LIAB_TER_CODE");
			capPROT_CLASS.Text						        =		objResourceMgr.GetString("cmbPROT_CLASS");
			//capRATING_METHOD.Text						    =		objResourceMgr.GetString("cmbRATING_METHOD");

			//Construction
			//capNO_OF_APARTMENTS.Text				=		objResourceMgr.GetString("txtNO_OF_APARTMENTS");
			capNO_OF_FAMILIES.Text					=		objResourceMgr.GetString("txtNO_OF_FAMILIES");
			capEXTERIOR_CONSTRUCTION.Text			=		objResourceMgr.GetString("cmbEXTERIOR_CONSTRUCTION");
			capFOUNDATION.Text						=		objResourceMgr.GetString("cmbFOUNDATION");
			//capFOUNDATION_OTHER_DESC.Text			=		objResourceMgr.GetString("txtPRIMARY_HEAT_OTHER_DESC");
			capROOF_TYPE.Text						=		objResourceMgr.GetString("cmbROOF_TYPE");
			capROOF_TYPE_OTHER_DESC.Text					=		objResourceMgr.GetString("txtROOF_OTHER_DESC");
			//capWIRING.Text							=		objResourceMgr.GetString("cmbWIRING");
			//capWIRING_LAST_INSPECTED.Text			=		objResourceMgr.GetString("txtWIRING_LAST_INSPECTED");
			capPRIMARY_HEAT_TYPE.Text				=		objResourceMgr.GetString("cmbPRIMARY_HEAT_TYPE");
			capSECONDARY_HEAT_TYPE.Text				=		objResourceMgr.GetString("cmbSECONDARY_HEAT_TYPE");
			capCONSTRUCTION_CODE.Text			   =      objResourceMgr.GetString("cmbCONSTRUCTION_CODE");
			//capMONTH_OCC_EACH_YEAR.Text				=		objResourceMgr.GetString("txtMONTH_OCC_EACH_YEAR");
			//capADD_COVERAGE_INFO.Text				=		objResourceMgr.GetString("txtADD_COVERAGE_INFO");
			//capIS_OUTSIDE_STAIR.Text				=		objResourceMgr.GetString("cmbIS_OUTSIDE_STAIR");

			//Square footage
			//capTOT_SQR_FOOTAGE.Text						    =		objResourceMgr.GetString("txtTOT_SQR_FOOTAGE");
			//capGARAGE_SQR_FOOTAGE.Text						=		objResourceMgr.GetString("txtGARAGE_SQR_FOOTAGE");
			//capBREEZE_SQR_FOOTAGE.Text						=		objResourceMgr.GetString("txtBREEZE_SQR_FOOTAGE");
			//capBASMT_SQR_FOOTAGE.Text						=		objResourceMgr.GetString("txtBASMT_SQR_FOOTAGE");
			capWIRING_RENOVATION.Text						=		objResourceMgr.GetString("cmbWIRING_RENOVATION");
			capWIRING_UPDATE_YEAR.Text						=		objResourceMgr.GetString("txtWIRING_UPDATE_YEAR");
			capPLUMBING_RENOVATION.Text						=		objResourceMgr.GetString("cmbPLUMBING_RENOVATION");
			capPLUMBING_UPDATE_YEAR.Text					=		objResourceMgr.GetString("txtPLUMBING_UPDATE_YEAR");
			capHEATING_RENOVATION.Text						=		objResourceMgr.GetString("cmbHEATING_RENOVATION");
			capHEATING_UPDATE_YEAR.Text						=		objResourceMgr.GetString("txtHEATING_UPDATE_YEAR");
			capROOFING_RENOVATION.Text						=		objResourceMgr.GetString("cmbROOFING_RENOVATION");
			capROOFING_UPDATE_YEAR.Text						=		objResourceMgr.GetString("txtROOFING_UPDATE_YEAR");
//			capNO_OF_AMPS.Text						        =		objResourceMgr.GetString("txtNO_OF_AMPS");
			capNO_OF_AMPS.Text						        =		objResourceMgr.GetString("cmbNO_OF_AMPS");

			capCIRCUIT_BREAKERS.Text						=		objResourceMgr.GetString("cmbCIRCUIT_BREAKERS");
				capALARM_CERT_ATTACHED.Text					=	objResourceMgr.GetString("cmbALARM_CERT_ATTACHED");
			//            capEXTERIOR_PAINT.Text						    =		objResourceMgr.GetString("cmbEXTERIOR_PAINT");
			//            capEXTERIOR_PAINT_YEAR.Text						=		objResourceMgr.GetString("txtEXTERIOR_PAINT_YEAR");

			//Prot devices
			//capPROTECTIVE_DEVICES.Text=	objResourceMgr.GetString("cmbPROTECTIVE_DEVICES");
			//capTEMPERATURE.Text	=objResourceMgr.GetString("cmbTEMPERATURE");
			//capSMOKE.Text=objResourceMgr.GetString("cmbSMOKE");
			//capBURGLAR.Text=objResourceMgr.GetString("cmbBURGLAR");
			//capFIRE_PLACES.Text=objResourceMgr.GetString("cmbFIRE_PLACES");
			//capNO_OF_WOOD_STOVES.Text=objResourceMgr.GetString("txtNO_OF_WOOD_STOVES");
			//capSWIMMING_POOL.Text=objResourceMgr.GetString("cmbSWIMMING_POOL");
			//capSWIMMING_POOL_TYPE.Text=objResourceMgr.GetString("txtSWIMMING_POOL_TYPE");
			
			// Added by mohit.
			capDWELLING_CONST_DATE.Text=objResourceMgr.GetString("txtDWELLING_CONST_DATE");
			lblNUM_LOC_ALARMS_APPLIES.Text=objResourceMgr.GetString("txtNUM_LOC_ALARMS_APPLIES");			
			capPRIMARY_HEAT_OTHER_DESC.Text=objResourceMgr.GetString("txtPRIMARY_HEAT_OTHER_DESC");			
			capSPRINKER.Text=objResourceMgr.GetString("cmbSPRINKER");			
			capIS_SUPERVISED.Text=objResourceMgr.GetString("cmbIS_SUPERVISED");
			CapLOCATED_IN_SUBDIVISION.Text			=	objResourceMgr.GetString("cmbLOCATED_IN_SUBDIVISION");
		}

		/// <summary>
		/// Populates the form fields with the values from database
		/// </summary>
		private void LoadData()
		{
			//Enable Disable Validators As per the Year Built
			if(Request.QueryString["CalledFrom"].ToString()=="HOME" || Request.QueryString["CalledFrom"].ToString()=="Home")
			{
				//In case Of Home
				string EffDate,EffYear;
				int DateDiff;
				int dyear=ClsHomeRating.GetYearBuiltOfDewellingForPolicy(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOL_ID.Value),int.Parse(hidPOL_VERSION_ID.Value),int.Parse(hidDWELLING_ID.Value));
				//If Year Year Of Built Is Pre 1950
				if(dyear<1950)
				{
					rfvWIRING_RENOVATION.Enabled=true;
					rfvPLUMBING_RENOVATION.Enabled =true;
					rfvHEATING_RENOVATION.Enabled=true;
					rfvROOFING_RENOVATION.Enabled=true;
					hiddyear.Value ="Pre";
				}
				else
				{
					rfvWIRING_RENOVATION.Enabled=false;
					rfvPLUMBING_RENOVATION.Enabled =false;
					rfvHEATING_RENOVATION.Enabled=false;
					rfvROOFING_RENOVATION.Enabled=false;
					hiddyear.Value="0";

				}
				// To get the policy effective date & compare with Year built so as to make Dwelling under construction mandatory or not.
				clsWatercraftInformation objInfo = new Cms.BusinessLayer.BlApplication.clsWatercraftInformation();	
				EffDate = objInfo.GetPolEffectiveDate(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOL_ID.Value),int.Parse(hidPOL_VERSION_ID.Value));			
				EffYear  = Convert.ToDateTime(EffDate).Year.ToString();   // Year component of POLICY_EFFECTIVE_DATE
				App_Effective_Date = EffDate;
				DateDiff = int.Parse(EffYear) - dyear;
				if (DateDiff <=2)
					{rfvIS_UNDER_CONSTRUCTION.Enabled=true;}
				else
					{
						rfvIS_UNDER_CONSTRUCTION.Enabled=false;
						spnIS_UNDER_CONSTRUCTION.Visible=false;
                    }
			}
			else
			{
				
				rfvWIRING_RENOVATION.Enabled=false;
				rfvPLUMBING_RENOVATION.Enabled =false;
				rfvHEATING_RENOVATION.Enabled=false;
				rfvROOFING_RENOVATION.Enabled=false;
				hiddyear.Value="0";
			}
			
			DataSet dsRating = ClsHomeRating.GetPolicyHomeRatingInfo(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOL_ID.Value),
				int.Parse(hidPOL_VERSION_ID.Value),
				int.Parse(hidDWELLING_ID.Value));
			
			//Rating info/////////////////////
			DataTable dtRating = dsRating.Tables[0];
			
			//----Changes By Mohit on 4/10/2005-----------------. 
			if ( dtRating == null || dtRating.Rows.Count == 0 )
			{
				hidDefaultTerr.Value=Convert.ToString(ClsHomeRating.GetPolicyDefaultTerritory(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOL_ID.Value),int.Parse(hidPOL_VERSION_ID.Value),int.Parse(hidDWELLING_ID.Value)));
				return;
			}


			this.hidOldData.Value = ClsCommon.GetXMLEncoded(dtRating);
	
			//txtHYDRANT_DIST.Text = Default.GetString(dtRating.Rows[0]["HYDRANT_DIST"]);
			//Check added to prevent existing records having earlier values from giving error
			if(Convert.ToInt32(dtRating.Rows[0]["HYDRANT_DIST"].ToString())>10000)
				cmbHYDRANT_DIST.SelectedValue = Default.GetString(dtRating.Rows[0]["HYDRANT_DIST"]);
			txtFIRE_STATION_DIST.Text = Default.GetString(dtRating.Rows[0]["FIRE_STATION_DIST"].ToString());
			//Check added to prevent existing records having earlier values from giving error
			//if(Convert.ToInt32(dtRating.Rows[0]["FIRE_STATION_DIST"].ToString())>10000)
			//	cmbFIRE_STATION_DIST.SelectedValue=Default.GetString(dtRating.Rows[0]["FIRE_STATION_DIST"].ToString());
			//txtPERSONAL_LIAB_TER_CODE.Text = Default.GetString(dtRating.Rows[0]["PERSONAL_LIAB_TER_CODE"]);
			ClsCommon.SelectValueinDDL(cmbPROT_CLASS,dtRating.Rows[0]["PROT_CLASS"]);
			
			txtDWELLING_CONST_DATE.Text = Default.GetString(dtRating.Rows[0]["DWELLING_CONST_DATE"]);
		
			
			ClsCommon.SelectValueinDDL(cmbIS_UNDER_CONSTRUCTION,dtRating.Rows[0]["IS_UNDER_CONSTRUCTION"]);
			//ClsCommon.SelectValueinDDL(cmbIS_AUTO_POL_WITH_CARRIER,dtRating.Rows[0]["IS_AUTO_POL_WITH_CARRIER"]); //Commented by Charles on 6-Nov-09 for Itrack 6722
			ClsCommon.SelectValueinDDL(cmbIS_SUPERVISED,dtRating.Rows[0]["IS_SUPERVISED"].ToString().Trim());
			//ClsCommon.SelectValueinDDL(cmbRATING_METHOD,dtRating.Rows[0]["RATING_METHOD"]);
		
			//end of rating info///////////////////////////////////

			//Construction info
			txtNO_OF_FAMILIES.Text = Default.GetString(dtRating.Rows[0]["NO_OF_FAMILIES"]);
			txtROOF_OTHER_DESC.Text = Default.GetString(dtRating.Rows[0]["ROOF_OTHER_DESC"]);
			txtPRIMARY_HEAT_OTHER_DESC.Text = Default.GetString(dtRating.Rows[0]["PRIMARY_HEAT_OTHER_DESC"]);
			txtSECONDARY_HEAT_OTHER_DESC.Text = Default.GetString(dtRating.Rows[0]["SECONDARY_HEAT_OTHER_DESC"]);
			//txtADD_COVERAGE_INFO.Text = Default.GetString(dtRating.Rows[0]["ADD_COVERAGE_INFO"]);
			 

			ClsCommon.SelectValueinDDL(cmbEXTERIOR_CONSTRUCTION,dtRating.Rows[0]["EXTERIOR_CONSTRUCTION"]);
			ClsCommon.SelectValueinDDL(cmbFOUNDATION,dtRating.Rows[0]["FOUNDATION"]);
			ClsCommon.SelectValueinDDL(cmbROOF_TYPE,dtRating.Rows[0]["ROOF_TYPE"]);
			//ClsCommon.SelectValueinDDL(cmbWIRING,dtRating.Rows[0]["WIRING"]);
			ClsCommon.SelectValueinDDL(cmbPRIMARY_HEAT_TYPE,dtRating.Rows[0]["PRIMARY_HEAT_TYPE"]);
			ClsCommon.SelectValueinDDL(cmbSECONDARY_HEAT_TYPE,dtRating.Rows[0]["SECONDARY_HEAT_TYPE"]);
			ClsCommon.SelectValueinDDL(cmbCONSTRUCTION_CODE,dtRating.Rows[0]["CONSTRUCTION_CODE"]);
			//ClsCommon.SelectValueinDDL(cmbIS_OUTSIDE_STAIR,dtRating.Rows[0]["IS_OUTSIDE_STAIR"]);
			//End of construction info

			//Square footage
			
			//txtTOT_SQR_FOOTAGE.Text = Default.GetString(dtRating.Rows[0]["TOT_SQR_FOOTAGE"]);
			//txtGARAGE_SQR_FOOTAGE.Text = Default.GetString(dtRating.Rows[0]["GARAGE_SQR_FOOTAGE"]);
			//txtBREEZE_SQR_FOOTAGE.Text = Default.GetString(dtRating.Rows[0]["BREEZE_SQR_FOOTAGE"]);
			//txtBASMT_SQR_FOOTAGE.Text = Default.GetString(dtRating.Rows[0]["BASMT_SQR_FOOTAGE"]);
			
			txtWIRING_UPDATE_YEAR.Text = Default.GetString(dtRating.Rows[0]["WIRING_UPDATE_YEAR"]);
			txtPLUMBING_UPDATE_YEAR.Text = Default.GetString(dtRating.Rows[0]["PLUMBING_UPDATE_YEAR"]);
			txtHEATING_UPDATE_YEAR.Text = Default.GetString(dtRating.Rows[0]["HEATING_UPDATE_YEAR"]);
			txtROOFING_UPDATE_YEAR.Text = Default.GetString(dtRating.Rows[0]["ROOFING_UPDATE_YEAR"]);
//			txtNO_OF_AMPS.Text = Default.GetString(dtRating.Rows[0]["NO_OF_AMPS"]);
			ClsCommon.SelectValueinDDL(cmbNO_OF_AMPS,dtRating.Rows[0]["NO_OF_AMPS"]);

			txtNUM_LOC_ALARMS_APPLIES.Text = Default.GetString(dtRating.Rows[0]["NUM_LOC_ALARMS_APPLIES"]);

			if ( dtRating.Rows[0]["NEED_OF_UNITS"] != System.DBNull.Value )
			{
				txtNEED_OF_UNITS.Text = dtRating.Rows[0]["NEED_OF_UNITS"].ToString();
			}
			

			ClsCommon.SelectValueinDDL(cmbWIRING_RENOVATION,dtRating.Rows[0]["WIRING_RENOVATION"]);
			ClsCommon.SelectValueinDDL(cmbPLUMBING_RENOVATION,dtRating.Rows[0]["PLUMBING_RENOVATION"]);
			ClsCommon.SelectValueinDDL(cmbHEATING_RENOVATION,dtRating.Rows[0]["HEATING_RENOVATION"]);
			ClsCommon.SelectValueinDDL(cmbROOFING_RENOVATION,dtRating.Rows[0]["ROOFING_RENOVATION"]);
			ClsCommon.SelectValueinDDL(cmbCIRCUIT_BREAKERS,dtRating.Rows[0]["CIRCUIT_BREAKERS"]);
			ClsCommon.SelectValueinDDL(cmbALARM_CERT_ATTACHED,dtRating.Rows[0]["ALARM_CERT_ATTACHED"]);
			ClsCommon.SelectValueinDDL(cmbSPRINKER,dtRating.Rows[0]["SPRINKER"]);
			ClsCommon.SelectValueinDDL(cmbLOCATED_IN_SUBDIVISION,dtRating.Rows[0]["LOCATED_IN_SUBDIVISION"]);

			if ( dtRating.Rows[0]["SUBURBAN_CLASS"] != System.DBNull.Value )
			{
				if ( dtRating.Rows[0]["SUBURBAN_CLASS"].ToString() == "Y" )
				{
					this.cbSUBURBAN_CLASS.Checked = true;
				}
			}
			//End of Footage/////////////////////////////
			
			//Protective devices
			if ( dtRating.Rows[0]["CENT_ST_BURG_FIRE"] != System.DBNull.Value )
			{
				if ( dtRating.Rows[0]["CENT_ST_BURG_FIRE"].ToString() == "Y" )
				{
					this.cblBurgFire.Items[0].Selected = true;
				}
			}
			
			if ( dtRating.Rows[0]["CENT_ST_FIRE"] != System.DBNull.Value )
			{
				if ( dtRating.Rows[0]["CENT_ST_FIRE"].ToString() == "Y" )
				{
					this.cblBurgFire.Items[1].Selected = true;
				}
			}

			if ( dtRating.Rows[0]["CENT_ST_BURG"] != System.DBNull.Value )
			{
				if ( dtRating.Rows[0]["CENT_ST_BURG"].ToString() == "Y" )
				{
					this.cblBurgFire.Items[2].Selected = true;
				}
			}
			//////////////////////
			///
			if ( dtRating.Rows[0]["DIR_FIRE_AND_POLICE"] != System.DBNull.Value )
			{
				if ( dtRating.Rows[0]["DIR_FIRE_AND_POLICE"].ToString() == "Y" )
				{
					this.cblDIRECT.Items[0].Selected = true;
				}
			}
			
			if ( dtRating.Rows[0]["DIR_FIRE"] != System.DBNull.Value )
			{
				if ( dtRating.Rows[0]["DIR_FIRE"].ToString() == "Y" )
				{
					this.cblDIRECT.Items[1].Selected = true;
				}
			}

			if ( dtRating.Rows[0]["DIR_POLICE"] != System.DBNull.Value )
			{
				if ( dtRating.Rows[0]["DIR_POLICE"].ToString() == "Y" )
				{
					this.cblDIRECT.Items[2].Selected = true;
				}
			}
			//////////////////
			if ( dtRating.Rows[0]["LOC_FIRE_GAS"] != System.DBNull.Value )
			{
				if ( dtRating.Rows[0]["LOC_FIRE_GAS"].ToString() == "Y" )
				{
					this.cblLOCAL.Items[0].Selected = true;
				}
			}

			if ( dtRating.Rows[0]["TWO_MORE_FIRE"] != System.DBNull.Value )
			{
				if ( dtRating.Rows[0]["TWO_MORE_FIRE"].ToString() == "Y" )
				{
					this.cblLOCAL.Items[1].Selected = true;
				}
			}

			//////////////////
			///
			/*
			if ( dtRating.Rows[0]["PROTECTIVE_DEVICES"] != System.DBNull.Value )
			{
				ClsCommon.SelectCheckBoxListText(this.cblBurgFire,dtRating.Rows[0]["PROTECTIVE_DEVICES"].ToString());
				ClsCommon.SelectCheckBoxListText(this.cblDIRECT,dtRating.Rows[0]["PROTECTIVE_DEVICES"].ToString());
				ClsCommon.SelectCheckBoxListText(this.cblLOCAL,dtRating.Rows[0]["PROTECTIVE_DEVICES"].ToString());
			}
			*/

			//			if ( dtRating.Rows[0]["SWIMMING_POOL"] != System.DBNull.Value )
			//			{
			//				ClsCommon.SelectValueinDDL(this.cmbSWIMMING_POOL,dtRating.Rows[0]["SWIMMING_POOL"].ToString());
			//			}
			//			
			//			if ( dtRating.Rows[0]["SWIMMING_POOL_TYPE"] != System.DBNull.Value )
			//			{
			//				ClsCommon.SelectValueinDDL(this.cmbSWIMMING_POOL_TYPE,dtRating.Rows[0]["SWIMMING_POOL_TYPE"].ToString());
			//			}
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
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private Cms.Model.Policy.Homeowners.ClsHomeRatingInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			Cms.Model.Policy.Homeowners.ClsHomeRatingInfo objHomeRatingInfo;

			objHomeRatingInfo = new Cms.Model.Policy.Homeowners.ClsHomeRatingInfo();
            
			//objHomeRatingInfo.HYDRANT_DIST              =	txtHYDRANT_DIST.Text==""? 0.0 : double.Parse(txtHYDRANT_DIST.Text);
			objHomeRatingInfo.HYDRANT_DIST=Convert.ToDouble(cmbHYDRANT_DIST.SelectedValue);
			objHomeRatingInfo.FIRE_STATION_DIST         =	txtFIRE_STATION_DIST.Text==""?0.0 :double.Parse(txtFIRE_STATION_DIST.Text);
			//objHomeRatingInfo.FIRE_STATION_DIST	= Convert.ToDouble(cmbFIRE_STATION_DIST.SelectedValue);
			objHomeRatingInfo.IS_UNDER_CONSTRUCTION     =	cmbIS_UNDER_CONSTRUCTION.SelectedValue;
			if(cmbIS_UNDER_CONSTRUCTION.SelectedIndex == 2)
			{
				objHomeRatingInfo.IS_SUPERVISED = cmbIS_SUPERVISED.SelectedValue.ToString().Trim(); 
			}
			else
			{
				objHomeRatingInfo.IS_SUPERVISED ="";
			}
			//Commented by Charles on 6-Nov-09 for Itrack 6722
			//When the state is Indiana and the page is called for Rental, save blank value for multi policy discount
			//if((strCalledFrom.ToUpper()=="REN" || strCalledFrom.ToUpper()=="RENTAL") && (hidStateID.Value=="14"))
			//{
				objHomeRatingInfo.IS_AUTO_POL_WITH_CARRIER = "";
			//}
			//else
			//	objHomeRatingInfo.IS_AUTO_POL_WITH_CARRIER  =	cmbIS_AUTO_POL_WITH_CARRIER.SelectedValue;

			//objHomeRatingInfo.PERSONAL_LIAB_TER_CODE    =	txtPERSONAL_LIAB_TER_CODE.Text;
			objHomeRatingInfo.PROT_CLASS                =	cmbPROT_CLASS.SelectedValue;
            
			//			if (cmbRATING_METHOD.SelectedItem !=null)
			//			{
			//				objHomeRatingInfo.RATING_METHOD             =	int.Parse (cmbRATING_METHOD.SelectedItem.Value) ;
			//			}

			objHomeRatingInfo.CUSTOMER_ID               = int.Parse(hidCUSTOMER_ID.Value);
			objHomeRatingInfo.POLICY_ID                   = int.Parse(hidPOL_ID.Value);
			objHomeRatingInfo.POLICY_VERSION_ID             = int.Parse(hidPOL_VERSION_ID.Value);
			objHomeRatingInfo.DWELLING_ID               = int.Parse(hidDWELLING_ID.Value); 
			
			if (txtNEED_OF_UNITS.Text.Trim() != "" )
			{
				objHomeRatingInfo.NEED_OF_UNITS = txtNEED_OF_UNITS.Text.Trim();
			}


			if((cmbWIRING_RENOVATION.SelectedValue.Trim()  != null) && (cmbWIRING_RENOVATION.SelectedValue.Trim()  !=""))            
				objHomeRatingInfo.WIRING_RENOVATION      =	int.Parse(cmbWIRING_RENOVATION.SelectedValue);
			
			if(cmbWIRING_RENOVATION.SelectedIndex == 0 || cmbWIRING_RENOVATION.SelectedIndex == 2)
			{
				//objHomeRatingInfo.WIRING_UPDATE_YEAR     =	0;
			}
			else
			{
				if ( txtWIRING_UPDATE_YEAR.Text.Trim() != "")
				{
					objHomeRatingInfo.WIRING_UPDATE_YEAR     =	int.Parse(txtWIRING_UPDATE_YEAR.Text);
				}

			}
			////
			
			if((cmbPLUMBING_RENOVATION.SelectedValue.Trim()  != null) && (cmbPLUMBING_RENOVATION.SelectedValue.Trim()  !=""))            
				objHomeRatingInfo.PLUMBING_RENOVATION      =	int.Parse(cmbPLUMBING_RENOVATION.SelectedValue);
			

			if ( cmbPLUMBING_RENOVATION.SelectedIndex == 0 ||   cmbPLUMBING_RENOVATION.SelectedIndex == 2)
			{
				//objHomeRatingInfo.PLUMBING_UPDATE_YEAR    =	0;
			}
			else
			{
				if ( txtPLUMBING_UPDATE_YEAR.Text.Trim() != "")
				{
					objHomeRatingInfo.PLUMBING_UPDATE_YEAR    =	int.Parse(txtPLUMBING_UPDATE_YEAR.Text);
				}
			}

			///
	
			if ( cmbHEATING_RENOVATION.SelectedValue.Trim() != null && cmbHEATING_RENOVATION.SelectedValue.Trim() !="" )            
			{
				objHomeRatingInfo.HEATING_RENOVATION     =	int.Parse(cmbHEATING_RENOVATION.SelectedValue);
			}

			if(cmbHEATING_RENOVATION.SelectedIndex == 0 || cmbHEATING_RENOVATION.SelectedIndex == 2)
			{
				//objHomeRatingInfo.HEATING_UPDATE_YEAR    =	0;
			}
			else
			{
				if ( txtHEATING_UPDATE_YEAR.Text.Trim() != "")
				{
					objHomeRatingInfo.HEATING_UPDATE_YEAR    =	int.Parse(txtHEATING_UPDATE_YEAR.Text);
				}
			}

			///
			if((cmbROOFING_RENOVATION.SelectedValue.Trim() != null) && (cmbROOFING_RENOVATION.SelectedValue.Trim() !=""))            
				objHomeRatingInfo.ROOFING_RENOVATION     =	int.Parse(cmbROOFING_RENOVATION.SelectedValue);

			if(cmbROOFING_RENOVATION.SelectedIndex == 0 || cmbROOFING_RENOVATION.SelectedIndex == 2)
			{
				//objHomeRatingInfo.ROOFING_UPDATE_YEAR    =	0;
			}
			else
			{
				if ( txtROOFING_UPDATE_YEAR.Text.Trim() != "")
				{
					objHomeRatingInfo.ROOFING_UPDATE_YEAR    =	int.Parse(txtROOFING_UPDATE_YEAR.Text);
				}
			}

//			if(txtNO_OF_AMPS.Text.Trim() !="")            
//				objHomeRatingInfo.NO_OF_AMPS             =	int.Parse(txtNO_OF_AMPS.Text);

			if((cmbNO_OF_AMPS.SelectedValue.Trim() != null) && (cmbNO_OF_AMPS.SelectedValue.Trim() !=""))            
				objHomeRatingInfo.NO_OF_AMPS      =	int.Parse(cmbNO_OF_AMPS.SelectedValue);

        
			if((cmbCIRCUIT_BREAKERS.SelectedValue.Trim() != null) && (cmbCIRCUIT_BREAKERS.SelectedValue.Trim() !=""))            
				objHomeRatingInfo.CIRCUIT_BREAKERS       =	cmbCIRCUIT_BREAKERS.SelectedValue;
			
			//Construction
			//			if(txtNO_OF_APARTMENTS.Text.Trim() != "")
			//				objHomeRatingInfo.NO_OF_APARTMENTS		=	Convert.ToInt32(txtNO_OF_APARTMENTS.Text);
			if(txtNO_OF_FAMILIES.Text.Trim() != "")
				objHomeRatingInfo.NO_OF_FAMILIES			=	Convert.ToInt32(txtNO_OF_FAMILIES.Text);
			if(cmbFOUNDATION.SelectedValue.Trim() != "")
				if((cmbCONSTRUCTION_CODE.SelectedValue.Trim() != null) && (cmbCONSTRUCTION_CODE.SelectedValue.Trim() !=""))            
					objHomeRatingInfo.CONSTRUCTION_CODE				 = Convert.ToInt32(cmbCONSTRUCTION_CODE.SelectedValue);
				else 
					objHomeRatingInfo.CONSTRUCTION_CODE = 0;
			objHomeRatingInfo.EXTERIOR_CONSTRUCTION	=	Convert.ToInt32(cmbEXTERIOR_CONSTRUCTION.SelectedValue);
			
			if (cmbEXTERIOR_CONSTRUCTION.SelectedItem.Text.ToLower().StartsWith("other") == false) 
			{
				objHomeRatingInfo.EXTERIOR_OTHER_DESC = "";
			}
			
			if(cmbFOUNDATION.SelectedValue.Trim() != "")
				objHomeRatingInfo.FOUNDATION				=	Convert.ToInt32(cmbFOUNDATION.SelectedValue);
			//objHomeRatingInfo.FOUNDATION_OTHER_DESC	=	txtFOUNDATION_OTHER_DESC.Text;
			if(cmbROOF_TYPE.SelectedValue!=null && cmbROOF_TYPE.SelectedValue!="")
				objHomeRatingInfo.ROOF_TYPE				=	Convert.ToInt32(cmbROOF_TYPE.SelectedValue);
			
			if (cmbROOF_TYPE.SelectedItem.Text.ToLower().StartsWith("other")) 
			{
				objHomeRatingInfo.ROOF_OTHER_DESC			=	txtROOF_OTHER_DESC.Text;
			}
			else
			{
				objHomeRatingInfo.ROOF_OTHER_DESC			=	"";
			}


			//----- Added by mohit..
			if (txtDWELLING_CONST_DATE.Text.Trim() != "")
				objHomeRatingInfo.DWELLING_CONST_DATE = ConvertToDate(txtDWELLING_CONST_DATE.Text);
			//----- End--------------
			
			//			if ( cmbWIRING.SelectedItem.Value != "" )
			//			{
			//				objHomeRatingInfo.WIRING					=	Convert.ToInt32(cmbWIRING.SelectedItem.Value);
			//			}


			//			if (txtWIRING_LAST_INSPECTED.Text.Trim() != "")
			//				objHomeRatingInfo.WIRING_LAST_INSPECTED=ConvertToDate(txtWIRING_LAST_INSPECTED.Text);

			//objHomeRatingInfo.WIRING_LAST_INSPECTED	=	ConvertToDate(txtWIRING_LAST_INSPECTED.Text);
			objHomeRatingInfo.PRIMARY_HEAT_TYPE		=	Convert.ToInt32(cmbPRIMARY_HEAT_TYPE.SelectedValue);

			//			if((cmbIS_OUTSIDE_STAIR.SelectedValue.Trim() != null) && (cmbIS_OUTSIDE_STAIR.SelectedValue.Trim() !=""))            
			//				objHomeRatingInfo.IS_OUTSIDE_STAIR     =	cmbIS_OUTSIDE_STAIR.SelectedValue;
			//			
			if (cmbPRIMARY_HEAT_TYPE.SelectedItem.Text.ToLower().StartsWith("other")) 
			{
				objHomeRatingInfo.PRIMARY_HEAT_OTHER_DESC	=	txtPRIMARY_HEAT_OTHER_DESC.Text.Trim();
			}
			else
			{
				objHomeRatingInfo.PRIMARY_HEAT_OTHER_DESC = "";
			}

			if (cmbSECONDARY_HEAT_TYPE.SelectedItem.Text.ToLower().StartsWith("other")) 
			{
				objHomeRatingInfo.SECONDARY_HEAT_OTHER_DESC	=	txtSECONDARY_HEAT_OTHER_DESC.Text.Trim();
			}
			else
			{
				objHomeRatingInfo.SECONDARY_HEAT_OTHER_DESC = "";
			}
			
			objHomeRatingInfo.SECONDARY_HEAT_TYPE		=	Convert.ToInt32(cmbSECONDARY_HEAT_TYPE.SelectedValue);
			if((cmbALARM_CERT_ATTACHED.SelectedValue.Trim() != null) && (cmbALARM_CERT_ATTACHED.SelectedValue.Trim() !=""))            
				objHomeRatingInfo.ALARM_CERT_ATTACHED       =	cmbALARM_CERT_ATTACHED.SelectedValue;

			//if(txtMONTH_OCC_EACH_YEAR.Text.Trim() != "")
			//	objHomeRatingInfo.MONTH_OCC_EACH_YEAR	=	Convert.ToInt32(txtMONTH_OCC_EACH_YEAR.Text);
			//objHomeRatingInfo.ADD_COVERAGE_INFO		=	txtADD_COVERAGE_INFO.Text;
			//objHomeRatingInfo.SWIMMING_POOL=cmbSWIMMING_POOL.SelectedValue;

			//			if(cmbSWIMMING_POOL.SelectedIndex == 0 || cmbSWIMMING_POOL.SelectedIndex == 2)
			//			{
			//				objHomeRatingInfo.SWIMMING_POOL_TYPE    =	System.DBNull.Value.ToString();
			//			}
			//			else
			//			{
			//				if ( cmbSWIMMING_POOL_TYPE.SelectedValue.Trim() !="")
			//				{
			//					objHomeRatingInfo.SWIMMING_POOL_TYPE    =	cmbSWIMMING_POOL_TYPE.SelectedValue;
			//				}
			//			}

			//Prot devices
			//Save values for protective devices section only for homeowner LOB, let them take default values
			//for rental lob
			if(strCalledFrom.ToUpper()=="HOME" || strCalledFrom.ToUpper()=="HOME")
			{
				if ( this.cblBurgFire.Items[0].Selected )
				{
					objHomeRatingInfo.CENT_ST_BURG_FIRE = "Y";
				}
				else
				{
					objHomeRatingInfo.CENT_ST_BURG_FIRE = "N";
				}
			
				if ( this.cblBurgFire.Items[1].Selected )
				{
					objHomeRatingInfo.CENT_ST_FIRE = "Y";
				}
				else
				{
					objHomeRatingInfo.CENT_ST_FIRE = "N";
				}

				if ( this.cblBurgFire.Items[2].Selected )
				{
					objHomeRatingInfo.CENT_ST_BURG = "Y";
				}
				else
				{
					objHomeRatingInfo.CENT_ST_BURG = "N";
				}
				//////////////
				///

				if ( this.cblDIRECT.Items[0].Selected )
				{
					objHomeRatingInfo.DIR_FIRE_AND_POLICE= "Y";
				}
				else
				{
					objHomeRatingInfo.DIR_FIRE_AND_POLICE = "N";
				}

				if ( this.cblDIRECT.Items[1].Selected )
				{
					objHomeRatingInfo.DIR_FIRE = "Y";
				}
				else
				{
					objHomeRatingInfo.DIR_FIRE = "N";
				}

				if ( this.cblDIRECT.Items[2].Selected )
				{
					objHomeRatingInfo.DIR_POLICE = "Y";
				}
				else
				{
					objHomeRatingInfo.DIR_POLICE = "N";
				}
				/////////////////////////////
				///

				if ( this.cblLOCAL.Items[0].Selected )
				{
					objHomeRatingInfo.LOC_FIRE_GAS = "Y";
				}
				else
				{
					objHomeRatingInfo.LOC_FIRE_GAS = "N";
				}

				if ( this.cblLOCAL.Items[1].Selected )
				{
					objHomeRatingInfo.TWO_MORE_FIRE = "Y";
				}
				else
				{
					objHomeRatingInfo.TWO_MORE_FIRE = "N";
				}
				if(this.cbSUBURBAN_CLASS.Checked)
				{
					objHomeRatingInfo.SUBURBAN_CLASS="Y";
				}
				else
				{
					objHomeRatingInfo.SUBURBAN_CLASS="N";
				}

				if((cmbLOCATED_IN_SUBDIVISION.SelectedValue.Trim() != null) && (cmbLOCATED_IN_SUBDIVISION.SelectedValue.Trim() !="") && this.cbSUBURBAN_CLASS.Checked)            
					objHomeRatingInfo.LOCATED_IN_SUBDIVISION       =	cmbLOCATED_IN_SUBDIVISION.SelectedValue;			
				//objHomeRatingInfo.NUM_LOC_ALARMS_APPLIES=int.Parse(null);
			}
			else
			{
				objHomeRatingInfo.CENT_ST_BURG_FIRE = "";
				objHomeRatingInfo.CENT_ST_FIRE		= "";
				objHomeRatingInfo.CENT_ST_BURG		= "";
				objHomeRatingInfo.DIR_FIRE_AND_POLICE= "";
				objHomeRatingInfo.DIR_FIRE			= "";
				objHomeRatingInfo.DIR_POLICE		= "";
				objHomeRatingInfo.LOC_FIRE_GAS		= "";
				objHomeRatingInfo.TWO_MORE_FIRE		= "";
				if(txtNUM_LOC_ALARMS_APPLIES.Text.Trim()!="")
					objHomeRatingInfo.NUM_LOC_ALARMS_APPLIES = int.Parse(txtNUM_LOC_ALARMS_APPLIES.Text.Trim());				
			}

			/*
			foreach(ListItem li in this.cblBurgFire.Items)
			{
				if ( li.Selected)
				{
					if ( objHomeRatingInfo.PROTECTIVE_DEVICES == null || objHomeRatingInfo.PROTECTIVE_DEVICES == "" )
					{
						objHomeRatingInfo.PROTECTIVE_DEVICES = objHomeRatingInfo.PROTECTIVE_DEVICES + li.Text;
					}
					else
					{
						objHomeRatingInfo.PROTECTIVE_DEVICES = objHomeRatingInfo.PROTECTIVE_DEVICES + "," + li.Text;
					}
					
				}

			}
			
			foreach(ListItem li in this.cblDIRECT.Items)
			{
				if ( li.Selected)
				{
					if ( objHomeRatingInfo.PROTECTIVE_DEVICES == null || objHomeRatingInfo.PROTECTIVE_DEVICES == "" )
					{
						objHomeRatingInfo.PROTECTIVE_DEVICES = objHomeRatingInfo.PROTECTIVE_DEVICES + li.Text;
					}
					else
					{
						objHomeRatingInfo.PROTECTIVE_DEVICES = objHomeRatingInfo.PROTECTIVE_DEVICES + "," + li.Text;
					}
				}		
			}

			foreach(ListItem li in this.cblLOCAL.Items)
			{

				if ( li.Selected)
				{
					if ( objHomeRatingInfo.PROTECTIVE_DEVICES == null || objHomeRatingInfo.PROTECTIVE_DEVICES == "" )
					{
						objHomeRatingInfo.PROTECTIVE_DEVICES = objHomeRatingInfo.PROTECTIVE_DEVICES + li.Text;
					}
					else
					{
						objHomeRatingInfo.PROTECTIVE_DEVICES = objHomeRatingInfo.PROTECTIVE_DEVICES + "," + li.Text;
					}
					
				}

						}
			*/
			if(cmbSPRINKER.SelectedItem!=null && cmbSPRINKER.SelectedItem.Text!="")
				objHomeRatingInfo.SPRINKER = int.Parse(cmbSPRINKER.SelectedItem.Value);
			//These  assignments are common to all pages.
			strFormSaved	                            =	hidFormSaved.Value;
			if(hidOldData.Value=="0")
				strRowId		                            =	hidRowId.Value;
			else
				strRowId		                            =	"";

            

			//oldXML		                                =   hidOldData.Value;
			//Returning the model object

			return objHomeRatingInfo;
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;	//For retreiving the return value of business class save function
				ClsHomeRating objHomeRating = new  ClsHomeRating();

				//Retreiving the form values into model class object
				Cms.Model.Policy.Homeowners.ClsHomeRatingInfo objHomeRatingInfo = GetFormValue();

				if(this.hidOldData.Value == "") //save case
				{
					objHomeRatingInfo.CREATED_BY = int.Parse(GetUserId());
					objHomeRatingInfo.CREATED_DATETIME = DateTime.Now;

					//Calling the add method of business layer class
					//intRetVal = objHomeRating.Add(objHomeRatingInfo);
					intRetVal = objHomeRating.AddPolicyHomeRatingInfo(objHomeRatingInfo,strCalledFrom);

					if(intRetVal>0)
					{	
						if(Request.QueryString["CALLEDFROM"].ToUpper()=="HOME" )
						{
							if(cmbSECONDARY_HEAT_TYPE.SelectedValue=="6224" || cmbSECONDARY_HEAT_TYPE.SelectedValue=="6223")
							{
								lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"556");
							}
							else
							{
								lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
							}
						}
						else
							lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						base.OpenEndorsementDetails();
						SetWorkFlow();
                        
					}
					else if(intRetVal == -1)
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"18");
						hidFormSaved.Value			=		"2";
					}
					else
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"20");
						hidFormSaved.Value			=	"2";
					}
					lblMessage.Visible = true;
				} // end save case
				else //UPDATE CASE
				{

					//Creating the Model object for holding the Old data
					Cms.Model.Policy.Homeowners.ClsHomeRatingInfo objOldHomeRatingInfo;
					objOldHomeRatingInfo = new Cms.Model.Policy.Homeowners.ClsHomeRatingInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldHomeRatingInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page
                    
					objHomeRatingInfo.MODIFIED_BY = int.Parse(GetUserId());
					objHomeRatingInfo.LAST_UPDATED_DATETIME = DateTime.Now;
                    

					//Updating the record using business layer class object
					//intRetVal	= objHomeRating.Update(objOldHomeRatingInfo,objHomeRatingInfo);
					intRetVal	= objHomeRating.UpdatePolicyHomeRatingInfo(objOldHomeRatingInfo,objHomeRatingInfo,strCalledFrom);
					
					if( intRetVal > 0 )			// update successfully performed
					{
						if(Request.QueryString["CALLEDFROM"].ToUpper()=="HOME" )
						{
							if(cmbSECONDARY_HEAT_TYPE.SelectedValue=="6224" || cmbSECONDARY_HEAT_TYPE.SelectedValue=="6223")
							{
								lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"558");
							}
							else
							{
								lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"31");
							}
						}
						else
							lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						base.OpenEndorsementDetails();
						SetWorkFlow();
					}
					else if(intRetVal == -1)	// Duplicate code exist, update failed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"18");
						hidFormSaved.Value		=	"1";
					}
					else 
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
						hidFormSaved.Value		=	"1";
					}
					lblMessage.Visible = true;
				}
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";

				return;
             
			}
			
			LoadData();
		}

		[Ajax.AjaxMethod()]
		public string AjaxGetProtectionClass(string protectionClass,int milesToDwell,string feetToHydrant,string lobCode)
		{
			CmsWeb.webservices.ClsWebServiceCommon obj =  new CmsWeb.webservices.ClsWebServiceCommon();
			string result = "";
			DataSet Ds = null;
			Ds = obj.GetProtectionClass(protectionClass,milesToDwell,feetToHydrant,lobCode);
			result = Ds.Tables[0].Rows[0][0].ToString();
			return result;
		}


	}
}
