/******************************************************************************************
<Author				: -  Vijay Arora
<Start Date			: -	 03-11-2005
<End Date			: -	 
<Description		: -  Contains details for Vehicle Information. 
<Review Date		: - 
<Reviewed By		: - 	

<Modified Date			: - 13/02/2006
<Modified By			: - Shafee
<Purpose				: - Implementation The Buisness Logic For Reject/Reduce Field

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
using Cms.BusinessLayer.BlCommon;
using Cms.CmsWeb;
using Cms.ExceptionPublisher;
using Cms.CmsWeb.Controls; 
using System.Xml;
using Cms.Model.Policy; 

namespace Cms.Policies.Aspx
{
	/// <summary>
	/// 
	/// </summary>
	public class PolicyVehicleInformation: Cms.Policies.policiesbase	
	{
		#region Page controls declaration
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidTERRITORY;        
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidVEHICLE_AGE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_VEHICLE_COMCLASS_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidVEHICLE_TYPE_PER;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidState;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_EFFECTIVE_YEAR;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_EFFECTIVE_MONTH;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidVehicleID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAppVersionID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidVIN;
		protected System.Web.UI.WebControls.Label capMILES_TO_WORK;
		protected System.Web.UI.WebControls.TextBox txtMILES_TO_WORK;		
		protected System.Web.UI.WebControls.Label capClassDesc;
		protected System.Web.UI.WebControls.DropDownList cmbClassDesc;		
		protected System.Web.UI.WebControls.RangeValidator rngMILES_TO_WORK;
		protected System.Web.UI.WebControls.Image imgZipLookup;
		protected System.Web.UI.WebControls.HyperLink hlkZipLookup;
		protected System.Web.UI.WebControls.CustomValidator csvVALID_SYMBOL;
		#endregion

		#region Local form variables
		//START:*********** Local form variables *************
		string oldXML;
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId, strFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPPID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidMakeCode;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAlert;
		//Defining the business layer class object
		ClsVehicleInformation  objVehicleInformation ;
		
		protected int intRetVal;
		private const string CALLED_FROM_CLIENT = "CLT";
		private const string CALLED_FROM_APPLICATION = "APP";
		private const string CALLED_FROM_MOTORCYCLE ="MOT";
		public const string CALLED_FROM_UMBRELLA ="UMB"; 
		private const string CALLED_FROM_PPA ="PPA";
		public string strCalledFrom="";
		protected  int gIntShowVINPopup=0,LOBID=0;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCheckZipSubmit;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSymbolCheck;
		protected System.Web.UI.WebControls.Label lblDelete;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidANTI_LCK_BRAKES;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capVIN;
		protected System.Web.UI.WebControls.TextBox txtVIN;
		protected System.Web.UI.WebControls.ImageButton lnkVINMASTER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvVIN;
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
		protected System.Web.UI.WebControls.Label capBODY_TYPE;
		protected System.Web.UI.WebControls.TextBox txtBODY_TYPE;
		protected System.Web.UI.WebControls.Label capPullClientAddress;
		protected Cms.CmsWeb.Controls.CmsButton btnPullClientAddress;
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
		protected System.Web.UI.WebControls.Label capREGISTERED_STATE;
		protected System.Web.UI.WebControls.DropDownList cmbREGISTERED_STATE;
		protected System.Web.UI.WebControls.Label capTERRITORY;
		protected System.Web.UI.WebControls.TextBox txtTERRITORY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvTERRITORY;
		protected System.Web.UI.WebControls.RegularExpressionValidator revTERRITORY;
		protected System.Web.UI.WebControls.Label capCLASS;
		
		protected System.Web.UI.WebControls.Label capUSE_VEHICLE;
		protected System.Web.UI.WebControls.DropDownList cmbAPP_USE_VEHICLE_ID;
		protected System.Web.UI.WebControls.Label capCLASS_PER;
		protected System.Web.UI.WebControls.DropDownList cmbAPP_VEHICLE_PERCLASS_ID;
		protected System.Web.UI.WebControls.Label capVEHICLE_TYPE_PER;
		protected System.Web.UI.WebControls.DropDownList cmbAPP_VEHICLE_PERTYPE_ID;
		protected System.Web.UI.WebControls.Label capCLASS_COM;
		protected System.Web.UI.WebControls.DropDownList cmbAPP_VEHICLE_COMCLASS_ID;
		protected System.Web.UI.WebControls.Label capVEHICLE_TYPE_COM;
		protected System.Web.UI.WebControls.DropDownList cmbAPP_VEHICLE_COMTYPE_ID;
			

		protected System.Web.UI.WebControls.Label capVEHICLE_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbVEHICLE_TYPE;
		protected System.Web.UI.WebControls.Label capST_AMT_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbST_AMT_TYPE;
		protected System.Web.UI.WebControls.Label capAMOUNT;
		protected System.Web.UI.WebControls.TextBox txtAMOUNT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAMOUNT;
		protected System.Web.UI.WebControls.RegularExpressionValidator revAMOUNT;
		protected System.Web.UI.WebControls.Label capSYMBOL;
		protected System.Web.UI.WebControls.TextBox txtSYMBOL;
		protected System.Web.UI.WebControls.RegularExpressionValidator revSYMBOL;
		protected System.Web.UI.WebControls.Label capVEHICLE_AGE;
		protected System.Web.UI.WebControls.TextBox txtVEHICLE_AGE;
		protected System.Web.UI.WebControls.Label lblAge;
		protected System.Web.UI.WebControls.RegularExpressionValidator revVEHICLE_AGE;
		protected System.Web.UI.WebControls.Label capIS_OWN_LEASE;
		protected System.Web.UI.WebControls.DropDownList cmbIS_OWN_LEASE;
		protected System.Web.UI.WebControls.Label capPURCHASE_DATE;
		protected System.Web.UI.WebControls.TextBox txtPURCHASE_DATE;
		protected System.Web.UI.WebControls.HyperLink hlkPURCHASE_DATE;
		protected System.Web.UI.WebControls.CustomValidator csvPURCHASE_DATE;
		protected System.Web.UI.WebControls.Label capIS_NEW_USED;
		protected System.Web.UI.WebControls.DropDownList cmbIS_NEW_USED;
		protected System.Web.UI.WebControls.Label capVEHICLE_USE;
		protected System.Web.UI.WebControls.DropDownList cmbVEHICLE_USE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvVEHICLE_USE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAUTO_CAR_POOL;
		protected System.Web.UI.WebControls.Label capMULTI_CAR;
		protected System.Web.UI.WebControls.DropDownList cmbMULTI_CAR;
		protected System.Web.UI.WebControls.Label capANNUAL_MILEAGE;
		protected System.Web.UI.WebControls.TextBox txtANNUAL_MILEAGE;
		//protected System.Web.UI.WebControls.RegularExpressionValidator revANNUAL_MILEAGE;
		protected System.Web.UI.WebControls.RangeValidator rngANNUAL_MILEAGE;
		protected System.Web.UI.WebControls.Label capPASSIVE_SEAT_BELT;
		protected System.Web.UI.WebControls.DropDownList cmbPASSIVE_SEAT_BELT;
		protected System.Web.UI.WebControls.Label capAIR_BAG;
		protected System.Web.UI.WebControls.DropDownList cmbAIR_BAG;
		protected System.Web.UI.WebControls.Label capANTI_LOCK_BRAKES;
		protected System.Web.UI.WebControls.DropDownList cmbANTI_LOCK_BRAKES;
		//protected System.Web.UI.WebControls.Label capUNDERINS_MOTOR_INJURY_COVE;
		//protected System.Web.UI.WebControls.DropDownList cmbUNDERINS_MOTOR_INJURY_COVE;
		//protected System.Web.UI.WebControls.Label capUNINS_MOTOR_INJURY_COVE;
		//protected System.Web.UI.WebControls.DropDownList cmbUNINS_MOTOR_INJURY_COVE;
		//protected System.Web.UI.WebControls.Label capUNINS_PROPERTY_DAMAGE_COVE;
		//protected System.Web.UI.WebControls.DropDownList cmbUNINS_PROPERTY_DAMAGE_COVE;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnCopy;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected System.Web.UI.HtmlControls.HtmlImage imgSelect;
		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_LOB;
		//protected System.Web.UI.HtmlControls.HtmlTableRow row1;
		//protected System.Web.UI.HtmlControls.HtmlTableRow row2;
		protected System.Web.UI.WebControls.LinkButton lnkVINMASTER1;
		protected System.Web.UI.WebControls.RegularExpressionValidator revPURCHASE_DATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvUSE_VEHICLE;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyVersionID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSYMBOL;
		//protected System.Web.UI.WebControls.Label capSAFETY_BELT;
		//protected System.Web.UI.WebControls.DropDownList cmbSAFETY_BELT;
		protected System.Web.UI.WebControls.Label capBUSS_PERM_RESI;
		protected System.Web.UI.WebControls.DropDownList cmbBUSS_PERM_RESI;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAPP_VEHICLE_PERTYPE_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAPP_VEHICLE_COMTYPE_ID;
		protected System.Web.UI.WebControls.Label capSNOWPLOW_CONDS;
		protected System.Web.UI.WebControls.DropDownList cmbSNOWPLOW_CONDS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSNOWPLOW_CONDS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMILES_TO_WORK;
		protected System.Web.UI.WebControls.Label capCAR_POOL;
		protected System.Web.UI.WebControls.DropDownList cmbCAR_POOL;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidVehCount;
		protected System.Web.UI.WebControls.Label capAUTO_POL_NO;
		protected System.Web.UI.WebControls.TextBox txtAUTO_POL_NO;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAUTO_POL_NO;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSetIndex;
		protected System.Web.UI.WebControls.Label capRADIUS_OF_USE;
		protected System.Web.UI.WebControls.Label capTRANSPORT_CHEMICAL;
		protected System.Web.UI.WebControls.Label capCOVERED_BY_WC_INSU;
		protected System.Web.UI.WebControls.TextBox txtRADIUS_OF_USE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvRADIUS_OF_USE;
		protected System.Web.UI.WebControls.RangeValidator rngRADIUS_OF_USE;
		protected System.Web.UI.WebControls.DropDownList cmbTRANSPORT_CHEMICAL;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvTRANSPORT_CHEMICAL;
		protected System.Web.UI.WebControls.DropDownList cmbCOVERED_BY_WC_INSU;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOVERED_BY_WC_INSU;
		protected System.Web.UI.WebControls.DropDownList cmbCommClass;
		protected System.Web.UI.WebControls.Label capCommClass;
		protected System.Web.UI.WebControls.CustomValidator csvGRG_ZIP;
		//Added by Asfa(11-July-2008) - iTrack #4471
		protected System.Web.UI.WebControls.RegularExpressionValidator revHEXA_DECIMAL;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvBUSS_PERM_RESI;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvREGISTERED_STATE;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;

		//added by Pravesh on 4 nov Itrack 4948
		protected System.Web.UI.WebControls.Label capIS_SUSPENDED;
		protected System.Web.UI.WebControls.DropDownList cmbIS_SUSPENDED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_SUSPENDED;	 
		// end here

		//added by Praveen Kumar(02-03-2009):Itrack 5518
		protected System.Web.UI.WebControls.Label capIS_SUSPENDED_COM;
		protected System.Web.UI.WebControls.DropDownList cmbIS_SUSPENDED_COM;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_SUSPENDED_COM;	 
		//End Praveen Kumar
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidINSURED_VEH_NUMBER;
		//END:*********** Local variables *************

        protected System.Web.UI.HtmlControls.HtmlInputHidden hidVEHICLE_COUNT;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidJAVASCRIPT_MSG;
		#endregion

		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetErrorMessages()
		{
			//Added by Asfa(11-July-2008) - iTrack #4471
			revHEXA_DECIMAL.ValidationExpression	= aRegExpHexadecimal;
			revHEXA_DECIMAL.ErrorMessage			= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");

			rfvSYMBOL.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("608");
			rfvINSURED_VEH_NUMBER.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"103");
			rfvVEHICLE_YEAR.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"104");
			rfvMAKE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"105");
			rfvMODEL.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"106");
			rfvVIN.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"461");
			revPURCHASE_DATE.ValidationExpression			= aRegExpDate;
			revPURCHASE_DATE.ErrorMessage					=  "<br>" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
			rfvGRG_CITY.ErrorMessage	= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"56");
			//	rfvGRG_COUNTRY.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"33");
			rfvGRG_STATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"35");
			//rfvCLASS.ErrorMessage		= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"219");
			rfvGRG_ZIP.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"37");
			revGRG_ZIP.ValidationExpression	=  aRegExpZip;
			revGRG_ZIP.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"24");
			rfvTERRITORY.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"108");
			 
			/*Modified by Asfa(11-July-2008) - iTrack #4443
			//revAMOUNT.ValidationExpression	= aRegExpDoublePositiveNonZero;
			*/
			revAMOUNT.ValidationExpression	= aRegExpPositiveCurrency; //Changed from aRegExpCurrencyformat,Charles,23-Nov-09,Itrack 6763
			revAMOUNT.ErrorMessage			= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"492");//Changed from 116,Charles,23-Nov-09,Itrack 6763
			rfvGRG_ADD1.ErrorMessage		= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"32");
			rfvAMOUNT.ErrorMessage			=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"335");
			revVEHICLE_AGE.ValidationExpression =aRegExpDouble;
			revVEHICLE_AGE.ErrorMessage 	= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"163");
		
			revSYMBOL.ValidationExpression =aRegExpInteger;
			revSYMBOL.ErrorMessage =Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"163");

			rngVEHICLE_YEAR.MaximumValue = (DateTime.Now.Year+1).ToString();
			rngVEHICLE_YEAR.MinimumValue = aAppMinYear  ;
			rngVEHICLE_YEAR.ErrorMessage =  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"221");

			revTERRITORY.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"222");
			revTERRITORY.ValidationExpression =aRegExpInteger;
			

			//			revPURCHASE_DATE.ValidationExpression		=	aRegExpDate;
			//			revPURCHASE_DATE.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"22");
			//revAMOUNT_COST_NEW.ValidationExpression     =  aRegExpDoublePositiveNonZero;
			//revP_SURCHARGES.ValidationExpression		=	aRegExpCurrencyformat;
			//revAMOUNT_COST_NEW.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"116");
			//revANNUAL_MILEAGE.ValidationExpression      =  aRegExpDouble;
			//revANNUAL_MILEAGE.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"126");
			//revP_SURCHARGES.ValidationExpression		=  aRegExpCurrency;
			//revP_SURCHARGES.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"125");
			rngANNUAL_MILEAGE.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"195");
			//rngANNUAL_MILEAGE.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"126");
			
			csvPURCHASE_DATE.ErrorMessage	= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"197");
			//rfvAMOUNT_COST_NEW.ErrorMessage		= 	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"403");
			rfvVEHICLE_USE.ErrorMessage		= 	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"404");
			rngMILES_TO_WORK.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");
			rfvMILES_TO_WORK.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("675");
			rfvAUTO_POL_NO.ErrorMessage		=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("839");
			rfvAUTO_CAR_POOL.ErrorMessage		=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1026");
			rfvRADIUS_OF_USE.ErrorMessage	=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("845");
			rfvTRANSPORT_CHEMICAL.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("846");
			rfvCOVERED_BY_WC_INSU.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("847");
			rngRADIUS_OF_USE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");
			csvGRG_ZIP.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("898");
			rfvSNOWPLOW_CONDS.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1027");
			rfvREGISTERED_STATE.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1029");

			
		}
		#endregion
		
		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{
		    //Ajax Added For Zip By Raghav.
			Ajax.Utility.RegisterTypeForAjax(typeof(PolicyVehicleInformation));  
			
			#region setting screen id
			if (Request.QueryString["CALLEDFROM"]!=null && Request.QueryString["CALLEDFROM"].ToString().Trim()!="")
			{
				strCalledFrom = Request.QueryString["CALLEDFROM"].ToString().Trim();	
				
			}
			switch(strCalledFrom.ToUpper())
			{
				case "ppa" :
				case "PPA" :				
					base.ScreenId	=	"227_0";
					break;
				case "mot" :
				case "MOT" :
					base.ScreenId	=	"231_0";
					break;
				case "UMB" :
					base.ScreenId	=	"275_0";
					break;
			}
			#endregion

            //base.RequiredPullCustAddWithCounty(txtGRG_ADD1,txtGRG_ADD2,txtGRG_CITY
            //    ,cmbGRG_COUNTRY,cmbGRG_STATE,txtGRG_ZIP,null,txtTERRITORY
            //    ,btnPullClientAddress);

            //Added By Lalit For read only filed
            txtTERRITORY.Text=hidTERRITORY.Value;
            base.RequiredPullCustAddWithCounty(txtGRG_ADD1, txtGRG_ADD2, txtGRG_CITY
            , cmbGRG_COUNTRY, cmbGRG_STATE, txtGRG_ZIP, null, txtTERRITORY
            , btnPullClientAddress);

			hlkPURCHASE_DATE.Attributes.Add("OnClick","fPopCalendar(document.APP_VEHICLES.txtPURCHASE_DATE,document.APP_VEHICLES.txtPURCHASE_DATE)"); //Javascript Implementation for Calender		
			//btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");
			btnReset.Attributes.Add("onclick","javascript:return ResetTheForm();");
			btnCopy.Attributes.Add("onclick","Javascript:DisableValidators();ShowCustomerVehicle(); return false;");
			txtAMOUNT.Attributes.Add("onBlur","this.value=formatCurrency(this.value);GetSymbol();");//DisableValidators(); Removed by Charles on 23-Nov-09 for Itrack 6763
			txtAMOUNT.Attributes.Add("onkeypress","javascript:if(event.keyCode==13){this.value=formatCurrency(this.value);GetSymbol();}"); //Added by Charles on 23-Nov-09 for Itrack 6763
			txtVEHICLE_YEAR.Attributes.Add ("onBlur","javascript:GetAgeOfVehicle();");
			imgZipLookup.   Attributes.Add("style","cursor:hand");
			txtSYMBOL.Attributes.Add("onBlur","javascript:txtSymbolMatch();");
			base.VerifyAddress(hlkZipLookup, txtGRG_ADD1,txtGRG_ADD2
				, txtGRG_CITY, cmbGRG_STATE, txtGRG_ZIP);
			//cmbUNINS_MOTOR_INJURY_COVE.Attributes.Add ("onChange","RejectUninsured();");
			string url=ClsCommon.GetLookupURL();			  

			//txtGRG_ZIP.Attributes.Add("OnBlur","javascript:DisableValidators();GetTerritory();");
			txtGRG_ZIP.Attributes.Add("OnBlur","javascript:GetZipForState();");
			
			lblMessage.Visible = false;
			SetErrorMessages();

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass	=	CmsButtonType.Write;
			btnReset.PermissionString		=	gstrSecurityXML;

			btnPullClientAddress.CmsButtonClass = CmsButtonType.Write;
			btnPullClientAddress.PermissionString = gstrSecurityXML;
			
			btnPullClientAddress.CausesValidation=false;

			btnDelete.CmsButtonClass	=	Cms.CmsWeb.Controls.CmsButtonType.Delete;
			btnDelete.PermissionString	=	gstrSecurityXML;

			btnCopy.CmsButtonClass	=	CmsButtonType.Write;
			btnCopy.PermissionString		=	gstrSecurityXML;

			btnSave.CmsButtonClass	=	CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;

			btnActivateDeactivate.CmsButtonClass = CmsButtonType.Write;
			btnActivateDeactivate.PermissionString = gstrSecurityXML;
			
			//Attaching the javascript function on click event
			btnPullClientAddress.Attributes.Add("onClick","javascript:PullCustomerAddress("
				+ "document.getElementById('" + txtGRG_ADD1.ID + "'),"
				+ "document.getElementById('" + txtGRG_ADD2.ID + "'),"
				+ "document.getElementById('" + txtGRG_CITY.ID + "'),"
				+ "document.getElementById('" + cmbGRG_COUNTRY.ID + "'),"
				+ "document.getElementById('" + cmbGRG_STATE.ID + "'),"
				+ "document.getElementById('" + txtGRG_ZIP.ID + "'),"
				+ "null,"
				+ "document.getElementById('" + txtTERRITORY.ID + "')"
				+ ");SetRegisteredState();return false;");

			
			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Aspx.PolicyVehicleInformation" ,System.Reflection.Assembly.GetExecutingAssembly());
			if(!Page.IsPostBack)
			{

				//txtVIN.BackColor = System.Drawing.Color.White; 
				btnSave.Attributes.Add("onclick","javascript:return Validate();");
				cmbAPP_VEHICLE_PERTYPE_ID.Attributes.Add("onChange","javascript:shwBUSS_PERM_RESI();ShowHidePopUpIcon();SetPerClass();ShowPerClass();CheckValidateVIN();DefaultVehicleUse();fxnDispSnowPlow();");//Added DefaultVehicleUse(),fxnDispSnowPlow() by Sibin on Itrack Issue 4884 on 2 Jan 08
				cmbAPP_VEHICLE_COMTYPE_ID.Attributes.Add("onChange","javascript:shwIS_SUSPENDED();");
				if (Request.QueryString["CALLEDFROM"]!=null && Request.QueryString["CALLEDFROM"].ToString().Trim()!="")
				{
					strCalledFrom = Request.QueryString["CALLEDFROM"].ToString().Trim();				
				}

				hidCalledFrom.Value =strCalledFrom.ToUpper();
				GetOldDataXML();
				SetCaptions();
				getPolicyEffectiveYear();
				txtANNUAL_MILEAGE.Attributes.Add("onBlur","this.value=formatCurrency(this.value);");
				FillDropdowns();
				int CustId = int.Parse(GetCustomerID());
				int PolId =int.Parse(GetPolicyID());
				int PolVerId=int.Parse(GetPolicyVersionID());
				
				//Itrack 5130
				//fxnSetMultiCarOptions();

				SetClassMode();
								
				//int stateId = ClsVehicleInformation.GetStateIdForpolicy(CustId,hidPolicyID.Value,hidPolicyVersionID.Value);
				// Check if state is Indiana then only display the rows containing the questions.
				/*if (stateId == 14)
				{
					row1.Visible=true;
					row2.Visible=true;
				}
				else
				{
					row1.Visible=false;
					row2.Visible=false;
				}*/			
				
				//------------------End----------------------------------.

				if (hidVehicleID.Value != "NEW") 
				{
					LoadVehicleUseClassType();
				}
				 
				hidVIN.Value = "0";
				EnableDisableTerritory();
		
				 
			}
			else
			{
				if (hidVIN.Value == "1")
				{
					operateVINMASTER();
					hidVIN.Value = "0";
				}
					
			}
			if (hidCheckZipSubmit.Value=="zip")
			{
				int result=0;
				result=ClsVehicleInformation.FetchTerritoryForZip(txtGRG_ZIP.Text,int.Parse(hidAPP_LOB.Value));	
				if (result != 0)
				{
					txtTERRITORY.Text=result.ToString();
                    hidTERRITORY.Value = result.ToString();
				}
				

				//save the lobid
				hidAPP_LOB.Value = GetLOBID();
			}
			//GetSymbolFromXML();
			SetWorkFlowControl();

			//Added by Swastika on 7th Mar'06 for Pol Iss #59
			if (Request.Form["__EVENTTARGET"] == "Deactive")
			{   
				DoInActive(hidAlert.Value);
			}

			if (Request.Form["__EVENTTARGET"] == "DeleteVehicle")
			{   
				DoDelete(hidAlert.Value);
			}

			//Itrack 5310
			int vehCount = 0;
			vehCount = getPolVehicleCount();
			
			//Added by Lalit FOr Activate deactivate vehicle
            btnActivateDeactivate.Attributes.Add("onclick", "javascript:return ActivateDeactivateVehicle();");
            btnDelete.Attributes.Add("onclick", "javascript:return ActivateDeactivateVehicle();");

		}//end pageload
		#endregion
		
		/// <summary>
		/// This is used to fill the vehicle use, class and type.
		/// </summary>
		private void LoadVehicleUseClassType()
		{
			objVehicleInformation = new  ClsVehicleInformation();
			
			if(strCalledFrom.ToUpper()=="PPA")
				objVehicleInformation.SetPolicyVehicleClassType(int.Parse(hidCustomerID.Value==""?"0": hidCustomerID.Value) ,
					int.Parse (hidPolicyID.Value ==""?"0":hidPolicyID.Value),
					int.Parse (hidPolicyVersionID.Value==""?"0":hidPolicyVersionID.Value),
					int.Parse (hidVehicleID.Value==""?"0":hidVehicleID.Value),
					cmbAPP_USE_VEHICLE_ID,cmbAPP_VEHICLE_PERCLASS_ID,cmbAPP_VEHICLE_PERTYPE_ID,cmbAPP_VEHICLE_COMCLASS_ID,cmbAPP_VEHICLE_COMTYPE_ID);
			else if(strCalledFrom.ToUpper()=="UMB")
				objVehicleInformation.SetPolicyUmbrellaVehicleClassType(int.Parse(hidCustomerID.Value==""?"0": hidCustomerID.Value) ,
					int.Parse (hidPolicyID.Value ==""?"0":hidPolicyID.Value),
					int.Parse (hidPolicyVersionID.Value==""?"0":hidPolicyVersionID.Value),
					int.Parse (hidVehicleID.Value==""?"0":hidVehicleID.Value),
					cmbAPP_USE_VEHICLE_ID,cmbAPP_VEHICLE_PERCLASS_ID,cmbAPP_VEHICLE_PERTYPE_ID,cmbAPP_VEHICLE_COMCLASS_ID,cmbAPP_VEHICLE_COMTYPE_ID);
				 
						
		}


		private int getPolicyEffectiveYear()
		{	
			string strEffDate;			
			string strEffYear;
			string strEffmonth;
			strEffDate  = new Cms.BusinessLayer.BlApplication.clsWatercraftInformation().GetPolEffectiveDate(int.Parse(GetCustomerID()),int.Parse(GetPolicyID()),int.Parse(GetPolicyVersionID()));			
			strEffYear  = Convert.ToDateTime(strEffDate).Year.ToString();   // Year component of POLICY_EFFECTIVE_DATE
			hidAPP_EFFECTIVE_YEAR.Value = strEffYear;
			strEffmonth =  Convert.ToDateTime(strEffDate).Month.ToString(); 
			hidAPP_EFFECTIVE_MONTH.Value =  strEffmonth; 
			return (int.Parse(strEffYear));			
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
				//Added by Swastika on 7th Mar'06 for #10
				cmbGRG_COUNTRY.Items.Insert(0,"");
				cmbGRG_COUNTRY.SelectedIndex = 1;
	
				
				dt = Cms.CmsWeb.ClsFetcher.State;


				cmbGRG_STATE.DataSource		=Cms.CmsWeb.ClsFetcher.ActiveState;
				cmbGRG_STATE.DataTextField	= "State_Name";
				cmbGRG_STATE.DataValueField	= "State_Id";
				cmbGRG_STATE.DataBind();
				cmbGRG_STATE.Items.Insert(0,"");

				cmbREGISTERED_STATE.DataSource =dt;
				cmbREGISTERED_STATE.DataTextField	= "State_Name";
				cmbREGISTERED_STATE.DataValueField	= "State_Id";
				cmbREGISTERED_STATE.DataBind();
				cmbREGISTERED_STATE.Items.Insert(0,"");

				cmbAPP_USE_VEHICLE_ID.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("VHUCP");
				cmbAPP_USE_VEHICLE_ID.DataTextField	= "LookupDesc";
				cmbAPP_USE_VEHICLE_ID.DataValueField	= "LookupID";
				cmbAPP_USE_VEHICLE_ID.DataBind();
				cmbAPP_USE_VEHICLE_ID.Items.Insert(0,"");
				cmbAPP_USE_VEHICLE_ID.SelectedIndex = 2;

				cmbAPP_VEHICLE_PERCLASS_ID.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("VHCLSP");
				cmbAPP_VEHICLE_PERCLASS_ID.DataTextField	= "LookupDesc";
				cmbAPP_VEHICLE_PERCLASS_ID.DataValueField	= "LookupID";
				cmbAPP_VEHICLE_PERCLASS_ID.DataBind();

				int CustId = int.Parse(GetCustomerID());
				int AppId =int.Parse(GetAppID());
				int AppVerId=int.Parse(GetAppVersionID());								
				int stateId = ClsVehicleInformation.GetStateIdForpolicy(CustId,hidPolicyID.Value,hidPolicyVersionID.Value);
				hidState.Value = stateId.ToString();
				// CHECK IF STATE IS MISHIGAN THEN REMOVE EXTRA OPTIONS.
				//22 MISHIGAN AND 14 INDIANA
				if (stateId == 22)
				{
					ListItem li;

					//6A Principal Female Operator - Age 21-24
					li = cmbAPP_VEHICLE_PERCLASS_ID.Items.FindByValue("11491");
					if (li != null)
						cmbAPP_VEHICLE_PERCLASS_ID.Items.Remove(li);

					//6B Principal Female Operator - Age 18-20
					li = cmbAPP_VEHICLE_PERCLASS_ID.Items.FindByValue("11492");
					if (li != null)
						cmbAPP_VEHICLE_PERCLASS_ID.Items.Remove(li);

					//6C Principal Female Operator - Age 16-17
					li = cmbAPP_VEHICLE_PERCLASS_ID.Items.FindByValue("11493");
					if (li != null)
						cmbAPP_VEHICLE_PERCLASS_ID.Items.Remove(li);
				
				
				}


				cmbAPP_VEHICLE_PERCLASS_ID.Items.Insert(0,"");

				cmbAPP_VEHICLE_COMCLASS_ID.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("VHCLSC");
				cmbAPP_VEHICLE_COMCLASS_ID.DataTextField	= "LookupDesc";
				cmbAPP_VEHICLE_COMCLASS_ID.DataValueField	= "LookupID";
				cmbAPP_VEHICLE_COMCLASS_ID.DataBind();
				cmbAPP_VEHICLE_COMCLASS_ID.Items.Insert(0,"");

				cmbAPP_VEHICLE_PERTYPE_ID.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("VHTYPP");
				cmbAPP_VEHICLE_PERTYPE_ID.DataTextField	= "LookupDesc";
				cmbAPP_VEHICLE_PERTYPE_ID.DataValueField	= "LookupID";
				cmbAPP_VEHICLE_PERTYPE_ID.DataBind();
				cmbAPP_VEHICLE_PERTYPE_ID.Items.Insert(0,"");

				cmbBUSS_PERM_RESI.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
				cmbBUSS_PERM_RESI.DataTextField="LookupDesc"; 
				cmbBUSS_PERM_RESI.DataValueField="LookupID";
				cmbBUSS_PERM_RESI.DataBind();
				cmbBUSS_PERM_RESI.Items.Insert(0,"");

				
				cmbSNOWPLOW_CONDS.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("SPCND");
				cmbSNOWPLOW_CONDS.DataTextField="LookupDesc"; 
				cmbSNOWPLOW_CONDS.DataValueField="LookupID";
				cmbSNOWPLOW_CONDS.DataBind();
				cmbSNOWPLOW_CONDS.Items.Insert(0,"");

				cmbCAR_POOL.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
				cmbCAR_POOL.DataTextField="LookupDesc"; 
				cmbCAR_POOL.DataValueField="LookupID";
				cmbCAR_POOL.DataBind();
				cmbCAR_POOL.Items.Insert(0,"");

				cmbTRANSPORT_CHEMICAL.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
				cmbTRANSPORT_CHEMICAL.DataTextField="LookupDesc"; 
				cmbTRANSPORT_CHEMICAL.DataValueField="LookupID";
				cmbTRANSPORT_CHEMICAL.DataBind();
				cmbTRANSPORT_CHEMICAL.Items.Insert(0,"");

				cmbCOVERED_BY_WC_INSU.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
				cmbCOVERED_BY_WC_INSU.DataTextField="LookupDesc"; 
				cmbCOVERED_BY_WC_INSU.DataValueField="LookupID";
				cmbCOVERED_BY_WC_INSU.DataBind();
				cmbCOVERED_BY_WC_INSU.Items.Insert(0,"");

				cmbCommClass.DataSource = clsapplication.GetCommClassDesc();
				cmbCommClass.DataTextField="LOOKUP_VALUE_DESC";
				cmbCommClass.DataValueField="KEY_COL";
				cmbCommClass.DataBind();
				cmbCommClass.Items.Insert(0,"");


				//Classic & Antique car - Grandfathered, for existing policies 
				//only where the inception date is prior to 01/01/2003
				int incepDate = ClsVehicleInformation.GetPolInceptionDate(CustId,int.Parse(hidPolicyID.Value),int.Parse(hidPolicyVersionID.Value));
				if (incepDate > 2002)
				{
					//Classic Car
					ListItem LI = cmbAPP_VEHICLE_PERTYPE_ID.Items.FindByValue("11868");
					if (LI != null)
					{
						cmbAPP_VEHICLE_PERTYPE_ID.Items.Remove(LI);
					}

					//Antique Car
					LI = cmbAPP_VEHICLE_PERTYPE_ID.Items.FindByValue("11869");
					if (LI != null)
					{
						cmbAPP_VEHICLE_PERTYPE_ID.Items.Remove(LI);
					}
					hidSetIndex.Value = "1";
				}

			
				cmbAPP_VEHICLE_COMTYPE_ID.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("VHTYPC");
				cmbAPP_VEHICLE_COMTYPE_ID.DataTextField	= "LookupDesc";
				cmbAPP_VEHICLE_COMTYPE_ID.DataValueField	= "LookupID";
				cmbAPP_VEHICLE_COMTYPE_ID.DataBind();
				cmbAPP_VEHICLE_COMTYPE_ID.Items.Insert(0,"");
				
				cmbST_AMT_TYPE.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("AMT");
				cmbST_AMT_TYPE.DataTextField	= "LookupDesc";
				cmbST_AMT_TYPE.DataValueField	= "LookupID";
				cmbST_AMT_TYPE.DataBind();
				cmbST_AMT_TYPE.Items.Insert(0,"");
				cmbST_AMT_TYPE.SelectedIndex=1;				
				cmbST_AMT_TYPE.Enabled=false;

				cmbVEHICLE_USE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("USECD");
				cmbVEHICLE_USE.DataTextField="LookupDesc"; 
				cmbVEHICLE_USE.DataValueField="LookupID";
				cmbVEHICLE_USE.DataBind();
				cmbVEHICLE_USE.SelectedIndex = 4;


				
				//RP - 18 Aug 2006 - Gen issue 3293 - Changed options to Yes No
				//cmbPASSIVE_SEAT_BELT.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("PRTCD");
				cmbPASSIVE_SEAT_BELT.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
							
				cmbPASSIVE_SEAT_BELT.DataTextField="LookupDesc"; 
				cmbPASSIVE_SEAT_BELT.DataValueField="LookupID";
				cmbPASSIVE_SEAT_BELT.DataBind();
				cmbPASSIVE_SEAT_BELT.Items.Insert(0,"");

				cmbANTI_LOCK_BRAKES.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("yesno");
				cmbANTI_LOCK_BRAKES.DataTextField="LookupDesc"; 
				cmbANTI_LOCK_BRAKES.DataValueField="LookupID";
				cmbANTI_LOCK_BRAKES.DataBind();
				cmbANTI_LOCK_BRAKES.Items.Insert(0,"");

				cmbAIR_BAG.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("%AIRB");
				cmbAIR_BAG.DataTextField="LookupDesc"; 
				cmbAIR_BAG.DataValueField="LookupID"; 
				cmbAIR_BAG.DataBind();
				cmbAIR_BAG.Items.Insert(0,"");

				cmbIS_SUSPENDED.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
				cmbIS_SUSPENDED.DataTextField="LookupDesc"; 
				cmbIS_SUSPENDED.DataValueField="LookupID";
				cmbIS_SUSPENDED.DataBind();
				cmbIS_SUSPENDED.Items.Insert(0,"");
				//Added By Shafi On 17 March 2006

				//ADDED BY PRAVEEN KUMAR(02-03-2009):ITRACK 5518
				cmbIS_SUSPENDED_COM.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
				cmbIS_SUSPENDED_COM.DataTextField="LookupDesc"; 
				cmbIS_SUSPENDED_COM.DataValueField="LookupID";
				cmbIS_SUSPENDED_COM.DataBind();
				cmbIS_SUSPENDED_COM.Items.Insert(0,"");
				//END PRAVEEN KUMAR

				

				if(hidCalledFrom.Value!=CALLED_FROM_UMBRELLA)
				{
					/*cmbSAFETY_BELT.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
					cmbSAFETY_BELT.DataTextField="LookupDesc"; 
					cmbSAFETY_BELT.DataValueField="LookupID";
					cmbSAFETY_BELT.DataBind();
					cmbSAFETY_BELT.Items.Insert(0,"");*/

					cmbMULTI_CAR.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("MLTCR");
					cmbMULTI_CAR.DataTextField="LookupDesc"; 
					cmbMULTI_CAR.DataValueField="LookupID";
					cmbMULTI_CAR.DataBind();
					cmbMULTI_CAR.Items.Insert(0,"");

				}




				/*cmbUNINS_MOTOR_INJURY_COVE.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
				cmbUNINS_MOTOR_INJURY_COVE.DataTextField="LookupDesc";
				cmbUNINS_MOTOR_INJURY_COVE.DataValueField="LookupID";
				cmbUNINS_MOTOR_INJURY_COVE.DataBind();
				cmbUNINS_MOTOR_INJURY_COVE.Items.Insert(0,"");*/

				/*cmbUNINS_PROPERTY_DAMAGE_COVE.DataSource= Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
				cmbUNINS_PROPERTY_DAMAGE_COVE.DataTextField="LookupDesc";
				cmbUNINS_PROPERTY_DAMAGE_COVE.DataValueField="LookupID";
				cmbUNINS_PROPERTY_DAMAGE_COVE.DataBind();
				cmbUNINS_PROPERTY_DAMAGE_COVE.Items.Insert(0,"");*/

				/*cmbUNDERINS_MOTOR_INJURY_COVE.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
				cmbUNDERINS_MOTOR_INJURY_COVE.DataTextField="LookupDesc";
				cmbUNDERINS_MOTOR_INJURY_COVE.DataValueField="LookupID";
				cmbUNDERINS_MOTOR_INJURY_COVE.DataBind();

				cmbUNDERINS_MOTOR_INJURY_COVE.Items.Insert(0,"");*/
			}
			catch(Exception exc)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(exc);
			}
			finally
			{}
		
		}
		/// <summary>
		/// validate posted data from form
		/// </summary>
		/// <returns>True if posted data is valid else false</returns>
		private bool doValidationCheck()
		{
			try
			{
				return true;
			}
			catch// (Exception ex)
			{
				return false;
			}
		}

		
		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsPolicyVehicleInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsPolicyVehicleInfo objVehicleInfo;
			objVehicleInfo = new ClsPolicyVehicleInfo();

			hidCustomerID.Value =GetCustomerID();
			//hidAPPID.Value=GetAppID();
			//hidAppVersionID.Value=GetAppVersionID();
			hidPolicyID.Value = Request["POLICY_ID"].ToString(); 
			hidPolicyVersionID.Value = Request["POLICY_VERSION_ID"].ToString(); 

			objVehicleInfo.INSURED_VEH_NUMBER=	int.Parse("0");
			objVehicleInfo.VEHICLE_YEAR=	txtVEHICLE_YEAR.Text ==null?"":txtVEHICLE_YEAR.Text ;
			objVehicleInfo.MAKE=	txtMAKE.Text ==null?"":txtMAKE.Text ;
			objVehicleInfo.MODEL=	txtMODEL.Text ==null?"":txtMODEL.Text ;
			objVehicleInfo.VIN	=	txtVIN.Text.ToString().ToUpper().Trim();
			objVehicleInfo.BODY_TYPE=	txtBODY_TYPE.Text ==null?"":txtBODY_TYPE.Text ;
			objVehicleInfo.GRG_ADD1=	txtGRG_ADD1.Text;
			objVehicleInfo.GRG_ADD2=	txtGRG_ADD2.Text;
			objVehicleInfo.GRG_CITY=	txtGRG_CITY.Text;
			objVehicleInfo.GRG_COUNTRY=	cmbGRG_COUNTRY.SelectedValue;
			objVehicleInfo.GRG_STATE=	cmbGRG_STATE.SelectedValue;
			objVehicleInfo.GRG_ZIP=	txtGRG_ZIP.Text;
			if(cmbBUSS_PERM_RESI.SelectedValue != null &&  cmbBUSS_PERM_RESI.SelectedValue != "")
				{objVehicleInfo.BUSS_PERM_RESI	=	int.Parse(cmbBUSS_PERM_RESI.SelectedValue);}
			if(cmbAPP_USE_VEHICLE_ID.SelectedValue == "11332")
				if(cmbIS_SUSPENDED.SelectedValue != null &&  cmbIS_SUSPENDED.SelectedValue != "")
					{objVehicleInfo.IS_SUSPENDED	=	int.Parse(cmbIS_SUSPENDED.SelectedValue);}
			//ADDED BY PRAVEEN KUMA(02-03-2009):ITRACK 5518
			if(cmbAPP_USE_VEHICLE_ID.SelectedValue == "11333")
				if ( cmbIS_SUSPENDED_COM.SelectedValue !=null && cmbIS_SUSPENDED_COM.SelectedValue != "")
					objVehicleInfo.IS_SUSPENDED = Convert.ToInt32(cmbIS_SUSPENDED_COM.SelectedValue);
			//END PRAVEEN KUMAR

			
			if(cmbREGISTERED_STATE.SelectedItem!=null)
			{
				objVehicleInfo.REGISTERED_STATE=	cmbREGISTERED_STATE.SelectedValue;
			}
            if (txtTERRITORY.Text != "")
                objVehicleInfo.TERRITORY = txtTERRITORY.Text;
            else
                objVehicleInfo.TERRITORY = hidTERRITORY.Value;
			
			//Commented by Sibin on 27 Feb 09 to correctly pass value in the database
