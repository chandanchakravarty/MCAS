/******************************************************************************************
	<Author					: -> Ravindra Kumar Gupta
	<Start Date				: -> 03-22-2006
	<End Date				: ->
	<Description			: -> Page to add umbrella Rating Info(Policy)
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
	/// Summary description for PolicyRatingInfo.
	/// </summary>
	public class PolicyRatingInfo : Cms.Policies.policiesbase 
	{
		
		#region Page Controls Declaration
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capHYDRANT_DIST;
		protected System.Web.UI.WebControls.DropDownList cmbHYDRANT_DIST;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvHYDRANT_DIST;
		protected System.Web.UI.WebControls.Label capFIRE_STATION_DIST;
		protected System.Web.UI.WebControls.TextBox txtFIRE_STATION_DIST;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvFIRE_STATION_DIST;
		protected System.Web.UI.WebControls.RegularExpressionValidator revFIRE_STATION_DIST;
		protected System.Web.UI.WebControls.Label capIS_UNDER_CONSTRUCTION;
		protected System.Web.UI.WebControls.DropDownList cmbIS_UNDER_CONSTRUCTION;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_UNDER_CONSTRUCTION;
		protected System.Web.UI.WebControls.Label capDWELLING_CONST_DATE;
		protected System.Web.UI.WebControls.TextBox txtDWELLING_CONST_DATE;
		protected System.Web.UI.WebControls.HyperLink hlkDWELLING_CONST_DATE;
		protected System.Web.UI.WebControls.Label lblDWELLING_CONST_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDWELLING_CONST_DATE;
		protected System.Web.UI.WebControls.CustomValidator csvDWELLING_CONST_DATE;
		protected System.Web.UI.WebControls.Label capPROT_CLASS;
		protected System.Web.UI.WebControls.TextBox txtPROT_CLASS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPROT_CLASS;
		protected System.Web.UI.WebControls.RangeValidator rngPROT_CLASS;
		protected System.Web.UI.WebControls.Label capIS_AUTO_POL_WITH_CARRIER;
		protected System.Web.UI.WebControls.DropDownList cmbIS_AUTO_POL_WITH_CARRIER;
		protected System.Web.UI.WebControls.Label capWIRING_RENOVATION;
		protected System.Web.UI.WebControls.DropDownList cmbWIRING_RENOVATION;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvWIRING_RENOVATION;
		protected System.Web.UI.WebControls.Label capWIRING_UPDATE_YEAR;
		protected System.Web.UI.WebControls.TextBox txtWIRING_UPDATE_YEAR;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvWIRING_UPDATE_YEAR;
		protected System.Web.UI.WebControls.CustomValidator csvpWIRING_UPDATE_YEAR;
		protected System.Web.UI.WebControls.Label capPLUMBING_RENOVATION;
		protected System.Web.UI.WebControls.DropDownList cmbPLUMBING_RENOVATION;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPLUMBING_RENOVATION;
		protected System.Web.UI.WebControls.Label capPLUMBING_UPDATE_YEAR;
		protected System.Web.UI.WebControls.TextBox txtPLUMBING_UPDATE_YEAR;
		protected System.Web.UI.WebControls.CustomValidator csvPLUMBING_UPDATE_YEAR;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPLUMBING_UPDATE_YEAR;
		protected System.Web.UI.WebControls.Label capHEATING_RENOVATION;
		protected System.Web.UI.WebControls.DropDownList cmbHEATING_RENOVATION;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvHEATING_RENOVATION;
		protected System.Web.UI.WebControls.Label capHEATING_UPDATE_YEAR;
		protected System.Web.UI.WebControls.TextBox txtHEATING_UPDATE_YEAR;
		protected System.Web.UI.WebControls.CustomValidator csvHEATING_UPDATE_YEAR;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvHEATING_UPDATE_YEAR;
		protected System.Web.UI.WebControls.Label capROOFING_RENOVATION;
		protected System.Web.UI.WebControls.DropDownList cmbROOFING_RENOVATION;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvROOFING_RENOVATION;
		protected System.Web.UI.WebControls.Label capROOFING_UPDATE_YEAR;
		protected System.Web.UI.WebControls.TextBox txtROOFING_UPDATE_YEAR;
		protected System.Web.UI.WebControls.CustomValidator csvROOFING_UPDATE_YEAR;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvROOFING_UPDATE_YEAR;
		protected System.Web.UI.WebControls.Label capNO_OF_AMPS;
		protected System.Web.UI.WebControls.TextBox txtNO_OF_AMPS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNO_OF_AMPS;
		protected System.Web.UI.WebControls.RangeValidator rngNO_OF_AMPS;
		protected System.Web.UI.WebControls.Label capCIRCUIT_BREAKERS;
		protected System.Web.UI.WebControls.DropDownList cmbCIRCUIT_BREAKERS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCIRCUIT_BREAKERS;
		protected System.Web.UI.WebControls.Label capNO_OF_FAMILIES;
		protected System.Web.UI.WebControls.TextBox txtNO_OF_FAMILIES;
		protected System.Web.UI.WebControls.RegularExpressionValidator revNO_OF_FAMILIES;
		protected System.Web.UI.WebControls.RangeValidator rngNO_OF_FAMILIES;
		protected System.Web.UI.WebControls.Label capCONSTRUCTION_CODE;
		protected System.Web.UI.WebControls.DropDownList cmbCONSTRUCTION_CODE;
		protected System.Web.UI.WebControls.Label capEXTERIOR_CONSTRUCTION;
		protected System.Web.UI.WebControls.DropDownList cmbEXTERIOR_CONSTRUCTION;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEXTERIOR_CONSTRUCTION;
		protected System.Web.UI.WebControls.Label capEXTERIOR_OTHER_DESC;
		protected System.Web.UI.WebControls.TextBox txtEXTERIOR_OTHER_DESC;
		protected System.Web.UI.WebControls.Label lblDESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEXTERIOR_OTHER_DESC;
		protected System.Web.UI.WebControls.Label capFOUNDATION;
		protected System.Web.UI.WebControls.DropDownList cmbFOUNDATION;
		protected System.Web.UI.WebControls.Label capROOF_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbROOF_TYPE;
		protected System.Web.UI.WebControls.Label capROOF_TYPE_OTHER_DESC;
		protected System.Web.UI.WebControls.TextBox txtROOF_OTHER_DESC;
		protected System.Web.UI.WebControls.Label lblROOF_OTHER_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvROOF_OTHER_DESC;
		protected System.Web.UI.WebControls.Label capPRIMARY_HEAT_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbPRIMARY_HEAT_TYPE;
		protected System.Web.UI.WebControls.Label capROOF_OTHER_DESC;
		protected System.Web.UI.WebControls.TextBox txtPRIMARY_HEAT_OTHER_DESC;
		protected System.Web.UI.WebControls.Label lblPR_HEAT_OTHER_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPRIMARY_HEAT_OTHER_DESC;
		protected System.Web.UI.WebControls.Label capSECONDARY_HEAT_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbSECONDARY_HEAT_TYPE;
		protected System.Web.UI.WebControls.Label capSECONDARY_HEAT_OTHER_DESC;
		protected System.Web.UI.WebControls.TextBox txtSECONDARY_HEAT_OTHER_DESC;
		protected System.Web.UI.WebControls.Label lblSC_HEAT_OTHER_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSECONDARY_HEAT_OTHER_DESC;
		protected System.Web.UI.WebControls.CheckBoxList cblBurgFire;
		protected System.Web.UI.WebControls.CheckBoxList cblDIRECT;
		protected System.Web.UI.WebControls.CheckBoxList cblLOCAL;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlTableRow trProtectiveDevices;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDWELLING_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidRowId;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDefaultTerr;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hiddyear;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidStateID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_VERSION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOCATION_ID;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
		#endregion 

		#region Page Variables Declaration

		protected string curDate="";
		protected int intDwellingID;
		
		#endregion 
	
		#region Page Load Event Handler
		private void Page_Load(object sender, System.EventArgs e)
		{
			hlkDWELLING_CONST_DATE.Attributes.Add("OnClick","fPopCalendar(document.POL_UMB_RATING_INFO.txtDWELLING_CONST_DATE,document.POL_UMB_RATING_INFO.txtDWELLING_CONST_DATE)");
			cmbIS_UNDER_CONSTRUCTION.Attributes.Add("onChange","javascript:DisplayDate();");

			base.ScreenId ="274_4";
			btnReset.Attributes.Add("onclick","javascript:return ResetForm();");
			lblMessage.Visible = false;
			SetErrorMessages();
			
			btnReset.CmsButtonClass			=	CmsButtonType.Write ;
			btnReset.PermissionString		=	gstrSecurityXML;

			btnSave.CmsButtonClass	        =	CmsButtonType.Write ;
			btnSave.PermissionString		=	gstrSecurityXML;
			
			curDate=DateTime.Now.ToString() ;  

			if(!Page.IsPostBack)
			{   
				LoadCombos();
				GetQueryString();
				int intCustomerID = Convert.ToInt32(this.hidCUSTOMER_ID .Value);
				int intPolicyID = Convert.ToInt32(this.hidPOLICY_ID .Value);
				int intPolicyVersionID = Convert.ToInt32(this.hidPOLICY_VERSION_ID.Value);
				int intLocationID=Convert.ToInt32 (Request.Params["LOCATION_ID1"]);
				intDwellingID=Convert.ToInt32(ClsUmbrellaDwelling.GetPolicyDwellingID(intCustomerID,intPolicyID ,intPolicyVersionID ,intLocationID));
				hidDWELLING_ID.Value =intDwellingID.ToString ();		
				
				if(intDwellingID == -1)
				{
					lblMessage.Text ="No Dwelling exists for this Location. Please add a Dwelling First";
					lblMessage.Visible =true;
					btnSave.Enabled =false;
				}
				else
				{				
					int intRecordExists =ClsUmbrellaRating.IsRecordExistsPolicy(intCustomerID,intPolicyID,intPolicyVersionID,intDwellingID);
					if(intRecordExists > 0 )
					{
						LoadData();
					}
				}

				SetCaptions();
				SetWorkFlow();

			}
		}
		#endregion


		#region GetQueryString Function 

		private void GetQueryString()
		{
			hidCUSTOMER_ID.Value	 =  GetCustomerID();
			hidPOLICY_ID .Value = GetPolicyID ();
			hidPOLICY_VERSION_ID.Value = GetPolicyVersionID ();
			hidLOCATION_ID.Value = Request.Params["LOCATION_ID"];
		}
		#endregion
		
		#region SetCaptions Function
		private void SetCaptions()
		{
			 
			System.Resources.ResourceManager objResourceMgr  = new System.Resources.ResourceManager("Cms.Policies.Aspx.Umbrella.PolicyRatingInfo" ,System.Reflection.Assembly.GetExecutingAssembly());

			capHYDRANT_DIST.Text					=		objResourceMgr.GetString("cmbHYDRANT_DIST");
			capFIRE_STATION_DIST.Text				=		objResourceMgr.GetString("txtFIRE_STATION_DIST");
			capPROT_CLASS.Text						=		objResourceMgr.GetString("txtPROT_CLASS");
			//Construction
			capNO_OF_FAMILIES.Text					=		objResourceMgr.GetString("txtNO_OF_FAMILIES");
			capEXTERIOR_CONSTRUCTION.Text			=		objResourceMgr.GetString("cmbEXTERIOR_CONSTRUCTION");
			capEXTERIOR_OTHER_DESC.Text				=		objResourceMgr.GetString("txtEXTERIOR_OTHER_DESC");
			capFOUNDATION.Text						=		objResourceMgr.GetString("cmbFOUNDATION");
			capROOF_TYPE.Text						=		objResourceMgr.GetString("cmbROOF_TYPE");
			capROOF_TYPE_OTHER_DESC.Text			=		objResourceMgr.GetString("txtROOF_OTHER_DESC");
			capPRIMARY_HEAT_TYPE.Text				=		objResourceMgr.GetString("cmbPRIMARY_HEAT_TYPE");
			capSECONDARY_HEAT_TYPE.Text				=		objResourceMgr.GetString("cmbSECONDARY_HEAT_TYPE");
			capCONSTRUCTION_CODE.Text			    =      objResourceMgr.GetString("cmbCONSTRUCTION_CODE");
			
			//Square footage
			capWIRING_RENOVATION.Text				=		objResourceMgr.GetString("cmbWIRING_RENOVATION");
			capWIRING_UPDATE_YEAR.Text				=		objResourceMgr.GetString("txtWIRING_UPDATE_YEAR");
			capPLUMBING_RENOVATION.Text				=		objResourceMgr.GetString("cmbPLUMBING_RENOVATION");
			capPLUMBING_UPDATE_YEAR.Text			=		objResourceMgr.GetString("txtPLUMBING_UPDATE_YEAR");
			capHEATING_RENOVATION.Text				=		objResourceMgr.GetString("cmbHEATING_RENOVATION");
			capHEATING_UPDATE_YEAR.Text				=		objResourceMgr.GetString("txtHEATING_UPDATE_YEAR");
			capROOFING_RENOVATION.Text				=		objResourceMgr.GetString("cmbROOFING_RENOVATION");
			capROOFING_UPDATE_YEAR.Text				=		objResourceMgr.GetString("txtROOFING_UPDATE_YEAR");
			capNO_OF_AMPS.Text					    =		objResourceMgr.GetString("txtNO_OF_AMPS");
			capCIRCUIT_BREAKERS.Text				=		objResourceMgr.GetString("cmbCIRCUIT_BREAKERS");
			
			capDWELLING_CONST_DATE.Text=objResourceMgr.GetString("txtDWELLING_CONST_DATE");
			//lblNUM_LOC_ALARMS_APPLIES.Text=objResourceMgr.GetString("txtNUM_LOC_ALARMS_APPLIES");
		}
		#endregion

		#region SetWorkFlow Function
		private void SetWorkFlow()
		{
			if(base.ScreenId == "274_4")
			{
				myWorkFlow.IsTop	=	false;
				myWorkFlow.ScreenID	=	base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID",GetCustomerID());
				myWorkFlow.AddKeyValue("POLICY_ID",GetPolicyID ());
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",GetPolicyVersionID());
				myWorkFlow.WorkflowModule ="POL";
				if(hidDWELLING_ID.Value!="0" && hidDWELLING_ID.Value.Trim() != "")
				{
					myWorkFlow.AddKeyValue("DWELLING_ID",hidDWELLING_ID.Value);					
					myWorkFlow.AddKeyValue("REMARKS","isnull(REMARKS,0)");
				}
				myWorkFlow.GetScreenStatus();
				myWorkFlow.SetScreenStatus();
			}
			else
			{
				myWorkFlow.Display	=	false;
			}
			
		}
		#endregion

		#region LoadCombos Function 
		private void LoadCombos()
		{
			
			DataSet dsLookup =Cms.BusinessLayer.BlApplication.ClsUmbrellaRating.GetUmbrellaRatingLookup();

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
			
			//Yes no for circuit breakers
			cmbCIRCUIT_BREAKERS.DataSource = dsLookup.Tables[2];
			cmbCIRCUIT_BREAKERS.DataTextField = "LOOKUP_VALUE_DESC";
			cmbCIRCUIT_BREAKERS.DataValueField = "LOOKUP_UNIQUE_ID";
			cmbCIRCUIT_BREAKERS.DataBind();
			cmbCIRCUIT_BREAKERS.Items.Insert(0,"");
				
			//Foundation code
			cmbFOUNDATION.DataSource = dsLookup.Tables[3];
			cmbFOUNDATION.DataTextField = "LOOKUP_VALUE_DESC";
			cmbFOUNDATION.DataValueField = "LOOKUP_UNIQUE_ID";
			cmbFOUNDATION.DataBind();

			//Exterior construction
			cmbEXTERIOR_CONSTRUCTION.DataSource = dsLookup.Tables[4];
			cmbEXTERIOR_CONSTRUCTION.DataTextField = "LOOKUP_VALUE_DESC";
			cmbEXTERIOR_CONSTRUCTION.DataValueField = "LOOKUP_UNIQUE_ID";
			cmbEXTERIOR_CONSTRUCTION.DataBind();
			cmbEXTERIOR_CONSTRUCTION.Items.Insert(0,"");
			
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
			cmbROOF_TYPE.Items.Insert(0,"");

			//Construction Code
			
			cmbCONSTRUCTION_CODE.DataSource = dsLookup.Tables[12];
			cmbCONSTRUCTION_CODE.DataTextField = "LOOKUP_VALUE_DESC";
			cmbCONSTRUCTION_CODE.DataValueField = "LOOKUP_UNIQUE_ID";
			cmbCONSTRUCTION_CODE.DataBind();
			cmbCONSTRUCTION_CODE.Items.Insert(0,"");
			
			cmbHYDRANT_DIST.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("RIFS");
			cmbHYDRANT_DIST.DataTextField = "LookupDesc";
			cmbHYDRANT_DIST.DataValueField = "LookupID";
			cmbHYDRANT_DIST.DataBind();
			
			ListItem liBoth = new ListItem();
			
			liBoth.Text = "Central Stations Burglary and Fire Alarm System";
			liBoth.Attributes.Add("onClick","Check(1,this.checked)");

			this.cblBurgFire.Items.Add(liBoth);
			this.cblBurgFire.Items.Add(new ListItem("Central Station Fire",""));
			this.cblBurgFire.Items.Add(new ListItem("Central Station Burglary",""));
			
			ListItem liDirect = new ListItem();
			liDirect.Text = "Direct to Fire and Police";

			liDirect.Attributes.Add("onClick","Check(2,this.checked)");

			this.cblDIRECT.Items.Add(liDirect);
			this.cblDIRECT.Items.Add(new ListItem("Direct to Fire",""));
			this.cblDIRECT.Items.Add(new ListItem("Direct to Police",""));

			this.cblLOCAL.Items.Add(new ListItem("Local Fire or local Gas Alarm",""));
			this.cblLOCAL.Items.Add(new ListItem("Two or more Local Fire Alarms",""));

			this.cblBurgFire.Attributes.Add("onclick", 
				"disableListItems('cblBurgFire', '0', '3')");
			
			this.cblDIRECT.Attributes.Add("onclick", 
				"disableListItems('cblDIRECT', '0', '3')");

		}
		#endregion

		#region SetErrorMessages Function 
		private void SetErrorMessages()
		{
			rfvFIRE_STATION_DIST.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("676");
			rngNO_OF_FAMILIES.ErrorMessage	=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"19");
			rfvHYDRANT_DIST.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			rfvPROT_CLASS.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");
			rngPROT_CLASS.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("412");
			revFIRE_STATION_DIST.ValidationExpression   = aRegExpDoublePositiveNonZero;
			revFIRE_STATION_DIST.ErrorMessage           = Cms.CmsWeb.ClsMessages.FetchGeneralMessage ("216");
			revNO_OF_FAMILIES.ValidationExpression		=	aRegExpInteger;
			revNO_OF_FAMILIES.ErrorMessage				=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"12");
			rfvEXTERIOR_CONSTRUCTION.ErrorMessage       =	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"14");
			rfvHEATING_UPDATE_YEAR.ErrorMessage			=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage ("155");
			rfvROOFING_UPDATE_YEAR.ErrorMessage			=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage ("157");
			rfvPLUMBING_UPDATE_YEAR.ErrorMessage		=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage ("153");
			rfvWIRING_UPDATE_YEAR.ErrorMessage			=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage ("151");
			rngNO_OF_AMPS.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage ("216");
			revDWELLING_CONST_DATE.ValidationExpression	  =	aRegExpDate;
			revDWELLING_CONST_DATE.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"22");
			csvDWELLING_CONST_DATE.ErrorMessage		= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("487");
			rfvIS_UNDER_CONSTRUCTION.ErrorMessage=Cms.CmsWeb.ClsMessages.GetMessage (base.ScreenId, "16");
			rfvCIRCUIT_BREAKERS.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("566");
			rfvNO_OF_AMPS.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("565");
			rfvEXTERIOR_OTHER_DESC.ErrorMessage=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"457");
			rfvROOF_OTHER_DESC.ErrorMessage=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"457");
			rfvPRIMARY_HEAT_OTHER_DESC.ErrorMessage	 =Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"457");
			rfvSECONDARY_HEAT_OTHER_DESC.ErrorMessage=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"457");

		}
		#endregion 

		#region LoadData Function
		private void LoadData()
		{
				
			
			DataSet dsRating = ClsUmbrellaRating.GetPolicyRatingInfo (
				int.Parse(hidCUSTOMER_ID.Value),
				int.Parse(hidPOLICY_ID .Value),
				int.Parse(hidPOLICY_VERSION_ID.Value),
				int.Parse(hidDWELLING_ID.Value));
			
			DataTable dtRating = dsRating.Tables[0];
			
			this.hidOldData.Value = Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(dtRating);
	
			
			if(Convert.ToInt32(dtRating.Rows[0]["HYDRANT_DIST"].ToString())>10000)
				cmbHYDRANT_DIST.SelectedValue = Cms.BusinessLayer.BlCommon.Default.GetString(dtRating.Rows[0]["HYDRANT_DIST"]);
			
			txtFIRE_STATION_DIST.Text = Cms.BusinessLayer.BlCommon.Default.GetString(dtRating.Rows[0]["FIRE_STATION_DIST"].ToString());
			
			txtPROT_CLASS.Text = Cms.BusinessLayer.BlCommon.Default.GetString(dtRating.Rows[0]["PROT_CLASS"]);
			
			txtDWELLING_CONST_DATE.Text = Cms.BusinessLayer.BlCommon.Default.GetString(dtRating.Rows[0]["DWELLING_CONST_DATE"]);
					
			Cms.BusinessLayer.BlCommon.ClsCommon.SelectValueinDDL(cmbIS_UNDER_CONSTRUCTION,dtRating.Rows[0]["IS_UNDER_CONSTRUCTION"]);
			Cms.BusinessLayer.BlCommon.ClsCommon.SelectValueinDDL(cmbIS_AUTO_POL_WITH_CARRIER,dtRating.Rows[0]["IS_AUTO_POL_WITH_CARRIER"]);
		
			//Construction info
			txtNO_OF_FAMILIES.Text = Cms.BusinessLayer.BlCommon.Default.GetString(dtRating.Rows[0]["NO_OF_FAMILIES"]);
			txtEXTERIOR_OTHER_DESC.Text = Cms.BusinessLayer.BlCommon.Default.GetString(dtRating.Rows[0]["EXTERIOR_OTHER_DESC"]);
			txtROOF_OTHER_DESC.Text = Cms.BusinessLayer.BlCommon.Default.GetString(dtRating.Rows[0]["ROOF_OTHER_DESC"]);
			txtPRIMARY_HEAT_OTHER_DESC.Text = Cms.BusinessLayer.BlCommon.Default.GetString(dtRating.Rows[0]["PRIMARY_HEAT_OTHER_DESC"]);
			txtSECONDARY_HEAT_OTHER_DESC.Text = Cms.BusinessLayer.BlCommon.Default.GetString(dtRating.Rows[0]["SECONDARY_HEAT_OTHER_DESC"]);
			
			Cms.BusinessLayer.BlCommon.ClsCommon.SelectValueinDDL(cmbEXTERIOR_CONSTRUCTION,dtRating.Rows[0]["EXTERIOR_CONSTRUCTION"]);
			Cms.BusinessLayer.BlCommon.ClsCommon.SelectValueinDDL(cmbFOUNDATION,dtRating.Rows[0]["FOUNDATION"]);
			Cms.BusinessLayer.BlCommon.ClsCommon.SelectValueinDDL(cmbROOF_TYPE,dtRating.Rows[0]["ROOF_TYPE"]);
			Cms.BusinessLayer.BlCommon.ClsCommon.SelectValueinDDL(cmbPRIMARY_HEAT_TYPE,dtRating.Rows[0]["PRIMARY_HEAT_TYPE"]);
			Cms.BusinessLayer.BlCommon.ClsCommon.SelectValueinDDL(cmbSECONDARY_HEAT_TYPE,dtRating.Rows[0]["SECONDARY_HEAT_TYPE"]);
			Cms.BusinessLayer.BlCommon.ClsCommon.SelectValueinDDL(cmbCONSTRUCTION_CODE,dtRating.Rows[0]["CONSTRUCTION_CODE"]);
			
			//End of construction info
					
			txtWIRING_UPDATE_YEAR.Text = Cms.BusinessLayer.BlCommon.Default.GetString(dtRating.Rows[0]["WIRING_UPDATE_YEAR"]);
			txtPLUMBING_UPDATE_YEAR.Text = Cms.BusinessLayer.BlCommon.Default.GetString(dtRating.Rows[0]["PLUMBING_UPDATE_YEAR"]);
			txtHEATING_UPDATE_YEAR.Text = Cms.BusinessLayer.BlCommon.Default.GetString(dtRating.Rows[0]["HEATING_UPDATE_YEAR"]);
			txtROOFING_UPDATE_YEAR.Text = Cms.BusinessLayer.BlCommon.Default.GetString(dtRating.Rows[0]["ROOFING_UPDATE_YEAR"]);
			txtNO_OF_AMPS.Text = Cms.BusinessLayer.BlCommon.Default.GetString(dtRating.Rows[0]["NO_OF_AMPS"]);
			
			Cms.BusinessLayer.BlCommon.ClsCommon.SelectValueinDDL(cmbWIRING_RENOVATION,dtRating.Rows[0]["WIRING_RENOVATION"]);
			Cms.BusinessLayer.BlCommon.ClsCommon.SelectValueinDDL(cmbPLUMBING_RENOVATION,dtRating.Rows[0]["PLUMBING_RENOVATION"]);
			Cms.BusinessLayer.BlCommon.ClsCommon.SelectValueinDDL(cmbHEATING_RENOVATION,dtRating.Rows[0]["HEATING_RENOVATION"]);
			Cms.BusinessLayer.BlCommon.ClsCommon.SelectValueinDDL(cmbROOFING_RENOVATION,dtRating.Rows[0]["ROOFING_RENOVATION"]);
			Cms.BusinessLayer.BlCommon.ClsCommon.SelectValueinDDL(cmbCIRCUIT_BREAKERS,dtRating.Rows[0]["CIRCUIT_BREAKERS"]);
						
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

		}
		#endregion
        
		#region GetFormValue Function 
		private ClsRatingInfo  GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsRatingInfo  objHomeRatingInfo = new ClsRatingInfo();
          			
			objHomeRatingInfo.HYDRANT_DIST			=	Convert.ToDouble(cmbHYDRANT_DIST.SelectedValue);
			objHomeRatingInfo.FIRE_STATION_DIST     =	txtFIRE_STATION_DIST.Text==""?0.0 :double.Parse(txtFIRE_STATION_DIST.Text);
			objHomeRatingInfo.IS_UNDER_CONSTRUCTION  =	cmbIS_UNDER_CONSTRUCTION.SelectedValue;
			objHomeRatingInfo.IS_AUTO_POL_WITH_CARRIER  =	cmbIS_AUTO_POL_WITH_CARRIER.SelectedValue;
			objHomeRatingInfo.PROT_CLASS                =	txtPROT_CLASS.Text;
            
			objHomeRatingInfo.CUSTOMER_ID               = int.Parse(hidCUSTOMER_ID.Value);
			objHomeRatingInfo.POLICY_ID                 = int.Parse(hidPOLICY_ID .Value);
			objHomeRatingInfo.POLICY_VERSION_ID         = int.Parse(hidPOLICY_VERSION_ID .Value);
			objHomeRatingInfo.DWELLING_ID               = int.Parse(hidDWELLING_ID.Value); 
			
			if((cmbWIRING_RENOVATION.SelectedValue.Trim()  != null) && (cmbWIRING_RENOVATION.SelectedValue.Trim()  !=""))            
				objHomeRatingInfo.WIRING_RENOVATION      =	int.Parse(cmbWIRING_RENOVATION.SelectedValue);
			
			if(cmbWIRING_RENOVATION.SelectedIndex == 0 || cmbWIRING_RENOVATION.SelectedIndex == 2)
			{
			}
			else
			{
				if ( txtWIRING_UPDATE_YEAR.Text.Trim() != "")
				{
					objHomeRatingInfo.WIRING_UPDATE_YEAR     =	int.Parse(txtWIRING_UPDATE_YEAR.Text);
				}

			}
			
			if((cmbPLUMBING_RENOVATION.SelectedValue.Trim()  != null) && (cmbPLUMBING_RENOVATION.SelectedValue.Trim()  !=""))            
				objHomeRatingInfo.PLUMBING_RENOVATION      =	int.Parse(cmbPLUMBING_RENOVATION.SelectedValue);
			

			if ( cmbPLUMBING_RENOVATION.SelectedIndex == 0 ||   cmbPLUMBING_RENOVATION.SelectedIndex == 2)
			{
			}
			else
			{
				if ( txtPLUMBING_UPDATE_YEAR.Text.Trim() != "")
				{
					objHomeRatingInfo.PLUMBING_UPDATE_YEAR    =	int.Parse(txtPLUMBING_UPDATE_YEAR.Text);
				}
			}
				
			if ( cmbHEATING_RENOVATION.SelectedValue.Trim() != null && cmbHEATING_RENOVATION.SelectedValue.Trim() !="" )            
			{
				objHomeRatingInfo.HEATING_RENOVATION     =	int.Parse(cmbHEATING_RENOVATION.SelectedValue);
			}

			if(cmbHEATING_RENOVATION.SelectedIndex == 0 || cmbHEATING_RENOVATION.SelectedIndex == 2)
			{
				
			}
			else
			{
				if ( txtHEATING_UPDATE_YEAR.Text.Trim() != "")
				{
					objHomeRatingInfo.HEATING_UPDATE_YEAR    =	int.Parse(txtHEATING_UPDATE_YEAR.Text);
				}
			}

			if((cmbROOFING_RENOVATION.SelectedValue.Trim() != null) && (cmbROOFING_RENOVATION.SelectedValue.Trim() !=""))            
				objHomeRatingInfo.ROOFING_RENOVATION     =	int.Parse(cmbROOFING_RENOVATION.SelectedValue);

			if(cmbROOFING_RENOVATION.SelectedIndex == 0 || cmbROOFING_RENOVATION.SelectedIndex == 2)
			{
			
			}
			else
			{
				if ( txtROOFING_UPDATE_YEAR.Text.Trim() != "")
				{
					objHomeRatingInfo.ROOFING_UPDATE_YEAR    =	int.Parse(txtROOFING_UPDATE_YEAR.Text);
				}
			}

			if(txtNO_OF_AMPS.Text.Trim() !="")            
				objHomeRatingInfo.NO_OF_AMPS             =	int.Parse(txtNO_OF_AMPS.Text);
        
			if((cmbCIRCUIT_BREAKERS.SelectedValue.Trim() != null) && (cmbCIRCUIT_BREAKERS.SelectedValue.Trim() !=""))            
				objHomeRatingInfo.CIRCUIT_BREAKERS       =	cmbCIRCUIT_BREAKERS.SelectedValue;
			
			//Construction
			
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
			else
			{								   
				objHomeRatingInfo.EXTERIOR_OTHER_DESC		=	txtEXTERIOR_OTHER_DESC.Text;
			}

			if(cmbFOUNDATION.SelectedValue.Trim() != "")
				objHomeRatingInfo.FOUNDATION				=	Convert.ToInt32(cmbFOUNDATION.SelectedValue);
			
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

			if (txtDWELLING_CONST_DATE.Text.Trim() != "")
				objHomeRatingInfo.DWELLING_CONST_DATE = ConvertToDate(txtDWELLING_CONST_DATE.Text);

			objHomeRatingInfo.PRIMARY_HEAT_TYPE		=	Convert.ToInt32(cmbPRIMARY_HEAT_TYPE.SelectedValue);

			if (cmbPRIMARY_HEAT_TYPE.SelectedItem.Text.ToLower().StartsWith("other")) 
			{
				objHomeRatingInfo.PRIMARY_HEAT_OTHER_DESC	=	txtPRIMARY_HEAT_OTHER_DESC.Text.Trim();
			}
			else
			{
				objHomeRatingInfo.PRIMARY_HEAT_OTHER_DESC = "";
			}

			objHomeRatingInfo.SECONDARY_HEAT_TYPE		=	Convert.ToInt32(cmbSECONDARY_HEAT_TYPE.SelectedValue);

			if (cmbSECONDARY_HEAT_TYPE.SelectedItem.Text.ToLower().StartsWith("other")) 
			{
				objHomeRatingInfo.SECONDARY_HEAT_OTHER_DESC	=	txtSECONDARY_HEAT_OTHER_DESC.Text.Trim();
			}
			else
			{
				objHomeRatingInfo.SECONDARY_HEAT_OTHER_DESC = "";
			}
			
					
			//Prot devices
			
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
			
			return objHomeRatingInfo;
		}


		#endregion 

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

		#region WebEvent Handler btnSave_Click
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			int intRetVal;
			ClsUmbrellaRating  objUmbrellaRating= new ClsUmbrellaRating ();
			
			ClsRatingInfo objRatingInfo = GetFormValue();

			string strCustomInfo;

			ClsUmbrellaDwelling objDwelling = new ClsUmbrellaDwelling();
			DataTable dtLoc = objDwelling.GetPolicyLocationDetails(Convert.ToInt32(this.hidLOCATION_ID.Value.Trim ()) );
			strCustomInfo = ";Location = " + dtLoc.Rows[0][0].ToString ();
			try
			{
				if(this.hidOldData.Value =="")
				{
					objRatingInfo.CREATED_BY = int.Parse(GetUserId());
					objRatingInfo.CREATED_DATETIME = DateTime.Now;

					//Save Data 
					intRetVal=objUmbrellaRating.AddPolicy(objRatingInfo,strCustomInfo);

					if(intRetVal > 0)
					{
						//Saved SuccessFully
						this.lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","29");
						hidFormSaved.Value		=	"1";
						SetWorkFlow();
						base.OpenEndorsementDetails();
					}
					else if(intRetVal == -1)
					{
						this.lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","21");
						hidFormSaved.Value			=	"2";
					}
				}

				else
				{
					//Creating the Model object for holding the Old data
					ClsRatingInfo  objOldRatingInfo = new ClsRatingInfo();
					
					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldRatingInfo,hidOldData.Value);
					
					objOldRatingInfo.CUSTOMER_ID               = int.Parse(hidCUSTOMER_ID.Value);
					objOldRatingInfo.POLICY_ID                 = int.Parse(hidPOLICY_ID .Value);
					objOldRatingInfo.POLICY_VERSION_ID         = int.Parse(hidPOLICY_VERSION_ID.Value);
					objOldRatingInfo.DWELLING_ID               = int.Parse(hidDWELLING_ID.Value); 
					
					objRatingInfo.MODIFIED_BY = int.Parse(GetUserId());
					objRatingInfo.LAST_UPDATED_DATETIME = DateTime.Now;
                    
					//Updating the record using business layer class object

					intRetVal	= objUmbrellaRating.UpdatePolicy(objOldRatingInfo,objRatingInfo,strCustomInfo);
					
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text		=Cms.CmsWeb.ClsMessages.FetchGeneralMessage ("31");
						hidFormSaved.Value		=	"1";
						SetWorkFlow();
						base.OpenEndorsementDetails();
					}
					else 
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("21");
						hidFormSaved.Value		=	"2";
					}
				}
				lblMessage.Visible = true;
				LoadData ();
				
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
			}
		}
		#endregion

	}
}
