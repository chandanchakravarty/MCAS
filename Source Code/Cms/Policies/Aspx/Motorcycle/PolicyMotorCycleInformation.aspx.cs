/******************************************************************************************
<Author					: -   Ashwini
<Start Date				: -	  9 Nov.,2005
<End Date				: -	
<Description			: -   Motorcycle Policy Information screen
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			:  
<Modified By			:  
<Purpose				:  
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
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlCommon ;
using Cms.CmsWeb;
using Cms.ExceptionPublisher;
using Cms.CmsWeb.Controls; 

namespace Cms.Policies.Aspx.Motorcycle
{
	/// <summary>
	/// Summary description for PolicyMotorCycleInformation.
	/// </summary>
	public class PolicyMotorCycleInformation : Cms.Policies.policiesbase
	{
		# region Page Control Declartion
		protected System.Web.UI.WebControls.CustomValidator csvGRG_ZIP;
		protected System.Web.UI.WebControls.DropDownList cmbCYCL_REGD_ROAD_USE;
		protected System.Web.UI.WebControls.Label capCYCL_REGD_ROAD_USE;
		protected System.Web.UI.WebControls.Label lblDelete;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capVIN;
		protected System.Web.UI.WebControls.TextBox txtVIN;
		protected System.Web.UI.WebControls.Label capINSURED_VEH_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtINSURED_VEH_NUMBER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvINSURED_VEH_NUMBER;
		protected System.Web.UI.WebControls.Label capVEHICLE_YEAR;
		protected System.Web.UI.WebControls.TextBox txtVEHICLE_YEAR;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvVEHICLE_YEAR;
		protected System.Web.UI.WebControls.RangeValidator rngVEHICLE_YEAR;
		protected System.Web.UI.WebControls.Label capMAKE;
		protected System.Web.UI.WebControls.TextBox txtMAKE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMAKE;
		protected System.Web.UI.WebControls.Label capMODEL;
		protected System.Web.UI.WebControls.TextBox txtMODEL;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMODEL;
		protected System.Web.UI.WebControls.Label capMOTORCYCLE_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbMOTORCYCLE_TYPE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMOTORCYCLE_TYPE;
		protected System.Web.UI.WebControls.Label capVEHICLE_CC;
		protected System.Web.UI.WebControls.TextBox txtVEHICLE_CC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvVEHICLE_CC;
		protected System.Web.UI.WebControls.RegularExpressionValidator revVEHICLE_CC;
		protected System.Web.UI.WebControls.Label capAPP_VEHICLE_CLASS;
		protected System.Web.UI.WebControls.DropDownList cmbAPP_VEHICLE_CLASS;
		protected System.Web.UI.WebControls.Label Label1;
		protected Cms.CmsWeb.Controls.CmsButton btnPullCustomerAddress;
		protected System.Web.UI.WebControls.Label capGRG_ADD1;
		protected System.Web.UI.WebControls.TextBox txtGRG_ADD1;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvGRG_ADD1;
		protected System.Web.UI.WebControls.Label capGRG_ADD2;
		protected System.Web.UI.WebControls.TextBox txtGRG_ADD2;
		protected System.Web.UI.WebControls.Label capGRG_CITY;
		protected System.Web.UI.WebControls.TextBox txtGRG_CITY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvGRG_CITY;
		protected System.Web.UI.WebControls.Label capGRG_COUNTRY;
		protected System.Web.UI.WebControls.DropDownList cmbGRG_COUNTRY;
		protected System.Web.UI.WebControls.Label capGRG_STATE;
		protected System.Web.UI.WebControls.DropDownList cmbGRG_STATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvGRG_STATE;
		protected System.Web.UI.WebControls.Label capGRG_ZIP;
		protected System.Web.UI.WebControls.TextBox txtGRG_ZIP;
		protected System.Web.UI.WebControls.RegularExpressionValidator revGRG_ZIP;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvGRG_ZIP;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvAMOUNT;
		protected System.Web.UI.WebControls.Label capREGISTERED_STATE;
		protected System.Web.UI.WebControls.DropDownList cmbREGISTERED_STATE;
        protected System.Web.UI.WebControls.DropDownList cmbVEHICLE_COVERAGE;
		protected System.Web.UI.WebControls.Label capTERRITORY;
		protected System.Web.UI.WebControls.TextBox txtTERRITORY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvTERRITORY;
		protected System.Web.UI.WebControls.RegularExpressionValidator revTERRITORY;
		protected System.Web.UI.WebControls.Label capAMOUNT;
		protected System.Web.UI.WebControls.TextBox txtAMOUNT;
		protected System.Web.UI.WebControls.RegularExpressionValidator revAMOUNT;
		protected System.Web.UI.WebControls.Label capVEHICLE_AGE;
		protected System.Web.UI.WebControls.TextBox txtVEHICLE_AGE;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected Cms.CmsWeb.Controls.CmsButton btnCopy;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlImage imgSelectForVehicleMake;
		protected System.Web.UI.HtmlControls.HtmlImage imgSelectVehicleModel;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCYCL_REGD_ROAD_USE_MSG;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidStateID;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_VEHICLE_CLASS;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidMotorType;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidVehicleID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidMakeCode;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_LOB;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCheckZipSubmit;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCheckMakeSubmit;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_EFFECTIVE_DATE; 
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidINSURED_VEH_NUMBER;
		protected System.Web.UI.WebControls.Label capSYMBOL;
		protected System.Web.UI.WebControls.TextBox txtSYMBOL;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSYMBOL;
		protected System.Web.UI.WebControls.RegularExpressionValidator revSYMBOL;

		//Added by Sibin on 26 Nov for Itrack Issue 5058
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCYCL_REGD_ROAD_USE;

		protected System.Web.UI.WebControls.RangeValidator rngSYMBOL;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
		protected System.Web.UI.WebControls.RangeValidator rngVEHICLE_CC;
		protected System.Web.UI.WebControls.Image imgZipLookup;
		protected System.Web.UI.WebControls.HyperLink hlkZipLookup;
		# endregion

		#region Local Variables
		//START:*********** Local form variables *************
		
		string oldXML;
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId, strFormSaved;
		Cms.BusinessLayer.BlApplication.ClsVehicleInformation  objVehicleInformation ;
		private const string CALLED_FROM_CLIENT = "CLT";
		private const string CALLED_FROM_APPLICATION = "APP";
		private const string CALLED_FROM_MOTORCYCLE ="MOT";
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPPID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAppVersionID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyVersionID;
		protected System.Web.UI.WebControls.Label capCOMPRH_ONLY;
		protected System.Web.UI.WebControls.DropDownList cmbCOMPRH_ONLY;
        //Added by Agniswar for Singapore Implementation
        protected System.Web.UI.WebControls.DropDownList cmbMAKE;
        protected System.Web.UI.WebControls.DropDownList cmbMODEL;
        protected System.Web.UI.WebControls.DropDownList cmbTRANSMISSION_TYPE;
        protected System.Web.UI.WebControls.DropDownList cmbFUEL_TYPE;
        protected System.Web.UI.WebControls.DropDownList cmbPAINT_TYPE;
        protected System.Web.UI.WebControls.DropDownList cmbRISK_CURRENCY;
        protected System.Web.UI.WebControls.TextBox txtCHASIS;
        protected System.Web.UI.WebControls.TextBox txtREG_NO;
        protected System.Web.UI.WebControls.TextBox txtTOTAL_PASSENGERS;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidMODEL;

		protected System.Web.UI.WebControls.Label lblAge;
		private string strCalledFrom="";
		public string WebServiceURL;
		#endregion 

		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
        /// Parameters: none cmbMAKE
		/// Return Type: none
		/// </summary>
		private void SetErrorMessages()
		{
			//rfvINSURED_VEH_NUMBER.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"103");
			rfvVEHICLE_YEAR.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"166");
			rfvMAKE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"168");
			rfvMODEL.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"169");
			rfvGRG_CITY.ErrorMessage	= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"56");
			rfvGRG_STATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"35");
			rfvGRG_ZIP.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"37");
			revGRG_ZIP.ValidationExpression	=  aRegExpZip;
			revGRG_ZIP.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"24");
			rfvTERRITORY.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"108");
			revAMOUNT.ValidationExpression  = aRegExpDoublePositiveNonZero; //changed to aRegExpCurrencyformat - Done by Sibin on 26 Nov 08 for Itrack Issue 5072;			
			revAMOUNT.ErrorMessage			= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"116");
			rfvGRG_ADD1.ErrorMessage		= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"32");
			revVEHICLE_CC.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"216");
			revVEHICLE_CC.ValidationExpression =aRegExpDoublePositiveNonZero;
			rfvVEHICLE_CC.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"132");		
			rngVEHICLE_YEAR.MaximumValue = DateTime.Now.AddYears(1).Year.ToString();
			//rngVEHICLE_YEAR.MinimumValue = aAppMinYear  ;
			rngVEHICLE_YEAR.ErrorMessage =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("673");
			revTERRITORY.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"222");
			rfvMOTORCYCLE_TYPE.ErrorMessage =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("679");
			revSYMBOL.ValidationExpression  = aRegExpInteger ;
			rfvSYMBOL.ErrorMessage          = "<br>" + Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			revSYMBOL.ErrorMessage			= "<br>" + Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
			rngSYMBOL.ErrorMessage          = "<br>" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");

			//Added by Sibin on 26 Nov for Itrack Issue 5058
			rfvCYCL_REGD_ROAD_USE.ErrorMessage          =  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"4");
		
			revTERRITORY.ValidationExpression =aRegExpInteger;	
			rngVEHICLE_CC.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");
			hidCYCL_REGD_ROAD_USE_MSG.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("850");
			csvGRG_ZIP.ErrorMessage				=		Cms.CmsWeb.ClsMessages.FetchGeneralMessage("898");
		}

		#endregion

		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{	
			Ajax.Utility.RegisterTypeForAjax(typeof(PolicyMotorCycleInformation)); 			
			// phone and extension control names: txtPHONE.Attributes.Add("OnBlur","Javascript:DisableExt('txtPHONE','txtEXT');");
			#region Setting Screen ID
			if (Request["CalledFrom"].ToString().Trim().ToUpper() == "MOT")
			{
				base.ScreenId="231_0";
			}
			#endregion
			lblMessage.Visible = false;
			WebServiceURL = ClsCommon.WebServiceURL;
			//SetErrorMessages();
			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass			=	CmsButtonType.Write;
			btnReset.PermissionString		=	gstrSecurityXML;
			
			btnCopy.CmsButtonClass			=	CmsButtonType.Write;
			btnCopy.PermissionString		=	gstrSecurityXML;

			btnSave.CmsButtonClass			=	CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;

			btnActivateDeactivate.CmsButtonClass	=	CmsButtonType.Write;
			btnActivateDeactivate.PermissionString	=	gstrSecurityXML;

			btnDelete.CmsButtonClass		=	CmsButtonType.Delete;
			btnDelete.PermissionString		=	gstrSecurityXML;

			btnPullCustomerAddress.CmsButtonClass	=	CmsButtonType.Execute;
			btnPullCustomerAddress.PermissionString	=	gstrSecurityXML;
			btnPullCustomerAddress.CausesValidation=false;

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			//btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");
			// Modified by Swastika on 2nd Mar'06 for Gen Iss #2355	
			btnReset.Attributes.Add("onclick","javascript:return resetValues();");
			cmbCYCL_REGD_ROAD_USE.Attributes.Add("onChange","javascript:cmbCYCL_REGD_ROAD_USE_Change();");			
			//lnkVINMASTER.Attributes.Add("OnClick","ShowLookUpWindow()");//"fPopCalendar(document.APP_LIST.txtAPP_EFFECTIVE_DATE,document.APP_LIST.txtAPP_EFFECTIVE_DATE)"); //Javascript Implementation for Calender				
			btnCopy.Attributes.Add("onclick","Javascript:DisableValidators();ShowCustomerVehicle(); return false;");
			txtAMOUNT.Attributes.Add("onBlur","this.value=formatCurrency(this.value);"); 
			txtVEHICLE_YEAR.Attributes.Add ("onBlur","javascript:GetAgeOfVehicle();");
			string url=ClsCommon.GetLookupURL();
			//txtGRG_ZIP.Attributes.Add("OnBlur","javascript:DisableValidators();GetTerritory();");
			txtGRG_ZIP.Attributes.Add("OnBlur","javascript:GetZipForState();");
			// Added by Swarup on 05-apr-2007
			imgZipLookup.Attributes.Add("style","cursor:hand");
			base.VerifyAddress(hlkZipLookup, txtGRG_ADD1,txtGRG_ADD2
				, txtGRG_CITY, cmbGRG_STATE, txtGRG_ZIP);
			btnSave.Attributes.Add("onClick","javascript:setUpperCase();return Validate();");//Done for Itrack Issue 5888 on 25 May 2009
			//changes made by sumit
			//Commented on 22 Sep 2009 (Praveen)
            //imgSelectForVehicleMake.Attributes.Add("onclick",@"javascript:OpenLookup('"+url+"','Manufacturer','Manufacturer','','txtMAKE','ManufacturerMC','Manufacturer','@LOBID='+varLOB);");
			//Added(Praveen)
			imgSelectForVehicleMake.Attributes.Add("onclick",@"javascript:OpenLookupMotorMake('"+url+"');");
			//varLOB=" where Manufacturer='" + txt +"'"
			//imgSelectVehicleModel.Attributes.Add("onclick",@"javascript:OpenLookup('"+url+"','Model','Model','','txtMODEL','ModelMC','ModelMC','@Manufacturer='+varLOB);");
			imgSelectVehicleModel.Attributes.Add("onclick",@"javascript:OpenLookupProxy('"+url+"');");
			//imgSelect.Attributes.Add("onclick",@"javascript:OpenLookup('"+url+"','TERR','TERR','','txtTERRITORY','Territory','Territory','@LOBID='+varLOB);");
			base.RequiredPullCustAddWithCounty(txtGRG_ADD1,txtGRG_ADD2,txtGRG_CITY
				,cmbGRG_COUNTRY,cmbGRG_STATE,txtGRG_ZIP,null,txtTERRITORY,btnPullCustomerAddress);
			btnPullCustomerAddress.Attributes.Add("onClick","javascript:PullCustomerAddress("
				+ "document.getElementById('" + txtGRG_ADD1.ID + "'),"
				+ "document.getElementById('" + txtGRG_ADD2.ID + "'),"
				+ "document.getElementById('" + txtGRG_CITY.ID + "'),"
				+ "document.getElementById('" + cmbGRG_COUNTRY.ID + "'),"
				+ "document.getElementById('" + cmbGRG_STATE.ID + "'),"
				+ "document.getElementById('" + txtGRG_ZIP.ID + "'),"
				+ "null,"
				+ "document.getElementById('" + txtTERRITORY.ID + "')"
				+ ");SetRegisteredState();GetZipForState();return false;");
			//Function GetZipForState() Added For Itrack Issue #6235. 

			//			if(hidCheckMakeSubmit.Value=="1")
			//			{
			//				SelectClassAndType();			
			//				hidCheckMakeSubmit.Value="0";
			//				hidFormSaved.Value="4";
			//				return;
			//			}
			objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Aspx.Motorcycle.PolicyMotorCycleInformation" ,System.Reflection.Assembly.GetExecutingAssembly());
			if(!Page.IsPostBack)
			{				
				if (Request.QueryString["CALLEDFROM"]!=null && Request.QueryString["CALLEDFROM"].ToString().Trim()!="")
					strCalledFrom = Request.QueryString["CALLEDFROM"].ToString().Trim();				
				hidCalledFrom.Value =strCalledFrom;
                //GetOldDataXML();
                //LoadModelType();
				
				SetCaptions();
                SetErrorMessages();

                string strSysID = GetSystemId();
                if (strSysID == "ALBAUAT")
                    strSysID = "ALBA";

                if (ClsCommon.IsXMLResourceExists(Request.PhysicalApplicationPath + "/Policies/support/PageXML/" + strSysID, "PolicyMotorCycleInformation.xml"))
                    setPageControls(Page, Request.PhysicalApplicationPath + "/Policies/support/PageXml/" + strSysID + "/PolicyMotorCycleInformation.xml");


                GetOldDataXML();
                LoadModelType();

				txtVEHICLE_CC.Attributes.Add("onBlur","javascript:SetSymbolRule();");
				#region "Loading dropdowns"				
				FillDropdowns();
				#endregion	
				hidAPP_EFFECTIVE_DATE.Value		= new Cms.BusinessLayer.BlApplication.clsWatercraftInformation().GetPolEffectiveDate(int.Parse(hidCustomerID.Value),int.Parse(hidPolicyID.Value),int.Parse(hidPolicyVersionID.Value));
				int stateID = ClsVehicleInformation.GetStateIdForpolicy(int.Parse(hidCustomerID.Value),hidPolicyID.Value,hidPolicyVersionID.Value);				
				//if(stateID == 14)//Commented by Charles on 20-Aug-09 for Itrack 6151
				//{
				//	capCOMPRH_ONLY.Visible=true;
				//	cmbCOMPRH_ONLY.Visible=true;
				//}
				//else
				//{
				//	capCOMPRH_ONLY.Visible=false;
				//	cmbCOMPRH_ONLY.Visible=false;
				//}					
				SetWorkflow();
			}
			if (hidCheckZipSubmit.Value=="zip")
			{
				int result=0;						
				result=ClsVehicleInformation.FetchTerritoryForZip(txtGRG_ZIP.Text,int.Parse(hidAPP_LOB.Value));	
				if (result != 0)
					txtTERRITORY.Text=result.ToString();
				//save the lobid				
				hidAPP_LOB.Value=GetLOBID();
			}			
		}
		//end pageload
		#endregion

        #region AJAX Methods
        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public DataSet AjaxFetchVehicleModelType(int MakeID)
        {
            DataSet ds = null;

            try
            {
                ds = new DataSet();

                DataTable dt1 = new DataTable();
                try
                {
                    dt1 = ClsLookup.GetVehicleModelByMake(MakeID);
                }
                catch
                { }
                ds.Tables.Add(dt1.Copy());
                ds.Tables[0].TableName = "VEHICLE_MODEL";

                DataTable dt2 = new DataTable();
                try
                {
                    dt2 = ClsLookup.GetVehicleTypeByMake(MakeID);
                }
                catch
                { }
                ds.Tables.Add(dt2.Copy());
                ds.Tables[1].TableName = "VEHICLE_TYPE";



                return ds;
            }
            catch
            {
                return null;
            }
        }

        #endregion

		private void GetStateID()
		{
			hidStateID.Value = ClsVehicleInformation.GetStateIdForpolicy(Convert.ToInt32(hidCustomerID.Value),hidPolicyID.Value,hidPolicyVersionID.Value).ToString();
		}
		
		private void FillDropdowns()
		{
			try 
			{
				DataTable dt = Cms.CmsWeb.ClsFetcher.Country;
				cmbGRG_COUNTRY.DataSource		= dt;
				cmbGRG_COUNTRY.DataTextField	= "Country_Name";
				cmbGRG_COUNTRY.DataValueField	= "Country_Id";
				cmbGRG_COUNTRY.DataBind();			
				cmbGRG_STATE.DataSource		= Cms.CmsWeb.ClsFetcher.ActiveState ;
				cmbGRG_STATE.DataTextField	= "State_Name";
				cmbGRG_STATE.DataValueField	= "State_Id";
				cmbGRG_STATE.DataBind();
				cmbGRG_STATE.Items.Insert(0,"");
				cmbREGISTERED_STATE.DataSource =Cms.CmsWeb.ClsFetcher.State;
				cmbREGISTERED_STATE.DataTextField	= "State_Name";
				cmbREGISTERED_STATE.DataValueField	= "State_Id";
				cmbREGISTERED_STATE.DataBind();
				cmbREGISTERED_STATE.Items.Insert(0,"");	
	 			// Comment by Agniswar 
                //cmbMOTORCYCLE_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("CYCTY");
                //cmbMOTORCYCLE_TYPE.DataTextField	= "LookupDesc";
                //cmbMOTORCYCLE_TYPE.DataValueField	= "LookupID";
                //cmbMOTORCYCLE_TYPE.DataBind();
                //cmbMOTORCYCLE_TYPE.Items.Insert(0,"");
				//added by vj on 18-10-2005
				cmbAPP_VEHICLE_CLASS.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("MCCLAS");
				cmbAPP_VEHICLE_CLASS.DataTextField	= "LookupDesc";
				cmbAPP_VEHICLE_CLASS.DataValueField	= "LookupID";
				cmbAPP_VEHICLE_CLASS.DataBind();
				cmbAPP_VEHICLE_CLASS.Items.Insert(0,"");
				//				
				//				//For Assign default value added by Manoj Rathore on 23 Mar 2007
				//				ListItem Li = null;
				//				Li.Value = Convert.ToString(cmbAPP_VEHICLE_CLASS.Items.FindByValue("11411").Selected= true);

				cmbCYCL_REGD_ROAD_USE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
				cmbCYCL_REGD_ROAD_USE.DataTextField="LookupDesc"; 
				cmbCYCL_REGD_ROAD_USE.DataValueField="LookupCode";
				cmbCYCL_REGD_ROAD_USE.DataBind();
				cmbCYCL_REGD_ROAD_USE.Items.Insert(0,"");

				cmbCOMPRH_ONLY.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
				cmbCOMPRH_ONLY.DataTextField="LookupDesc"; 
				cmbCOMPRH_ONLY.DataValueField="LookupID";
				cmbCOMPRH_ONLY.DataBind();

                cmbMAKE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("VIN");
                cmbMAKE.DataTextField = "LookupDesc";
                cmbMAKE.DataValueField = "LookupID";
                cmbMAKE.DataBind();
                cmbMAKE.Items.Insert(0, "");
                cmbMAKE.SelectedIndex = -1;

                //cmbMODEL -- Ajax call

                //cmbVEHICLE_TYPE -- Ajax call

                cmbTRANSMISSION_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("VTRANS");
                cmbTRANSMISSION_TYPE.DataTextField = "LookupDesc";
                cmbTRANSMISSION_TYPE.DataValueField = "LookupID";
                cmbTRANSMISSION_TYPE.DataBind();
                cmbTRANSMISSION_TYPE.Items.Insert(0, "");
                cmbTRANSMISSION_TYPE.SelectedIndex = -1;

                cmbFUEL_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("FTYCD");
                cmbFUEL_TYPE.DataTextField = "LookupDesc";
                cmbFUEL_TYPE.DataValueField = "LookupID";
                cmbFUEL_TYPE.DataBind();
                cmbFUEL_TYPE.Items.Insert(0, "");
                cmbFUEL_TYPE.SelectedIndex = -1;

                cmbPAINT_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("VPAINT");
                cmbPAINT_TYPE.DataTextField = "LookupDesc";
                cmbPAINT_TYPE.DataValueField = "LookupID";
                cmbPAINT_TYPE.DataBind();
                cmbPAINT_TYPE.Items.Insert(0, "");
                cmbPAINT_TYPE.SelectedIndex = -1;

                cmbVEHICLE_COVERAGE.DataSource = ClsLookup.GetLookupVehicleCoverages(int.Parse(GetLOBID()));
                cmbVEHICLE_COVERAGE.DataTextField = "COV_DES";
                cmbVEHICLE_COVERAGE.DataValueField = "COV_ID";
                cmbVEHICLE_COVERAGE.DataBind();
                cmbVEHICLE_COVERAGE.Items.Insert(0, "");

				
			}
			catch(Exception exc)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(exc);
			}
			finally
			{}		
		}
		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private Cms.Model.Policy.ClsPolicyVehicleInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			Cms.Model.Policy.ClsPolicyVehicleInfo objVehicleInfo;
			objVehicleInfo = new Cms.Model.Policy.ClsPolicyVehicleInfo();
			//modified by vj on 13-10-2005 to update the insured_number when record saved.
            //objVehicleInfo.INSURED_VEH_NUMBER =int.Parse(hidINSURED_VEH_NUMBER.Value);//	int.Parse (txtINSURED_VEH_NUMBER.Text==""?"0":txtINSURED_VEH_NUMBER.Text);
			hidPolicyID.Value = Request["POLICY_ID"].ToString(); 
			hidPolicyVersionID.Value = Request["POLICY_VERSION_ID"].ToString(); 
			objVehicleInfo.VEHICLE_YEAR=	txtVEHICLE_YEAR.Text;
			//objVehicleInfo.MAKE=	txtMAKE.Text ;
            objVehicleInfo.MAKE = cmbMAKE.SelectedValue;
            //objVehicleInfo.MODEL = txtMODEL.Text;
            objVehicleInfo.MODEL = hidMODEL.Value;//cmbMODEL.SelectedValue;
            objVehicleInfo.VIN=	txtVIN.Text;			
			objVehicleInfo.GRG_ADD1=	txtGRG_ADD1.Text;
			objVehicleInfo.GRG_ADD2=	txtGRG_ADD2.Text;
			objVehicleInfo.GRG_CITY=	txtGRG_CITY.Text;
			objVehicleInfo.GRG_COUNTRY=	cmbGRG_COUNTRY.SelectedValue;
			objVehicleInfo.GRG_STATE=	cmbGRG_STATE.SelectedValue;
			objVehicleInfo.GRG_ZIP=	txtGRG_ZIP.Text;
			objVehicleInfo.REGISTERED_STATE=	cmbREGISTERED_STATE.SelectedValue;
			objVehicleInfo.TERRITORY=	txtTERRITORY.Text;		 
			objVehicleInfo.CUSTOMER_ID = int.Parse(hidCustomerID.Value);
            objVehicleInfo.VEHICLE_COVERAGE = cmbVEHICLE_COVERAGE.SelectedValue;//ADDED BY AVIJITT FOR TFS 3573 DATED ON 08/02/2012
			if(txtAMOUNT.Text.Trim()!="" && txtAMOUNT.Text.Trim()!="0.0" )
			{
				objVehicleInfo.AMOUNT=	double.Parse (txtAMOUNT.Text ==""?"0.0":txtAMOUNT.Text);
			}	
			else if(txtAMOUNT.Text.Trim()=="" )
				objVehicleInfo.AMOUNT=0;

			if(txtVEHICLE_AGE.Text.Trim()!="")
			{
				objVehicleInfo.VEHICLE_AGE=	int.Parse(txtVEHICLE_AGE.Text==""?"0":txtVEHICLE_AGE.Text);
			}			
			if(txtVEHICLE_CC.Text.Trim()!="")
			{
				objVehicleInfo.VEHICLE_CC =int.Parse(txtVEHICLE_CC.Text==""?"0":txtVEHICLE_CC.Text);
			}
			if(objVehicleInfo.VEHICLE_CC<=50 && cmbCYCL_REGD_ROAD_USE.SelectedItem!=null && cmbCYCL_REGD_ROAD_USE.SelectedItem.Value!="")
			{
				objVehicleInfo.CYCL_REGD_ROAD_USE = int.Parse(cmbCYCL_REGD_ROAD_USE.SelectedItem.Value);
			}
			else
				objVehicleInfo.CYCL_REGD_ROAD_USE = -1;
            //if (cmbMOTORCYCLE_TYPE.SelectedItem!=null)
            //{
            //    objVehicleInfo.MOTORCYCLE_TYPE =int.Parse (cmbMOTORCYCLE_TYPE.SelectedItem.Value);
            //}

            objVehicleInfo.MOTORCYCLE_TYPE = int.Parse(hidMotorType.Value);

			objVehicleInfo.POLICY_ID =   int.Parse(hidPolicyID.Value); 
			objVehicleInfo.POLICY_VERSION_ID = int.Parse(hidPolicyVersionID.Value);
			if (hidVehicleID.Value !=null )
			{
				if (hidVehicleID.Value.ToString()=="NEW")
					objVehicleInfo.VEHICLE_ID = 0;// int.Parse (ClsVehicleInformation.GetNewVehicleID(hidCalledFrom.Value));
				else
				{
					objVehicleInfo.VEHICLE_ID = int.Parse (hidVehicleID.Value.ToString());
					//objVehicleInfo.INSURED_VEH_NUMBER=int.Parse(txtINSURED_VEH_NUMBER.Text);
                    if(hidINSURED_VEH_NUMBER.Value !="" && hidINSURED_VEH_NUMBER.Value!=null)
                    objVehicleInfo.INSURED_VEH_NUMBER = int.Parse(hidINSURED_VEH_NUMBER.Value);
				}
			}		
	
			if(txtVEHICLE_CC.Text.Trim()!="")
			{
				objVehicleInfo.SYMBOL=int.Parse(txtSYMBOL.Text==""?"0":txtSYMBOL.Text);
			}
			
			//added by vj on 18-10-2005
			//Sumit chhabra:16-01-2006-With the class field being disabled, value for class will be taken from hidden field
			if (cmbAPP_VEHICLE_CLASS.SelectedItem.Text != "")
			{
				objVehicleInfo.APP_VEHICLE_CLASS = Convert.ToInt32(cmbAPP_VEHICLE_CLASS.SelectedValue); 
			}

			//			if(hidAPP_VEHICLE_CLASS.Value!="")
			//				objVehicleInfo.APP_VEHICLE_CLASS = Convert.ToInt32(hidAPP_VEHICLE_CLASS.Value);
			if(cmbCOMPRH_ONLY.SelectedValue !=null &&  cmbCOMPRH_ONLY.SelectedValue != "")
				objVehicleInfo.COMPRH_ONLY = int.Parse(cmbCOMPRH_ONLY.SelectedValue);


            //Added by Agniswar for Singapore implementation
            objVehicleInfo.CHASIS_NUMBER = txtCHASIS.Text;
            objVehicleInfo.REGN_PLATE_NUMBER = txtREG_NO.Text;
            objVehicleInfo.TRANSMISSION_TYPE = cmbTRANSMISSION_TYPE.SelectedValue;
            objVehicleInfo.TOTAL_DRIVERS = int.Parse(txtTOTAL_PASSENGERS.Text == "" ? "0" : txtTOTAL_PASSENGERS.Text);
            objVehicleInfo.FUEL_TYPE = cmbFUEL_TYPE.SelectedValue;
            objVehicleInfo.BODY_TYPE = cmbPAINT_TYPE.SelectedValue;
            
            if (cmbRISK_CURRENCY.SelectedValue != null && cmbRISK_CURRENCY.SelectedValue != "")
                objVehicleInfo.RISK_CURRENCY = int.Parse(cmbRISK_CURRENCY.SelectedValue);

              //These  assignments are common to all pages.
              strFormSaved = hidFormSaved.Value;
			strRowId		=	hidVehicleID.Value;
			oldXML		= @hidOldData.Value;
			//Returning the model object
			return objVehicleInfo;
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
			this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		#region "Web Event Handlers"
		/// <summary>
		/// If form is posted back then add entry in database using the BL object
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal=0;	//For retreiving the return value of business class save function
				objVehicleInformation = new  ClsVehicleInformation();
				//Retreiving the form values into model class object
				Cms.Model.Policy.ClsPolicyVehicleInfo  objVehicleInfo = GetFormValue();				
				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objVehicleInfo.CREATED_BY = int.Parse(GetUserId());
					//objVehicleInfo.CREATED_DATETIME = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy"));
                    objVehicleInfo.CREATED_DATETIME = DateTime.Parse(DateTime.Now.ToShortDateString());
					//Calling the add method of business layer class
					if (hidCalledFrom.Value.Equals("")||hidCalledFrom.Value.Equals(CALLED_FROM_MOTORCYCLE))
					{
						objVehicleInfo.IS_ACTIVE ="Y";
						intRetVal = objVehicleInformation.AddPolicyMotorCycle(objVehicleInfo);
					}				
					strRowId = intRetVal.ToString() ;//objVehicleInfo.VEHICLE_ID.ToString();
					hidVehicleID.Value =  intRetVal.ToString() ;//objVehicleInfo.VEHICLE_ID.ToString();
					if(intRetVal>0)
					{
						hidCustomerID.Value  = objVehicleInfo.CUSTOMER_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						hidIS_ACTIVE.Value = "Y";

                        //added by kuldeep to save premium just after vehicle details;
                        Generate_Premium();
						SetWorkflow();

						//Showing the endorsement popup window
						base.OpenEndorsementDetails();
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
					Cms.Model.Policy.ClsPolicyVehicleInfo objOldVehicleInfo;
					objOldVehicleInfo = new Cms.Model.Policy.ClsPolicyVehicleInfo();
					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldVehicleInfo,@hidOldData.Value);
					//Setting those values into the Model object which are not in the page
					objVehicleInfo.MODIFIED_BY = int.Parse(GetUserId());
					//objVehicleInfo.LAST_UPDATED_DATETIME = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy"));
                    objVehicleInfo.LAST_UPDATED_DATETIME = DateTime.Parse(DateTime.Now.ToShortDateString());
					objVehicleInfo.IS_ACTIVE = "Y";
					//Updating the record using business layer class object
					if (hidCalledFrom.Value.Equals("")||hidCalledFrom.Value.Equals(CALLED_FROM_MOTORCYCLE))
					{
						intRetVal	= objVehicleInformation.UpdatePolicyMotorcycle(objOldVehicleInfo,objVehicleInfo);
					}				
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";

                        //added by kuldeep to save premium just after vehicle details;
                        Generate_Premium();
						SetWorkflow();

						//Showing the endorsement popup window
						base.OpenEndorsementDetails();
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
				// If control is coming from CLIENT then APPID and APPVersionID will not be considered.
				//if (hidCalledFrom.Value.Equals("")||hidCalledFrom.Value.Equals(CALLED_FROM_MOTORCYCLE))				
				//{					
				hidOldData.Value = @ClsVehicleInformation.FetchCycleXMLFromPolicyVehicleTable(int.Parse(hidCustomerID.Value==""?"0": hidCustomerID.Value) ,int.Parse (hidPolicyID.Value ==""?"0":hidPolicyID.Value),int.Parse (hidPolicyVersionID.Value==""?"0":hidPolicyVersionID.Value),int.Parse (hidVehicleID.Value==""?"0":hidVehicleID.Value));
                LoadModelType();
                if (ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value).ToString() != "")//Added By Pradeep Kushwaha on 17-sep-2010
                    btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value).ToString());

				//}			
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
				//Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			finally
			{
				if(objVehicleInformation!= null)
					objVehicleInformation.Dispose();
			}
		}		
		#endregion

		#region Handling Delete Event
		private void btnDelete_Click(object sender, System.EventArgs e)
		{

			ClsDriverDetail objDriverDetails = new  ClsDriverDetail();
			Cms.Model.Policy.ClsPolicyVehicleInfo objPolicyVehicleInfo = new Cms.Model.Policy.ClsPolicyVehicleInfo();
			objVehicleInformation = new Cms.BusinessLayer.BlApplication.ClsVehicleInformation();
			//ClsVehicleInformation.cs
			int result;
			try
			{
				//base.PopulateModelObject(objPolicyVehicleInfo,hidOldData.Value);				
				objPolicyVehicleInfo.CUSTOMER_ID = int.Parse(hidCustomerID.Value);
				objPolicyVehicleInfo.POLICY_ID = int.Parse(hidPolicyID.Value);
				objPolicyVehicleInfo.POLICY_VERSION_ID = int.Parse(hidPolicyVersionID.Value);
				objPolicyVehicleInfo.VEHICLE_ID = int.Parse(hidVehicleID.Value);

				objPolicyVehicleInfo.CREATED_BY = objPolicyVehicleInfo.MODIFIED_BY = int.Parse(GetUserId());
				result=objVehicleInformation.DeletePolicyVehicle(objPolicyVehicleInfo,"",CALLED_FROM_MOTORCYCLE); 
				if(result>=0)
					//			int intRetVal;	
					//			int intCustomerID = int.Parse(hidCustomerID.Value);
					//			int intPolicyID=  int.Parse(hidPolicyID.Value);
					//			int intPolicyVersionID	= int.Parse(hidPolicyVersionID.Value);
					//			int intVehicleId = int.Parse(hidVehicleID.Value);
					//			string strCalledFrom=hidCalledFrom.Value;			
					//			objVehicleInformation = new Cms.BusinessLayer.BlApplication.ClsVehicleInformation();
					//			intRetVal = objVehicleInformation.DeletePolicyVehicle(intCustomerID,intPolicyID,intPolicyVersionID,intVehicleId);			
					//			if(intRetVal>0)
				{
					lblDelete.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","127");
					hidFormSaved.Value = "5";
					hidOldData.Value = "";
					trBody.Attributes.Add("style","display:none");

					//Showing the endorsement popup window
					base.OpenEndorsementDetails();
				}
					//			else if(intRetVal == -1)
				else if(result == -1)
				{
					lblDelete.Text		=	ClsMessages.GetMessage(base.ScreenId,"128");
					hidFormSaved.Value		=	"2";
				}
				SetWorkflow();
				lblDelete.Visible = true;
			}
			catch(Exception exc)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"28") + " - " + exc.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(exc);
				hidFormSaved.Value			=	"2";
			}
		}
		#endregion

		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			
			try
			{				
				ClsVehicleInformation objVehicleInformation = new  ClsVehicleInformation();
				//Retreiving the form values into model class object
				Cms.Model.Policy.ClsPolicyVehicleInfo  objVehicleInfo= new Cms.Model.Policy.ClsPolicyVehicleInfo(); //GetFormValue(); //Commented by Charles on 10-Sep-09 for Itrack 6375		
				base.PopulateModelObject(objVehicleInfo,hidOldData.Value); //Uncommented by Charles on 10-Sep-09 for Itrack 6375
				//Added by Charles on 10-Sep-09 for Itrack 6375				
				objVehicleInfo.CUSTOMER_ID=int.Parse(hidCustomerID.Value);
				objVehicleInfo.POLICY_ID=int.Parse(hidPolicyID.Value);
				objVehicleInfo.POLICY_VERSION_ID=int.Parse(hidPolicyVersionID.Value);
				objVehicleInfo.VEHICLE_ID=int.Parse(hidVehicleID.Value);//Added till here
								
				objVehicleInfo.CREATED_BY = objVehicleInfo.MODIFIED_BY = int.Parse(GetUserId());
				if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
				{	
					
					objVehicleInformation.ActivateDeactivateAutoMotorVehiclePolicy(objVehicleInfo,"N",CALLED_FROM_MOTORCYCLE);
					btnActivateDeactivate.Text="Activate";	
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"41");
					hidIS_ACTIVE.Value="N";
                    // itrack no 867
                   // btnActivateDeactivate.Visible = false;
				}
				else
				{					
					objVehicleInformation.ActivateDeactivateAutoMotorVehiclePolicy(objVehicleInfo,"Y",CALLED_FROM_MOTORCYCLE);
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"40");
					hidIS_ACTIVE.Value="Y";
				}
				//hidFormSaved.Value			=	"0";
				hidFormSaved.Value			=	"0";
				
				//Generating the XML again
				//hidOldData.Value = ClsVehicleInformation.FetchVehicleXMLFromAPPVehicleTable(int.Parse(hidCustomerID.Value==""?"0": hidCustomerID.Value) ,int.Parse (hidAPPID.Value ==""?"0":hidAPPID.Value),int.Parse (hidAppVersionID.Value==""?"0":hidAppVersionID.Value),int.Parse (hidVehicleID.Value==""?"0":hidVehicleID.Value));
				hidOldData.Value = @ClsVehicleInformation.FetchCycleXMLFromPolicyVehicleTable(int.Parse(hidCustomerID.Value==""?"0": hidCustomerID.Value) ,int.Parse (hidPolicyID.Value ==""?"0":hidPolicyID.Value),int.Parse (hidPolicyVersionID.Value==""?"0":hidPolicyVersionID.Value),int.Parse (hidVehicleID.Value==""?"0":hidVehicleID.Value));
                if (ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value).ToString() != "")//Added By Pradeep Kushwaha on 10-sep-2010
                    btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value).ToString());
				base.OpenEndorsementDetails();
				ClientScript.RegisterStartupScript(this.GetType(),"REFRESHGRID","<script>RefreshWebGrid(1," + hidVehicleID.Value + ");</script>");

			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21")+ " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				//ExceptionManager.Publish(ex);
			}
			finally
			{
				lblMessage.Visible = true;
				//				if(objDriverDetail!= null)
				//					objDriverDetail.Dispose();
			}
		}

		private void SetCaptions()
		{
			capINSURED_VEH_NUMBER.Text			=		objResourceMgr.GetString("txtINSURED_VEH_NUMBER");
			capVEHICLE_YEAR.Text				=		objResourceMgr.GetString("cmbVEHICLE_YEAR");
			capMAKE.Text						=		objResourceMgr.GetString("cmbMAKE");
			capMODEL.Text						=		objResourceMgr.GetString("cmbMODEL");
			capVIN.Text							=		objResourceMgr.GetString("txtVIN");
			capGRG_ADD1.Text					=		objResourceMgr.GetString("txtGRG_ADD1");
			capGRG_ADD2.Text					=		objResourceMgr.GetString("txtGRG_ADD2");
			capGRG_CITY.Text					=		objResourceMgr.GetString("txtGRG_CITY");
			capGRG_COUNTRY.Text					=		objResourceMgr.GetString("cmbGRG_COUNTRY");
			capGRG_STATE.Text					=		objResourceMgr.GetString("cmbGRG_STATE");
			capGRG_ZIP.Text						=		objResourceMgr.GetString("txtGRG_ZIP");
			capREGISTERED_STATE.Text			=		objResourceMgr.GetString("cmbREGISTERED_STATE");
			capTERRITORY.Text					=		objResourceMgr.GetString("txtTERRITORY");
			capAMOUNT.Text						=		objResourceMgr.GetString("txtAMOUNT");
			capVEHICLE_AGE.Text					=		objResourceMgr.GetString("txtVEHICLE_AGE");
			capMOTORCYCLE_TYPE.Text				= 		objResourceMgr.GetString("cmbMOTORCYCLE_TYPE");
			capVEHICLE_CC.Text					=		objResourceMgr.GetString("txtVEHICLE_CC");	
			capSYMBOL.Text                      =       objResourceMgr.GetString("txtSYMBOL");  
			capCYCL_REGD_ROAD_USE.Text			=		objResourceMgr.GetString("cmbCYCL_REGD_ROAD_USE");
			capCOMPRH_ONLY.Text                 =       objResourceMgr.GetString("cmbCOMPRH_ONLY");  			
		}
		

		private void GetOldDataXML()
		{
			string strCUSTOMER_ID = "";
			// If VEHICLE_ID is passed then it is a case of update else it is a case of add
			if (Request.QueryString["VH_ID"]!=null && Request.QueryString["VH_ID"].ToString()!="") // UPDATE MODE
			{			
				strCUSTOMER_ID = GetCustomerID();
				hidCustomerID.Value =GetCustomerID();
				hidPolicyID.Value =Request.QueryString["POLICY_ID"].ToString();
				hidPolicyVersionID.Value =Request.QueryString["POLICY_VERSION_ID"].ToString();
				hidVehicleID.Value = Request["VH_ID"].ToString(); 
				hidOldData.Value = @ClsVehicleInformation.FetchCycleXMLFromPolicyVehicleTable(int.Parse(hidCustomerID.Value==""?"0": hidCustomerID.Value) ,int.Parse (hidPolicyID.Value ==""?"0":hidPolicyID.Value),int.Parse (hidPolicyVersionID.Value==""?"0":hidPolicyVersionID.Value),int.Parse (hidVehicleID.Value==""?"0":hidVehicleID.Value));

                if (ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value).ToString() != "")//Added By Pradeep Kushwaha on 10-sep-2010
                    btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value).ToString());
                // itrack no 867
                //if (ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value).ToString() == "N") {
                //    btnActivateDeactivate.Visible = false;
                //}
            
			}
			else
			{
				// IN ADD NEW CASE - we will take the customerid from the session		
				strCUSTOMER_ID = GetCustomerID();
				hidCustomerID.Value =GetCustomerID();								
				string strPOLICY_ID =GetPolicyID();
				string strPOLICY_VERSION_ID= GetPolicyVersionID(); 
				hidPolicyID.Value =strPOLICY_ID;
				hidPolicyVersionID.Value =strPOLICY_VERSION_ID;						
				txtINSURED_VEH_NUMBER.Text = "To be generated";
				hidVehicleID.Value = "NEW";								
			}			
			hidAPP_LOB.Value = GetLOBID();
		}

        private void LoadModelType()
        {
            if (hidVehicleID.Value != "0" && hidVehicleID.Value != "" && hidVehicleID.Value != "NEW") 
            {
                DataSet dsVeh = @ClsVehicleInformation.FetchCycleInfoFromPolicyVehicleTable(int.Parse(hidCustomerID.Value == "" ? "0" : hidCustomerID.Value), int.Parse(hidPolicyID.Value == "" ? "0" : hidPolicyID.Value), int.Parse(hidPolicyVersionID.Value == "" ? "0" : hidPolicyVersionID.Value), int.Parse(hidVehicleID.Value == "" ? "0" : hidVehicleID.Value));

                DataTable dtVeh = dsVeh.Tables[0];

                if (dtVeh.Rows.Count != 0)
                {                    
                    if (dtVeh.Rows[0]["MAKE"] != System.DBNull.Value)
                    {
                        cmbMAKE.SelectedValue = dtVeh.Rows[0]["MAKE"].ToString();
                    }

                    DataSet dsModel = AjaxFetchVehicleModelType(int.Parse(dtVeh.Rows[0]["MAKE"].ToString()));

                    cmbMODEL.DataSource = dsModel.Tables[0];
                    cmbMODEL.DataTextField = "MODEL";
                    cmbMODEL.DataValueField = "ID";
                    cmbMODEL.DataBind();
                    cmbMODEL.Items.Insert(0, "");

                    cmbMOTORCYCLE_TYPE.DataSource = dsModel.Tables[1];
                    cmbMOTORCYCLE_TYPE.DataTextField = "MODEL_TYPE";
                    cmbMOTORCYCLE_TYPE.DataValueField = "ID";
                    cmbMOTORCYCLE_TYPE.DataBind();
                    cmbMOTORCYCLE_TYPE.Items.Insert(0, "");

                    if (dtVeh.Rows[0]["MODEL"] != System.DBNull.Value)
                    {
                        cmbMODEL.SelectedValue = dtVeh.Rows[0]["MODEL"].ToString();
                        hidMODEL.Value = dtVeh.Rows[0]["MODEL"].ToString();
                        cmbMODEL.SelectedIndex = cmbMODEL.Items.IndexOf(cmbMODEL.Items.FindByValue(cmbMODEL.SelectedValue));
                    }

                    if (dtVeh.Rows[0]["MOTORCYCLE_TYPE"] != System.DBNull.Value)
                    {
                        cmbMOTORCYCLE_TYPE.SelectedValue = dtVeh.Rows[0]["MOTORCYCLE_TYPE"].ToString();
                        hidMotorType.Value = dtVeh.Rows[0]["MOTORCYCLE_TYPE"].ToString();
                        cmbMOTORCYCLE_TYPE.SelectedIndex = cmbMOTORCYCLE_TYPE.Items.IndexOf(cmbMOTORCYCLE_TYPE.Items.FindByValue(cmbMOTORCYCLE_TYPE.SelectedValue));
                    }
                }
            }


        }
		private void SetWorkflow()
		{
			if(base.ScreenId == "231_0")
			{
				myWorkFlow.IsTop	=	false;
				myWorkFlow.ScreenID	=	base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID",GetCustomerID());
				myWorkFlow.AddKeyValue("POLICY_ID",GetPolicyID());
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",GetPolicyVersionID());
				if (hidVehicleID.Value != "0" && hidVehicleID.Value != ""  && hidVehicleID.Value.ToUpper() != "NEW")
					myWorkFlow.AddKeyValue("VEHICLE_ID",hidVehicleID.Value);
				myWorkFlow.GetScreenStatus();
				myWorkFlow.SetScreenStatus();
			}
			else
				myWorkFlow.Display	=	false;
		}
		
		private void SelectClassAndType()
		{
			DataTable dtClassTypeInfo;			
			dtClassTypeInfo	=	ClsVehicleInformation.GetClassAndTypeForMotorcycle(txtMAKE.Text,txtMODEL.Text);	
			if(dtClassTypeInfo!=null)
			{
				if(dtClassTypeInfo.Rows.Count>0)
				{
					if(dtClassTypeInfo.Rows[0]["TypeId"] != null)
						cmbMOTORCYCLE_TYPE.SelectedValue = dtClassTypeInfo.Rows[0]["TypeId"].ToString();
					//				if(dtClassTypeInfo.Rows[0]["ClassId"] != null)										
					//				{
					//					cmbAPP_VEHICLE_CLASS.ClearSelection();
					//					ListItem li=cmbAPP_VEHICLE_CLASS.Items.FindByText(dtClassTypeInfo.Rows[0]["ClassId"].ToString());
					//					if(li!=null)
					//						li.Selected=true;
					//					//cmbAPP_VEHICLE_CLASS.SelectedItem.Text	= dtClassTypeInfo.Rows[0]["ClassId"].ToString();						
					//				}
					//cmbAPP_VEHICLE_CLASS.SelectedValue			= dtClassTypeInfo.Rows[0]["ClassId"].ToString();				
				}
			}
		}

		//Ajax Added For Zip.  
		#region AJAX CALLS 
		[Ajax.AjaxMethod()]
		public string AjaxFetchTerritoryForZipStateLob(string zipId,int lobId, int stateId,int intCustomerId,int intAppId,int intAppVersionId,string calledFrom,int intvehicleuse) 
		{
			CmsWeb.webservices.ClsWebServiceCommon obj =  new CmsWeb.webservices.ClsWebServiceCommon();
			string result = "";
			result = obj.FetchTerritoryForZipStateLob(zipId,lobId, stateId,intCustomerId,intAppId,intAppVersionId,calledFrom,intvehicleuse);
			return result;
		}
		#endregion
        //Added by kuldeep to calculate erate premium at the time of submit
        protected void Generate_Premium()
        {
            //Cms.Application.Aspx.Quote.Quote objQuote = new Application.Aspx.Quote.Quote("QUOTE_POL", int.Parse(hidCustomerID.Value), int.Parse(hidPolicyID.Value), int.Parse(hidPolicyVersionID.Value), hidAPP_LOB.Value, 0, "false", "0",false);
            //objQuote.Generate_Quote_Details();

        }

	}
    

}