//			if(cmbST_AMT_TYPE.SelectedItem!=null)
//			{
//				objVehicleInfo.ST_AMT_TYPE=	cmbST_AMT_TYPE.SelectedValue;
//			}
			objVehicleInfo.CUSTOMER_ID = int.Parse(hidCustomerID.Value);

			if(txtAMOUNT.Text.Trim()!="")
			{
				objVehicleInfo.AMOUNT=	double.Parse(txtAMOUNT.Text);
			}
			if(txtSYMBOL.Text.Trim()!="")
			{
				objVehicleInfo.SYMBOL=	int.Parse (txtSYMBOL.Text);
			}

            //if(txtVEHICLE_AGE.Text.Trim()!="")
            //{
            //    objVehicleInfo.VEHICLE_AGE=	int.Parse (txtVEHICLE_AGE.Text==""?"0":txtVEHICLE_AGE.Text);
            //}

            //Added By Lait For assign javascript calculated value in readonly control 
            if (hidVEHICLE_AGE.Value != "")
            {
                objVehicleInfo.VEHICLE_AGE = int.Parse(hidVEHICLE_AGE.Value == "" ? "0" : hidVEHICLE_AGE.Value);
            }
            
			objVehicleInfo.POLICY_ID = Convert.ToInt32(hidPolicyID.Value);
			objVehicleInfo.POLICY_VERSION_ID = Convert.ToInt32(hidPolicyVersionID.Value);


			if (hidVehicleID.Value !=null )
			{
				if (hidVehicleID.Value.ToString()=="NEW")
				{
					objVehicleInfo.VEHICLE_ID = 0;// int.Parse (ClsVehicleInformation.GetNewVehicleID(hidCalledFrom.Value));
				}
				else
				{
					objVehicleInfo.VEHICLE_ID = int.Parse (hidVehicleID.Value.ToString());
				}
			}


			objVehicleInfo.IS_OWN_LEASE=	cmbIS_OWN_LEASE.SelectedValue;
			
			if (txtPURCHASE_DATE.Text.Trim()!="")		
			{
				objVehicleInfo.PURCHASE_DATE=Convert.ToDateTime(txtPURCHASE_DATE.Text);	//txtPURCHASE_DATE.Text=="" ? Convert.ToDateTime("1/1/1900"): Convert.ToDateTime(txtPURCHASE_DATE.Text);
			}
			else
			{
				objVehicleInfo.PURCHASE_DATE=Convert.ToDateTime("1/1/1900");
			}
			objVehicleInfo.IS_NEW_USED=	cmbIS_NEW_USED.SelectedValue;		
			objVehicleInfo.VEHICLE_USE=	cmbVEHICLE_USE.SelectedValue;	
			/*if(cmbUNINS_MOTOR_INJURY_COVE.SelectedValue=="10963")
			{
				objVehicleInfo.UNINS_MOTOR_INJURY_COVE    = cmbUNINS_MOTOR_INJURY_COVE.SelectedValue;
				objVehicleInfo.UNDERINS_MOTOR_INJURY_COVE = cmbUNINS_MOTOR_INJURY_COVE.SelectedValue;
				objVehicleInfo.UNINS_PROPERTY_DAMAGE_COVE = cmbUNINS_MOTOR_INJURY_COVE.SelectedValue;


			}
			else
			{
				objVehicleInfo.UNDERINS_MOTOR_INJURY_COVE = cmbUNDERINS_MOTOR_INJURY_COVE.SelectedValue;
				objVehicleInfo.UNINS_MOTOR_INJURY_COVE = cmbUNINS_MOTOR_INJURY_COVE.SelectedValue;
				objVehicleInfo.UNINS_PROPERTY_DAMAGE_COVE = cmbUNINS_PROPERTY_DAMAGE_COVE.SelectedValue;
			}*/
			
			

			
			if(hidState.Value!="14")//Not Indiana State
			{
				if(cmbPASSIVE_SEAT_BELT.SelectedItem!=null && cmbPASSIVE_SEAT_BELT.SelectedItem.Value!="")
					objVehicleInfo.PASSIVE_SEAT_BELT=	cmbPASSIVE_SEAT_BELT.SelectedValue;
			}			

			objVehicleInfo.AIR_BAG=	cmbAIR_BAG.SelectedValue;
			objVehicleInfo.ANTI_LOCK_BRAKES  =	cmbANTI_LOCK_BRAKES.SelectedValue;

			objVehicleInfo.APP_USE_VEHICLE_ID = Convert.ToInt32(cmbAPP_USE_VEHICLE_ID.SelectedValue);
			
			
			if (cmbAPP_VEHICLE_COMTYPE_ID.SelectedValue != null && cmbAPP_VEHICLE_COMTYPE_ID.SelectedValue != "") 
			{
				objVehicleInfo.APP_VEHICLE_COMTYPE_ID =  Convert.ToInt32(cmbAPP_VEHICLE_COMTYPE_ID.SelectedValue);
			}

			if (cmbAPP_VEHICLE_PERCLASS_ID.SelectedValue != null && cmbAPP_VEHICLE_PERCLASS_ID.SelectedValue != "")
			{
				objVehicleInfo.APP_VEHICLE_PERCLASS_ID = Convert.ToInt32(cmbAPP_VEHICLE_PERCLASS_ID.SelectedValue);
			}

			if (cmbAPP_VEHICLE_PERTYPE_ID.SelectedValue != null && cmbAPP_VEHICLE_PERTYPE_ID.SelectedValue != "")
			{
				objVehicleInfo.APP_VEHICLE_PERTYPE_ID = Convert.ToInt32(cmbAPP_VEHICLE_PERTYPE_ID.SelectedValue);
			}

			//Modified by Sibin on 27 Feb 09 to correctly pass value in the database
			if(cmbST_AMT_TYPE.SelectedItem!=null && (cmbAPP_VEHICLE_PERTYPE_ID.SelectedValue=="11869" || cmbAPP_VEHICLE_PERTYPE_ID.SelectedValue=="11868"))
			{
				objVehicleInfo.ST_AMT_TYPE=	"8708";
			}
			else
			{
				objVehicleInfo.ST_AMT_TYPE=	"8707";
			}
			if (hidCalledFrom.Value!=CALLED_FROM_UMBRELLA)
			{
				/*Vehicle Types
					11870->Camper& Travel Trailer
					11618->Suspended-Comp Only
					11337->Utility Trailer
				*/
				if(objVehicleInfo.APP_VEHICLE_PERTYPE_ID == 11870 || objVehicleInfo.APP_VEHICLE_PERTYPE_ID == 11618 || objVehicleInfo.APP_VEHICLE_PERTYPE_ID == 11337)
					objVehicleInfo.MULTI_CAR = "11918";//11918->Not applicable
				else if(cmbMULTI_CAR.SelectedValue != null && cmbMULTI_CAR.SelectedValue != "")
					objVehicleInfo.MULTI_CAR=	cmbMULTI_CAR.SelectedValue;

			/*	if (cmbSAFETY_BELT.SelectedValue != null && cmbSAFETY_BELT.SelectedValue != "")
					objVehicleInfo.SAFETY_BELT = cmbSAFETY_BELT.SelectedValue;*/
				
				if(objVehicleInfo.MULTI_CAR == "11920" && txtAUTO_POL_NO.Text.Trim()!="") //11920 Other policy with Wolverine
					objVehicleInfo.AUTO_POL_NO	= txtAUTO_POL_NO.Text.Trim();
			}

			//For Personal user, set data for commercial to be empty
			if(objVehicleInfo.APP_USE_VEHICLE_ID==11332)
			{
				objVehicleInfo.APP_VEHICLE_COMCLASS_ID = 0;
				objVehicleInfo.APP_VEHICLE_COMTYPE_ID = 0;				
				if(objVehicleInfo.VEHICLE_USE=="11270") // Drive to Work/School
				{
					objVehicleInfo.MILES_TO_WORK=txtMILES_TO_WORK.Text.Trim();
					if(int.Parse(objVehicleInfo.MILES_TO_WORK)>25)
					{
						if (cmbCAR_POOL.SelectedValue != null && cmbCAR_POOL.SelectedValue != "" )
						{
							objVehicleInfo.CAR_POOL = Convert.ToInt32(cmbCAR_POOL.SelectedValue);
						}
					}
				}
				else if(objVehicleInfo.VEHICLE_USE=="11272") // Snowplow Conditions
				{
					if (cmbSNOWPLOW_CONDS.SelectedValue != null && cmbSNOWPLOW_CONDS.SelectedValue != "" )
					{
						objVehicleInfo.SNOWPLOW_CONDS = Convert.ToInt32(cmbSNOWPLOW_CONDS.SelectedValue);
					}
				}
				if (txtANNUAL_MILEAGE.Text.Trim()!="")
				{
					objVehicleInfo.ANNUAL_MILEAGE=double.Parse(txtANNUAL_MILEAGE.Text.Trim());
				}
				else
				{
					objVehicleInfo.ANNUAL_MILEAGE=-1;  
				}
			}
			else
			{
				objVehicleInfo.APP_VEHICLE_PERCLASS_ID = 0;
				objVehicleInfo.APP_VEHICLE_PERTYPE_ID = 0;
				objVehicleInfo.ANNUAL_MILEAGE=-1;  
				if(txtRADIUS_OF_USE.Text != null && txtRADIUS_OF_USE.Text !="")
					objVehicleInfo.RADIUS_OF_USE	= int.Parse(txtRADIUS_OF_USE.Text);
				if (cmbTRANSPORT_CHEMICAL.SelectedValue != null && cmbTRANSPORT_CHEMICAL.SelectedValue != "" )
				{
					objVehicleInfo.TRANSPORT_CHEMICAL = cmbTRANSPORT_CHEMICAL.SelectedValue.ToString();
				}
				if (hidState.Value == ((int)enumState.Michigan).ToString() && cmbCOVERED_BY_WC_INSU.SelectedValue != null && cmbCOVERED_BY_WC_INSU.SelectedValue != "" )
				{
					objVehicleInfo.COVERED_BY_WC_INSU= cmbCOVERED_BY_WC_INSU.SelectedValue.ToString();
				}
				if (cmbCommClass.SelectedValue != null && cmbCommClass.SelectedValue != "" )
				{
					objVehicleInfo.CLASS_DESCRIPTION = cmbCommClass.SelectedValue.ToString().Split('~')[0].ToString();
				}
				if(hidAPP_VEHICLE_COMCLASS_ID.Value!="")
				{
					objVehicleInfo.APP_VEHICLE_COMCLASS_ID = Convert.ToInt32(hidAPP_VEHICLE_COMCLASS_ID.Value);
				}
			}					

			
			

			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hidVehicleID.Value;
			oldXML			=	@hidOldData.Value;
			//Returning the model object

			return objVehicleInfo;
		}
		#endregion

		#region GetFormValueForUmbrella
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private Cms.Model.Policy.Umbrella.ClsVehicleInfo  GetFormValueForUmbrella()
		{
			//Creating the Model object for holding the New data
			Cms.Model.Policy.Umbrella.ClsVehicleInfo objVehicleInfo = new Cms.Model.Policy.Umbrella.ClsVehicleInfo ();

			hidCustomerID.Value =GetCustomerID();
			//hidAPPID.Value=GetAppID();
			//hidAppVersionID.Value=GetAppVersionID();
			hidPolicyID.Value = Request["POLICY_ID"].ToString(); 
			hidPolicyVersionID.Value = Request["POLICY_VERSION_ID"].ToString(); 

			objVehicleInfo.INSURED_VEH_NUMBER=	int.Parse("0");
			objVehicleInfo.VEHICLE_YEAR=	txtVEHICLE_YEAR.Text ==null?"":txtVEHICLE_YEAR.Text ;
			objVehicleInfo.MAKE=	txtMAKE.Text ==null?"":txtMAKE.Text ;
			objVehicleInfo.MODEL=	txtMODEL.Text ==null?"":txtMODEL.Text ;
			objVehicleInfo.VIN=	txtVIN.Text.ToString().ToUpper().Trim();
			objVehicleInfo.BODY_TYPE=	txtBODY_TYPE.Text ==null?"":txtBODY_TYPE.Text ;
			objVehicleInfo.GRG_ADD1=	txtGRG_ADD1.Text;
			objVehicleInfo.GRG_ADD2=	txtGRG_ADD2.Text;
			objVehicleInfo.GRG_CITY=	txtGRG_CITY.Text;
			objVehicleInfo.GRG_COUNTRY=	cmbGRG_COUNTRY.SelectedValue;
			objVehicleInfo.GRG_STATE=	cmbGRG_STATE.SelectedValue;
			objVehicleInfo.GRG_ZIP=	txtGRG_ZIP.Text;
			
			if(cmbREGISTERED_STATE.SelectedItem!=null)
			{
				objVehicleInfo.REGISTERED_STATE=	cmbREGISTERED_STATE.SelectedValue;
			}
            if (txtTERRITORY.Text != "")
                objVehicleInfo.TERRITORY = txtTERRITORY.Text;
            else
                objVehicleInfo.TERRITORY = hidTERRITORY.Value;
			
			//Commented by Sibin on 27 Feb 09 to correctly pass value in the database
//			if(cmbST_AMT_TYPE.SelectedItem!=null)
//			{
//				objVehicleInfo.ST_AMT_TYPE=	cmbST_AMT_TYPE.SelectedValue;
//			}
			objVehicleInfo.CUSTOMER_ID = int.Parse(hidCustomerID.Value);

			if(txtAMOUNT.Text.Trim()!="")
			{
				objVehicleInfo.AMOUNT=	double.Parse(txtAMOUNT.Text);
			}
			if(txtSYMBOL.Text.Trim()!="")
			{
				objVehicleInfo.SYMBOL=	int.Parse (txtSYMBOL.Text);
			}

            //if(txtVEHICLE_AGE.Text.Trim()!="")
            //{
            //    objVehicleInfo.VEHICLE_AGE=	int.Parse (txtVEHICLE_AGE.Text==""?"0":txtVEHICLE_AGE.Text);
            //}


            //Added By Lalit For assign javascript calculated value into control or store in databasae 
            if (hidVEHICLE_AGE.Value != "")
            {
                objVehicleInfo.VEHICLE_AGE = int.Parse(hidVEHICLE_AGE.Value == "" ? "0" : hidVEHICLE_AGE.Value);
            }


			objVehicleInfo.POLICY_ID = Convert.ToInt32(hidPolicyID.Value);
			objVehicleInfo.POLICY_VERSION_ID = Convert.ToInt32(hidPolicyVersionID.Value);


			if (hidVehicleID.Value !=null )
			{
				if (hidVehicleID.Value.ToString()=="NEW")
				{
					objVehicleInfo.VEHICLE_ID = 0;// int.Parse (ClsVehicleInformation.GetNewVehicleID(hidCalledFrom.Value));
				}
				else
				{
					objVehicleInfo.VEHICLE_ID = int.Parse (hidVehicleID.Value.ToString());
				}
			}


			objVehicleInfo.IS_OWN_LEASE=	cmbIS_OWN_LEASE.SelectedValue;
			
			if (txtPURCHASE_DATE.Text.Trim()!="")		
			{
				objVehicleInfo.PURCHASE_DATE=Convert.ToDateTime(txtPURCHASE_DATE.Text);	//txtPURCHASE_DATE.Text=="" ? Convert.ToDateTime("1/1/1900"): Convert.ToDateTime(txtPURCHASE_DATE.Text);
			}
			else
			{
				objVehicleInfo.PURCHASE_DATE=Convert.ToDateTime("1/1/1900");
			}
			objVehicleInfo.IS_NEW_USED=	cmbIS_NEW_USED.SelectedValue;		
			objVehicleInfo.VEHICLE_USE=	cmbVEHICLE_USE.SelectedValue;	
			/*if(cmbUNINS_MOTOR_INJURY_COVE.SelectedValue=="10963")
			{
				objVehicleInfo.UNINS_MOTOR_INJURY_COVE    = cmbUNINS_MOTOR_INJURY_COVE.SelectedValue;
				objVehicleInfo.UNDERINS_MOTOR_INJURY_COVE = cmbUNINS_MOTOR_INJURY_COVE.SelectedValue;
				objVehicleInfo.UNINS_PROPERTY_DAMAGE_COVE = cmbUNINS_MOTOR_INJURY_COVE.SelectedValue;


			}
			else
			{
				objVehicleInfo.UNDERINS_MOTOR_INJURY_COVE = cmbUNDERINS_MOTOR_INJURY_COVE.SelectedValue;
				objVehicleInfo.UNINS_MOTOR_INJURY_COVE = cmbUNINS_MOTOR_INJURY_COVE.SelectedValue;
				objVehicleInfo.UNINS_PROPERTY_DAMAGE_COVE = cmbUNINS_PROPERTY_DAMAGE_COVE.SelectedValue;
			}*/
			objVehicleInfo.MULTI_CAR=	cmbMULTI_CAR.SelectedValue;
			if (txtANNUAL_MILEAGE.Text.Trim()!="")
			{
				objVehicleInfo.ANNUAL_MILEAGE=double.Parse(txtANNUAL_MILEAGE.Text.Trim());
			}
			else
			{
				objVehicleInfo.ANNUAL_MILEAGE=-1;  
			}

			
			objVehicleInfo.PASSIVE_SEAT_BELT=	cmbPASSIVE_SEAT_BELT.SelectedValue;
			objVehicleInfo.AIR_BAG=	cmbAIR_BAG.SelectedValue;
			objVehicleInfo.ANTI_LOCK_BRAKES  =	cmbANTI_LOCK_BRAKES.SelectedValue;

			objVehicleInfo.APP_USE_VEHICLE_ID = Convert.ToInt32(cmbAPP_USE_VEHICLE_ID.SelectedValue);
			if (cmbAPP_VEHICLE_COMCLASS_ID.SelectedValue != null && cmbAPP_VEHICLE_COMCLASS_ID.SelectedValue != "" )
			{
				objVehicleInfo.APP_VEHICLE_COMCLASS_ID = Convert.ToInt32(cmbAPP_VEHICLE_COMCLASS_ID.SelectedValue);
			}
			
			if (cmbAPP_VEHICLE_COMTYPE_ID.SelectedValue != null && cmbAPP_VEHICLE_COMTYPE_ID.SelectedValue != "") 
			{
				objVehicleInfo.APP_VEHICLE_COMTYPE_ID =  Convert.ToInt32(cmbAPP_VEHICLE_COMTYPE_ID.SelectedValue);
			}

			if (cmbAPP_VEHICLE_PERCLASS_ID.SelectedValue != null && cmbAPP_VEHICLE_PERCLASS_ID.SelectedValue != "")
			{
				objVehicleInfo.APP_VEHICLE_PERCLASS_ID = Convert.ToInt32(cmbAPP_VEHICLE_PERCLASS_ID.SelectedValue);
			}

			if (cmbAPP_VEHICLE_PERTYPE_ID.SelectedValue != null && cmbAPP_VEHICLE_PERTYPE_ID.SelectedValue != "")
			{
				objVehicleInfo.APP_VEHICLE_PERTYPE_ID = Convert.ToInt32(cmbAPP_VEHICLE_PERTYPE_ID.SelectedValue);
			}
			
			//Modified by Sibin on 27 Feb 09 to correctly pass value in the database
			if(cmbST_AMT_TYPE.SelectedItem!=null && (cmbAPP_VEHICLE_PERTYPE_ID.SelectedValue=="11869" || cmbAPP_VEHICLE_PERTYPE_ID.SelectedValue=="11868"))
			{
				objVehicleInfo.ST_AMT_TYPE=	"8708";
			}
			else
			{
				objVehicleInfo.ST_AMT_TYPE=	"8707";
			}
			//For Personal user, set data for commercial to be empty
			if(objVehicleInfo.APP_USE_VEHICLE_ID==11332)
			{
				objVehicleInfo.APP_VEHICLE_COMCLASS_ID = 0;
				objVehicleInfo.APP_VEHICLE_COMTYPE_ID = 0;
			}
			else
			{
				objVehicleInfo.APP_VEHICLE_PERCLASS_ID = 0;
				objVehicleInfo.APP_VEHICLE_PERTYPE_ID = 0;
			}
			
			objVehicleInfo.MILES_TO_WORK=txtMILES_TO_WORK.Text.Trim();

			//objVehicleInfo.SAFETY_BELT = cmbSAFETY_BELT.SelectedValue;

			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hidVehicleID.Value;
			oldXML			=	@hidOldData.Value;
			//Returning the model object

			return objVehicleInfo;
		}
		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
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
				objVehicleInformation = new ClsVehicleInformation();

				//Retreiving the form values into model class object
				ClsPolicyVehicleInfo objVehicleInfo = null;
				Cms.Model.Policy.Umbrella.ClsVehicleInfo objUmVehicleInfo = null;
				if(strCalledFrom.ToUpper()=="UMB")
				{
					objUmVehicleInfo=GetFormValueForUmbrella();
				}
				else
				{
					objVehicleInfo =GetFormValue();
				}

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					if(strCalledFrom.ToUpper()=="UMB")
					{
						objUmVehicleInfo.CREATED_BY = int.Parse(GetUserId());
						objUmVehicleInfo.CREATED_DATETIME = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy"));
						intRetVal = objVehicleInformation.AddUmbrellaPolicyVehicle(objUmVehicleInfo,"");
						if(intRetVal>0)
							hidCustomerID.Value = objUmVehicleInfo.CUSTOMER_ID.ToString();
					}
					else if(strCalledFrom.ToUpper()=="PPA")
					{
						objVehicleInfo.CREATED_BY = int.Parse(GetUserId());
						objVehicleInfo.CREATED_DATETIME = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy"));
						intRetVal = objVehicleInformation.AddPolicyVehicle(objVehicleInfo);
						//Itrack 5130
						//fxnSetMultiCarOptions();
						if(intRetVal>0)
							hidCustomerID.Value = objVehicleInfo.CUSTOMER_ID.ToString();

					}


					strRowId = intRetVal.ToString() ;//objVehicleInfo.VEHICLE_ID.ToString();
					hidVehicleID.Value =  intRetVal.ToString() ;//objVehicleInfo.VEHICLE_ID.ToString();
					if(intRetVal>0)
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						hidIS_ACTIVE.Value = "Y";
						SetWorkFlowControl();

						//Opening the endorsement details page
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
					ClsPolicyVehicleInfo objOldVehicleInfo;
					objOldVehicleInfo = new ClsPolicyVehicleInfo();

					Cms.Model.Policy.Umbrella.ClsVehicleInfo objOldUmVehicleInfo= new Cms.Model.Policy.Umbrella.ClsVehicleInfo ();
					//Setting  the Old Page details(XML File containing old details) into the Model Object
					if(strCalledFrom.ToUpper()=="UMB")
					{
						base.PopulateModelObject(objOldUmVehicleInfo ,@hidOldData.Value);
						objUmVehicleInfo.MODIFIED_BY = int.Parse(GetUserId());
						objUmVehicleInfo.LAST_UPDATED_DATETIME = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy"));
						objUmVehicleInfo.IS_ACTIVE = hidIS_ACTIVE.Value;
						//objUmVehicleInfo.INSURED_VEH_NUMBER = int.Parse(txtINSURED_VEH_NUMBER.Text);
                        objUmVehicleInfo.INSURED_VEH_NUMBER = int.Parse(hidINSURED_VEH_NUMBER.Value);
						intRetVal	= objVehicleInformation.UpdatePolicyUmbrellaVehicle(objOldUmVehicleInfo ,objUmVehicleInfo );

					}
					else
					{
						base.PopulateModelObject(objOldVehicleInfo,@hidOldData.Value);
						//Setting those values into the Model object which are not in the page
						objVehicleInfo.MODIFIED_BY = int.Parse(GetUserId());
						objVehicleInfo.LAST_UPDATED_DATETIME = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy"));
						objVehicleInfo.IS_ACTIVE = hidIS_ACTIVE.Value;
                        if (txtINSURED_VEH_NUMBER.Text != "")
						objVehicleInfo.INSURED_VEH_NUMBER = int.Parse(txtINSURED_VEH_NUMBER.Text);
                        else if (hidINSURED_VEH_NUMBER.Value != "")
                            objVehicleInfo.INSURED_VEH_NUMBER = int.Parse(hidINSURED_VEH_NUMBER.Value);

                        intRetVal	= objVehicleInformation.UpdatePolicyVehicle(objOldVehicleInfo,objVehicleInfo);
						//Itrack 5310
						//fxnSetMultiCarOptions();
					}
					
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						SetWorkFlowControl();

						//Opening the endorsement details page
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
				
				if(strCalledFrom.ToUpper()=="PPA")
					hidOldData.Value = @ClsVehicleInformation.FetchPolicyVehicleXML(int.Parse(hidCustomerID.Value==""?"0": hidCustomerID.Value) ,int.Parse (hidPolicyID.Value ==""?"0":hidPolicyID.Value),int.Parse (hidPolicyVersionID.Value==""?"0":hidPolicyVersionID.Value),int.Parse (hidVehicleID.Value==""?"0":hidVehicleID.Value));
				else if(strCalledFrom.ToUpper()=="UMB")
					hidOldData.Value = @ClsVehicleInformation.FetchPolicyUmbrellaVehicleXML(int.Parse(hidCustomerID.Value==""?"0": hidCustomerID.Value) ,int.Parse (hidPolicyID.Value ==""?"0":hidPolicyID.Value),int.Parse (hidPolicyVersionID.Value==""?"0":hidPolicyVersionID.Value),int.Parse (hidVehicleID.Value==""?"0":hidVehicleID.Value));

                if (ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value).ToString() != "")////Added By Pradeep Kushwaha on 10-sep-2010
                    btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value).ToString());
            

				LoadVehicleUseClassType(); 
				
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

		//Set territory readonly only for Automobile
		private void EnableDisableTerritory()		
		{
			//if (hidCalledFrom.Value.Equals(CALLED_FROM_UMBRELLA))
            if (strCalledFrom.ToUpper() == CALLED_FROM_UMBRELLA.ToUpper())
            {
                txtTERRITORY.ReadOnly = false;

            }
            else
                txtTERRITORY.ReadOnly = true;
		}

		
		#endregion

		protected void fxnSetMultiCarOptions()
		{	// Get Vehicle count to pre-select Multi Car options
			if(hidCalledFrom.Value!=CALLED_FROM_UMBRELLA)
			{
				int intVehCount = 0;
				//Itrack 5310 : Commented
				//if (Request.QueryString["VH_ID"]!=null && Request.QueryString["VH_ID"].ToString()!="") // UPDATE MODE
					//if (intRetVal !=0) // UPDATE MODE
				//	intVehCount = ClsVehicleInformation.GetPolVehicleCount(int.Parse(hidCustomerID.Value==""?"0": hidCustomerID.Value) ,int.Parse (hidPolicyID.Value ==""?"0":hidPolicyID.Value),int.Parse (hidPolicyVersionID.Value==""?"0":hidPolicyVersionID.Value),int.Parse(Request.QueryString["VH_ID"].ToString()));
				//else
				//	intVehCount = ClsVehicleInformation.GetPolVehicleCount(int.Parse(hidCustomerID.Value==""?"0": hidCustomerID.Value) ,int.Parse (hidPolicyID.Value ==""?"0":hidPolicyID.Value),int.Parse (hidPolicyVersionID.Value==""?"0":hidPolicyVersionID.Value),0);

				if(intVehCount == 0 )    // No PPA Veh
				{hidVehCount.Value = "0";}
				else if(intVehCount >= 1)	// 1 PPA Veh
				{hidVehCount.Value = "1";}
				else
				{hidVehCount.Value = "2";}
			}
			
		}

		//Itrack 5310
		protected int getPolVehicleCount()
		{
			int intVehCount = 0;
			if(hidCalledFrom.Value!=CALLED_FROM_UMBRELLA)
			{
				intVehCount = ClsVehicleInformation.GetPolVehicleCount(int.Parse(hidCustomerID.Value==""?"0": hidCustomerID.Value) ,int.Parse (hidPolicyID.Value ==""?"0":hidPolicyID.Value),int.Parse (hidPolicyVersionID.Value==""?"0":hidPolicyVersionID.Value),1);
				//if(intVehCount >= 1)
					hidVehCount.Value = intVehCount.ToString();

			}
			return intVehCount;

		}


		private void SetCaptions()
		{
			capINSURED_VEH_NUMBER.Text			=		objResourceMgr.GetString("txtINSURED_VEH_NUMBER");
			capVEHICLE_YEAR.Text				=		objResourceMgr.GetString("cmbVEHICLE_YEAR");
			capMAKE.Text						=		objResourceMgr.GetString("cmbMAKE");
			capMODEL.Text						=		objResourceMgr.GetString("cmbMODEL");
			capVIN.Text							=		objResourceMgr.GetString("txtVIN");
			capBODY_TYPE.Text					=		objResourceMgr.GetString("cmbBODY_TYPE");
			capGRG_ADD1.Text					=		objResourceMgr.GetString("txtGRG_ADD1");
			capGRG_ADD2.Text					=		objResourceMgr.GetString("txtGRG_ADD2");
			capGRG_CITY.Text					=		objResourceMgr.GetString("txtGRG_CITY");
			capGRG_COUNTRY.Text					=		objResourceMgr.GetString("cmbGRG_COUNTRY");
			capGRG_STATE.Text					=		objResourceMgr.GetString("cmbGRG_STATE");
			capGRG_ZIP.Text						=		objResourceMgr.GetString("txtGRG_ZIP");
			capREGISTERED_STATE.Text			=		objResourceMgr.GetString("cmbREGISTERED_STATE");
			capTERRITORY.Text					=		objResourceMgr.GetString("txtTERRITORY");
			capSNOWPLOW_CONDS.Text				=		objResourceMgr.GetString("cmbSNOWPLOW_CONDS");
			capST_AMT_TYPE.Text					=		objResourceMgr.GetString("cmbST_AMT_TYPE");
			capAMOUNT.Text						=		objResourceMgr.GetString("txtAMOUNT");
			capSYMBOL.Text						=		objResourceMgr.GetString("txtSYMBOL");
			capVEHICLE_AGE.Text					=		objResourceMgr.GetString("txtVEHICLE_AGE");
			
			capIS_OWN_LEASE.Text						=		objResourceMgr.GetString("cmbIS_OWN_LEASE");
			capPURCHASE_DATE.Text						=		objResourceMgr.GetString("txtPURCHASE_DATE");
			capIS_NEW_USED.Text							=		objResourceMgr.GetString("cmbIS_NEW_USED");
			capVEHICLE_USE.Text							=		objResourceMgr.GetString("cmbVEHICLE_USE");
			capMULTI_CAR.Text							=		objResourceMgr.GetString("cmbMULTI_CAR");
			capANNUAL_MILEAGE.Text						=		objResourceMgr.GetString("txtANNUAL_MILEAGE");
			capPASSIVE_SEAT_BELT.Text					=		objResourceMgr.GetString("cmbPASSIVE_SEAT_BELT");
			capAIR_BAG.Text								=		objResourceMgr.GetString("cmbAIR_BAG");
			capANTI_LOCK_BRAKES.Text					=		objResourceMgr.GetString("cmbANTI_LOCK_BRAKES");
		
			//capUNDERINS_MOTOR_INJURY_COVE.Text			=		objResourceMgr.GetString("cmbUNDERINS_MOTOR_INJURY_COVE");
			//capUNINS_MOTOR_INJURY_COVE.Text				=		objResourceMgr.GetString("cmbUNINS_MOTOR_INJURY_COVE");
			//capUNINS_PROPERTY_DAMAGE_COVE.Text			=		objResourceMgr.GetString("cmbUNINS_PROPERTY_DAMAGE_COVE");
	
			capUSE_VEHICLE.Text					=		objResourceMgr.GetString("cmbAPP_USE_VEHICLE_ID");
			capCLASS_PER.Text					=		objResourceMgr.GetString("cmbAPP_VEHICLE_PERCLASS_ID");
			capVEHICLE_TYPE_PER.Text			=		objResourceMgr.GetString("cmbAPP_VEHICLE_PERTYPE_ID");
			capCLASS_COM.Text					=		objResourceMgr.GetString("cmbAPP_VEHICLE_COMCLASS_ID");
			capVEHICLE_TYPE_COM.Text			=		objResourceMgr.GetString("cmbAPP_VEHICLE_COMTYPE_ID");
			capMILES_TO_WORK.Text				=		objResourceMgr.GetString("txtMILES_TO_WORK");
			//capSAFETY_BELT.Text                 =       objResourceMgr.GetString("cmbSAFETY_BELT");
			capBUSS_PERM_RESI.Text              =       objResourceMgr.GetString("cmbBUSS_PERM_RESI");
			capCAR_POOL.Text					=		objResourceMgr.GetString("cmbCAR_POOL");
			capAUTO_POL_NO.Text					=		objResourceMgr.GetString("txtAUTO_POL_NO");
			capRADIUS_OF_USE.Text					=		objResourceMgr.GetString("txtRADIUS_OF_USE");
			capTRANSPORT_CHEMICAL.Text					=		objResourceMgr.GetString("cmbTRANSPORT_CHEMICAL");
			capCOVERED_BY_WC_INSU.Text					=		objResourceMgr.GetString("cmbCOVERED_BY_WC_INSU");
			capCommClass.Text					=		objResourceMgr.GetString("cmbCommClass");
			capIS_SUSPENDED.Text					=		objResourceMgr.GetString("cmbIS_SUSPENDED");
			capIS_SUSPENDED_COM.Text				=		objResourceMgr.GetString("cmbIS_SUSPENDED_COM");
            hidJAVASCRIPT_MSG.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "2");

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
				if(strCalledFrom.ToUpper()=="PPA")
					hidOldData.Value = @ClsVehicleInformation.FetchPolicyVehicleXML(int.Parse(hidCustomerID.Value==""?"0": hidCustomerID.Value) ,int.Parse (hidPolicyID.Value ==""?"0":hidPolicyID.Value),int.Parse (hidPolicyVersionID.Value==""?"0":hidPolicyVersionID.Value),int.Parse (hidVehicleID.Value==""?"0":hidVehicleID.Value));
				else if(strCalledFrom.ToUpper()=="UMB")
					hidOldData.Value = @ClsVehicleInformation.FetchPolicyUmbrellaVehicleXML(int.Parse(hidCustomerID.Value==""?"0": hidCustomerID.Value) ,int.Parse (hidPolicyID.Value ==""?"0":hidPolicyID.Value),int.Parse (hidPolicyVersionID.Value==""?"0":hidPolicyVersionID.Value),int.Parse (hidVehicleID.Value==""?"0":hidVehicleID.Value));
                if (ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value).ToString() != "")//Added By Pradeep Kushwaha on 10-sep-2010
                    btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value).ToString());
                if (ClsCommon.FetchValueFromXML("VEHICLE_COUNT", hidOldData.Value).ToString() != "")
                    hidVEHICLE_COUNT.Value = ClsCommon.FetchValueFromXML("VEHICLE_COUNT", hidOldData.Value).ToString();
				
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

				//save the lobid
				hidAPP_LOB.Value = GetLOBID();
			}				
		}
		private void operateVINMASTER()
		{
			try
			{
				hidFormSaved.Value = "3";

				/* Check the VIN text. 
				 *    IF a VIN is filled and is >=11 chrs long then 
				 *	  find the data corresponding to the VIN and fill it in the screen
				 *	  else open the popup and allow the user to select */
				if  (txtVIN.Text.Trim()!="" &&  txtVIN.Text.Trim().Length>=10)
				{
					//Dataset dsTemp = new DataSet();
					DataSet dsTemp = ClsVehicleInformation.FetchVINMasterDetailsFromVIN(txtVIN.Text.Trim().Substring(0,10));
					if (dsTemp!=null)
					{
						if(	dsTemp.Tables[0]!=null && dsTemp.Tables[0].Rows.Count>0)
						{
							
							// Show values in controls.
							txtVEHICLE_YEAR.Text= dsTemp.Tables[0].Rows[0]["Model_year"].ToString(); 
							txtMAKE.Text= dsTemp.Tables[0].Rows[0]["MAKE_CODE"].ToString() + "-" + dsTemp.Tables[0].Rows[0]["Make_Name"].ToString();
							txtBODY_TYPE.Text =dsTemp.Tables[0].Rows[0]["Body_Type"].ToString();
							txtMODEL.Text= dsTemp.Tables[0].Rows[0]["Series_Name"].ToString();
							txtSYMBOL.Text= dsTemp.Tables[0].Rows[0]["Symbol"].ToString();
							
							//changed by anurag verma on 22/09/2005 for fetching anti_lck_brake and populating its combo box

							string anti_brake="";
							ListItem li=new ListItem(); 
							anti_brake=dsTemp.Tables[0].Rows[0]["ANTI_LCK_BRAKES"].ToString(); 
							
							if(!anti_brake.Equals(""))
							{
								li=cmbANTI_LOCK_BRAKES.Items.FindByText("Yes");
								if(li!=null)
								{
									cmbANTI_LOCK_BRAKES.ClearSelection();
									li.Selected=true; 
								}
							}
							else
							{
								
								li=cmbANTI_LOCK_BRAKES.Items.FindByText("No");
								if(li!=null)
								{
									cmbANTI_LOCK_BRAKES.ClearSelection();
									li.Selected=true; 
								}
							}
							string air_bag="";
							ListItem li_bag=new ListItem(); 
							air_bag=dsTemp.Tables[0].Rows[0]["AIRBAG"].ToString(); 
							cmbAIR_BAG.SelectedIndex=-1;
							if(!air_bag.Equals(""))
							{
								cmbAIR_BAG.SelectedValue=air_bag;
							}

                            if (ClsCommon.IsInteger(hidVEHICLE_AGE.Value.Trim()))
							{
								int ageOfVehicle=0;
                                ageOfVehicle = (System.DateTime.Today.Year - int.Parse(hidVEHICLE_AGE.Value.Trim() == "" ? "0" : hidVEHICLE_AGE.Value.Trim()));
								txtVEHICLE_AGE.Text = ageOfVehicle.ToString();
                                hidVEHICLE_AGE.Value = ageOfVehicle.ToString();
							}
						}
						else
						{
							gIntShowVINPopup=1;
						}
					}
					else
					{
						gIntShowVINPopup=1;
					}
				}
				else
				{
					gIntShowVINPopup=1;
				}
				//gIntShowVINPopup=1;
			}
			catch(Exception exc)
			{
				throw(exc);
			}
			finally
			{}				
		}

		private void lnkVINMASTER1_Click(object sender, System.EventArgs e)
		{
			try
			{
				/* Check the VIN text. 
				 *    IF a VIN is filled and is >=11 chrs long then 
				 *	  find the data corresponding to the VIN and fill it in the screen
				 *	  else open the popup and allow the user to select */
				if  (txtVIN.Text.Trim()!="" &&  txtVIN.Text.Trim().Length>=11)
				{
					//Dataset dsTemp = new DataSet();
					DataSet dsTemp = ClsVehicleInformation.FetchVINMasterDetailsFromVIN(txtVIN.Text.Trim().Substring(0,11));
					if (dsTemp!=null)
					{
						if(	dsTemp.Tables[0]!=null && dsTemp.Tables[0].Rows.Count>0)
						{
							// Show values in controls.
							txtVEHICLE_YEAR.Text= dsTemp.Tables[0].Rows[0]["Model_year"].ToString(); 
							txtMAKE.Text= dsTemp.Tables[0].Rows[0]["MAKE_CODE"].ToString() + "-" + dsTemp.Tables[0].Rows[0]["Make_Name"].ToString();
							txtBODY_TYPE.Text =dsTemp.Tables[0].Rows[0]["Body_Type"].ToString();
							txtMODEL.Text= dsTemp.Tables[0].Rows[0]["Series_Name"].ToString();
							txtSYMBOL.Text= dsTemp.Tables[0].Rows[0]["Symbol"].ToString();
							
							//changed by anurag verma on 22/09/2005 for fetching anti_lck_brake and populating its combo box

							string anti_brake="";
							ListItem li=new ListItem(); 
							anti_brake=dsTemp.Tables[0].Rows[0]["ANTI_LCK_BRAKES"].ToString(); 
							if(!anti_brake.Equals(""))
							{
								li=cmbANTI_LOCK_BRAKES.Items.FindByText("Yes");
								if(li!=null)
								{
									li.Selected=true; 
								}
							}
							else
							{
								
								li=cmbANTI_LOCK_BRAKES.Items.FindByText("No");
								if(li!=null)
								{
									li.Selected=true; 
								}
							}

                            if (ClsCommon.IsInteger(hidVEHICLE_AGE.Value.Trim()))
							{
								int ageOfVehicle=0;
                                ageOfVehicle = (System.DateTime.Today.Year - int.Parse(hidVEHICLE_AGE.Value.Trim() == "" ? "0" : hidVEHICLE_AGE.Value.Trim()));
								txtVEHICLE_AGE.Text = ageOfVehicle.ToString();
                                hidVEHICLE_AGE.Value = ageOfVehicle.ToString();

							}
						}
						else
						{
							gIntShowVINPopup=1;
						}
					}
					else
					{
						gIntShowVINPopup=1;
					}
				}
				else
				{
					gIntShowVINPopup=1;
				}

			}
			catch(Exception exc)
			{
				throw(exc);
			}
			finally
			{}

		}

		
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			
			int intCustomerID = int.Parse(hidCustomerID.Value);
			int intPolicyID=  int.Parse(hidPolicyID.Value);
			int intPolicyVersionID	= int.Parse(hidPolicyVersionID.Value);
			int intVehicleId = int.Parse(hidVehicleID.Value);
			string strCalledFrom=hidCalledFrom.Value;
			
			objVehicleInformation = new Cms.BusinessLayer.BlApplication.ClsVehicleInformation();
			Cms.Model.Policy.ClsPolicyVehicleInfo  objVehicleInfo = new ClsPolicyVehicleInfo();//GetFormValue();		
			//Added by Charles on 10-Sep-09 for Itrack 6375
			base.PopulateModelObject(objVehicleInfo,@hidOldData.Value);
			objVehicleInfo.CUSTOMER_ID =intCustomerID;
			objVehicleInfo.POLICY_ID=intPolicyID;
			objVehicleInfo.POLICY_VERSION_ID=intPolicyVersionID;
			objVehicleInfo.VEHICLE_ID=intVehicleId;//Added till here	

			objVehicleInfo.CREATED_BY=objVehicleInfo.MODIFIED_BY = int.Parse(GetUserId());
			//if(strCalledFrom.ToUpper()=="PPA")
			//{
				// Added by Swastika on 7th Mar'06 for Pol Iss #59
			int DriverCount ;
			DriverCount = objVehicleInformation.GetDriverCountForAssignedVehiclePolicy(intCustomerID,intPolicyID ,intPolicyVersionID ,intVehicleId,hidCalledFrom.Value.ToUpper());
			if(DriverCount>0)
				ShowAlertMessageForDelete();
			else
			{						
				if(hidCalledFrom.Value.ToUpper()=="UMB")
					intRetVal = objVehicleInformation.DeletePolicyUmbrellaVehicle(intCustomerID,intPolicyID,intPolicyVersionID,intVehicleId);			
				else
					intRetVal = objVehicleInformation.DeletePolicyVehicle(objVehicleInfo,"");
					//intRetVal = objVehicleInformation.DeletePolicyVehicle(intCustomerID,intPolicyID,intPolicyVersionID,intVehicleId);				
					
			}
			//Itrack 5310
			//fxnSetMultiCarOptions();
			if(intRetVal>0)
			{
				lblDelete.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","127");
				hidFormSaved.Value = "5";
				hidOldData.Value = "";
				trBody.Attributes.Add("style","display:none");
				SetWorkFlowControl();

				//Opening the endorsement details page
				base.OpenEndorsementDetails();
			}
			else if(intRetVal == -1)
			{
				lblDelete.Text		=	ClsMessages.GetMessage(base.ScreenId,"128");
				hidFormSaved.Value		=	"2";
			}
			lblDelete.Visible = true;
		}

		private void GetSymbolFromXML()
		{
			if(hidSymbolCheck.Value!="0")
			{
				
				try
				{
					double start,end,amount;
					int year=0;
				
					string strSymbol="";
					if(txtAMOUNT.Text.Trim()=="")
						amount=0;
					else
						amount=Double.Parse(txtAMOUNT.Text.Trim());

					if(txtVEHICLE_YEAR.Text.Trim()!="")
					{
						year=int.Parse(txtVEHICLE_YEAR.Text.Trim());
					}				

					XmlDocument xDoc=new XmlDocument();
					XmlNodeList xNodeList;
					xDoc.Load(Server.MapPath(Request.ApplicationPath  + "/cmsweb/QuickQuote/AUTO_DEFAULT_VALUES.XML")); 				
					if(year>=1990)
						xNodeList=xDoc.SelectNodes("quickQuote/vehicle/CalculateSymbols[@VehicleType='OTH' and @Year=1990]");
					else
						xNodeList=xDoc.SelectNodes("quickQuote/vehicle/CalculateSymbols[@VehicleType='OTH' and @Year=1989]");

				
				
					foreach(XmlNode xnode in xNodeList)
					{
						XmlNodeList symbNodes = xnode.SelectNodes("Symbol");					
						foreach(XmlNode symbNode in symbNodes )
						{
							start = Double.Parse(symbNode.Attributes["StartRange"].Value);
							end = Double.Parse(symbNode.Attributes["EndRange"].Value);
							strSymbol = symbNode.Attributes["Sym"].Value;
							if(amount>=start && amount<=end)
							{
								txtSYMBOL.Text = strSymbol;
								hidFormSaved.Value="4";
								hidSymbolCheck.Value="0";
								return;
							}
						}

					}
				
					lblMessage.Text = " - (" + xNodeList[0].InnerText + "%)";
					/*xNodeList=xDoc.SelectNodes("PRODUCTMASTER/PRODUCT[@ID='AUTO-P']/FACTOR[@ID='DRIVERDISCOUNT']/NODE[@ID ='DRIVERDISC']/ATTRIBUTES/@PREMIERDRIVERCREDIT"); 
					capPremierDriver.Text = " - (" + xNodeList[0].InnerText + "%)";
					xNodeList=xDoc.SelectNodes("PRODUCTMASTER/PRODUCT[@ID='AUTO-P']/FACTOR[@ID='GOODSTUDENT']/NODE[@ID ='GOODSTUDENTDISCOUNT']/ATTRIBUTES/@CREDIT"); 
					capGoodStudent.Text = " - (" + xNodeList[0].InnerText + "%)";*/
				
				}
				catch(Exception ex)
				{
					lblMessage.Text	=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("27");
					lblMessage.Visible	=	true;
					Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
					hidFormSaved.Value		=		"0";
				}

				
			}
		}

		

		private void SetWorkFlowControl()
		{
			if(base.ScreenId == "227_0" || base.ScreenId == "275_0")
			{
				myWorkFlow.IsTop = false;
				myWorkFlow.ScreenID	=	base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID",GetCustomerID());
				myWorkFlow.AddKeyValue("POLICY_ID",GetPolicyID());
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",GetPolicyVersionID());
				if(hidVehicleID.Value!="" && hidVehicleID.Value!="0" && hidVehicleID.Value.ToUpper()!="NEW")
					myWorkFlow.AddKeyValue("VEHICLE_ID",hidVehicleID.Value);
				myWorkFlow.WorkflowModule="POL";
				myWorkFlow.GetScreenStatus();
				myWorkFlow.SetScreenStatus();
			}
			else
			{
				myWorkFlow.Display	=	false;
			}
		}

	

		// Added by Swastika on 9th Mar'06 for Pol Iss #57
		private void ShowAlertMessage()
		{
			string strAlert = "";	
			
			strAlert = "<script> " + 
				" returnValue= getUserConfirmationForDeactivate();" + 
				" if(returnValue==6) " + 
				" document.getElementById('hidAlert').value='1'; " + 
				"else " + 
				" document.getElementById('hidAlert').value='0';" + 
				" __doPostBack('Deactive','" + hidAlert.Value + "') ; " + 
				"</script> ";
			ClientScript.RegisterStartupScript(this.GetType(),"AlertMessage",strAlert);
		}

		// Added by Swastika on 9th Mar'06 for Pol Iss #57
		private void DoInActive(string AlertValue)
		{
			if (AlertValue.Trim() == "1")
			{
				ClsVehicleInformation objVehicleInformation = new  ClsVehicleInformation();
				Cms.Model.Policy.ClsPolicyVehicleInfo  objVehicleInfo= null;
				Cms.Model.Policy.Umbrella.ClsVehicleInfo objUmVehicleInfo =null;
				if(hidCalledFrom.Value.ToUpper()=="UMB")
				{
					objUmVehicleInfo=GetFormValueForUmbrella();
					objVehicleInformation.ActivateDeactivateUmbrellaVehiclePolicy(objUmVehicleInfo ,"N");
					hidOldData.Value = ClsVehicleInformation.FetchPolicyUmbrellaVehicleXML(int.Parse(hidCustomerID.Value==""?"0": hidCustomerID.Value) ,int.Parse (hidPolicyID.Value ==""?"0":hidPolicyID.Value),int.Parse (hidPolicyVersionID.Value==""?"0":hidPolicyVersionID.Value),int.Parse (hidPolicyID.Value==""?"0":hidVehicleID.Value));
				}
				else
				{
					objVehicleInfo =GetFormValue ();
					objVehicleInformation.ActivateDeactivateAutoMotorVehiclePolicy(objVehicleInfo,"N");
					hidOldData.Value = ClsVehicleInformation.FetchCycleXMLFromPolicyVehicleTable(int.Parse(hidCustomerID.Value==""?"0": hidCustomerID.Value) ,int.Parse (hidPolicyID.Value ==""?"0":hidPolicyID.Value),int.Parse (hidPolicyVersionID.Value==""?"0":hidPolicyVersionID.Value),int.Parse (hidPolicyID.Value==""?"0":hidVehicleID.Value));
				}
				lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"41");
				hidIS_ACTIVE.Value="N";
				hidFormSaved.Value="0";
				
				lblMessage.Visible = true;
				ClientScript.RegisterStartupScript(this.GetType(), "REFRESHGRID","<script>RefreshWebGrid(1," + hidVehicleID.Value + ");</script>");
			}
		}
//		// Added by Swastika on 9th Mar'06 for Pol Iss #57
		private void DoDelete(string AlertValue)
		{
			if (AlertValue.Trim() == "1")
			{
				ClsVehicleInformation objVehicleInformation = new  ClsVehicleInformation();
				Cms.Model.Policy.ClsPolicyVehicleInfo  objVehicleInfo = GetFormValue();		
				
					
				if(hidCalledFrom.Value.ToUpper()=="UMB")
					intRetVal = objVehicleInformation.DeletePolicyUmbrellaVehicle(objVehicleInfo.CUSTOMER_ID,objVehicleInfo.POLICY_ID,objVehicleInfo.POLICY_VERSION_ID,objVehicleInfo.VEHICLE_ID);					
				else
					intRetVal = objVehicleInformation.DeletePolicyVehicle(objVehicleInfo,"");
					//intRetVal = objVehicleInformation.DeletePolicyVehicle(objVehicleInfo.CUSTOMER_ID,objVehicleInfo.POLICY_ID,objVehicleInfo.POLICY_VERSION_ID,objVehicleInfo.VEHICLE_ID);
				
				if(intRetVal>0)
				{
					lblDelete.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","127");
					hidFormSaved.Value = "5";
					hidOldData.Value = "";
					trBody.Attributes.Add("style","display:none");
					SetWorkFlowControl();

					//Opening the endorsement details page
					base.OpenEndorsementDetails();
				}
				else if(intRetVal == -1)
				{
					lblDelete.Text		=	ClsMessages.GetMessage(base.ScreenId,"128");
					hidFormSaved.Value		=	"2";
				}
				lblDelete.Visible = true;
			}
		}
//
//		// Added by Swastika on 9th Mar'06 for Pol Iss #57
		private void ShowAlertMessageForDelete()
		{
			string strAlert = "";	
			
			strAlert = "<script> " + 
				" returnValue= getUserConfirmationForDelete(); " + 
				" if(returnValue==6) " + 
				" document.getElementById('hidAlert').value='1'; " + 
				"else " + 
				" document.getElementById('hidAlert').value='0';" + 
				" __doPostBack('DeleteVehicle','" + hidAlert.Value + "') ; " + 
				"</script> ";
			ClientScript.RegisterStartupScript(this.GetType(),"AlertMessage",strAlert);
		}

		// Added by Swastika on 9th Mar'06 for Pol Issue #57
		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			int intCustomerID = int.Parse(hidCustomerID.Value);
			int intPolicyID=  int.Parse(hidPolicyID.Value);
			int intPolicyVersionID	= int.Parse(hidPolicyVersionID.Value);
			int intVehicleId = int.Parse(hidVehicleID.Value);

			try
			{				
				ClsVehicleInformation objVehicleInformation = new  ClsVehicleInformation();
				
				Cms.Model.Policy.ClsPolicyVehicleInfo  objVehicleInfo = null;

				Cms.Model.Policy.Umbrella.ClsVehicleInfo objUmVehicleInfo=null;

				if(hidCalledFrom.Value.ToUpper()=="UMB")
				{
					//objUmVehicleInfo=GetFormValueForUmbrella(); //Commented by Charles on 10-Sep-09 for Itrack 6375
					//Added by Charles on 10-Sep-09 for Itrack 6375
					objUmVehicleInfo=new Cms.Model.Policy.Umbrella.ClsVehicleInfo();
					base.PopulateModelObject(objUmVehicleInfo,@hidOldData.Value);
					objUmVehicleInfo.CUSTOMER_ID=intCustomerID;
					objUmVehicleInfo.POLICY_ID=intPolicyID;
					objUmVehicleInfo.POLICY_VERSION_ID=intPolicyVersionID;
					objUmVehicleInfo.VEHICLE_ID=intVehicleId;//Added till here	

					objUmVehicleInfo.CREATED_BY=objUmVehicleInfo.MODIFIED_BY=int.Parse(GetUserId());
				}
				else
				{
					//objVehicleInfo =GetFormValue(); //Commented by Charles on 10-Sep-09 for Itrack 6375
					//Added by Charles on 10-Sep-09 for Itrack 6375
					objVehicleInfo=new ClsPolicyVehicleInfo();
					base.PopulateModelObject(objVehicleInfo,@hidOldData.Value);
					objVehicleInfo.CUSTOMER_ID=intCustomerID;
					objVehicleInfo.POLICY_ID=intPolicyID;
					objVehicleInfo.POLICY_VERSION_ID=intPolicyVersionID;
					objVehicleInfo.VEHICLE_ID=intVehicleId;//Added till here	

					objVehicleInfo.CREATED_BY=objVehicleInfo.MODIFIED_BY=int.Parse(GetUserId());
				}
				
				if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
				{	
					//if(strCalledFrom.ToUpper()=="PPA")
					//{
					int DriverCount = objVehicleInformation.GetDriverCountForAssignedVehiclePolicy(intCustomerID,intPolicyID ,intPolicyVersionID ,intVehicleId,hidCalledFrom.Value.ToUpper());
                    if (DriverCount > 0)
                    {
                        ShowAlertMessage();
                        return;
                    }
                    else
                    {
                        if (hidCalledFrom.Value.ToUpper() == "UMB")
                        {
                            objVehicleInformation.ActivateDeactivateUmbrellaVehiclePolicy(objUmVehicleInfo, "N");
                        }
                        else
                        {
                            objVehicleInformation.ActivateDeactivateAutoMotorVehiclePolicy(objVehicleInfo, "N");
                            //Itrack 5130
                            //fxnSetMultiCarOptions();
                        }
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "41");
                        hidIS_ACTIVE.Value = "N";
                        trBody.Attributes.Add("style", "display:none");
                        ClientScript.RegisterStartupScript(this.GetType(),"REFRESHGRID", "<script>parent.strSelectedRecordXML='';parent.RemoveTab(5,parent);parent.RemoveTab(4,parent);parent.RemoveTab(3,parent);parent.RemoveTab(2,parent);RefreshWebGrid('5','1',true,true); </script>");
                    }
					//}
					/*else if(strCalledFrom.ToUpper()=="UMB")
					{
						objVehicleInformation.ActivateDeactivateUmbrellaVehiclePolicy(objVehicleInfo,"N");
						lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"41");
						hidIS_ACTIVE.Value="N";
					}*/
				}
				else
				{			
					if(strCalledFrom.ToUpper()=="PPA")
						objVehicleInformation.ActivateDeactivateAutoMotorVehiclePolicy(objVehicleInfo,"Y");
					else if(strCalledFrom.ToUpper()=="UMB")
					{
						objVehicleInformation.ActivateDeactivateUmbrellaVehiclePolicy(objUmVehicleInfo ,"Y");
					}
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"40");
					hidIS_ACTIVE.Value="Y";
					trBody.Attributes.Add("style","display:inline");
				}
				hidFormSaved.Value			=	"0";
				if(strCalledFrom.ToUpper()=="PPA")
					hidOldData.Value = @ClsVehicleInformation.FetchCycleXMLFromPolicyVehicleTable(int.Parse(hidCustomerID.Value==""?"0": hidCustomerID.Value) ,int.Parse (hidPolicyID.Value ==""?"0":hidPolicyID.Value),int.Parse (hidPolicyVersionID.Value==""?"0":hidPolicyVersionID.Value),int.Parse (hidVehicleID.Value==""?"0":hidVehicleID.Value));
				else if(strCalledFrom.ToUpper()=="UMB")
					hidOldData.Value = @ClsVehicleInformation.FetchPolicyUmbrellaVehicleXML(int.Parse(hidCustomerID.Value==""?"0": hidCustomerID.Value) ,int.Parse (hidPolicyID.Value ==""?"0":hidPolicyID.Value),int.Parse (hidPolicyVersionID.Value==""?"0":hidPolicyVersionID.Value),int.Parse (hidVehicleID.Value==""?"0":hidVehicleID.Value));
                
                if (ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value).ToString() != "")////Added By Pradeep Kushwaha on 10-sep-2010
                    btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value).ToString());
            
				base.OpenEndorsementDetails();
			    ClientScript.RegisterStartupScript(this.GetType(),"REFRESHGRID","<script>RefreshWebGrid(1," + hidVehicleID.Value + ");</script>");

			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21")+ " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
			}
			finally
			{
				lblMessage.Visible = true;
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
	

		//Fetch Sysmbol
		[Ajax.AjaxMethod()]
		public string AjaxGetSymbolForAppPolicy(string vehicleType,int amount, int year) 
		{
			CmsWeb.webservices.ClsWebServiceCommon obj =  new CmsWeb.webservices.ClsWebServiceCommon();
			string result = "";
			result = obj.GetSymbolForAppPolicy(vehicleType,amount, year);
			return result;
			
		}

		[Ajax.AjaxMethod()]
		public string AjaxGetDetailsFromVIN(string VIN)
		{
			CmsWeb.webservices.ClsWebServiceCommon obj =  new CmsWeb.webservices.ClsWebServiceCommon();
			string result = "";
			result = obj.GetDetailsFromVIN(VIN);
			return result;

		}

		/// <summary>
		/// This Function will Ftech the Highest Symbol for Vehicle Type
		/// </summary>
		/// <param name="vehicleType"></param>
		/// <returns></returns>
		[Ajax.AjaxMethod()]
		public string AjaxValidateSymbol(string vehicleType, int year)
		{
			CmsWeb.webservices.ClsWebServiceCommon obj =  new CmsWeb.webservices.ClsWebServiceCommon();
			string result = "";
			result = obj.GetHighestSymbol(vehicleType,year);
			return result;
		}
		#endregion	
         
		#region Class field to be readonly for users other than underwriters
		//Class field to be readonly for all users (3 Sep 2008)
		private void SetClassMode()
		{
			if(hidCalledFrom.Value!="" && hidCalledFrom.Value.ToUpper()=="PPA" && GetUserTypeId()!=null && GetUserTypeId()!="")// && GetUserTypeId()!=USER_TYPE_UNDERWRITER)
			{
				cmbAPP_VEHICLE_PERCLASS_ID.Enabled = false;
			}                                
		}
		#endregion
	}
}


